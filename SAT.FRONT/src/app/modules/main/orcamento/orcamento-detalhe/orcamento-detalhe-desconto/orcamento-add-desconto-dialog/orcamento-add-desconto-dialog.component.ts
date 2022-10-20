import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBarConfig, MatSnackBar } from '@angular/material/snack-bar';
import { OrcamentoDescontoService } from 'app/core/services/orcamento-desconto.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { OrcamentoDesconto, OrcamentoFormaDescontoEnum, OrcamentoTipoDescontoEnum } from 'app/core/types/orcamento.types';
import Enumerable from 'linq';
import moment from 'moment';
import { Subject } from 'rxjs';
import { OrcamentoAddOutroServicoDialogComponent } from '../../orcamento-detalhe-outro-servico/orcamento-add-outro-servico-dialog/orcamento-add-outro-servico-dialog.component';

@Component({
  selector: 'app-orcamento-add-desconto-dialog',
  templateUrl: './orcamento-add-desconto-dialog.component.html'
})
export class OrcamentoAddDescontoDialogComponent implements OnInit
{
  snackConfigDanger: MatSnackBarConfig = { duration: 2000, panelClass: 'danger', verticalPosition: 'top', horizontalPosition: 'right' };
  snackConfigSuccess: MatSnackBarConfig = { duration: 2000, panelClass: 'success', verticalPosition: 'top', horizontalPosition: 'right' };

  form: FormGroup;
  protected _onDestroy = new Subject<void>();
  nomesTipo: string[] = [];
  nomesCampo: string[] = [];
  codOrc: number;

  constructor (@Inject(MAT_DIALOG_DATA) private data: any,
    private _formBuilder: FormBuilder,
    private _orcDescontoService: OrcamentoDescontoService,
    private _orcService: OrcamentoService,
    private _snack: MatSnackBar,
    public dialogRef: MatDialogRef<OrcamentoAddOutroServicoDialogComponent>)
  {
    if (data)
      this.codOrc = data.codOrc;
  }

  async ngOnInit()
  {
    this.inicializarForm();
    this.obterDados();
  }

  private inicializarForm(): void
  {
    this.form = this._formBuilder.group({
      codOrc: [this.codOrc, [Validators.required]],
      nomeCampo: [undefined, [Validators.required]],
      valor: [undefined, [Validators.required]],
      motivo: [undefined, [Validators.required]]
    });
  }

  async obterDados()
  {
    this.nomesTipo = Enumerable.from(OrcamentoTipoDescontoEnum)
      .select(i => i.value)
      .orderBy(i => i)
      .toArray();

    this.nomesCampo = Enumerable.from(OrcamentoFormaDescontoEnum)
      .select(i => i.value)
      .orderBy(i => i)
      .toArray();
  }

  inserir(): void
  {
    var desconto: OrcamentoDesconto = this.form.getRawValue();
    desconto.valorTotal = desconto.valor;
    desconto.dataCadastro = moment().format('yyyy-MM-DD HH:mm:ss');

    this._orcDescontoService.criar(desconto).subscribe(m =>
    {
      this._orcService.atualizarTotalizacao(m.codOrc);
      this._snack.open('Serviço adicionado com sucesso.', null, this.snackConfigSuccess).afterDismissed().toPromise();
      this.dialogRef.close(desconto);
    },
      e =>
      {
        this._snack.open('Erro ao adicionar serviço.', null, this.snackConfigDanger).afterDismissed().toPromise();
        this.dialogRef.close(null);
      });
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}