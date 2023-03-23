import { AfterViewInit, ChangeDetectorRef, Component, Input, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { RelatorioAtendimentoDetalhePecaService } from 'app/core/services/relatorio-atendimento-detalhe-peca.service';
import { RelatorioAtendimentoDetalheService } from 'app/core/services/relatorio-atendimento-detalhe.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { OSBancadaPecas } from 'app/core/types/os-bancada-pecas.types';
import { RelatorioAtendimentoDetalhePeca } from 'app/core/types/relatorio-atendimento-detalhe-peca.type';
import { RelatorioAtendimentoDetalhe } from 'app/core/types/relatorio-atendimento-detalhe.type';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import Enumerable from 'linq';
import _ from 'lodash';
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
    ratDetalhes: RelatorioAtendimentoDetalhe [] = [];
    ratDetalhesPeca: RelatorioAtendimentoDetalhePeca [] = [];
    userSession: UsuarioSessao;
    isLoading: boolean = false;

    constructor(
        private _userService: UserService,
        private _cdr: ChangeDetectorRef,
        private _ratService: RelatorioAtendimentoService,
        private _ratDetalheService: RelatorioAtendimentoDetalheService,
        private _ratDetalhePecaService: RelatorioAtendimentoDetalhePecaService,
        private _dialog: MatDialog,
        private _router: Router,
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    async ngAfterViewInit(){
        this.rat = await this._ratService.obterPorCodigo(this.codRat).toPromise();
        
        this.ratDetalhes = Enumerable.from(this.rat.relatorioAtendimentoDetalhes)
        .where(i => i.codAcao == 19)
        .toArray();  

        this.ratDetalhesPeca = Enumerable.from(this.ratDetalhes)
        .selectMany(i => i.relatorioAtendimentoDetalhePecas)
        .toArray();  

        this._cdr.detectChanges();
    }

    async obterRatDetalhes(codRATDetalhe: number){
        var detalhe = Enumerable.from(this.rat.relatorioAtendimentoDetalhes)
        .where(i => i.codRATDetalhe == codRATDetalhe)
        .toArray()
        .shift();

        return detalhe;
    }

    async obterOS(codRATDetalhe: number){
        var codos = (await this.obterRatDetalhes(codRATDetalhe)).codOS;

        this._router.navigate(['/ordem-servico/detalhe/' + codos]);
    }

    abrirHistorico(codRatDetalhesPecas: number) {
        const dialogRef = this._dialog.open(PartesPecasControleDetalhesHistoricoComponent, {
            data: { codRatDetalhesPecas: codRatDetalhesPecas }
        });

        dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
            if (confirmacao) {
                    this.ngAfterViewInit();
            }
        });
    }

    validarStatus(oSBancadaPecas: OSBancadaPecas) {
        // var osBancadaPecas = oSBancadaPecas;

        // if (osBancadaPecas.indPecaDevolvida == 1)
        //     return "Devolvida";

        // else if (osBancadaPecas.indPecaLiberada == 1)
        //     return "Liberada";

        // return "Transferido";
    }

    async onChange($event: MatSlideToggleChange, osBancadaPecas: OSBancadaPecas) {
        // if ($event.checked && osBancadaPecas.indPecaLiberada == 1)
        //     osBancadaPecas.indPecaDevolvida = 1;

        // else {
        //     osBancadaPecas.indPecaDevolvida = 0;
        //     osBancadaPecas.indImpressao = 0;
        // }

        // this._osBancadaPecasService.atualizar(osBancadaPecas).subscribe();
    }
}