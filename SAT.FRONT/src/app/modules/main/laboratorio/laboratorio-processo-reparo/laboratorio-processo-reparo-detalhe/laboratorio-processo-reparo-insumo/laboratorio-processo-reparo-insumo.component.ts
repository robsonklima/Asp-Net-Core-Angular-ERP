import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ORCheckListService } from 'app/core/services/or-checklist.service';
import { ORItemInsumoService } from 'app/core/services/or-item-insumo.service';
import { ORItemService } from 'app/core/services/or-item.service';
import { PecasLaboratorioService } from 'app/core/services/pecas-laboratorio.service';
import { ORCheckList } from 'app/core/types/or-checklist.types';
import { ORItemInsumo, ORItemInsumoParameters } from 'app/core/types/or-item-insumo.types';
import { ORItem } from 'app/core/types/or-item.types';
import { PecasLaboratorio } from 'app/core/types/pecas-laboratorio.types';
import { statusConst } from 'app/core/types/status-types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { mensagensConst, toastTypesConst } from 'app/core/types/generic.types'
import moment from 'moment';
import { Subject } from 'rxjs';
import { Utils } from 'app/core/utils/utils';

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
  listaPecas: any[] = [];
  protected _onDestroy = new Subject<void>();
  loading: boolean = true;
  form: FormGroup;

  constructor(
    private _userService: UserService,
    private _orItemService: ORItemService,
    private _orChecklistService: ORCheckListService,
    private _orItemInsumoService: ORItemInsumoService,
    private _pecasLaboratorioService: PecasLaboratorioService,
    private _snack: CustomSnackbarService,
    private _utils: Utils,
    private _formBuilder: FormBuilder
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.criarForm();
    this.orItem = await this._orItemService.obterPorCodigo(this.codORItem).toPromise();
    this.checklists = await this.obterCheckList();
    this.pecasLab = await this.obterPecasLab();
    await this.obterPecasInsumos();
    this.loading = false;
  }

  private async obterCheckList(): Promise<ORCheckList> {
    return (await this._orChecklistService.obterPorParametros({
      codPeca: this.orItem.codPeca,
    }).toPromise()).items.shift();
  }

  private criarForm() {
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

  private async obterPecasLab(): Promise<PecasLaboratorio[]> {
    const params = { codChecklist: this.checklists.codORCheckList };

    return (await this._pecasLaboratorioService.obterPorParametros(params).toPromise()).items;
  }

  private async obterPecasInsumos() {
    const params: ORItemInsumoParameters = { codORItem: this.codORItem }
    const insumos = (await this._orItemInsumoService.obterPorParametros(params).toPromise()).items;

    this.listaPecas = this.pecasLab.map((peca) => {
      return {
        ...peca,
        ...{ qtd: insumos.filter(i => i.codPeca == peca.codPeca)?.shift()?.quantidade }
      };
    }).sort(this._utils.dynamicSort("nomePeca"));;

    console.log(this.listaPecas);
    
  }

  public async inputHandlerQtdUtilizada(ev: any, pecasLab: PecasLaboratorio) {
    const qtd = ev.data;
    if(!qtd) return;

    const insumo: any = await this.obterInsumo(pecasLab);
    const obj: any = {
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
    }

    debugger

    if (!insumo) {
      this._orItemInsumoService.criar({...insumo, ...obj, ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.usuarioSessao.usuario.codUsuario,
      }}).subscribe(() => {
        this._snack.exibirToast(mensagensConst.SUCESSO_AO_CRIAR, toastTypesConst.SUCCESS);
      }, () => {
        this._snack.exibirToast(mensagensConst.ERRO_AO_CRIAR, toastTypesConst.ERROR);
      });
    } else {
      this._orItemInsumoService.atualizar({...insumo, ...obj}).subscribe(() => {
        this._snack.exibirToast(mensagensConst.SUCESSO_AO_ATUALIZAR, toastTypesConst.SUCCESS);
      }, () => {
        this._snack.exibirToast(mensagensConst.ERRO_AO_ATUALIZAR, toastTypesConst.ERROR);
      });
    }
  }

  public obterQuantidade() {
    return 10;
  }

  public teste() {
    return 20;
  }
}
