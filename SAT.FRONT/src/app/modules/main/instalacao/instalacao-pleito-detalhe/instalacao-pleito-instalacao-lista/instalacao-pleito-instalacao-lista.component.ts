import { Component, Input, OnInit } from '@angular/core';
import { InstalacaoPleito } from 'app/core/types/instalacao-pleito.types';

@Component({
  selector: 'app-instalacao-pleito-instalacao-lista',
  templateUrl: './instalacao-pleito-instalacao-lista.component.html'
})
export class InstalacaoPleitoInstalacaoListaComponent implements OnInit {
  @Input() instalPleito: InstalacaoPleito;

  constructor() { }

  ngOnInit(): void {
    console.log(this.instalPleito);
    
  }
}
