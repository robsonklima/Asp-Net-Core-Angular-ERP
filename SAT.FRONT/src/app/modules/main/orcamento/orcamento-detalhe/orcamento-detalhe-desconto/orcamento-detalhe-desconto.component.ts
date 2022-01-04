import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { Orcamento, OrcamentoDesconto } from 'app/core/types/orcamento.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';

@Component({
  selector: 'app-orcamento-detalhe-desconto',
  templateUrl: './orcamento-detalhe-desconto.component.html',
  styles: [`
        .list-grid-descontos {
            grid-template-columns: auto 100px 100px 100px 100px 100px;
            
            @screen sm {
                grid-template-columns: auto 100px 100px 100px 100px 100px;
            }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class OrcamentoDetalheDescontoComponent implements OnInit
{

  isLoading: boolean;
  @Input() descontos: OrcamentoDesconto[];

  constructor (public _dialog: MatDialog) { }

  ngOnInit(): void { }

  excluirDesconto(desconto: OrcamentoDesconto) 
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: `Deseja remover o desconto ${desconto.nomeCampo}?`,
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