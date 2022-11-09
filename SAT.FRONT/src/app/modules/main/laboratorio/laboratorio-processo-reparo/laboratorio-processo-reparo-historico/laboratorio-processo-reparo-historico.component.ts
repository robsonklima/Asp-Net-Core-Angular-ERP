import { AfterViewInit, Component, Inject } from '@angular/core';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ORItem } from 'app/core/types/or-item.types';
import { ORService } from 'app/core/services/or.service';
import { OR } from 'app/core/types/OR.types';
import { Filial } from 'app/core/types/filial.types';
import { FilialService } from 'app/core/services/filial.service';
import { ORItemInsumo, ORItemInsumoParameters } from 'app/core/types/or-item-insumo.types';
import { ORItemInsumoService } from 'app/core/services/or-item-insumo.service';
import { ItemXORCheckList, ItemXORCheckListParameters } from 'app/core/types/item-or-checklist.types';
import { ItemXORCheckListService } from 'app/core/services/item-or-checklist.service';

@Component({
  selector: 'app-laboratorio-processo-reparo-historico',
  templateUrl: './laboratorio-processo-reparo-historico.component.html'
})
export class LaboratorioProcessoReparoHistoricoComponent implements AfterViewInit {
  isLoading: boolean = false;
  userSession: UsuarioSessao;
  codORItem: number;
  or: OR;
  orItem: ORItem;
  filial: Filial;
  insumos: ORItemInsumo[];
  checklists: ItemXORCheckList[];
  protected _onDestroy = new Subject<void>();
  displayedColumns: string[] = ['nome', 'status', 'justificativa'];
  displayedColumns2: string[] = ['descricao', 'nivel', 'acao', 'realizacao'];

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _orService: ORService,
    private _filialService: FilialService,
    private _orItemInsumoService: ORItemInsumoService,
    private _itemXChecklistService: ItemXORCheckListService,
    private _userService: UserService,
    private dialogRef: MatDialogRef<LaboratorioProcessoReparoHistoricoComponent>) {
      if (data)
      {
          this.orItem = data.item;
      }
  
      this.userSession = JSON.parse(this._userService.userSession);
    }

  async ngAfterViewInit() {
    this.or = await this._orService.obterPorCodigo(this.orItem.codOR).toPromise();
    this.filial = await this._filialService.obterPorCodigo(this.or.codOrigem).toPromise();
    await this.obterInsumos();
    await this.obterChecklists();
   }
  
  private async obterInsumos(){
    const params: ORItemInsumoParameters = {
			codORItem: this.orItem.codORItem,
      indAtivo: 1
		}
		const data = await this._orItemInsumoService.obterPorParametros(params).toPromise();
		this.insumos = data.items;
  }

  private async obterChecklists(){
    const params: ItemXORCheckListParameters = {
      codORItem: this.orItem.codORItem,
      indAtivo: 1
    }
    const data = await this._itemXChecklistService.obterPorParametros(params).toPromise();
    this.checklists = data.items;
  }

  private registrarEmitters() { }
}