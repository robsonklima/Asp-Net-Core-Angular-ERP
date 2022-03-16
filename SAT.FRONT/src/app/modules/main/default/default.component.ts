import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { fuseAnimations } from '@fuse/animations';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import _ from 'lodash';
import { PerfilEnum } from 'app/core/types/perfil.types';

@Component({
    selector: 'default',
    templateUrl: './default.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations,
})

export class DefaultComponent implements OnInit, OnDestroy
{
    sessionData: UsuarioSessao;
    protected _onDestroy = new Subject<void>();

    constructor(
        private _userService: UserService
    )
    {
        this.sessionData = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void
    {
        
    }

    obterMensagemBoasVindas(): string {
        const hoje = new Date()
        const horaCorrente = hoje.getHours();
        const nomeUsuario = this.sessionData?.usuario?.nomeUsuario?.split(" ").shift().charAt(0).toUpperCase() + this.sessionData?.usuario?.nomeUsuario?.slice(1);

        if (horaCorrente < 12) {
            return `Bom dia, ${nomeUsuario}!`;
        } else if (horaCorrente < 18) {
            return `Boa tarde, ${nomeUsuario}!`;
        } else {
            return `Boa noite, ${nomeUsuario}!`;
        }
    }

    verificarPermissaoAcessoSlide(slide: string): boolean {
        const perfil = this.sessionData.usuario.codPerfil;

        switch (slide) {
            case 'SERVICOS':
                return perfil === PerfilEnum.ADM_DO_SISTEMA || perfil === PerfilEnum.PV_COORDENADOR_DE_CONTRATO;
            case 'SERVIDORES':
                return perfil === PerfilEnum.ADM_DO_SISTEMA;
            case 'DISPONIBILIDADE':
                return perfil === PerfilEnum.ADM_DO_SISTEMA;
            case 'OCIOSIDADE':
                return perfil === PerfilEnum.ADM_DO_SISTEMA;
            case 'UTILIZACAO':
                return perfil === PerfilEnum.ADM_DO_SISTEMA;
            case 'BOAS_VINDAS':
                return perfil !== PerfilEnum.ADM_DO_SISTEMA && perfil !== PerfilEnum.PV_COORDENADOR_DE_CONTRATO;
            default:
                return false;
        }
    }

    ngOnDestroy()
    {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}