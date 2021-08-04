import { Component, Input, OnInit } from '@angular/core';
import { FotoModalidadeEnum } from 'app/core/types/foto.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';

@Component({
  selector: 'app-ordem-servico-fotos',
  templateUrl: './ordem-servico-fotos.component.html'
})
export class OrdemServicoFotosComponent implements OnInit {
  @Input() os: OrdemServico;

  constructor() {}

  ngOnInit(): void {
  }

  formatarModalidadeFoto(modalidade: string): string {
    return FotoModalidadeEnum[modalidade];
  }
}
