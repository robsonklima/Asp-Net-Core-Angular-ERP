import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ORItemService } from 'app/core/services/or-item.service';
import { ORTempoReparoService } from 'app/core/services/or-tempo-reparo.service';
import { ORService } from 'app/core/services/or.service';
import { ORCheckList } from 'app/core/types/or-checklist.types';
import { ORItem } from 'app/core/types/or-item.types';
import { ORTempoReparo } from 'app/core/types/or-tempo-reparo.types';
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
  tempoReparo: ORTempoReparo;
  orCheckList: ORCheckList;

  constructor(
    private _userService: UserService,
    private _route: ActivatedRoute,
    private _snack: CustomSnackbarService,
    private _orItemService: ORItemService,
    private _orTempoReparoService: ORTempoReparoService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codORItem = +this._route.snapshot.paramMap.get('codORItem');
    
    this.item = await this._orItemService
      .obterPorCodigo(this.codORItem)
      .toPromise();

    this.loading = false;
  }

  toggleStatusReparo() {
    if (!this.tempoReparo?.indAtivo) {
      this._orTempoReparoService
        .criar({
          codORItem: this.item.codORItem,
          codUsuarioCad: this.userSession.usuario.codUsuario,
          dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
          dataHoraInicio: moment().format('YYYY-MM-DD HH:mm:ss'),
          codTecnico: this.userSession.usuario.codUsuario,
          dataHoraFim: null,
          indAtivo: 1
        })
        .subscribe((tr) => {
          this.tempoReparo = tr;
          this._snack.exibirToast('Reparo iniciado', 'success');
        },
        () => {
          this._snack.exibirToast('Erro ao finalizar reparo', 'error');
        });
    } else {
      this._orTempoReparoService
        .atualizar({
          ...this.tempoReparo,
          ...{
            indAtivo: 0,
            dataHoraFim: moment().format('YYYY-MM-DD HH:mm:ss')
          }
        })
        .subscribe((tr) => {
          this.tempoReparo = tr;
          this._snack.exibirToast('Reparo finalizado', 'success');
        },
        () => {
          this._snack.exibirToast('Erro ao finalizar reparo', 'error');
        });
    }
  }
}
