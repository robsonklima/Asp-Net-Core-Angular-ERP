import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { appConfig } from 'app/core/config/app.config';
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

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any) 
  {

    if (data)
      this.protocolo = data.protocolo;
  }

  async ngOnInit()
  {
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
    var sum = Enumerable.from(dp.despesaPeriodoTecnico.despesas).where(i => i.indAtivo == 1)
      .selectMany(i => i.despesaItens).where(i => i.indAtivo == 1 && i.codDespesaTipo != DespesaTipoEnum.KM && i.codDespesaTipo != DespesaTipoEnum.COMBUSTIVEL).sum(i => i.despesaValor);

    return sum.toFixed(2).replace(".", ",");
  }

  obterAdiantamento(dp: DespesaProtocoloPeriodoTecnico)
  {
    return "TO DO"
  }

  obterSaldo(dp: DespesaProtocoloPeriodoTecnico)
  {
    return "TO DO";
  }
}