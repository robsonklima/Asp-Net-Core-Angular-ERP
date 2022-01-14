import { Component, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { appConfig } from 'app/core/config/app.config';
import { FilialService } from 'app/core/services/filial.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Filial } from 'app/core/types/filial.types';
import { Orcamento } from 'app/core/types/orcamento.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import moment from 'moment';

@Component({
  selector: 'app-orcamento-impressao',
  templateUrl: './orcamento-impressao.component.html',
  styleUrls: ['./orcamento-impressao.component.scss'],
  providers: [
    {
      provide: LOCALE_ID,
      useValue: 'pt'
    }
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrcamentoImpressaoComponent implements OnInit
{
  codOrcamento: number;
  orcamento: Orcamento;
  os: OrdemServico;
  filial: Filial;
  loading: boolean;

  constructor (
    private _orcService: OrcamentoService,
    private _osService: OrdemServicoService,
    private _filialService: FilialService,
    private _route: ActivatedRoute,
  ) { }

  async ngOnInit()
  {
    this.loading = true;
    this.codOrcamento = +this._route.snapshot.paramMap.get('codOrc');

    await this._orcService.obterPorCodigo(this.codOrcamento)
      .subscribe(
        async (successData) =>
        {
          this.orcamento = successData;
          this.os = await this._osService.obterPorCodigo(this.orcamento.codigoOrdemServico).toPromise();
          this.filial = await this._filialService.obterPorCodigo(this.orcamento.codigoFilial).toPromise();
          this.loading = false;
        },
        (err) => { }
      );
  }

  print()
  {
    var contentToPrint = document.getElementById("print-area").innerHTML;
    var windowPopup = window.open('', '_blank', 'width=500,height=500');
    windowPopup.document.open();
    windowPopup.document.write(`<html><head><link rel="stylesheet" type="text/css" href="${appConfig.tailwind_css}"/></head><body onload = "window.print()"> ${contentToPrint} </body></html>`);
    windowPopup.document.title = `ORC_${this.codOrcamento}.pdf`;
    windowPopup.onafterprint = window.close;
    windowPopup.document.close();
  }

  obterData()
  {
    return moment().format('DD/MM/yy');
  }
}