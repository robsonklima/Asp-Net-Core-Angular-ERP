import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ConferenciaParticipanteService } from 'app/core/services/conferencia-participante.service';
import { ConferenciaService } from 'app/core/services/conferencia.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { UsuarioService } from 'app/core/services/usuario.service';
import { Usuario, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';

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
    this.obterUsuarios();
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
    debugger;

    
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
}
