import { Component, Input, OnInit } from '@angular/core';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
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
    private _relatorioAtendimentoService: RelatorioAtendimentoService,
    private _exportacaoService: ExportacaoService,
    private _snack: CustomSnackbarService
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

  exportar(laudo) {
    let exportacaoParam: Exportacao = {
      formatoArquivo: ExportacaoFormatoEnum.PDF,
      tipoArquivo: ExportacaoTipoEnum.LAUDO,
      entityParameters: {
        codLaudo: laudo.codLaudo
      }
    }

    this._exportacaoService
      .exportar(FileMime.PDF, exportacaoParam)
      .catch(e => { this._snack.exibirToast(`Não foi possível realizar o download ${e.message}`) });
  }
}
