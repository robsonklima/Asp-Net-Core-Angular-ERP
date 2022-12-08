import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-ordem-servico-stn-form-historico',
  templateUrl: './ordem-servico-stn-form-historico.component.html'
})
export class OrdemServicoStnFormHistoricoComponent implements OnInit {
    @Input() codOS: number;
  constructor() { }

  ngOnInit(): void {
  }
}
