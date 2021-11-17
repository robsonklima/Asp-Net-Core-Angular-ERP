import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { appConfig } from 'app/core/config/app.config';
import { DespesaProtocolo } from 'app/core/types/despesa-protocolo.types';

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
    this.isLoading = true;
    this.isLoading = false;
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
}