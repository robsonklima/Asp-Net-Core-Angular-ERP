import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-indicadores-filiais-detalhados',
  templateUrl: './indicadores-filiais-detalhados.component.html' 
})
export class IndicadoresFiliaisDetalhadosComponent implements OnInit {
  codFilial: number;
  userSession: UsuarioSessao;

  constructor(
    private _route: ActivatedRoute,
    private _userService: UserService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.codFilial = +this._route.snapshot.paramMap.get('codFilial') || this.userSession.usuario.codFilial;
  }
}