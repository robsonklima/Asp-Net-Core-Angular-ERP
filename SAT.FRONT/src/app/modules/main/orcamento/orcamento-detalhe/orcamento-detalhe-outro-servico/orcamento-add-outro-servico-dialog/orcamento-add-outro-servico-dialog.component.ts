import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { TipoServicoService } from 'app/core/services/tipo-servico.service';
import { TipoServico } from 'app/core/types/tipo-servico.types';
import moment from 'moment';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-orcamento-add-outro-servico-dialog',
  templateUrl: './orcamento-add-outro-servico-dialog.component.html'
})
export class OrcamentoAddOutroServicoDialogComponent implements OnInit
{
  form: FormGroup;
  protected _onDestroy = new Subject<void>();
  tipoServicos: TipoServico[] = [];

  constructor (
    private _formBuilder: FormBuilder,
    private _tipoService: TipoServicoService,
    public dialogRef: MatDialogRef<OrcamentoAddOutroServicoDialogComponent>
  )
  {
  }

  async ngOnInit()
  {
    this.inicializarForm();
    await this.obterDados();
  }

  private inicializarForm(): void
  {
    this.form = this._formBuilder.group({
      tipo: [undefined, [Validators.required]],
      descricao: [undefined, [Validators.required]],
      quantidade: [undefined, [Validators.required]],
      valorUnitario: [undefined, [Validators.required]]
    });
  }

  obterDados()
  {
    this.obterTiposServico();
  }

  async obterTiposServico() { this.tipoServicos = (await this._tipoService.obterPorParametros({}).toPromise()).items; }

  inserir(): void
  {

  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}