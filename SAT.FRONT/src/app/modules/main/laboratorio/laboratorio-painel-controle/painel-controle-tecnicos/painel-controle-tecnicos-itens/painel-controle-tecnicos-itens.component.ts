import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ORService } from 'app/core/services/or.service';
import { ViewLaboratorioTecnicoBancada } from 'app/core/types/bancada-laboratorio.types';
import { OR } from 'app/core/types/or.types';

@Component({
  selector: 'app-painel-controle-tecnicos-itens',
  templateUrl: './painel-controle-tecnicos-itens.component.html'
})
export class PainelControleTecnicosItensComponent implements OnInit {
  or: OR;
  tecnicoBancada: ViewLaboratorioTecnicoBancada;
  loading: boolean = true;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _orService: ORService,
    private _cdr: ChangeDetectorRef
  ) {
    this.tecnicoBancada = data?.tecnicoBancada;
  }

  async ngOnInit() {
    this.or = await this._orService.obterPorCodigo(this.tecnicoBancada.codOR).toPromise();
    this.loading = false;
    this._cdr.detectChanges();
  }
}
