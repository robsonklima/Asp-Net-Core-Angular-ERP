import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-indicadores-filiais-detalhados',
  templateUrl: './indicadores-filiais-detalhados.component.html'
})
export class IndicadoresFiliaisDetalhadosComponent implements OnInit {
  loading: boolean = true;

  constructor() {}

  ngOnInit(): void {
    setTimeout(() => {
      this.loading = false;
    }, 3500);
  }
}