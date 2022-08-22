import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ConferenciaService } from 'app/core/services/conferencia.service';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { UsuarioService } from 'app/core/services/usuario.service';
import { Usuario, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-conferencia-form',
  templateUrl: './conferencia-form.component.html'
})
export class ConferenciaFormComponent implements OnInit {
  form: FormGroup;
  usuarioSessao: UsuarioSessao;
  loading: boolean = false;
  usuarios: Usuario[];
  usuariosFiltro: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _snack: CustomSnackbarService,
    private _userService: UserService,
    private _conferenciaService: ConferenciaService,
    private _formBuilder: FormBuilder,
    private _usuarioService: UsuarioService,
    private _location: Location
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.inicializarForm();
    this.registrarEmitters();
    this.obterUsuarios();
  }

  registrarEmitters() {
    this.usuariosFiltro.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(500),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterUsuarios(this.usuariosFiltro.value);
			});
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      codConferencia: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      codUsuarios: [undefined, Validators.required],
      nome: [undefined, Validators.required],
      indAtivo: [true],
    });
  }

  private async obterUsuarios(filtro: string=null) {
    const data = await this._usuarioService.obterPorParametros({ 
      indAtivo: 1, filter: filtro, sortActive: 'NomeUsuario', sortDirection: 'ASC', pageSize: 50
    }).toPromise();
    this.usuarios = data.items;
  }

  salvar(): void {
    const form = this.form.getRawValue();

    const participantes = form.codUsuarios.map(codUsuario => {
      return { 
        codUsuarioParticipante: codUsuario,
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.usuarioSessao.usuario.codUsuario,
      }
    });

    let obj = {
      ...form,
      ...{
        sala: `SAT_${ this.usuarioSessao.usuario.codUsuario.toUpperCase() }_${ moment().format('yyyyMMDDHHmmsss') }`,
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.usuarioSessao.usuario.codUsuario,
        participantes: participantes,
        indAtivo: +form.indAtivo,
      }
    };

    this._conferenciaService.criar(obj).subscribe(() => {
      this._snack.exibirToast(`Conferência ${obj.nome} adicionada com sucesso!`, "success");
      this._location.back();
    });
  } 

  ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
