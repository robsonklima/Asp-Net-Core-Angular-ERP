import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ConferenciaService } from 'app/core/services/conferencia.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Conferencia } from 'app/core/types/conferencia.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';

@Component({
  selector: 'app-conferencia-form',
  templateUrl: './conferencia-form.component.html'
})
export class ConferenciaFormComponent implements OnInit {
  codConferencia: number;
  conferencia: Conferencia;
  form: FormGroup;
  isAddMode: boolean;
  usuarioSessao: UsuarioSessao;
  loading: boolean = false;

  constructor(
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _conferenciaService: ConferenciaService,
    private _formBuilder: FormBuilder,
    private _location: Location
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codConferencia = +this._route.snapshot.paramMap.get('codConferencia');
    this.isAddMode = !this.codConferencia;
    this.inicializarForm();

    if (!this.isAddMode) {
      this.conferencia =  await this._conferenciaService.obterPorCodigo(this.codConferencia).toPromise();
      this.form.patchValue(this.conferencia);
    }
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      codConferencia: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      nome: [undefined, Validators.required],
      indAtivo: [undefined],
    });
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.conferencia,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.usuarioSessao.usuario.codUsuario,
        indAtivo: +form.indAtivo
      }
    };

    this._conferenciaService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast(`Conferência ${obj.nome} atualizada com sucesso!`, "success");
      this._location.back();

    });
  }

  criar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...form,
      ...{
        sala: `SAT_${ this.usuarioSessao.usuario.codUsuario.toUpperCase() }_${ moment().format('yyyyMMDDHHmmsss') }`,
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.usuarioSessao.usuario.codUsuario,
        indAtivo: +form.indAtivo,
      }
    };

    this._conferenciaService.criar(obj).subscribe(() => {
      this._snack.exibirToast(`Conferência ${obj.nome} adicionada com sucesso!`, "success");
      this._location.back();
    });
  } 
}
