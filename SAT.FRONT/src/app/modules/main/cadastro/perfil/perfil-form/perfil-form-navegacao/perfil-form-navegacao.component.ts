import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { NavegacaoConfiguracao } from 'app/core/types/navegacao-configuracao.types';
import { Navegacao } from 'app/core/types/navegacao.types';
import { PerfilSetor } from 'app/core/types/perfil-setor.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import _ from 'lodash';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-perfil-form-navegacao',
  templateUrl: './perfil-form-navegacao.component.html',
})
export class PerfilFormNavegacaoComponent implements OnInit {
  @Input() perfilSetor: PerfilSetor;

  userSession: UsuarioSessao;
  navegacao: Navegacao[] = [];
  navegacaoConfiguracao: NavegacaoConfiguracao;
  form: FormGroup;
  isAddMode: boolean;
  searching: boolean;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _userService: UserService  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.inicializarForm();
    this.registrarEmitters();
    console.log(this.perfilSetor);

    this.obterDados();
  }

  async obterDados() { }


  inicializarForm() {
    this.form = this._formBuilder.group({
    });
  }

  registrarEmitters() {  }

  salvar() {     }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
