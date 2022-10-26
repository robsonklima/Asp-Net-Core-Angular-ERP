import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { fuseAnimations } from '@fuse/animations';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import _ from 'lodash';
import { PerfilEnum } from 'app/core/types/perfil.types';

@Component({
    selector: 'default',
    templateUrl: './default.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations,
})

export class DefaultComponent implements OnInit, OnDestroy {
    sessionData: UsuarioSessao;
    protected _onDestroy = new Subject<void>();

    constructor(
        private _userService: UserService
    ) {
        this.sessionData = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void {

    }

    obterMensagemBoasVindas(): string {
        const hoje = new Date()
        const horaCorrente = hoje.getHours();
        const primeiroNome = this.sessionData?.usuario?.nomeUsuario?.split(" ").shift();

        if (horaCorrente < 12)
        {
            return `Bom dia ${primeiroNome}`;
        } else if (horaCorrente < 18)
        {
            return `Boa tarde ${primeiroNome}`;
        } else
        {
            return `Boa noite ${primeiroNome}`;
        }
    }

    verificarPermissaoAcessoSlide(slide: string): boolean {
        const perfil = this.sessionData.usuario.codPerfil;

        switch (slide)
        {
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
            case 'LOGS':
                return perfil === PerfilEnum.ADM_DO_SISTEMA || perfil == PerfilEnum.PV_COORDENADOR_DE_CONTRATO;
            case 'BOAS_VINDAS':
                return perfil !== PerfilEnum.ADM_DO_SISTEMA && perfil !== PerfilEnum.PV_COORDENADOR_DE_CONTRATO;
            default:
                return false;
        }
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}