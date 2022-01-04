import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { OrcamentoMaterial } from 'app/core/types/orcamento.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';

@Component({
  selector: 'app-orcamento-detalhe-material',
  templateUrl: './orcamento-detalhe-material.component.html',
  styles: [`
        .list-grid-material {
            grid-template-columns: 100px auto 100px 75px 100px 100px 100px 100px;
            
            @screen sm {
                grid-template-columns: 100px auto 100px 75px 100px 100px 100px 100px;
            }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrcamentoDetalheMaterialComponent implements OnInit
{

  codOrc: number;
  isLoading: boolean;
  @Input() materiais: OrcamentoMaterial[];

  constructor (public _dialog: MatDialog) { }

  ngOnInit(): void { }

  excluirMaterial(material: OrcamentoMaterial) 
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: `Deseja remover o material ${material.codigoMagnus}?`,
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