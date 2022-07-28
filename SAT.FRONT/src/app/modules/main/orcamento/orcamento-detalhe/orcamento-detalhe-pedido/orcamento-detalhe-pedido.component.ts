import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-orcamento-detalhe-pedido',
  templateUrl: './orcamento-detalhe-pedido.component.html'
})
export class OrcamentoDetalhePedidoComponent implements OnInit {
  @Input() codOrc: number;
  isLoading: boolean;

  constructor() { }

  ngOnInit(): void {

  }
}
