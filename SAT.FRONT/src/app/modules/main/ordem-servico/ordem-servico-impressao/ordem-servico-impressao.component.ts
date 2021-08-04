import { Component, Input, OnInit } from '@angular/core';
import { OrdemServico } from 'app/core/types/ordem-servico.types';

@Component({
  selector: 'app-ordem-servico-impressao',
  templateUrl: './ordem-servico-impressao.component.html',
})
export class OrdemServicoImpressaoComponent implements OnInit {
  @Input() os: OrdemServico;

  constructor() { }

  ngOnInit(): void {}
}
