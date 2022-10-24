import { Component, Input, OnInit } from '@angular/core';
import { OrdemServico } from 'app/core/types/ordem-servico.types';

@Component({
  selector: 'app-ordem-servico-alertas',
  templateUrl: './ordem-servico-alertas.component.html'
})
export class OrdemServicoAlertasComponent implements OnInit {
  @Input() os: OrdemServico;
  isLoading: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }
}
