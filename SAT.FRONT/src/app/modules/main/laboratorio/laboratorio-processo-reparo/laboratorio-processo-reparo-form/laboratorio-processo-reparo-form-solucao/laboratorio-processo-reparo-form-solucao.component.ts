import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-laboratorio-processo-reparo-form-solucao',
  templateUrl: './laboratorio-processo-reparo-form-solucao.component.html'
})
export class LaboratorioProcessoReparoFormSolucaoComponent implements OnInit {
  @Input() codORItem: number;
  
  constructor() { }

  ngOnInit(): void {
  }

}
