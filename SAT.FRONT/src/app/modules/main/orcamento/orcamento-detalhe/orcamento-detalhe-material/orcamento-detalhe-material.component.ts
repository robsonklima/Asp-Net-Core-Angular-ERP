import { AfterViewInit, ChangeDetectorRef, Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { OrcamentoMaterial } from 'app/core/types/orcamento.types';
import { UserService } from 'app/core/user/user.service';
import { IEditableItem, IEditableItemList } from 'app/shared/components/interfaces/ieditable-item-list';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { isEqual } from 'lodash';

@Component({
  selector: 'app-orcamento-detalhe-material',
  templateUrl: './orcamento-detalhe-material.component.html',
  styles: [`
        .list-grid-material {
            grid-template-columns: 100px auto 100px 75px 100px 100px 100px 150px;
            
            @screen sm {
                grid-template-columns: 100px auto 100px 75px 100px 100px 100px 150px;
            }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrcamentoDetalheMaterialComponent implements IEditableItemList, AfterViewInit
{
  isLoading: boolean = false;
  isEditing: boolean = false;
  editableList: IEditableItem[] = [];

  @Input() materiais: OrcamentoMaterial[];

  constructor (public _dialog: MatDialog, private _cdRef: ChangeDetectorRef, private _userService: UserService) { }

  ngAfterViewInit(): void
  {
    this.editableList = this.createEditableList();
  }

  createEditableList(): IEditableItem[]
  {
    return this.materiais.map(i =>
    {
      var item: IEditableItem =
      {
        item: i,
        oldItem: Object.assign({}, i),
        isEditing: false,
        onEdit: () => this.editar(item),
        onCancel: () => this.cancelar(item),
        onSave: () => this.salvar(item),
        onDelete: () => this.excluirMaterial(item),
        isEqual: () => this.isEqual(item),
        isInvalid: () => this.isInvalid(item)
      };
      return item;
    });
  }

  editar(material: IEditableItem): void
  {
    if (!material) return;
    this.isEditing = true;
    material.isEditing = true;
  }

  salvar(material: IEditableItem): void
  {
    if (!material) return;
    this.isEditing = false;
    material.isEditing = false;
  }

  cancelar(material: IEditableItem): void
  {
    if (!material) return;

    material.item = Object.assign({}, material.oldItem);
    this.isEditing = false;
    material.isEditing = false;
    this._cdRef.detectChanges();
  }

  isEqual(material: IEditableItem): boolean
  {
    if (!material) return;
    return isEqual(material.oldItem, material.item);
  }

  isInvalid(material: IEditableItem): boolean
  {
    if (material.item.valorUnitario <= 0 || material.item.quantidade <= 0)
      return true;

    return false;
  }

  toNumber(value)
  {
    return +value;
  }

  excluirMaterial(material: IEditableItem) 
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: `Deseja remover o material ${material?.item.codigoMagnus}?`,
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      },
      backdropClass: 'static'
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
    {
      if (confirmacao)
        console.log("oi");
    });
  }
}