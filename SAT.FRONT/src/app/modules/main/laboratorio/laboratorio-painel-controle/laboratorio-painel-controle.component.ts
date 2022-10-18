import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-laboratorio-painel-controle',
  templateUrl: './laboratorio-painel-controle.component.html'
})
export class LaboratorioPainelControleComponent implements OnInit {
  visualizacao: string = 'Técnicos Disponíveis';

  constructor() { }

  ngOnInit(): void {

  }
}
