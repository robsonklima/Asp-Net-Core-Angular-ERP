import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Usuario } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
    selector: 'configuracoes-conta',
    templateUrl: './conta.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ConfiguracoesContaComponent implements OnInit {
    form: FormGroup;
    userSession: UserSession;
    usuario: Usuario;

    constructor(
        private _formBuilder: FormBuilder,
        private _userSvc: UserService
    ) {
        this.userSession = JSON.parse(this._userSvc.userSession);
    }

    async ngOnInit() {
        this.usuario = await this._userSvc.obterPorCodigo(this.userSession.usuario.codUsuario).toPromise();

        this.form = this._formBuilder.group({
            nomeUsuario: this.usuario.nomeUsuario,
            codUsuario: this.usuario.codUsuario,
            email: ['hughes.brian@mail.com', Validators.email],
            phone: ['121-490-33-12'],
        });
    }
}
