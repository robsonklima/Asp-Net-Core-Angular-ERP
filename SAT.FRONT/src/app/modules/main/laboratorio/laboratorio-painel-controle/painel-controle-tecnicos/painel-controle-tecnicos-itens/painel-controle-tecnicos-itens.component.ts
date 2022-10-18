import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ViewLaboratorioTecnicoBancada } from 'app/core/types/bancada-laboratorio.types';

@Component({
  selector: 'app-painel-controle-tecnicos-itens',
  templateUrl: './painel-controle-tecnicos-itens.component.html'
})
export class PainelControleTecnicosItensComponent implements OnInit {
  tecnicoBancada: ViewLaboratorioTecnicoBancada;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.tecnicoBancada = data?.tecnicoBancada;
  }

  ngOnInit(): void {

  }
}
