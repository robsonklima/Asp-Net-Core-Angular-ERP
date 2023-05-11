import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ORCheckListService } from 'app/core/services/or-checklist.service';
import { ORItemInsumoService } from 'app/core/services/or-item-insumo.service';
import { ORItemService } from 'app/core/services/or-item.service';
import { PecasLaboratorioService } from 'app/core/services/pecas-laboratorio.service';
import { ORCheckList, ORCheckListData } from 'app/core/types/or-checklist.types';
import { ORItemInsumo } from 'app/core/types/or-item-insumo.types';
import { ORItem } from 'app/core/types/or-item.types';
import { PecasLaboratorio } from 'app/core/types/pecas-laboratorio.types';
import { statusConst } from 'app/core/types/status-types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-laboratorio-processo-reparo-insumo',
  templateUrl: './laboratorio-processo-reparo-insumo.component.html'
})
export class LaboratorioProcessoReparoInsumoComponent implements OnInit {
  @Input() codORItem: number;
  usuarioSessao: UsuarioSessao;
  orItem: ORItem;
  checklists: ORCheckList;
  orItemInsumo: ORItemInsumo;
  pecasLab: PecasLaboratorio[] = [];
  protected _onDestroy = new Subject<void>();
  loading: boolean = true;
  form: FormGroup;

  constructor(
    private _userService: UserService,
    private _orItemService: ORItemService,
    private _orChecklistService: ORCheckListService,
    private _orItemInsumoService: ORItemInsumoService,
    private _pecasLaboratorioService: PecasLaboratorioService,
    private _formBuilder: FormBuilder
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.criarForm();
    this.orItem = await this._orItemService.obterPorCodigo(this.codORItem).toPromise();
    this.checklists = await this.obterCheckList();
    this.pecasLab = await this.obterPecasInsumos();
    this.loading = false;
  }

  private async obterCheckList(): Promise<ORCheckList> {
    return (await this._orChecklistService.obterPorParametros({
      codPeca: this.orItem.codPeca,
    }).toPromise()).items.shift();
  }

  criarForm() {
    this.form = this._formBuilder.group({
      qtdUtilizada: [undefined]
    });
  }

  private async obterInsumo(item: PecasLaboratorio): Promise<ORItemInsumo> {
    return (await this._orItemInsumoService.obterPorParametros({
      codORItem: this.orItem.codORItem,
      codPeca: item.codPeca,
      indAtivo: statusConst.ATIVO
    }).toPromise()).items.shift();
  }

  private async obterPecasInsumos(): Promise<PecasLaboratorio[]> {
    return (await this._pecasLaboratorioService.obterPorParametros({
      codChecklist: this.checklists.codORCheckList
    }).toPromise()).items;
  }

  public async inputHandlerQtdUtilizada(ev: any, pecasLab: PecasLaboratorio) {
    const qtd = ev.data;
    const insumo: any = await this.obterInsumo(pecasLab);

    if (insumo && qtd == 0) {
      this._orItemInsumoService.deletar(insumo.codORItemInsumo).subscribe();
    } else if (qtd > 0 && !insumo) {
      this._orItemInsumoService.criar({
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.usuarioSessao.usuario.codUsuario,
        dataHoraOritem: this.orItem.dataHoraCad,
        codORItem: this.orItem.codORItem,
        codOR: this.orItem.codOR,
        codStatus: this.orItem.codStatus,
        codPeca: pecasLab.codPeca,
        quantidade: qtd,
        indConfLog: 0,
        indConfLab: 0,
        indAtivo: statusConst.ATIVO,
        codStatusPendente: 0
      }).subscribe();
    } else {
      this._orItemInsumoService.atualizar(insumo).subscribe();
    }
  }

  public obterQuantidade() {
    return 10;
  }
}
