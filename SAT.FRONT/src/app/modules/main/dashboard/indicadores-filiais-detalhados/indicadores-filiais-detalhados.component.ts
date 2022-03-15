import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { FuseAlertType } from '@fuse/components/alert';
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
  alert: { type: FuseAlertType; message: string } = {
    type   : 'success',
    message: ''
  };
  showAlert: boolean = false;

  constructor(
    private _userService: UserService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    if (!this.userSession.usuario.codFilial) {
      this.alert = {
        type   : 'error',
        message: 'Você não possui filial vinculada ao seu usuário'
      };

      this.showAlert = true;
    };
  }
}