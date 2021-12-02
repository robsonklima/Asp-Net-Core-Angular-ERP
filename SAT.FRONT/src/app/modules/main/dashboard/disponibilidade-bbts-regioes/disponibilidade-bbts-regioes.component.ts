import { Component, Input, OnInit } from '@angular/core';
import { Filtro } from 'app/core/types/filtro.types';

@Component({
  selector: 'app-disponibilidade-bbts-regioes',
  templateUrl: './disponibilidade-bbts-regioes.component.html'
})
export class DisponibilidadeBbtsRegioesComponent implements OnInit {
  @Input() filtro: Filtro;
  constructor() { }

  ngOnInit(): void {
  }

}
