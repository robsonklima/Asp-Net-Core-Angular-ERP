import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ItemXORCheckListService } from 'app/core/services/item-or-checklist.service';
import { ORCheckListService } from 'app/core/services/or-checklist.service';
import { ItemXORCheckList, ItemXORCheckListData } from 'app/core/types/item-or-checklist.types';
import { ORCheckListItem } from 'app/core/types/or-checklist-item.types';
import { ORCheckListData } from 'app/core/types/or-checklist.types';
import { ORItem } from 'app/core/types/or-item.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import _ from 'lodash';

@Component({
  selector: 'app-laboratorio-processo-reparo-form-checklist',
  templateUrl: './laboratorio-processo-reparo-form-checklist.component.html'
})
export class LaboratorioProcessoReparoFormChecklistComponent implements OnInit {
  @Input() orItem: ORItem;
  loading: boolean = true;
  userSession: UserSession;
  itens: ORCheckListItem[] = [];
  itensMarcados: ItemXORCheckList[] = [];
  niveis: number[] = [1, 2, 3];
  nivelSelecionado: number;
  form: FormGroup;

  constructor(
    private _userService: UserService,
    private _orCheckList: ORCheckListService,
    private _itemChecklistService: ItemXORCheckListService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.itensMarcados = await (await this.obterCheckListEItens()).items;
    this.loading = false;    
  }

  private async obterCheckList(): Promise<ORCheckListData> {
    return await this._orCheckList
      .obterPorParametros({ codPeca: this.orItem.codPeca })
      .toPromise();
  }

  private async obterCheckListEItens(): Promise<ItemXORCheckListData> {
    return await this._itemChecklistService
      .obterPorParametros({ codORItem: this.orItem.codORItem })
      .toPromise();
  }

  async filtrarNivel(ev: any) {
    this.nivelSelecionado = ev;
    this.itens = (await this.obterCheckList()).items.shift().itens.filter(i => i.nivel == ev);
  }

  public verificarItemSelecionado(codORCheckList: number): boolean {
    const checkListItem = _.find(this.itensMarcados, { 
      codORCheckList: codORCheckList, 
      codORItem: this.orItem.codORItem,
      indAtivo: statusConst.ATIVO
    });

    return checkListItem != null;
  }

  public async toggleRealizado(ev: any, item: ORCheckListItem) {   
    if (ev.checked) {
      this._itemChecklistService.criar({
        codORItem: this.orItem.codORItem,
        codORCheckList: item.codORCheckList,
        codORCheckListItem: item.codORCheckListItem,
        indAtivo: 1,
        nivel: item.nivel
      }).subscribe((checklist) => {
        
      });
    } else {
      const checklist = _.find(this.itens, { codORCheckListItem: item.codORCheckListItem });

      if (checklist)
        this._itemChecklistService.deletar(checklist.codORCheckListItem).toPromise();
    }
  }
}
