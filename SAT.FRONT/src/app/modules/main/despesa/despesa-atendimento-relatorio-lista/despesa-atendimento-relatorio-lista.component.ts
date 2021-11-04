import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-despesa-atendimento-relatorio-lista',
  templateUrl: './despesa-atendimento-relatorio-lista.component.html'
})
export class DespesaAtendimentoRelatorioListaComponent implements OnInit
{

  constructor (
    private _route: ActivatedRoute) { }

  private codDespesaPeriodo: number;

  ngOnInit(): void
  {
    this.codDespesaPeriodo = +this._route.snapshot.paramMap.get('codDespesaPeriodo');
  }
}
