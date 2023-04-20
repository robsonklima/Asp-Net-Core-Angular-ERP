import { Component, Inject, OnInit } from '@angular/core';
import { TecnicoContaService } from 'app/core/services/tecnico-conta-service';
import { TecnicoContaListaComponent } from '../tecnico-conta-lista/tecnico-conta-lista.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TecnicoConta } from 'app/core/types/tecnico.types';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-tecnico-conta-form',
  templateUrl: './tecnico-conta-form-dialog.component.html'
})
export class TecnicoContaFormDialogComponent implements OnInit {
  conta: TecnicoConta;
  form: FormGroup;
  isAddMode: boolean;

  constructor(
    private _tecnicoContaService: TecnicoContaService,
    private _formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.conta = data?.conta;
  }

  ngOnInit(): void {
    this.inicializarForm();

    if (this.conta)
      this.isAddMode = false;
  }

  inicializarForm() {
    this.form = this._formBuilder.group({
      numConta: [undefined, [Validators.required]],
      numAgencia: [undefined, [Validators.required]],
      numBanco: [undefined, [Validators.required]]
    });

    if (!this.isAddMode)
      this.form.patchValue(this.conta);
  }

  salvar() {

  }
}
