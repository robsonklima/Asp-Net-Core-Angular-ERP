import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PlantaoTecnico, PlantaoTecnicoClienteData } from 'app/core/types/plantao-tecnico.types';
import { DialogData } from '../tecnico-plantao-informacoes/tecnico-plantao-informacoes.component';
import { TecnicoPlantaoListaComponent } from '../tecnico-plantao-lista/tecnico-plantao-lista.component';
import { PlantaoTecnicoClienteService } from 'app/core/services/plantao-tecnico-cliente.service';

@Component({
  selector: 'app-tecnico-plantao-clientes',
  templateUrl: './tecnico-plantao-clientes.component.html',
  styles: [`tr:nth-child(odd) { background-color: rgb(239,245,254); }`]
})
export class TecnicoPlantaoClientesComponent implements OnInit {
  plantaoTecnico: PlantaoTecnico;
  plantaoClientes: PlantaoTecnicoClienteData;

  constructor(
    public dialogRef: MatDialogRef<TecnicoPlantaoListaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private _plantaoTecnicoClienteSrv: PlantaoTecnicoClienteService
  ) {
    this.plantaoTecnico = data.plantaoTecnico;
  }

  async ngOnInit(){
    this.plantaoClientes = await this._plantaoTecnicoClienteSrv.obterPorParametros({ codPlantaoTecnico: this.plantaoTecnico.codPlantaoTecnico}).toPromise();
  }
}
