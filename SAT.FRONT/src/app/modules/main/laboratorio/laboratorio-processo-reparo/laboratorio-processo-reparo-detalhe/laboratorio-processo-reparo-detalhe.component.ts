import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ORItemService } from 'app/core/services/or-item.service';
import { ORTempoReparoService } from 'app/core/services/or-tempo-reparo.service';
import { ORCheckList } from 'app/core/types/or-checklist.types';
import { ORItem } from 'app/core/types/or-item.types';
import { orStatusConst } from 'app/core/types/or-status.types';
import { OR } from 'app/core/types/OR.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';

@Component({
  selector: 'app-laboratorio-processo-reparo-detalhe',
  templateUrl: './laboratorio-processo-reparo-detalhe.component.html'
})
export class LaboratorioProcessoReparoDetalheComponent implements OnInit {
  item: ORItem;
  or: OR;
  codORItem: number;
  loading: boolean = true;
  userSession: UserSession;
  orCheckList: ORCheckList;

  constructor(
    private _userService: UserService,
    private _route: ActivatedRoute,
    private _snack: CustomSnackbarService,
    private _orItemService: ORItemService,
    private _orTempoReparoService: ORTempoReparoService,
    private _cdr: ChangeDetectorRef
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codORItem = +this._route.snapshot.paramMap.get('codORItem');
    
    this.item = await this._orItemService
      .obterPorCodigo(this.codORItem)
      .toPromise();

    console.log(this.item);
    

    this.loading = false;
  }

  async toggleStatusReparo() {
    if (!this.item?.temposReparo?.length) {
      this._orItemService.atualizar({ 
        ...this.item, 
        ...{ 
          codStatus: orStatusConst.EM_REPARO,
          codTecnico: this.userSession.usuario.codUsuario
        }
      }).subscribe();

      await this._orTempoReparoService
        .criar({
          codORItem: this.item.codORItem,
          codUsuarioCad: this.userSession.usuario.codUsuario,
          dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
          dataHoraInicio: moment().format('YYYY-MM-DD HH:mm:ss'),
          codTecnico: this.userSession.usuario.codUsuario,
          dataHoraFim: null,
          indAtivo: 1
        })
        .subscribe(() => {
          this.ngOnInit();
          this._snack.exibirToast('Reparo iniciado', 'success');
        },
        () => {
          this._snack.exibirToast('Erro ao finalizar reparo', 'error');
        });
    } else {
      const novoStatus = +!this.item?.temposReparo[0].indAtivo;

      await this._orTempoReparoService
        .atualizar({
          ...this.item?.temposReparo[0],
          ...{
            indAtivo: novoStatus,
            dataHoraFim: moment().format('YYYY-MM-DD HH:mm:ss')
          }
        }).toPromise();

      if (novoStatus) {
        this.item = await this._orItemService.atualizar({ 
          ...this.item, 
          ...{ 
            codStatus: orStatusConst.EM_REPARO,
            codTecnico: this.userSession.usuario.codUsuario
          }
        }).toPromise();

        this._snack.exibirToast('Reparo iniciado', 'success');
      } else {
        this.item = await this._orItemService.atualizar({ 
          ...this.item, 
          ...{ 
            codStatus: orStatusConst.TRANSFERIDO_TECNICO,
            codTecnico: this.userSession.usuario.codUsuario,
          }
        }).toPromise();

        this._snack.exibirToast('Reparo finalizado', 'success');
      }
      
      this.ngOnInit();
    }
  }
}
