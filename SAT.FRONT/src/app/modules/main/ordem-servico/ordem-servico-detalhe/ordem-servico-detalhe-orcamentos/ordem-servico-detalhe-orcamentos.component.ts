import { Component, Input, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { Orcamento } from 'app/core/types/orcamento.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UserSession } from 'app/core/user/user.types';
import { OrcamentoRevisaoDialogComponent } from 'app/modules/main/orcamento/orcamento-revisao-dialog/orcamento-revisao-dialog.component';

@Component({
  selector: 'app-ordem-servico-detalhe-orcamentos',
  templateUrl: './ordem-servico-detalhe-orcamentos.component.html',
  styles: [`
        .list-grid-orcamentos-os {
            grid-template-columns: 100px 100px auto 130px 130px;
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
export class OrdemServicoDetalheOrcamentosComponent implements OnInit {
  isLoading: boolean = false;
  valorPecasDesatualizado: boolean = false;
  @Input() orcamentos: Orcamento[] = [];
  @Input() os: OrdemServico;
  userSession: UserSession;

  constructor(
    private _dialog: MatDialog,
  ) {}

  async ngOnInit() {}

  criarOrcamento() {
    this._dialog.open(OrcamentoRevisaoDialogComponent, {
      data: { os: this.os }
    });
  }
}