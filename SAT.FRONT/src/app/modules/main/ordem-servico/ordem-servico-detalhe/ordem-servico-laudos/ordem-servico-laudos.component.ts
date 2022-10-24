import { Component, Input, OnInit } from '@angular/core';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { Laudo } from 'app/core/types/laudo.types';
import { RelatorioAtendimento, RelatorioAtendimentoData } from 'app/core/types/relatorio-atendimento.types';
import Enumerable from 'linq';

@Component({
  selector: 'app-ordem-servico-laudos',
  templateUrl: './ordem-servico-laudos.component.html'
})
export class OrdemServicoLaudosComponent implements OnInit {
  @Input() codOS: number;
  isLoading: boolean = true;
  relatoriosAtendimento: RelatorioAtendimento[] = [];
  laudos: Laudo[] = [];

  constructor(
    private _relatorioAtendimentoService: RelatorioAtendimentoService
  ) { }

  async ngOnInit() {
    this.relatoriosAtendimento = (await this.obterRATS()).items;
    this.obterLaudos();
    this.isLoading = false;
  }

  private async obterRATS(): Promise<RelatorioAtendimentoData> {
    return this._relatorioAtendimentoService
      .obterPorParametros({ codOS: this.codOS })
      .toPromise();
  }

  private obterLaudos() {
    this.laudos = Enumerable.from(this.relatoriosAtendimento)
      .selectMany(i => i.laudos)
      .toArray();
  }
}
