import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-laboratorio-processo-reparo-form-checklist',
  templateUrl: './laboratorio-processo-reparo-form-checklist.component.html'
})
export class LaboratorioProcessoReparoFormChecklistComponent implements OnInit {
  @Input() codORItem: number;

  constructor() { }

  ngOnInit(): void {
  }

}
