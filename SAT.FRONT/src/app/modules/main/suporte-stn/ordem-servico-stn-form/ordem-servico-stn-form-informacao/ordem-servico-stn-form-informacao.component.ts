import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-ordem-servico-stn-form-informacao',
  templateUrl: './ordem-servico-stn-form-informacao.component.html'
})
export class OrdemServicoStnFormInformacaoComponent implements OnInit {
    @Input() codOS: number;
  constructor() { }

  ngOnInit(){}

  salvar(){}
}
