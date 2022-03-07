import { Component, OnInit } from '@angular/core';
import { UsuariosLogados } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-utilizacao',
  templateUrl: './utilizacao.component.html'
})
export class UtilizacaoComponent implements OnInit {
  usuariosLogados: UsuariosLogados;
  loading: boolean;

  constructor(
    private _userService: UserService
  ) { }

  ngOnInit(): void {
    this.obterUsuariosLogados();
  }

  private async obterUsuariosLogados() {
    this.loading = true;
    
    this.usuariosLogados = await this._userService.obterUsuariosLogados().toPromise();

    this.loading = false;
  }
}