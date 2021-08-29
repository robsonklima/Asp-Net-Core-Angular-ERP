import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'app-dashboard-filtro',
  templateUrl: './dashboard-filtro.component.html'
})
export class DashboardFiltroComponent implements OnInit {
  @Input() sidenav: MatSidenav;
  form: FormGroup;

  constructor(
    private _formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.inicializarForm();
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      periodoInicio: [undefined],
      periodoFim: [undefined]
    });
  }

  aplicar(): void {
    
  }

  limpar(): void {

  }
}
