import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { interval, Subject } from 'rxjs';
import { startWith, takeUntil } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { UsuarioSessao, UsuariosLogados } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Monitoramento } from 'app/core/types/monitoramento.types';
import { IISLogService } from 'app/core/services/iislog.service';
import { IISLog } from 'app/core/types/iislog.types';
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
    listaMonitoramento: Monitoramento[] = [];
    ultimoProcessamento: string;
    loading: boolean;
    opcoesDatas: any[] = [];
    dataAtual = moment().format('yyyy-MM-DD HH:mm:ss');
    velocidadeInternet: string;
    eventosOciosos: IISLog[] = [];
    usuariosLogados: UsuariosLogados;
    protected _onDestroy = new Subject<void>();

    constructor(
        private _userService: UserService,
        private _iisLogService: IISLogService
    )
    {
        this.sessionData = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void
    {
        this.obterOpcoesDatas();

        interval(10 * 60 * 1000)
            .pipe(
                startWith(0),
                takeUntil(this._onDestroy)
            )
            .subscribe(() =>
            {
                this.obterDados();
            });
    }

    async obterDados(data: string = '')
    {
        this.loading = true;

        await this.obterEventosOciosos(data);
        await this.obterUsuariosLogados();
        
        this.ultimoProcessamento = moment().format('yyyy-MM-DD HH:mm:ss');

        this.loading = false;
    }    

    private obterEventosOciosos(data: string=null): Promise<any>
    {
        return new Promise((resolve, reject) =>
        {
            this._iisLogService.obterPorParametros({
                data: data || moment().format('yyyy-MM-DD HH:mm:ss')
            }).subscribe((data: IISLog[]) =>
            {
                this.eventosOciosos = data;
                resolve(data);
            }, () =>
            {
                reject();
            });
        })
    }

    filtrarMonitoramento(tipo: string)
    {
        return this.listaMonitoramento.filter(m => m.tipo == tipo)
    }

    private obterOpcoesDatas()
    {
        for (let i = 4; i >= 0; i--)
        {
            this.opcoesDatas.push({
                data: moment().add(-i, 'days').format('yyyy-MM-DD HH:mm:ss'),
                prompt: moment().add(-i, 'days').locale('pt').format('dddd').replace('-feira', '')
            });
        }
    }

    private async obterUsuariosLogados() {
        this.usuariosLogados = await this._userService.obterUsuariosLogados().toPromise();
    }

    obterNomeRecurso(nome: string): string {
        if (nome.toUpperCase().includes('SAT.V2.API'))
            return 'SAT V2 API';

        if (nome.toUpperCase().includes('PRJSATWEB'))
            return 'SAT Antigo';

        if (nome.toUpperCase().includes('PRJSATWEBAPI'))
            return 'API Smartphone';

        if (nome.toUpperCase().includes('PRJSATWEBTECNICO'))
            return 'SAT Web Técnico';

        if (nome.toUpperCase().includes('PRJSATWEBOLD'))
            return 'SAT ASP';

        if (nome.toUpperCase().includes('POS/'))
            return 'SAT POS';
    }

    obterCorRecurso(nome: string): string {
        if (nome.toUpperCase().includes('SAT.V2.API'))
            return 'text-amber-500';

        if (nome.toUpperCase().includes('PRJSATWEB'))
            return 'text-blue-500';

        if (nome.toUpperCase().includes('PRJSATWEBAPI'))
            return 'text-orange-500';

        if (nome.toUpperCase().includes('PRJSATWEBTECNICO'))
            return 'text-pink-500';

        if (nome.toUpperCase().includes('PRJSATWEBOLD'))
            return 'text-orange-900';

        if (nome.toUpperCase().includes('POS/'))
            return 'text-green-500';
    }

    pesquisarDadosPorData(data: string)
    {
        this.obterDados(data);
    }

    obterMensagemBoasVindas(): string {
        const hoje = new Date()
        const horaCorrente = hoje.getHours();
        const primeiroNomeUsuario = this.sessionData?.usuario?.nomeUsuario?.split(" ").shift();

        if (horaCorrente < 12) {
            return `Bom dia, ${primeiroNomeUsuario}!`;
        } else if (horaCorrente < 18) {
            return `Boa tarde, ${primeiroNomeUsuario}!`;
        } else {
            return `Boa noite, ${primeiroNomeUsuario}!`;
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