import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PerfilSetor } from 'app/core/types/perfil-setor.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import _ from 'lodash';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-perfil-form-recursos-bloqueados',
  templateUrl: './perfil-form-recursos-bloqueados.component.html'
})
export class PerfilFormRecursosBloqueadosComponent implements OnInit {
  @Input() perfilSetor: PerfilSetor;

  userSession: UsuarioSessao;
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

    this.obterDados();
  }

  async obterDados() {}


  inicializarForm() {
    this.form = this._formBuilder.group({
      // codAtendimento: [undefined],
      // dataHoraAberturaSTN: [undefined],
      // codOrigemChamadoSTN: [undefined],
      // codStatusSTN: [undefined],
      // codTipoChamadoSTN: [undefined],
      // nomeUsuario: [undefined],
      // codTecnicos: [undefined],
      // codTipoCausa: [undefined],
      // acaoSTN: [undefined],
      // codDefeito: [undefined]
    });
  }

  registrarEmitters() {}

   salvar() {}

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
