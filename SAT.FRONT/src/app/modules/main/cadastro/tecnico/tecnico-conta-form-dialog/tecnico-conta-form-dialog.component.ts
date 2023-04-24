import { Component, Inject, OnInit } from '@angular/core';
import { TecnicoContaService } from 'app/core/services/tecnico-conta-service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Tecnico, TecnicoConta } from 'app/core/types/tecnico.types';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import moment from 'moment';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { UserService } from 'app/core/user/user.service';
import { Usuario } from 'app/core/types/usuario.types';
import { UserSession } from 'app/core/user/user.types';
import { statusConst } from 'app/core/types/status-types';
import { TecnicoContaListaComponent } from '../tecnico-conta-lista/tecnico-conta-lista.component';

@Component({
  selector: 'app-tecnico-conta-form',
  templateUrl: './tecnico-conta-form-dialog.component.html'
})
export class TecnicoContaFormDialogComponent implements OnInit {
  conta: TecnicoConta;
  tecnico: Tecnico;
  form: FormGroup;
  isAddMode: boolean;
  userSession: UserSession;

  constructor(
    private _tecnicoContaService: TecnicoContaService,
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _userService: UserService,
    private _dialogRef: MatDialogRef<TecnicoContaListaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.conta = data?.conta;
    this.tecnico = data?.tecnico;
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.inicializarForm();

    if (!this.conta)
      this.isAddMode = true;
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
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private async criar() {
    const form = this.form.getRawValue();

    let obj = {
      ...this.conta,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        indPadrao: 0,
        indAtivo: statusConst.ATIVO,
        codTecnico: this.tecnico.codTecnico
      }
    };

    debugger

    this._tecnicoContaService.criar(obj).subscribe(() => {
      this._snack.exibirToast(`Conta ${obj.numAgencia}/${obj.numBanco} adicionada com sucesso!`, "success");
      this._dialogRef.close(true);
    }, e => {
      this._snack.exibirToast(`Erro ao criar conta!`, "error");
    });
  }

  private async atualizar() {
    const form = this.form.getRawValue();

    let obj = {
      ...this.conta,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
        indPadrao: 0,
        indAtivo: statusConst.ATIVO
      }
    };

    this._tecnicoContaService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast(`Conta ${obj.numAgencia}/${obj.numBanco} atualizada com sucesso!`, "success");

      this._dialogRef.close(true);
    });
  }
}
