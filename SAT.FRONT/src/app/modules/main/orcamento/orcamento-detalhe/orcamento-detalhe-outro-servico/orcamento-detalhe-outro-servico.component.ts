import { AfterViewInit, ChangeDetectorRef, Component, Input, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { fuseAnimations } from '@fuse/animations';
import { IEditableItem, IEditableItemList } from 'app/core/base-components/interfaces/ieditable-item-list';
import { OrcamentoOutroServicoService } from 'app/core/services/orcamento-outro-servico.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { OrcamentoOutroServico } from 'app/core/types/orcamento.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { OrcamentoAddOutroServicoDialogComponent } from './orcamento-add-outro-servico-dialog/orcamento-add-outro-servico-dialog.component';

@Component({
  selector: 'app-orcamento-detalhe-outro-servico',
  templateUrl: './orcamento-detalhe-outro-servico.component.html',
  styles: [`
        .list-grid-servicos {
            grid-template-columns: 250px auto 70px 100px 100px 60px;
            
            @screen sm {
                grid-template-columns: 250px auto 70px 100px 100px 60px;
            }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class OrcamentoDetalheOutroServicoComponent implements IEditableItemList<OrcamentoOutroServico>, AfterViewInit
{
  snackConfigDanger: MatSnackBarConfig = { duration: 2000, panelClass: 'danger', verticalPosition: 'top', horizontalPosition: 'right' };
  snackConfigSuccess: MatSnackBarConfig = { duration: 2000, panelClass: 'success', verticalPosition: 'top', horizontalPosition: 'right' };

  isLoading: boolean;
  @Input() outrosServicos: OrcamentoOutroServico[];
  @Input() codOrc: number;
  editableList: IEditableItem<OrcamentoOutroServico>[];
  isEditing: boolean;

  constructor (public _dialog: MatDialog,
    private _cdRef: ChangeDetectorRef,
    private _orcService: OrcamentoService,
    private _snack: MatSnackBar,
    private _orcOutroServicoService: OrcamentoOutroServicoService)
  { }

  ngAfterViewInit(): void { this.editableList = this.createEditableList(); }

  editar(servico: IEditableItem<OrcamentoOutroServico>): void
  {
    servico.oldItem = Object.assign({}, servico.item);
    this.isEditing = true;
    servico.isEditing = true;
  }

  salvar(servico: IEditableItem<OrcamentoOutroServico>): void
  {
    servico.item.valorUnitario = parseFloat((servico.item.valorUnitario.toString().replace(/[^0-9,.]/g, '')).replace(',', '.'));
    servico.item.quantidade = parseFloat((servico.item.quantidade.toString().replace(/[^0-9,.]/g, '')).replace(',', '.'));
    servico.item.valorTotal = servico.item.valorUnitario * servico.item.quantidade;

    this._orcOutroServicoService.atualizar(servico.item).subscribe(m =>
    {
      this._orcService.atualizarTotalizacao(m.codOrc);
      this._snack.open('Servico atualizado com sucesso.', null, this.snackConfigSuccess).afterDismissed().toPromise();
      servico.oldItem = Object.assign({}, m);
    },
      e =>
      {
        this._snack.open('Erro ao atualizar serviço.', null, this.snackConfigDanger).afterDismissed().toPromise();
      });

    this.isEditing = false;
    servico.isEditing = false;
  }

  cancelar(servico: IEditableItem<OrcamentoOutroServico>): void
  {
    servico.item = Object.assign({}, servico.oldItem);
    this.isEditing = false;
    servico.isEditing = false;
    this._cdRef.detectChanges();
  }

  isEqual(item: IEditableItem<OrcamentoOutroServico>): boolean { return false; }

  isInvalid(item: IEditableItem<OrcamentoOutroServico>): boolean { return false; }

  createEditableList(): IEditableItem<OrcamentoOutroServico>[]
  {
    return this.outrosServicos.map(i =>
    {
      var item: IEditableItem<OrcamentoOutroServico> =
      {
        item: i,
        isEditing: false,
        onEdit: () => this.editar(item),
        onCancel: () => this.cancelar(item),
        onSave: () => this.salvar(item),
        onDelete: () => this.excluirOutroServico(item),
        isEqual: () => this.isEqual(item),
        isInvalid: () => this.isInvalid(item)
      };
      return item;
    });
  }

  excluirOutroServico(servico: IEditableItem<OrcamentoOutroServico>) 
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: `Deseja remover o serviço ${servico.item.descricao}?`,
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

  adicionarOutroServico()
  {
    const dialogRef = this._dialog.open(OrcamentoAddOutroServicoDialogComponent, {
      data: {
        codOrc: this.codOrc
      },
      backdropClass: 'static',
      width: '600px'
    });

    dialogRef.afterClosed().subscribe((novoServico: OrcamentoOutroServico) =>
    {
      if (novoServico)
      {
        var item: IEditableItem<OrcamentoOutroServico> =
        {
          item: novoServico,
          isEditing: false,
          onEdit: () => this.editar(item),
          onCancel: () => this.cancelar(item),
          onSave: () => this.salvar(item),
          onDelete: () => this.excluirOutroServico(item),
          isEqual: () => this.isEqual(item),
          isInvalid: () => this.isInvalid(item)
        };

        this.editableList.push(item);
      }
    });
  }
}