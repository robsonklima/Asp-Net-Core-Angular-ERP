import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { IndicadoresFiliaisComponent } from '../indicadores-filiais.component';

@Component({
  selector: 'app-indicadores-filiais-opcoes',
  templateUrl: './indicadores-filiais-opcoes.component.html',
})
export class IndicadoresFiliaisOpcoesComponent {

  constructor(
    protected _dialogRef: MatDialogRef<IndicadoresFiliaisComponent>,
  ) { }

  cancelar() {
    this._dialogRef.close();
  }

  navegar(filial: string) {    
    this._dialogRef.close(filial);
  }
}
