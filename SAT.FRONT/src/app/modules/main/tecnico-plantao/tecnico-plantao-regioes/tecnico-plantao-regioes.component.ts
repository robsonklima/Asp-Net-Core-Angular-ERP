import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PlantaoTecnico, PlantaoTecnicoRegiao, PlantaoTecnicoRegiaoData } from 'app/core/types/plantao-tecnico.types';
import { DialogData } from '../tecnico-plantao-informacoes/tecnico-plantao-informacoes.component';
import { TecnicoPlantaoListaComponent } from '../tecnico-plantao-lista/tecnico-plantao-lista.component';
import { PlantaoTecnicoRegiaoService } from 'app/core/services/plantao-tecnico-regiao.service';

@Component({
  selector: 'app-tecnico-plantao-regioes',
  templateUrl: './tecnico-plantao-regioes.component.html',
  styles: [`tr:nth-child(odd) { background-color: rgb(239,245,254); }`]
})
export class TecnicoPlantaoRegioesComponent implements OnInit {
  plantaoTecnico: PlantaoTecnico;
  plantaoRegioes: PlantaoTecnicoRegiaoData;

  constructor(
    public dialogRef: MatDialogRef<TecnicoPlantaoListaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private _plantaoTecnicoRegiaoSrv: PlantaoTecnicoRegiaoService
  ) {
    this.plantaoTecnico = data.plantaoTecnico;
  }

  async ngOnInit(){
    this.plantaoRegioes = await this._plantaoTecnicoRegiaoSrv.obterPorParametros({ codPlantaoTecnico: this.plantaoTecnico.codPlantaoTecnico}).toPromise();
  }
}
