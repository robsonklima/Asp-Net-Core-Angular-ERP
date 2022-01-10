import { Component, Input, LOCALE_ID, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { Orcamento } from 'app/core/types/orcamento.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';

@Component({
  selector: 'app-ordem-servico-detalhe-orcamento',
  templateUrl: './ordem-servico-detalhe-orcamento.component.html',
  styles: [`
        .list-grid-orcamentos {
            grid-template-columns: 100px auto 100px 100px;
            
            @screen sm {
                grid-template-columns: 100px auto 100px 100px;
            }
        }
    `],
  providers: [
    {
      provide: LOCALE_ID,
      useValue: 'pt'
    }
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrdemServicoDetalheOrcamentoComponent
{

  isLoading: boolean = false;
  @Input() orcamentos: Orcamento[] = [];
  @Input() codOS;

  constructor (private _dialog: MatDialog, private _orcamentoService: OrcamentoService, private _activatedRoute: ActivatedRoute, private _router: Router) { }

  criarNovoOrcamento()
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Deseja criar um novo orçamento?',
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
    {
      if (confirmacao)
      {
        this._orcamentoService.criarNovoOrcamento(this.codOS).then(orc =>
        {
          this._router.navigateByUrl('orcamento/detalhe/' + orc?.codOrc);
        });
      }
    });

  }
}