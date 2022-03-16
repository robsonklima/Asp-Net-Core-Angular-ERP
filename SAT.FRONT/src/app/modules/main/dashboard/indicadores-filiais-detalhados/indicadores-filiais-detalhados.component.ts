import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'app-indicadores-filiais-detalhados',
  templateUrl: './indicadores-filiais-detalhados.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class IndicadoresFiliaisDetalhadosComponent implements OnInit {
  loading: boolean = true;

  constructor() {}

  ngOnInit(): void {
    
  }
}