import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { OrcamentoAprovacao } from 'app/core/types/orcamento.types';
import { EmailValidator } from 'app/core/validators/email.validator';

@Component({
  selector: 'app-orcamento-aprovacao',
  templateUrl: './orcamento-aprovacao.component.html',
})
export class OrcamentoAprovacaoComponent implements OnInit {
  codOrc: number;
  form: FormGroup;

  constructor(
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _formBuilder: FormBuilder,
    private _orcamentoService: OrcamentoService
  ) { }

  ngOnInit(): void {
    this.codOrc = +this._route.snapshot.paramMap.get('codOrc');
    this.inicializarForm();
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      motivo: [undefined,[Validators.required]],
      nome: [undefined, [Validators.required]],
      email: [undefined, [Validators.required, EmailValidator()]],
      departamento: [undefined, [Validators.required]],
      telefone: [undefined, [Validators.required]],
      ramal: [undefined,[Validators.required]],
      isAprovado: [true, undefined],
    });
  }

  aprovarReprovar() {
    const form: any = this.form.getRawValue();

    let aprovacao: OrcamentoAprovacao = {
      ...form,
			...{
        codOrc: this.codOrc
      },
    }

    this._orcamentoService.aprovar(aprovacao).subscribe(() => {
      this._snack.exibirToast("Aprovação enviada com sucesso!");
    });
  }
}
