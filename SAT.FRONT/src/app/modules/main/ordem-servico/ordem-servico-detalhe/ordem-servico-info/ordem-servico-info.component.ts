import { Component, Input, OnInit } from '@angular/core';
import { OrdemServico } from 'app/core/types/ordem-servico.types';

@Component({
  selector: 'app-ordem-servico-info',
  templateUrl: './ordem-servico-info.component.html'
})
export class OrdemServicoInfoComponent implements OnInit {
  @Input() os: OrdemServico;

  constructor() { }

  ngOnInit(): void {
  }
}
