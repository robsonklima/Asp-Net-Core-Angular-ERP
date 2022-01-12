import { AfterViewInit, ChangeDetectorRef, Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { IEditableItem, IEditableItemList } from 'app/core/base-components/interfaces/ieditable-item-list';
import { OrcamentoOutroServicoService } from 'app/core/services/orcamento-outro-servico.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { OrcamentoOutroServico } from 'app/core/types/orcamento.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';

@Component({
  selector: 'app-orcamento-detalhe-outro-servico',
  templateUrl: './orcamento-detalhe-outro-servico.component.html',
  styles: [`
        .list-grid-servicos {
            grid-template-columns: auto 100px 100px 100px 80px;
            
            @screen sm {
                grid-template-columns: auto 100px 100px 100px 80px;
            }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class OrcamentoDetalheOutroServicoComponent implements IEditableItemList<OrcamentoOutroServico>, AfterViewInit
{
  isLoading: boolean;
  @Input() outrosServicos: OrcamentoOutroServico[];
  editableList: IEditableItem<OrcamentoOutroServico>[];
  isEditing: boolean;

  constructor (public _dialog: MatDialog,
    private _cdRef: ChangeDetectorRef,
    private _orcService: OrcamentoService,
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
      servico.oldItem = Object.assign({}, m);
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
}