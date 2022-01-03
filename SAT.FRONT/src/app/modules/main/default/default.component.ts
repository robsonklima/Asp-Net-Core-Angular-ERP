import { Component, ViewEncapsulation } from '@angular/core';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';

@Component({
    selector: 'default',
    templateUrl: './default.component.html',
    encapsulation: ViewEncapsulation.None
})


export class DefaultComponent {
    sessionData: UsuarioSessao;

    constructor(
        private _userService: UserService
    ) {
        this.sessionData = JSON.parse(this._userService.userSession);

    }

    ngOnInit() {

    }
}
