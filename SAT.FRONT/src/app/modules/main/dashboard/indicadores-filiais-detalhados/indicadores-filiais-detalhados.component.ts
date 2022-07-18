import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FilialService } from 'app/core/services/filial.service';
import { Filial } from 'app/core/types/filial.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-indicadores-filiais-detalhados',
  templateUrl: './indicadores-filiais-detalhados.component.html' 
})
export class IndicadoresFiliaisDetalhadosComponent implements OnInit {
  codFilial: number;
  filial: Filial;
  userSession: UsuarioSessao;

  constructor(
    private _route: ActivatedRoute,
    private _filialService: FilialService,
    private _userService: UserService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codFilial = this.userSession.usuario.codFilial || +this._route.snapshot.paramMap.get('codFilial');

    this.filial = await this._filialService.obterPorCodigo(this.codFilial).toPromise();
  }
}