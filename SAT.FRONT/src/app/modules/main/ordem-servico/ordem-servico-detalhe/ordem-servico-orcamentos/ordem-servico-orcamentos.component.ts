import { T } from '@angular/cdk/keycodes';
import { Component, Input, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { Orcamento } from 'app/core/types/orcamento.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UserSession } from 'app/core/user/user.types';
import { OrcamentoCotacaoDialogComponent } from 'app/modules/main/orcamento/orcamento-cotacao-dialog/orcamento-cotacao-dialog.component';
import { OrcamentoRevisaoDialogComponent } from 'app/modules/main/orcamento/orcamento-revisao-dialog/orcamento-revisao-dialog.component';

@Component({
  selector: 'app-ordem-servico-orcamentos',
  templateUrl: './ordem-servico-orcamentos.component.html',
  styles: [`
        .list-grid-orcamentos-os {
            grid-template-columns: 100px 100px auto 180px 150px 130px 130px;
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
export class OrdemServicoOrcamentosComponent implements OnInit {
  isLoading: boolean = false;
  @Input() os: OrdemServico;
  orcamentos: Orcamento[] = [];
  userSession: UserSession;

  constructor(
    private _dialog: MatDialog,
    private _orcamentoService: OrcamentoService
  ) {}

  async ngOnInit() {
    this.obterOrcamentos();
  }

  async obterOrcamentos() {
    const data = await this._orcamentoService.obterPorParametros({
     codigoOrdemServico: this.os.codOS
   }).toPromise();

   this.orcamentos = data.items;
 }

  criarOrcamento() {
    this._dialog.open(OrcamentoRevisaoDialogComponent, {
      data: { os: this.os }
    });
  }

  solicitarCotacao() {
    this._dialog.open(OrcamentoCotacaoDialogComponent, {
      width: '800px',
      data: {
        os: this.os
      }
    });
  }
}