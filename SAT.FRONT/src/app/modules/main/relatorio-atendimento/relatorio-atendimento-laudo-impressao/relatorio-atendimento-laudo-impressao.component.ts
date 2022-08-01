import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appConfig } from 'app/core/config/app.config';
import { FotoService } from 'app/core/services/foto.service';
import { LaudoService } from 'app/core/services/laudo.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { Foto } from 'app/core/types/foto.types';
import { Laudo } from 'app/core/types/laudo.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import Enumerable from 'linq';
import moment from 'moment';

@Component({
  selector: 'app-relatorio-atendimento-laudo-impressao',
  templateUrl: './relatorio-atendimento-laudo-impressao.component.html',
  styleUrls: ['./relatorio-atendimento-laudo-impressao.component.scss']
})
export class RelatorioAtendimentoLaudoImpressaoComponent implements OnInit
{

  codOS: number;
  codLaudo: number;
  codRAT: number;
  os: OrdemServico;
  laudo: Laudo;
  rat: RelatorioAtendimento;
  tecnico: Tecnico;
  fotos: Foto[];
  loading: boolean;
  fotosLaudos: Foto[];

  constructor (
    private _ordemServicoService: OrdemServicoService,
    private _ratService: RelatorioAtendimentoService,
    private _route: ActivatedRoute,
    private _laudoService: LaudoService,
    private _fotoService: FotoService
  ) { }

  async ngOnInit()
  {
    this.loading = true;

    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    this.codLaudo = +this._route.snapshot.paramMap.get('codLaudo');
    this.codRAT = +this._route.snapshot.paramMap.get('codRAT');

    await this.obterLaudo();
    await this.obterOS();
    await this.obterRAT();

    this.loading = false;
  }

  private async obterLaudo()
  {
    this.laudo = (await this._laudoService.obterPorCodigo(this.codLaudo).toPromise());
  }

  private async obterOS()
  {
    this.os = (await this._ordemServicoService.obterPorCodigo(this.codOS).toPromise());
  }

  private async obterRAT()
  {
    if (this.codRAT) return
      this.rat = (await this._ratService.obterPorCodigo(this.codRAT).toPromise());

    if (this.rat?.numRAT && this.codOS)
      this.fotos = (await this._fotoService.obterPorParametros({ numRAT: this.rat.numRAT, codOS: this.codOS }).toPromise())?.items;
  }

  print()
  {
    var contentToPrint = document.getElementById("print-area").innerHTML;
    var windowPopup = window.open('', '_blank', 'width=500,height=500');
    windowPopup.document.open();
    windowPopup.document.write(`<html><head><link rel="stylesheet" type="text/css" href="${appConfig.tailwind_css}"/></head><body onload = "window.print()"> ${contentToPrint} </body></html>`);
    windowPopup.document.title = `OS_${this.codOS}.pdf`;
    windowPopup.onafterprint = window.close;
    windowPopup.document.close();
  }

  obterLaudoSituacaoFotos(index: number)
  {
    return Enumerable.from(this.fotos)
      .where(i => i.modalidade.toLowerCase().startsWith('laudo') && i.modalidade.slice(-1) == (index + 1).toString())
      .toArray();
  }

  obterAssinaturas()
  {
    return Enumerable.from(this.fotos)
      .where(i => i.modalidade.toLowerCase().startsWith('assinatura') && i.modalidade.toLowerCase().endsWith('laudo'))
      .toArray();
  }

  obterData()
  {
    return moment().format('DD/MM/yy');
  }
}