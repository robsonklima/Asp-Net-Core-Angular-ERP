import { ActivatedRoute } from '@angular/router';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { Location } from '@angular/common';
import { statusConst } from 'app/core/types/status-types';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { FormaPagamento } from 'app/core/types/forma-pagamento.types';
import { FormaPagamentoService } from 'app/core/services/forma-pagamento.service';

@Component({
  selector: 'app-formas-pagamento-form',
  templateUrl: './formas-pagamento-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class FormasPagamentoFormComponent implements OnInit, OnDestroy {
  codFormaPagto: number;
  isAddMode: boolean;
  form: FormGroup;
  formaPagamento: FormaPagamento;

  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _formasPagamentoService: FormaPagamentoService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.codFormaPagto = +this._route.snapshot.paramMap.get('codFormaPagto');
    this.isAddMode = !this.codFormaPagto;

    this.form = this._formBuilder.group({
      codFormaPagto: [
        {
          value: undefined,
          disabled: true
        },
      ],
      descFormaPagto: ['', Validators.required],
      percAjuste: [undefined, Validators.required],
      indAtivo: [statusConst.ATIVO]
    });

    if (!this.isAddMode) {
      this._formasPagamentoService.obterPorCodigo(this.codFormaPagto)
        .pipe(first())
        .subscribe(data => {
          this.form.patchValue(data);
          this.formaPagamento = data;
        })
    }
  }

  public salvar(): void {

    const form = this.form.getRawValue();

    let obj = {
      ...this.formaPagamento,
      ...form,
      indAtivo: +form.indAtivo
    };

    if (this.isAddMode) {
      this._formasPagamentoService.criar(obj).subscribe(() => {
        this._snack.exibirToast(`Forma de pagamento adicionada com sucesso!`, "success");
        this._location.back();
      });
    } else {
      this._formasPagamentoService.atualizar(obj).subscribe(() => {
        this._snack.exibirToast(`Forma de pagamento atualizada com sucesso!`, "success");
        this._location.back();
      });
    }
  }

  ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
