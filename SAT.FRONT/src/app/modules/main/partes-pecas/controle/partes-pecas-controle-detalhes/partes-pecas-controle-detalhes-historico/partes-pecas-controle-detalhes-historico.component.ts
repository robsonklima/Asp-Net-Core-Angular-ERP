import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { Subject } from 'rxjs';
import _ from 'lodash';
import { PartesPecasControleDetalhesComponent } from '../partes-pecas-controle-detalhes.component';
import { RelatorioAtendimentoDetalhePecaStatus, RelatorioAtendimentoDetalhePecaStatusParameters } from 'app/core/types/relatorio-atendimento-detalhe-peca-status.types';
import { RelatorioAtendimentoDetalhePecaStatusService } from 'app/core/services/relatorio-atendimento-detalhe-peca-status.service';


@Component({
    selector: 'app-partes-pecas-controle-detalhes-historico',
    templateUrl: './partes-pecas-controle-detalhes-historico.component.html'
})
export class PartesPecasControleDetalhesHistoricoComponent implements OnInit {
    codRatDetalhesPecas: number;
    ratPecaStatus: RelatorioAtendimentoDetalhePecaStatus[] = [];
    userSession: UserSession;
    loading: boolean = true;
    protected _onDestroy = new Subject<void>();

    constructor(
        @Inject(MAT_DIALOG_DATA) data: any,
        public _dialogRef: MatDialogRef<PartesPecasControleDetalhesComponent>,
        private _ratDetalhesPecaStatusService: RelatorioAtendimentoDetalhePecaStatusService,
        private _userService: UserService,
    ) {
        this.codRatDetalhesPecas = data?.codRatDetalhesPecas;
        this.userSession = JSON.parse(this._userService.userSession);
    }

    async ngOnInit() {
        this.obterDados();
        this.registrarEmitters();
        this.loading = false;
    }

    async obterDados() {
        this.loading = true;

        const parametros: RelatorioAtendimentoDetalhePecaStatusParameters = {
            sortActive: 'codRATDetalhesPecasStatus',
            sortDirection: 'desc',
            codRATDetalhesPecas: this.codRatDetalhesPecas,
        }

        const data = await this._ratDetalhesPecaStatusService.obterPorParametros(parametros).toPromise();
        this.ratPecaStatus = data.items;

        this.loading = false;
    }


    private registrarEmitters() {}

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}

