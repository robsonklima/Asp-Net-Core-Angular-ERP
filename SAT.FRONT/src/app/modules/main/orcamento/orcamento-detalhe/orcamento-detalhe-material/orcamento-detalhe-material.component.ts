import { Component, Input, OnInit } from '@angular/core';
import { Orcamento } from 'app/core/types/orcamento.types';

@Component({
  selector: 'app-orcamento-detalhe-material',
  templateUrl: './orcamento-detalhe-material.component.html'
})
export class OrcamentoDetalheMaterialComponent implements OnInit
{

  codOrc: number;
  isLoading: boolean;
  dataSourceData: any;
  @Input() orcamento: Orcamento;

  constructor () { }

  ngOnInit(): void
  {
  }

  excluir()
  {

  }

}
