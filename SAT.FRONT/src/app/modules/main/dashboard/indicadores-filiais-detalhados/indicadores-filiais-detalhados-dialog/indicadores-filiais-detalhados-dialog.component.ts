import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IndicadoresFiliaisComponent } from '../../indicadores-filiais/indicadores-filiais.component';

export interface DialogData
{
  codFilial: number;
}

@Component({
  selector: 'app-indicadores-filiais-detalhados-dialog',
  templateUrl: './indicadores-filiais-detalhados-dialog.component.html'
})
export class IndicadoresFiliaisDetalhadosDialogComponent implements OnInit {
  codFilial: number;

  constructor(
    public dialogRef: MatDialogRef<IndicadoresFiliaisComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
  ) {
    this.codFilial = data.codFilial;
  }

  ngOnInit(): void {
  }
}
