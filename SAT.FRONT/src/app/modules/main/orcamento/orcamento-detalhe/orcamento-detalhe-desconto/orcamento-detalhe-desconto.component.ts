import { AfterViewInit, ChangeDetectorRef, Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { fuseAnimations } from '@fuse/animations';
import { IEditableItem, IEditableItemList } from 'app/core/base-components/interfaces/ieditable-item-list';
import { OrcamentoDescontoService } from 'app/core/services/orcamento-desconto.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { OrcamentoDesconto } from 'app/core/types/orcamento.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';

@Component({
  selector: 'app-orcamento-detalhe-desconto',
  templateUrl: './orcamento-detalhe-desconto.component.html',
  styles: [`
        .list-grid-descontos {
            grid-template-columns: auto 150px 100px 100px 100px 60px;
            
            @screen sm {
                grid-template-columns: auto 150px 100px 100px 100px 60px;
            }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class OrcamentoDetalheDescontoComponent implements IEditableItemList<OrcamentoDesconto>, AfterViewInit
{

  isLoading: boolean;
  @Input() descontos: OrcamentoDesconto[];
  @Input() codOrc: number;
  editableList: IEditableItem<OrcamentoDesconto>[];
  isEditing: boolean;

  constructor (public _dialog: MatDialog,
    private _cdRef: ChangeDetectorRef,
    private _orcService: OrcamentoService,
    private _orcDescontoService: OrcamentoDescontoService,
    private _snack: MatSnackBar) { }

  ngAfterViewInit(): void
  {
    this.editableList = this.createEditableList();
  }

  editar(desconto: IEditableItem<OrcamentoDesconto>): void
  {
    desconto.oldItem = Object.assign({}, desconto.item);
    this.isEditing = true;
    desconto.isEditing = true;
  }

  salvar(desconto: IEditableItem<OrcamentoDesconto>): void
  {
    this.isEditing = false;
    desconto.isEditing = false;
  }

  cancelar(desconto: IEditableItem<OrcamentoDesconto>): void
  {
    desconto.item = Object.assign({}, desconto.oldItem);
    this.isEditing = false;
    desconto.isEditing = false;
    this._cdRef.detectChanges();
  }

  isEqual(item: IEditableItem<OrcamentoDesconto>): boolean { return false; }

  isInvalid(item: IEditableItem<OrcamentoDesconto>): boolean { return false; }

  createEditableList(): IEditableItem<OrcamentoDesconto>[]
  {
    return this.descontos.map(i =>
    {
      var item: IEditableItem<OrcamentoDesconto> =
      {
        item: i,
        isEditing: false,
        onEdit: () => this.editar(item),
        onCancel: () => this.cancelar(item),
        onSave: () => this.salvar(item),
        onDelete: () => this.excluirDesconto(item),
        isEqual: () => this.isEqual(item),
        isInvalid: () => this.isInvalid(item)
      };
      return item;
    });
  }

  excluirDesconto(desconto: IEditableItem<OrcamentoDesconto>) 
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: `Deseja remover o desconto ${desconto.item.nomeCampo}?`,
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
      {

      }
    });
  }

  adicionarDesconto()
  {

  }
}