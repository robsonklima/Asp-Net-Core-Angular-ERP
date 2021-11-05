import { Inject, Component, LOCALE_ID, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DespesaItemService } from 'app/core/services/despesa-item.service';
import { DespesaTipoService } from 'app/core/services/despesa-tipo.service';
import { DespesaItem, DespesaTipo } from 'app/core/types/despesa.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import Enumerable from 'linq';
import moment from 'moment';

@Component({
  selector: 'app-despesa-item-dialog',
  templateUrl: './despesa-item-dialog.component.html',
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})
export class DespesaItemDialogComponent implements OnInit
{
  despesaItemForm: FormGroup;
  userSession: UserSession;
  tiposDespesa: DespesaTipo[] = [];
  codDespesa: number;

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _formBuilder: FormBuilder,
    private _despesaTipoSvc: DespesaTipoService,
    private _despesaItemSvc: DespesaItemService,
    private _userSvc: UserService,
    private dialogRef: MatDialogRef<DespesaItemDialogComponent>)
  {
    if (data)
    {
      this.codDespesa = data.codDespesa;
    }
    this.userSession = JSON.parse(this._userSvc.userSession);

    this.obterTiposDespesa();
    this.criarFormularioDespesaItem();
  }

  async ngOnInit() { }

  private async obterTiposDespesa()
  {
    this.tiposDespesa = (await this._despesaTipoSvc.obterPorParametros({ indAtivo: 1 }).toPromise()).items;
  }

  private criarFormularioDespesaItem()
  {
    this.despesaItemForm = this._formBuilder.group({
      step1: this._formBuilder.group({
        codDespesaTipo: [undefined, Validators.required]
      }),
      step2: this._formBuilder.group({
        notaFiscal: [undefined],
        valor: [undefined, Validators.required]
      }),
      step3: this._formBuilder.group({
        revision: [undefined]
      }),
    });
  }

  confirmar(): void
  {
    var despesaItem: DespesaItem =
    {
      codDespesa: this.codDespesa,
      numNF: this.despesaItemForm.value.step2.notaFiscal,
      codDespesaTipo: this.despesaItemForm.value.step1.codDespesaTipo,
      despesaValor: this.despesaItemForm.value.step2.valor,
      codUsuarioCad: this.userSession.usuario.codUsuario,
      dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss'),
      codDespesaItemAlerta: 1
    };

    this._despesaItemSvc.criar(despesaItem)
      .subscribe(
        () => this.dialogRef.close(true),
        () => this.dialogRef.close(false));
  }

  obterTipoDespesa()
  {
    return Enumerable.from(this.tiposDespesa)
      .firstOrDefault(i => i.codDespesaTipo == this.despesaItemForm.value.step1.codDespesaTipo)?.nomeTipo;
  }
}