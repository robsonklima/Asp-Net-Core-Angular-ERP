import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrdemServico } from 'app/core/types/ordem-servico.types';

@Component({
  selector: 'app-ordem-servico-impressao',
  templateUrl: './ordem-servico-impressao.component.html',
})
export class OrdemServicoImpressaoComponent implements OnInit
{
  codOS: number;
  os: OrdemServico;

  constructor (
    private _ordemServicoService: OrdemServicoService,
    private _route: ActivatedRoute
  ) { }

  async ngOnInit()
  {
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    this.os = await this._ordemServicoService.obterPorCodigo(this.codOS).toPromise();
  }

  print()
  {
    var contentToPrint = document.getElementById("print-area").innerHTML;
    var windowPopup = window.open('', '_blank', 'width=500,height=500');
    windowPopup.document.open();
    windowPopup.document.write('<html><head><link rel="stylesheet" type="text/css" href="https://unpkg.com/tailwindcss@^1.0/dist/tailwind.min.css" /></head><body onload = "window.print()" > ' + contentToPrint + ' </body></html > ');
    windowPopup.document.close();
  }
}
