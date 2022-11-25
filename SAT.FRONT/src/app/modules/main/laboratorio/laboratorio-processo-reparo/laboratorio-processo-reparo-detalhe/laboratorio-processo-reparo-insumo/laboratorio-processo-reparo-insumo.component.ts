import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ORCheckListService } from 'app/core/services/or-checklist.service';
import { ORItemInsumoService } from 'app/core/services/or-item-insumo.service';
import { ORItemService } from 'app/core/services/or-item.service';
import { ORCheckList, ORCheckListData } from 'app/core/types/or-checklist.types';
import { ORItemInsumo } from 'app/core/types/or-item-insumo.types';
import { ORItem } from 'app/core/types/or-item.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import _ from 'lodash';
import moment from 'moment';

@Component({
  selector: 'app-laboratorio-processo-reparo-insumo',
  templateUrl: './laboratorio-processo-reparo-insumo.component.html'
})
export class LaboratorioProcessoReparoInsumoComponent implements OnInit {
  @Input() codORItem: number;
  usuarioSessao: UsuarioSessao;
  orItem: ORItem;
  checklists: ORCheckList[] = [];
  orItemInsumo: ORItemInsumo;
  loading: boolean = true;
  form: FormGroup;
  qtdUtilizada: number;

  constructor(
    private _userService: UserService,
    private _orItemService: ORItemService,
    private _orChecklistService: ORCheckListService,
    private _orItemInsumoService: ORItemInsumoService,
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.criarForm();
    this.orItem = await this._orItemService.obterPorCodigo(this.codORItem).toPromise();
    this.checklists = (await this.obterCheckList()).items;
    this.loading = false;
  }

  private async obterCheckList(): Promise<ORCheckListData> {
    return await this._orChecklistService.obterPorParametros({ codPeca: this.orItem.codPeca }).toPromise();
  }

  criarForm() {
    this.form = this._formBuilder.group({
      qtdUtilizada: [undefined]
    });
  }

  async obterInsumos(item: ORCheckList) {
    this.orItemInsumo = (await this._orItemInsumoService.obterPorParametros({
      codORItem: this.orItem.codORItem,
      codPeca: item.codPeca,
      indAtivo: 1
    }).toPromise()).items.shift();
  }

  async toggleRealizado(ev: any, item: ORCheckList) {
    if (this.qtdUtilizada) {
      if (ev.checked) {
        this._orItemInsumoService.criar({
          dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
          codUsuarioCad: this.usuarioSessao.usuario.codUsuario,
          dataHoraOritem: this.orItem.dataHoraCad,
          codORItem: this.orItem.codORItem,
          codOR: this.orItem.codOR,
          codPeca: item.codPeca,
          codStatus: this.orItem.codStatus,
          quantidade: this.qtdUtilizada,
          indConfLog: 0,
          indConfLab: 0,
          indAtivo: 1,
          codStatusPendente: 0
        }).subscribe();
      } else {
        this.obterInsumos(item);
        this._orItemInsumoService.deletar(this.orItemInsumo.codORItemInsumo).subscribe();
      }
    } else {
      this._snack.exibirToast('Preencha a quantidade de peças utilizadas', 'error');
    }
  }
}
