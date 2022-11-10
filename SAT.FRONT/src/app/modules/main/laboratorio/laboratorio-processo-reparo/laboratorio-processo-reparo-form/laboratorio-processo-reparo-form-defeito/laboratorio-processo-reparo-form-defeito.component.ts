import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-laboratorio-processo-reparo-form-defeito',
  templateUrl: './laboratorio-processo-reparo-form-defeito.component.html'
})
export class LaboratorioProcessoReparoFormDefeitoComponent implements OnInit {
  @Input() codORItem: number;

  constructor() { }

  ngOnInit(): void {
  }

}
