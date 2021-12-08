import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appConfig } from 'app/core/config/app.config';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Foto } from 'app/core/types/foto.types';
import { Laudo } from 'app/core/types/laudo.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import Enumerable from 'linq';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-ordem-servico-impressao',
  templateUrl: './ordem-servico-impressao.component.html',
  styleUrls: ['./ordem-servico-impressao.component.scss'],
})
export class OrdemServicoImpressaoComponent implements OnInit
{
  codOS: number;
  os: OrdemServico;
  loading: boolean;
  fotosLaudos: Foto[];

  constructor (
    private _ordemServicoService: OrdemServicoService,
    private _route: ActivatedRoute
  ) { }

  async ngOnInit()
  {
    this.loading = true;
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    await this._ordemServicoService.obterPorCodigo(this.codOS).pipe(
      finalize(() =>
      {
        this.fotosLaudos = Enumerable.from(this.os?.fotos)
          .where(i => i.modalidade.includes("LAUDO"))
          .toArray();

        this.loading = false;
      }))
      .subscribe(
        (successData) => { this.os = successData; },
        (err) => { }
      );
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

  getLaudos(rat: RelatorioAtendimento)
  {
    if (!this.os?.fotos?.length) return;

    return Enumerable.from(this.fotosLaudos)
      .where(i => i.numRAT == rat.numRAT).toArray();
  }
}