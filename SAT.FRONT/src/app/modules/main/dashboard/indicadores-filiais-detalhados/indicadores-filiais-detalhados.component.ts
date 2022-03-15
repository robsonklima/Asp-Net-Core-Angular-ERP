import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
  selector: 'app-indicadores-filiais-detalhados',
  templateUrl: './indicadores-filiais-detalhados.component.html',
  encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations
})
export class IndicadoresFiliaisDetalhadosComponent implements OnInit {
  userSession: UserSession;
  codFilial: number;

  constructor(
    private _userService: UserService,
    private _route: ActivatedRoute
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.codFilial = +this._route.snapshot.paramMap.get('codFilial') || this.userSession.usuario.codFilial;
  }
}