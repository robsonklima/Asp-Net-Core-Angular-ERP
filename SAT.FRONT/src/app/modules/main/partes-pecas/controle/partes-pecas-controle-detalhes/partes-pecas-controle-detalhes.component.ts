import { AfterViewInit, ChangeDetectorRef, Component, Input, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { RelatorioAtendimentoDetalhePecaService } from 'app/core/services/relatorio-atendimento-detalhe-peca.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimentoDetalhePeca } from 'app/core/types/relatorio-atendimento-detalhe-peca.type';
import { RelatorioAtendimentoDetalhe } from 'app/core/types/relatorio-atendimento-detalhe.type';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { statusServicoConst } from 'app/core/types/status-servico.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import Enumerable from 'linq';
import _, { forEach } from 'lodash';
import moment from 'moment';
import { PartesPecasControleDetalhesHistoricoFormComponent } from './partes-pecas-controle-detalhes-historico-form/partes-pecas-controle-detalhes-historico-form.component';
import { PartesPecasControleDetalhesHistoricoComponent } from './partes-pecas-controle-detalhes-historico/partes-pecas-controle-detalhes-historico.component';

@Component({
    selector: 'app-partes-pecas-controle-detalhes',
    templateUrl: './partes-pecas-controle-detalhes.component.html',
    styles: [
        /* language=SCSS */
        `.list-grid-ppd {
            grid-template-columns: 10% 20% 10% 10% 10% 10% 10% 20%;
        }`
    ],
    animations: fuseAnimations,
    encapsulation: ViewEncapsulation.None,
})
export class PartesPecasControleDetalhesComponent implements AfterViewInit {
    @Input() codRat: number;
    rat: RelatorioAtendimento;
    ratDetalhes: RelatorioAtendimentoDetalhe[] = [];
    ratDetalhesPeca: RelatorioAtendimentoDetalhePeca[] = [];
    userSession: UsuarioSessao;
    isLoading: boolean = false;

    constructor(
        private _userService: UserService,
        private _cdr: ChangeDetectorRef,
        private _ratService: RelatorioAtendimentoService,
        private _ratDetalhePecaService: RelatorioAtendimentoDetalhePecaService,
        private _osService: OrdemServicoService,
        private _dialog: MatDialog,
        private _router: Router,
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    async ngAfterViewInit() {
        this.rat = await this._ratService.obterPorCodigo(this.codRat).toPromise();

        this.ratDetalhes = Enumerable.from(this.rat.relatorioAtendimentoDetalhes)
            .where(i => i.codAcao == 19)
            .toArray();

        this.ratDetalhesPeca = Enumerable.from(this.ratDetalhes)
            .selectMany(i => i.relatorioAtendimentoDetalhePecas)
            .toArray();

        this._cdr.detectChanges();
    }

    async obterRatDetalhes(codRATDetalhe: number) {
        var detalhe = Enumerable.from(this.rat.relatorioAtendimentoDetalhes)
            .where(i => i.codRATDetalhe == codRATDetalhe)
            .toArray()
            .shift();

        return detalhe;
    }

    async obterRatDetalhePeca(codRATDetalhePeca: number) {
        var detalhePeca = Enumerable.from(this.ratDetalhesPeca)
            .where(i => i.codRATDetalhePeca == codRATDetalhePeca)
            .toArray()
            .shift();

        return detalhePeca;
    }

    async obterOS(codRATDetalhe: number) {
        var codos = (await this.obterRatDetalhes(codRATDetalhe)).codOS;

        this._router.navigate(['/ordem-servico/detalhe/' + codos]);
    }

    abrirHistorico(codRatDetalhesPecas: number) {
        const dialogRef = this._dialog.open(PartesPecasControleDetalhesHistoricoComponent, {
            data: { codRatDetalhesPecas: codRatDetalhesPecas }
        });

        dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
            if (confirmacao) {
            }
        });
    }

    adicionarStatus(codRatDetalhesPecas: number) {
        const dialogRef = this._dialog.open(PartesPecasControleDetalhesHistoricoFormComponent, {
            data: { codRatDetalhesPecas: codRatDetalhesPecas }
        });

        dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
            if (confirmacao) {
                this.ngAfterViewInit();
            }
        });
    }

    validarRegraIntervenção(status: string){
        var isAlterarStatusIntervenção = true;

        this.ratDetalhesPeca.forEach(i => {
            if(i.descStatus != status)
                return isAlterarStatusIntervenção = false;
        });

        return isAlterarStatusIntervenção;
    }

    liberarPeca(codRATDetalhePeca: number) {
        const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
            data:
            {
                titulo: 'Confirmação',
                mensagem: 'Deseja confirmar a liberação das peças?',
                buttonText: {
                    ok: 'Sim',
                    cancel: 'Não'
                }
            }
        });

        dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
            if (confirmacao) {
                const ratDetalhesPeca: RelatorioAtendimentoDetalhePeca = (await this.obterRatDetalhePeca(codRATDetalhePeca));
                ratDetalhesPeca.qtdeLib = ratDetalhesPeca.qtdePecas;
                ratDetalhesPeca.indOK = 1;
                ratDetalhesPeca.codUsuarioManut = this.userSession.usuario.codUsuario;
                ratDetalhesPeca.codUsuarioManutencao = this.userSession.usuario.codUsuario;
                ratDetalhesPeca.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');
                ratDetalhesPeca.descStatus = 'PEÇA LIBERADA';

                const codos = (await this.obterRatDetalhes(ratDetalhesPeca.codRATDetalhe)).codOS;
                const os: OrdemServico = await this._osService.obterPorCodigo(codos).toPromise();
                os.codStatusServico = statusServicoConst.PECAS_LIBERADAS;
                os.codUsuarioManut = this.userSession.usuario.codUsuario;
                os.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');

                const rat: RelatorioAtendimento = this.rat;
                rat.codStatusServico = statusServicoConst.PECAS_LIBERADAS;
                rat.codUsuarioManut = this.userSession.usuario.codUsuario;
                rat.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');

                this._ratDetalhePecaService.atualizar(ratDetalhesPeca).subscribe();

                if(this.validarRegraIntervenção(ratDetalhesPeca.descStatus)){
                    this._osService.atualizar(os).subscribe();
                    this._ratService.atualizar(rat).subscribe();
                }
            }
        });
    }

    pendenciarPeca(codRATDetalhePeca: number) {
        const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
            data:
            {
                titulo: 'Confirmação',
                mensagem: 'Deseja confirmar as peças faltantes?',
                buttonText: {
                    ok: 'Sim',
                    cancel: 'Não'
                }
            }
        });

        dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
            if (confirmacao) {
                const ratDetalhesPeca: RelatorioAtendimentoDetalhePeca = (await this.obterRatDetalhePeca(codRATDetalhePeca));
                ratDetalhesPeca.indCentral = 1;
                ratDetalhesPeca.codUsuarioManut = this.userSession.usuario.codUsuario;
                ratDetalhesPeca.codUsuarioManutencao = this.userSession.usuario.codUsuario;
                ratDetalhesPeca.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');
                ratDetalhesPeca.descStatus = 'PEÇA FALTANTE';

                const codos = (await this.obterRatDetalhes(ratDetalhesPeca.codRATDetalhe)).codOS
                const os: OrdemServico = await this._osService.obterPorCodigo(codos).toPromise();
                os.codStatusServico = statusServicoConst.PECA_FALTANTE;
                os.codUsuarioManut = this.userSession.usuario.codUsuario;
                os.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');

                const rat: RelatorioAtendimento = this.rat;
                rat.codStatusServico = statusServicoConst.PECA_FALTANTE;
                rat.codUsuarioManut = this.userSession.usuario.codUsuario;
                rat.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');
                
                this._ratDetalhePecaService.atualizar(ratDetalhesPeca).subscribe();
                this._osService.atualizar(os).subscribe();
                this._ratService.atualizar(rat).subscribe();
            }
        });
    }

    transitarPeca(codRATDetalhePeca: number) {
        const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
            data:
            {
                titulo: 'Confirmação',
                mensagem: 'Deseja confirmar a liberação das peças?',
                buttonText: {
                    ok: 'Sim',
                    cancel: 'Não'
                }
            }
        });

        dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
            if (confirmacao) {
                var ratDetalhesPeca: RelatorioAtendimentoDetalhePeca = (await this.obterRatDetalhePeca(codRATDetalhePeca));
                ratDetalhesPeca.qtdeLib = ratDetalhesPeca.qtdePecas;
                ratDetalhesPeca.codUsuarioManut = this.userSession.usuario.codUsuario;
                ratDetalhesPeca.codUsuarioManutencao = this.userSession.usuario.codUsuario;
                ratDetalhesPeca.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');
                ratDetalhesPeca.descStatus = 'PEÇA EM TRÂNSITO';

                var codos = (await this.obterRatDetalhes(ratDetalhesPeca.codRATDetalhe)).codOS
                var os: OrdemServico = await this._osService.obterPorCodigo(codos).toPromise();
                os.codStatusServico = statusServicoConst.PECA_EM_TRANSITO;
                os.codUsuarioManut = this.userSession.usuario.codUsuario;
                os.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');

                var rat: RelatorioAtendimento = this.rat;
                rat.codStatusServico = statusServicoConst.PECA_EM_TRANSITO;
                rat.codUsuarioManut = this.userSession.usuario.codUsuario;
                rat.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');

                this._ratDetalhePecaService.atualizar(ratDetalhesPeca).subscribe();

                if(this.validarRegraIntervenção(ratDetalhesPeca.descStatus)){
                    this._osService.atualizar(os).subscribe();
                    this._ratService.atualizar(rat).subscribe();
                }
            }
        });
    }
}