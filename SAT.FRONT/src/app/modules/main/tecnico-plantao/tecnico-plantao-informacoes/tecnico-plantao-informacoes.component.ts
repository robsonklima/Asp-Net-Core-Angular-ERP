import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { PlantaoTecnico } from 'app/core/types/plantao-tecnico.types';
import { TecnicoPlantaoListaComponent } from '../tecnico-plantao-lista/tecnico-plantao-lista.component';

export interface DialogData
{
  plantaoTecnico: PlantaoTecnico;
}

@Component({
  selector: 'app-tecnico-plantao-informacoes',
  templateUrl: './tecnico-plantao-informacoes.component.html',
  styles: [`tr:nth-child(odd) { background-color: rgb(239,245,254); }`]
})
export class TecnicoPlantaoInformacoesComponent implements OnInit {
  plantaoTecnico: PlantaoTecnico;
  loading: boolean;
  chamados: OrdemServico[] = []

  constructor(
    public dialogRef: MatDialogRef<TecnicoPlantaoListaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private _ordemServicoService: OrdemServicoService
  ) {
    this.plantaoTecnico = data.plantaoTecnico;
  }

  ngOnInit(): void {
    this.obterChamadosTransferidos();
  }

  private async obterChamadosTransferidos() {
    this.loading = true;
    
    const data = await this._ordemServicoService.obterPorParametros({ 
      codTecnico: this.plantaoTecnico.codTecnico,
      codStatusServicos: "8"
    }).toPromise();

    this.chamados = data.items;
    this.loading = false;
  }
}
