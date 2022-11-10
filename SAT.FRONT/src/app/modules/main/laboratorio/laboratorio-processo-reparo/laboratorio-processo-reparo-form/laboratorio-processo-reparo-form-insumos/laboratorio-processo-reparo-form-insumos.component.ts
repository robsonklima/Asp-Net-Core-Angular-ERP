import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-laboratorio-processo-reparo-form-insumos',
  templateUrl: './laboratorio-processo-reparo-form-insumos.component.html'
})
export class LaboratorioProcessoReparoFormInsumosComponent implements OnInit {
  @Input() codORItem: number;

  constructor() { }

  ngOnInit(): void {
  }

}
