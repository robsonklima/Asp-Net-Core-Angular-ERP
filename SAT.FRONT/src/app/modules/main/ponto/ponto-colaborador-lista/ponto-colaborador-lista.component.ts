import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-ponto-colaborador-lista',
  templateUrl: './ponto-colaborador-lista.component.html'
})
export class PontoColaboradorListaComponent implements OnInit {
  codPontoPeriodo: number;

  constructor(
    private _route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.codPontoPeriodo = +this._route.snapshot.paramMap.get('codPontoPeriodo');
  }
}
