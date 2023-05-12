import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-relatorio-atendimento-detalhe',
  templateUrl: './relatorio-atendimento-detalhe.component.html'
})
export class RelatorioAtendimentoDetalheComponent implements OnInit {
  codOS: number;
  codRAT: number;
  loading: boolean = false;
  isAddMode: boolean;

  constructor(
    private _route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
		this.codRAT = +this._route.snapshot.paramMap.get('codRAT');
		this.isAddMode = !this.codRAT;
  }
}
