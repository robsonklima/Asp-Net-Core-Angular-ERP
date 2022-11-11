import { Component, Input, OnInit } from '@angular/core';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import _ from 'lodash';

@Component({
  selector: 'app-laboratorio-processo-reparo-insumo',
  templateUrl: './laboratorio-processo-reparo-insumo.component.html'
})
export class LaboratorioProcessoReparoInsumoComponent implements OnInit {
  @Input() codORItem: number;
  usuarioSessao: UsuarioSessao;
  constructor(
    private _userService: UserService,

  ) { 
      this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {}

}
