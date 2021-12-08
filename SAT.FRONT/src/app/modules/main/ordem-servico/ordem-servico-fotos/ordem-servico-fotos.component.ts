import { Component, Input, OnInit } from '@angular/core';
import { Foto, FotoModalidadeEnum } from 'app/core/types/foto.types';

@Component({
  selector: 'app-ordem-servico-fotos',
  templateUrl: './ordem-servico-fotos.component.html'
})
export class OrdemServicoFotosComponent implements OnInit
{
  @Input() fotos: Foto[];

  constructor () { }

  ngOnInit(): void
  {
  }

  formatarModalidadeFoto(modalidade: string): string
  {
    return FotoModalidadeEnum[modalidade];
  }
}
