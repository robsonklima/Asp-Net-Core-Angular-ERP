import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { appConfig } from 'app/core/config/app.config';
import { DespesaAdiantamentoPeriodoService } from 'app/core/services/despesa-adiantamento-periodo.service';
import { DespesaAdiantamentoPeriodo, DespesaAdiantamentoPeriodoData } from 'app/core/types/despesa-adiantamento.types';
import { DespesaProtocolo, DespesaProtocoloPeriodoTecnico } from 'app/core/types/despesa-protocolo.types';
import { DespesaTipoEnum } from 'app/core/types/despesa.types';
import Enumerable from 'linq';

@Component({
  selector: 'app-despesa-protocolo-detalhe-impressao',
  templateUrl: './despesa-protocolo-detalhe-impressao.component.html',
  styleUrls: ['./despesa-protocolo-detalhe-impressao.component.scss']
})
export class DespesaProtocoloDetalheImpressaoComponent implements OnInit
{
  isLoading: boolean;
  protocolo: DespesaProtocolo;
  adiantamentos: DespesaAdiantamentoPeriodo[] = [];


  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _despesaAdiantamentoPeriodoSvc: DespesaAdiantamentoPeriodoService)
  {

    if (data)
      this.protocolo = data.protocolo;
  }

  async ngOnInit()
  {
    this.isLoading = true;

    await this.obterAdiantamentos();

    this.isLoading = false;
  }

  private async obterAdiantamentos()
  {
    for (const e of this.protocolo.despesaProtocoloPeriodoTecnico)
    {
      var adiantamentos = (await this._despesaAdiantamentoPeriodoSvc.obterPorParametros({
        codTecnico: e.despesaPeriodoTecnico.codTecnico,
        codDespesaPeriodo: e.despesaPeriodoTecnico.codDespesaPeriodo
      }).toPromise()).items;

      this.adiantamentos.push(...adiantamentos);
    }
  }

  print()
  {
    var contentToPrint = document.getElementById("print-area").innerHTML;
    var windowPopup = window.open('', '_blank', 'width=500,height=500');
    windowPopup.document.open();
    windowPopup.document.write(`<html><head><link rel="stylesheet" type="text/css" href="${appConfig.tailwind_css}"/></head><body onload = "window.print()"> ${contentToPrint} </body></html>`);
    windowPopup.document.title = `Protocolo_${this.protocolo.codDespesaProtocolo}.pdf`
    windowPopup.onafterprint = window.close;
    windowPopup.document.close();
  }

  calcularTipoDespesa(dp: DespesaProtocoloPeriodoTecnico, tipo: DespesaTipoEnum)
  {
    var sum = Enumerable.from(dp.despesaPeriodoTecnico.despesas).where(i => i.indAtivo == 1)
      .selectMany(i => i.despesaItens).where(i => i.indAtivo == 1
        && i.codDespesaTipo == tipo).sum(i => i.despesaValor);

    return sum.toFixed(2).replace(".", ",");
  }

  calcularAdiantamento(dp: DespesaProtocoloPeriodoTecnico)
  {
    var codTecnico = dp.despesaPeriodoTecnico.codTecnico;
    var codPeriodo = dp.despesaPeriodoTecnico.codDespesaPeriodo;

    return Enumerable.from(this.adiantamentos).where(i => i.despesaAdiantamento.indAtivo == 1
      && i.despesaAdiantamento.codTecnico == codTecnico && i.despesaPeriodo.codDespesaPeriodo == codPeriodo)
      .sum(i => i.valorAdiantamentoUtilizado);
  }

  calcularDespesaTotal(dp: DespesaProtocoloPeriodoTecnico)
  {
    return Enumerable.from(dp.despesaPeriodoTecnico.despesas).where(i => i.indAtivo == 1)
      .selectMany(i => i.despesaItens).where(i => i.indAtivo == 1 && i.codDespesaTipo != DespesaTipoEnum.KM && i.codDespesaTipo != DespesaTipoEnum.COMBUSTIVEL).sum(i => i.despesaValor);
  }

  obterValorAluguelCarro(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.ALUGUEL_CARRO);
  }

  valorDescontinuado()
  {
    return "---";
  }

  obterValorCartaoCombustivel(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.KM);
  }

  obterValorCartaoTel(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.CARTAO_TEL);
  }

  obterValorCorreio(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.CORREIO);
  }

  obterValorEstacionamento(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.ESTACIONAMENTO);
  }

  obterValorFerramentas(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.FERRAMENTAS);
  }

  obterValorFrete(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.FRETE);
  }

  obterValorHotel(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.HOTEL);
  }

  obterValorOnibus(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.ONIBUS);
  }

  obterValorOutros(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.OUTROS);
  }

  obterValorPA(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.PA);
  }

  obterValorPecas(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.PECAS);
  }

  obterValorPed(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.PEDAGIO);
  }

  obterValorTaxi(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.TAXI);
  }

  obterValorTelefone(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.TELEFONE);
  }

  obterValorInternet(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularTipoDespesa(dp, DespesaTipoEnum.INTERNET);
  }

  obterTotal(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularDespesaTotal(dp).toFixed(2).replace(".", ",");
  }

  obterAdiantamento(dp: DespesaProtocoloPeriodoTecnico)
  {
    return this.calcularAdiantamento(dp).toFixed(2).replace(".", ",");
  }

  obterSaldo(dp: DespesaProtocoloPeriodoTecnico)
  {
    return (this.calcularAdiantamento(dp) - this.calcularDespesaTotal(dp)).toFixed(2).replace(".", ",");;
  }
}