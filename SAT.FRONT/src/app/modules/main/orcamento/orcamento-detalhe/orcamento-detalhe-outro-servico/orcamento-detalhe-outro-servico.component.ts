import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { Orcamento, OrcamentoOutroServico } from 'app/core/types/orcamento.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';

@Component({
  selector: 'app-orcamento-detalhe-outro-servico',
  templateUrl: './orcamento-detalhe-outro-servico.component.html',
  styles: [`
        .list-grid-servicos {
            grid-template-columns: auto 100px 100px 100px 100px;
            
            @screen sm {
                grid-template-columns: auto 100px 100px 100px 100px;
            }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class OrcamentoDetalheOutroServicoComponent implements OnInit
{
  codOrc: number;
  isLoading: boolean;
  @Input() outrosServicos: OrcamentoOutroServico[];

  constructor (public _dialog: MatDialog) { }

  ngOnInit(): void { }

  excluirOutroServico(servico: OrcamentoOutroServico) 
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: `Deseja remover o serviço ${servico.descricao}?`,
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