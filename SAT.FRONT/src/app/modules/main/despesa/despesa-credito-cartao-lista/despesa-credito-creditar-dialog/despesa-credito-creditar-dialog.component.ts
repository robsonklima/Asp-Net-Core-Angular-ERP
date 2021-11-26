import { Component, Inject, LOCALE_ID, ViewEncapsulation } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { DespesaCreditosCartaoListView } from 'app/core/types/despesa-periodo.types';
import { TecnicoCategoriaCreditoEnum } from 'app/core/types/tecnico.types';

@Component({
  selector: 'app-despesa-credito-creditar-dialog',
  templateUrl: './despesa-credito-creditar-dialog.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})
export class DespesaCreditoCreditarDialogComponent
{
  despesaCreditosCartaoListView: DespesaCreditosCartaoListView;

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<DespesaCreditoCreditarDialogComponent>)
  {
    if (data)
      this.despesaCreditosCartaoListView = data.despesaCreditosCartaoListView;
  }

  getCategoriaCredito(c: TecnicoCategoriaCreditoEnum)
  {
    return TecnicoCategoriaCreditoEnum[c];
  }

  creditar(): void
  {
    this.dialogRef.close(true);
  }

  compensar(): void
  {
    this.dialogRef.close(true);
  }

  cancelar(): void
  {
    this.dialogRef.close(false);
  }
}