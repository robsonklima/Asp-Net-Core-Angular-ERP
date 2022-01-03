import { Component, Input, OnInit } from '@angular/core';
import { OrcamentoDadosLocal, OrcamentoDadosLocalEnum } from 'app/core/types/orcamento.types';

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


  obterTitulo()
  {
    switch (this.dadosLocal.tipo)
    {
      case OrcamentoDadosLocalEnum.ATENDIMENTO:
        return "Atendimento/Ocorrência";
      case OrcamentoDadosLocalEnum.FATURAMENTO:
        return "Faturamento";
      case OrcamentoDadosLocalEnum.NOTA_FISCAL:
        return "Nota Fiscal";
    }
  }
}