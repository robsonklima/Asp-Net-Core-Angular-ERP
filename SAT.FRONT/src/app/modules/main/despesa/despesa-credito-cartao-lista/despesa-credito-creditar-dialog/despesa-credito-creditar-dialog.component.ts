import { Component, Inject, LOCALE_ID, ViewEncapsulation } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { DespesaCreditosCartaoListView, DespesaPeriodoTecnico } from 'app/core/types/despesa-periodo.types';
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
  despesaPeriodoTecnico: DespesaPeriodoTecnico;
  isLoading: boolean = false;

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _despesaPeriodioTecnicoSvc: DespesaPeriodoTecnicoService,
    private _dialogRef: MatDialogRef<DespesaCreditoCreditarDialogComponent>)
  {
    if (data)
    {
      this.despesaCreditosCartaoListView = data.despesaCreditosCartaoListView;
      this.despesaPeriodoTecnico = data.despesaPeriodoTecnico;
    }
    this.obterDados();
  }

  async obterDados()
  {
    this.isLoading = true;

    await this.calcularCategoriaCredito();

    this.isLoading = false;
  }

  async calcularCategoriaCredito()
  {
    this.despesaPeriodoTecnico =
      (await this._despesaPeriodioTecnicoSvc
        .obterClassificacaoCreditoTecnico(this.despesaPeriodoTecnico)
        .toPromise());
  }

  getCategoriaCredito(c: TecnicoCategoriaCreditoEnum)
  {
    return TecnicoCategoriaCreditoEnum[c];
  }

  creditar(): void
  {
    this._dialogRef.close(true);
  }

  compensar(): void
  {
    this._dialogRef.close(true);
  }

  cancelar(): void
  {
    this._dialogRef.close(false);
  }
}