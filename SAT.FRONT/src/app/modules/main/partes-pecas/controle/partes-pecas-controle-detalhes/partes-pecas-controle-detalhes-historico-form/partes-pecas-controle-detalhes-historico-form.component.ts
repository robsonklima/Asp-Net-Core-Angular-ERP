import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import moment from 'moment';
import _ from 'lodash';
import { PartesPecasControleDetalhesComponent } from '../partes-pecas-controle-detalhes.component';
import { RelatorioAtendimentoDetalhePecaStatus } from 'app/core/types/relatorio-atendimento-detalhe-peca-status.types';
import { RelatorioAtendimentoPecaStatus, RelatorioAtendimentoPecaStatusParameters } from 'app/core/types/relatorio-atendimento-peca-status.types';
import { RelatorioAtendimentoDetalhePecaStatusService } from 'app/core/services/relatorio-atendimento-detalhe-peca-status.service';
import { RelatorioAtendimentoPecaStatusService } from 'app/core/services/relatorio-atendimento-peca-status.service';

@Component({
    selector: 'app-partes-pecas-controle-detalhes-historico-form',
    templateUrl: './partes-pecas-controle-detalhes-historico-form.component.html'
})
export class PartesPecasControleDetalhesHistoricoFormComponent implements OnInit {
    codRatDetalhesPecas: number;
    ratDetalhePecaStatus: RelatorioAtendimentoDetalhePecaStatus;
    status: RelatorioAtendimentoPecaStatus[] = [];
    userSession: UserSession;
    public form: FormGroup;
    loading: boolean = true;
    pecaFilterCtrl: FormControl = new FormControl();
    protected _onDestroy = new Subject<void>();

    constructor(
        @Inject(MAT_DIALOG_DATA) data: any,
        private _formBuilder: FormBuilder,
        public _dialogRef: MatDialogRef<PartesPecasControleDetalhesComponent>,
        private _ratDetalhePecaStatusServico: RelatorioAtendimentoDetalhePecaStatusService,
        private _ratPecaStatusServico: RelatorioAtendimentoPecaStatusService,
        private _userService: UserService,
        private _snack: CustomSnackbarService,
        private dialogRef: MatDialogRef<PartesPecasControleDetalhesComponent>
    ) {
        this.codRatDetalhesPecas = data?.codRatDetalhesPecas;
        this.userSession = JSON.parse(this._userService.userSession);
    }

    async ngOnInit() {
        this.inicializarForm();
        this.obterStatus();
        this.loading = false;
    }

    private async obterStatus() {
        let params: RelatorioAtendimentoPecaStatusParameters = {
            sortActive: 'codRatpecasStatus',
            sortDirection: 'asc',
            pageSize: 1000
        };
        const data = await this._ratPecaStatusServico
            .obterPorParametros(params)
            .toPromise();
        this.status = data.items;
    }

    private inicializarForm() {
        this.form = this._formBuilder.group({
            descricao: [undefined],
            nroMinuta: [undefined],
            nroNf: [undefined],
            dataChegada: [undefined],
            dataEmbarque: [undefined],
            transportadora: [undefined],
            codRATDetalhesPecas: [undefined],
            codRatpecasStatus: [undefined],
        });
    }

    cancelar() {
        this._dialogRef.close();
    }

    async criar() {
        const form: any = this.form.getRawValue();

        let obj: RelatorioAtendimentoDetalhePecaStatus = {
            ...form,
            ...{
                codRATDetalhesPecas: this.codRatDetalhesPecas,
                codUsuarioCad: this.userSession.usuario.codUsuario,
                dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
            }
        };

        this._ratDetalhePecaStatusServico.criar(obj).subscribe(() => {
            this._snack.exibirToast("Status adicionado com sucesso!", "success");
            this.dialogRef.close(true);
        }, e => {
            this.form.enable();
        });

        this.dialogRef.close(true);
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}

