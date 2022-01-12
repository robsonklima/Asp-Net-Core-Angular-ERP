import { ChangeDetectionStrategy, Component, EventEmitter, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
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

    @Output() respostaPanel = new EventEmitter();
    carregado: boolean;

    formInformacoes: FormGroup;
    userSession: UserSession;
    usuario: Usuario;

    constructor(
        private _userSvc: UserService
    ) {
        this.userSession = JSON.parse(this._userSvc.userSession);
    }

    async ngOnInit() {
        this.carregado = false;
        this.usuario = await this._userSvc.obterPorCodigo(this.userSession.usuario.codUsuario).toPromise();
        this.carregado = true;
        this.respostaPanel.emit(this.carregado);
    }
}