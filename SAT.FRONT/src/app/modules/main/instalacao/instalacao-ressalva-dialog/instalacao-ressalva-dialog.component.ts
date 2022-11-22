import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { InstalacaoRessalvaService } from 'app/core/services/instalacao-ressalva.service';
import { InstalacaoRessalva } from 'app/core/types/instalacao-ressalva.types';
import { InstalacaoListaComponent } from '../instalacao-lista/instalacao-lista.component';

@Component({
  selector: 'app-instalacao-ressalva-dialog',
  templateUrl: './instalacao-ressalva-dialog.component.html'
})
export class InstalacaoRessalvaDialogComponent implements OnInit {
  codInstalacao: number;
  ressalvas: InstalacaoRessalva[] = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) protected data: any,
    protected dialogRef: MatDialogRef<InstalacaoListaComponent>,
    private _ressalvaService: InstalacaoRessalvaService
  ) { 
    this.codInstalacao = data?.codInstalacao;
  }

  async ngOnInit() {
    console.log(this.codInstalacao);    
    this.obterRessalvas();
  }

  async salvar() {
    
  }

  async obterRessalvas(){
    debugger
    const data = await this._ressalvaService.obterPorParametros({
      codInstalacao: this.codInstalacao
    }).toPromise();

    console.log(data);    
  }

  fechar() {
    this.dialogRef.close();
  }
}
