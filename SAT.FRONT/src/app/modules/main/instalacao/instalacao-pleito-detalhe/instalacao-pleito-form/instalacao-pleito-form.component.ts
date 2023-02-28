import { Component, Input, OnInit } from '@angular/core';
import { InstalacaoPleito } from 'app/core/types/instalacao-pleito.types';

@Component({
  selector: 'app-instalacao-pleito-form',
  templateUrl: './instalacao-pleito-form.component.html'
})
export class InstalacaoPleitoFormComponent implements OnInit {
  @Input() instalPleito: InstalacaoPleito;

  constructor() {}

  ngOnInit(): void {
    console.log(this.instalPleito);
    
  }
}
