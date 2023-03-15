import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { appConfig } from 'app/core/config/app.config';
import { DespesaAdiantamentoPeriodoService } from 'app/core/services/despesa-adiantamento-periodo.service';
import { DespesaAdiantamentoPeriodo } from 'app/core/types/despesa-adiantamento.types';
import { DespesaProtocolo, DespesaProtocoloImpressaoListView, DespesaProtocoloPeriodoTecnico } from 'app/core/types/despesa-protocolo.types';
import { DespesaTipoEnum } from 'app/core/types/despesa.types';
import { statusConst } from 'app/core/types/status-types';
import Enumerable from 'linq';
import moment from 'moment';

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
  listView: DespesaProtocoloImpressaoListView[] = [];

  totalAluguelCarro: number = 0;
  totalCartaoCombustivel: number = 0;
  totalCartaoTel: number = 0;
  totalCorreios: number = 0;
  totalEstacionamento: number = 0;
  totalFerramentas: number = 0;
  totalFrete: number = 0;
  totalHotel: number = 0;
  totalOnibus: number = 0;
  totalOutros: number = 0;
  totalPA: number = 0;
  totalPecas: number = 0;
  totalPedagio: number = 0;
  totalTaxi: number = 0;
  totalTelefone: number = 0;
  totalRefeicao: number = 0;
  totalInternet: number = 0;
  totalDespesas: number = 0;
  totalAdiantamentos: number = 0;
  totalSaldos: number = 0;

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
    this.prepararLista();

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
    windowPopup.document.title = `Protocolo_${this.protocolo.codDespesaProtocolo}.pdf`;
    windowPopup.onafterprint = window.close;
    windowPopup.document.close();
  }

  private prepararLista()
  {
    this.protocolo.despesaProtocoloPeriodoTecnico.forEach(dp =>
    {
      this.listView.push(
        {
          dp: dp.codDespesaPeriodoTecnico,
          filial: dp.despesaPeriodoTecnico.tecnico.filial.nomeFilial,
          tecnico: dp.despesaPeriodoTecnico.tecnico.nome,
          periodo: moment(dp.despesaPeriodoTecnico.despesaPeriodo.dataInicio).format('DD/MM/yy') + " até " + moment(dp.despesaPeriodoTecnico.despesaPeriodo.dataFim).format('DD/MM/yy'),
          aluguelCarro: this.obterValorAluguelCarro(dp),
          cartaoCombustivel: this.obterValorCartaoCombustivel(dp),
          cartaoTel: this.obterValorCartaoTel(dp),
          combustivel: this.valorDescontinuado(),
          correios: this.obterValorCorreio(dp),
          estacionamento: this.obterValorEstacionamento(dp),
          ferramentas: this.obterValorFerramentas(dp),
          frete: this.obterValorFrete(dp),
          hotel: this.obterValorHotel(dp),
          onibus: this.obterValorOnibus(dp),
          outros: this.obterValorOutros(dp),
          pa: this.obterValorPA(dp),
          pecas: this.obterValorPecas(dp),
          pedagio: this.obterValorPedagio(dp),
          km: this.valorDescontinuado(),
          refeicao: this.obterRefeicao(dp),
          taxi: this.obterValorTaxi(dp),
          telefone: this.obterValorTelefone(dp),
          internet: this.obterValorInternet(dp),
          despesas: this.obterValorTotalDespesa(dp),
          adiantamentos: this.obterValorAdiantamento(dp),
          saldo: this.obterValorSaldo(dp)
        })
    })
  }

  private calcularTipoDespesa(dp: DespesaProtocoloPeriodoTecnico, tipo: DespesaTipoEnum)
  {
    return Enumerable.from(dp.despesaPeriodoTecnico.despesas).where(i => i.indAtivo == statusConst.ATIVO)
      .selectMany(i => i.despesaItens).where(i => i.indAtivo == statusConst.ATIVO
        && i.codDespesaTipo == tipo).sum(i => i.despesaValor);
  }

  private calcularAdiantamento(dp: DespesaProtocoloPeriodoTecnico)
  {
    var codTecnico = dp.despesaPeriodoTecnico.codTecnico;
    var codPeriodo = dp.despesaPeriodoTecnico.codDespesaPeriodo;

    return Enumerable.from(this.adiantamentos).where(i => i.despesaAdiantamento.indAtivo == statusConst.ATIVO
      && i.despesaAdiantamento.codTecnico == codTecnico && i.despesaPeriodo.codDespesaPeriodo == codPeriodo)
      .sum(i => i.valorAdiantamentoUtilizado);
  }

  private calcularDespesaTotal(dp: DespesaProtocoloPeriodoTecnico)
  {
    return Enumerable.from(dp.despesaPeriodoTecnico.despesas).where(i => i.indAtivo == statusConst.ATIVO)
      .selectMany(i => i.despesaItens).where(i => i.indAtivo == statusConst.ATIVO && i.codDespesaTipo != DespesaTipoEnum.KM).sum(i => i.despesaValor);
  }

  valorDescontinuado()
  {
    return "---";
  }

  toInterface(n: number)
  {
    return n.toFixed(2).replace(".", ",");
  }

  private obterValorAluguelCarro(dp: DespesaProtocoloPeriodoTecnico)
  {
    var aluguelCarro = this.calcularTipoDespesa(dp, DespesaTipoEnum.ALUGUEL_CARRO);
    this.totalAluguelCarro += aluguelCarro;

    return this.toInterface(aluguelCarro);
  }

  private obterValorCartaoCombustivel(dp: DespesaProtocoloPeriodoTecnico)
  {
    var cb = this.calcularTipoDespesa(dp, DespesaTipoEnum.KM);
    this.totalCartaoCombustivel += cb;

    return this.toInterface(cb);
  }

  private obterValorCartaoTel(dp: DespesaProtocoloPeriodoTecnico)
  {
    var cartaoTel = this.calcularTipoDespesa(dp, DespesaTipoEnum.CARTAO_TEL);
    this.totalCartaoTel += cartaoTel;

    return this.toInterface(cartaoTel);
  }

  private obterValorCorreio(dp: DespesaProtocoloPeriodoTecnico)
  {
    var correios = this.calcularTipoDespesa(dp, DespesaTipoEnum.CORREIO);
    this.totalCorreios += correios;

    return this.toInterface(correios);
  }

  private obterValorEstacionamento(dp: DespesaProtocoloPeriodoTecnico)
  {
    var estacionamento = this.calcularTipoDespesa(dp, DespesaTipoEnum.ESTACIONAMENTO);
    this.totalEstacionamento += estacionamento;

    return this.toInterface(estacionamento);
  }

  private obterValorFerramentas(dp: DespesaProtocoloPeriodoTecnico)
  {
    var ferramentas = this.calcularTipoDespesa(dp, DespesaTipoEnum.FERRAMENTAS);
    this.totalFerramentas += ferramentas;

    return this.toInterface(ferramentas);
  }

  private obterValorFrete(dp: DespesaProtocoloPeriodoTecnico)
  {
    var frete = this.calcularTipoDespesa(dp, DespesaTipoEnum.FRETE);
    this.totalFrete += frete;

    return this.toInterface(frete);
  }

  private obterValorHotel(dp: DespesaProtocoloPeriodoTecnico)
  {
    var hotel = this.calcularTipoDespesa(dp, DespesaTipoEnum.HOTEL);
    this.totalHotel += hotel;

    return this.toInterface(hotel);
  }

  private obterValorOnibus(dp: DespesaProtocoloPeriodoTecnico)
  {
    var onibus = this.calcularTipoDespesa(dp, DespesaTipoEnum.ONIBUS);
    this.totalOnibus += onibus;

    return this.toInterface(onibus);
  }

  private obterValorOutros(dp: DespesaProtocoloPeriodoTecnico)
  {
    var outros = this.calcularTipoDespesa(dp, DespesaTipoEnum.OUTROS);
    this.totalOutros += outros;

    return this.toInterface(outros);
  }

  private obterValorPA(dp: DespesaProtocoloPeriodoTecnico)
  {
    var pa = this.calcularTipoDespesa(dp, DespesaTipoEnum.PA);
    this.totalPA += pa;

    return this.toInterface(pa);
  }

  private obterValorPecas(dp: DespesaProtocoloPeriodoTecnico)
  {
    var pecas = this.calcularTipoDespesa(dp, DespesaTipoEnum.PECAS);
    this.totalPecas += pecas;

    return this.toInterface(pecas);
  }

  private obterValorPedagio(dp: DespesaProtocoloPeriodoTecnico)
  {
    var pedagio = this.calcularTipoDespesa(dp, DespesaTipoEnum.PEDAGIO);
    this.totalPedagio += pedagio;

    return this.toInterface(pedagio);
  }

  private obterValorTaxi(dp: DespesaProtocoloPeriodoTecnico)
  {
    var taxi = this.calcularTipoDespesa(dp, DespesaTipoEnum.TAXI);
    this.totalTaxi += taxi;

    return this.toInterface(taxi);
  }

  private obterValorTelefone(dp: DespesaProtocoloPeriodoTecnico)
  {
    var telefone = this.calcularTipoDespesa(dp, DespesaTipoEnum.TELEFONE);
    this.totalTelefone += telefone;

    return this.toInterface(telefone);
  }

  private obterRefeicao(dp: DespesaProtocoloPeriodoTecnico)
  {
    var refeicao = this.calcularTipoDespesa(dp, DespesaTipoEnum.REFEICAO);
    this.totalRefeicao += refeicao;

    return this.toInterface(refeicao);
  }

  private obterValorInternet(dp: DespesaProtocoloPeriodoTecnico)
  {
    var internet = this.calcularTipoDespesa(dp, DespesaTipoEnum.INTERNET);
    this.totalInternet += internet;

    return this.toInterface(internet);
  }

  private obterValorTotalDespesa(dp: DespesaProtocoloPeriodoTecnico)
  {
    var total = this.calcularDespesaTotal(dp);
    this.totalDespesas += total;

    return this.toInterface(total);
  }

  private obterValorAdiantamento(dp: DespesaProtocoloPeriodoTecnico)
  {
    var adiantamento = this.calcularAdiantamento(dp);
    this.totalAdiantamentos += adiantamento;

    return this.toInterface(adiantamento);
  }

  private obterValorSaldo(dp: DespesaProtocoloPeriodoTecnico)
  {
    var saldo = this.calcularAdiantamento(dp) - this.calcularDespesaTotal(dp);
    this.totalSaldos += saldo;

    return this.toInterface(saldo);
  }
}