import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ItemXORCheckListService } from 'app/core/services/item-or-checklist.service';
import { ORCheckListService } from 'app/core/services/or-checklist.service';
import { ItemXORCheckList, ItemXORCheckListData } from 'app/core/types/item-or-checklist.types';
import { ORCheckListItem } from 'app/core/types/or-checklist-item.types';
import { ORCheckList, ORCheckListData } from 'app/core/types/or-checklist.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import _ from 'lodash';

@Component({
  selector: 'app-laboratorio-processo-reparo-form-checklist',
  templateUrl: './laboratorio-processo-reparo-form-checklist.component.html'
})
export class LaboratorioProcessoReparoFormChecklistComponent implements OnInit {
  @Input() codORItem: number;
  @Input() codPeca: number;
  loading: boolean = true;
  userSession: UserSession;
  orCheckList: ORCheckList;
  itensChecklists: ItemXORCheckList[] = [];
  form: FormGroup;

  constructor(
    private _userService: UserService,
    private _orCheckList: ORCheckListService,
    private _itemChecklistService: ItemXORCheckListService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.orCheckList = await (await this.obterCheckList()).items.shift();
    this.itensChecklists = await (await this.obterCheckListEItens()).items;
  }

  private async obterCheckList(): Promise<ORCheckListData> {
    return await this._orCheckList.obterPorParametros({ codPeca: this.codPeca }).toPromise();
  }

  private async obterCheckListEItens(): Promise<ItemXORCheckListData> {
    return await this._itemChecklistService.obterPorParametros({ codORItem: this.codORItem }).toPromise();
  }

  public toggleRealizado(ev: any, item: ORCheckListItem) {
    console.log(ev.checked, item);
  }

  public verificarItemSelecionado(codORCheckList: number): boolean {
    const checkListItem = _.find(this.itensChecklists, { 
      codORCheckList: codORCheckList, 
      codORItem: this.codORItem,
      indAtivo: statusConst.ATIVO
    });

    //console.log(checkListItem != null);
    
    return checkListItem != null;
  }
}
