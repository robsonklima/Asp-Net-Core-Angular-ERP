import { Component, Input, OnInit } from '@angular/core';
import { Laudo } from 'app/core/types/laudo.types';

@Component({
  selector: 'app-ordem-servico-laudo',
  templateUrl: './ordem-servico-laudo.component.html'
})
export class OrdemServicoLaudoComponent implements OnInit {
  @Input() laudo: Laudo;

  constructor() { }

  ngOnInit(): void {
  }
}
