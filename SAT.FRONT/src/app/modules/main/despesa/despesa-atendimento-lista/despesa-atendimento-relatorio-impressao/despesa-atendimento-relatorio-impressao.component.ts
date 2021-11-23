import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { appConfig } from 'app/core/config/app.config';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { DespesaAdiantamentoPeriodo } from 'app/core/types/despesa-adiantamento.types';
import { DespesaPeriodoTecnico } from 'app/core/types/despesa-periodo.types';
import { DespesaProtocolo, DespesaProtocoloImpressaoListView } from 'app/core/types/despesa-protocolo.types';
import { Despesa, DespesaTipoEnum } from 'app/core/types/despesa.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import Enumerable from 'linq';

@Component({
  selector: 'app-despesa-atendimento-relatorio-impressao',
  templateUrl: './despesa-atendimento-relatorio-impressao.component.html',
  styleUrls: ['./despesa-atendimento-relatorio-impressao.component.scss']
})
export class DespesaAtendimentoRelatorioImpressaoComponent implements OnInit
{
  isLoading: boolean;
  codDespesaPeriodoTecnico: number;
  despesaPeriodoTecnico: DespesaPeriodoTecnico;
  centroDeCusto: string = appConfig.rd_centro_de_custo;
  ordensServico: OrdemServico[] = [];

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService,
    private _ordemServicoSvc: OrdemServicoService)
  {

    if (data)
      this.codDespesaPeriodoTecnico = data.codDespesaPeriodoTecnico;
  }

  async ngOnInit()
  {
    this.isLoading = true;

    this.despesaPeriodoTecnico =
      (await this._despesaPeriodoTecnicoSvc.obterPorCodigo(this.codDespesaPeriodoTecnico).toPromise());

    await this.obterOS();

    console.log(this.despesaPeriodoTecnico.despesas)

    this.isLoading = false;
  }

  print()
  {
    var contentToPrint = document.getElementById("print-area").innerHTML;
    var windowPopup = window.open('', '_blank', 'width=500,height=500');
    windowPopup.document.open();
    windowPopup.document.write(`<html><head><link rel="stylesheet" type="text/css" href="${appConfig.tailwind_css}"/></head><body onload = "window.print()"> ${contentToPrint} </body></html>`);
    windowPopup.document.title = `RD_${this.codDespesaPeriodoTecnico}.pdf`;
    windowPopup.onafterprint = window.close;
    windowPopup.document.close();
  }

  obterCartaoCombustivel()
  {
    return Enumerable.from(this.despesaPeriodoTecnico.tecnico.despesaCartaoCombustivelTecnico)
      .orderByDescending(i => i.dataHoraInicio)
      .firstOrDefault().despesaCartaoCombustivel;
  }

  async obterOS()
  {
    var codigos = Enumerable.from(this.despesaPeriodoTecnico.despesas)
      .select(i => i.relatorioAtendimento.codOS)
      .distinct();

    for (const c of codigos)
    {
      var os = (await this._ordemServicoSvc.obterPorCodigo(c).toPromise());
      this.ordensServico.push(os);
    };
  }

  obterCliente(dp: Despesa)
  {
    return Enumerable.from(this.ordensServico)
      .firstOrDefault(i => i.codOS == dp.relatorioAtendimento.codOS).cliente.nomeFantasia;
  }

  obterTotalDespesa()
  {
    return Enumerable.from(this.despesaPeriodoTecnico.despesas)
      .selectMany(i => i.despesaItens)
      .sum(i => i.despesaValor);
  }

  obterTotalQuilometragem()
  {
    return Enumerable.from(this.despesaPeriodoTecnico.despesas)
      .selectMany(i => i.despesaItens)
      .where(i => i.codDespesaTipo == DespesaTipoEnum.KM)
      .sum(i => i.kmPercorrido);
  }
}