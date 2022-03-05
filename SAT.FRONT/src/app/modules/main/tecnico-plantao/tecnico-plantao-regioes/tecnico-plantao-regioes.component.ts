import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PlantaoTecnico } from 'app/core/types/plantao-tecnico.types';
import { DialogData } from '../tecnico-plantao-informacoes/tecnico-plantao-informacoes.component';
import { TecnicoPlantaoListaComponent } from '../tecnico-plantao-lista/tecnico-plantao-lista.component';

@Component({
  selector: 'app-tecnico-plantao-regioes',
  templateUrl: './tecnico-plantao-regioes.component.html',
  styles: [`tr:nth-child(odd) { background-color: rgb(239,245,254); }`]
})
export class TecnicoPlantaoRegioesComponent implements OnInit {
  plantaoTecnico: PlantaoTecnico;

  constructor(
    public dialogRef: MatDialogRef<TecnicoPlantaoListaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData
  ) {
    this.plantaoTecnico = data.plantaoTecnico;
  }

  ngOnInit(): void {
  }
}
