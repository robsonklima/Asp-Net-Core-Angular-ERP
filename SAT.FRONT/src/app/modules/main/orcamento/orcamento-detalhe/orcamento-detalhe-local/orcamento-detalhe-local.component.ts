import { Component, Input, OnInit } from '@angular/core';
import { OrcamentoDadosLocal } from 'app/core/types/orcamento.types';

@Component({
  selector: 'app-orcamento-detalhe-local',
  templateUrl: './orcamento-detalhe-local.component.html'
})
export class OrcamentoDetalheLocalComponent implements OnInit
{
  @Input() dadosLocal: OrcamentoDadosLocal;

  constructor () { }

  ngOnInit(): void
  {
  }
}