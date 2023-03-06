import { AfterViewInit, ChangeDetectorRef, Component, Input, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrcamentoPecasEspecService } from 'app/core/services/orcamento-pecas-espec.service';
import { OsBancadaPecasOrcamentoService } from 'app/core/services/os-bancada-pecas-orcamento.service';
import { OrcamentoPecasEspec, OrcamentoPecasEspecData, OrcamentoPecasEspecParameters } from 'app/core/types/orcamento-pecas-espec.types';
import { OsBancadaPecasOrcamento } from 'app/core/types/os-bancada-pecas-orcamento.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import _ from 'lodash';

@Component({
    selector: 'app-laboratorio-orcamento-peca-lista',
    templateUrl: './laboratorio-orcamento-peca-lista.component.html',
    styles: [
        /* language=SCSS */
        `.list-grid-u {
            grid-template-columns: 70px auto 70px 80px 50px 50px 100px 100px 70px 70px 50px;
        }`
    ],
    animations: fuseAnimations,
    encapsulation: ViewEncapsulation.None,
})
export class LaboratorioOrcamentoPecaListaComponent implements AfterViewInit {
    @Input() codOrcamento: number;
    orcamento: OsBancadaPecasOrcamento;
    dataSourceData: OrcamentoPecasEspecData;
    userSession: UsuarioSessao;
    isLoading: boolean = false;

    constructor(
        private _userService: UserService,
        private _cdr: ChangeDetectorRef,
        private _osBancadaPecaOrcamentoService: OsBancadaPecasOrcamentoService,
        private _orcamentoPecasEspecService: OrcamentoPecasEspecService,
        private _snack: CustomSnackbarService,
        private _dialog: MatDialog
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    async ngAfterViewInit() {
        this.orcamento = await this._osBancadaPecaOrcamentoService.obterPorCodigo(this.codOrcamento).toPromise();
        this.obterDados();

        this._cdr.detectChanges();
    }

    private async obterDados() {
        const parametros: OrcamentoPecasEspecParameters = {
            ...{
                sortActive: 'codPeca',
                sortDirection: 'desc',
                codOrcamento: this.codOrcamento
            }
        }

        const data = await this._orcamentoPecasEspecService.obterPorParametros(parametros).toPromise();
        this.dataSourceData = data;

        this.isLoading = false;
        this._cdr.detectChanges();
    }

    deletar(orcamentoPeca: OrcamentoPecasEspec) {
        const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
            data: {
                titulo: 'Confirmação',
                message: `Deseja remover a peça ${orcamentoPeca.peca.codMagnus}?`,
                buttonText: {
                    ok: 'Sim',
                    cancel: 'Não'
                }
            },
            backdropClass: 'static'
        });

        dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
            if (confirmacao) {
                this.orcamento.valorTotal = this.orcamento.valorTotal - orcamentoPeca.valorTotal;
                this._osBancadaPecaOrcamentoService.atualizar({ ...this.orcamento, ...{ valorTotal: this.orcamento.valorTotal } }).subscribe();
                this._orcamentoPecasEspecService.deletar(orcamentoPeca.codOrcamentoPecasEspec).toPromise();
                this._snack.exibirToast('Peça excluida com sucesso', 'success');
                this.obterDados();
            }
        });
    }

    atualizar(orcamentoPeca: OrcamentoPecasEspec) {
        orcamentoPeca.valorPecaDesconto = orcamentoPeca.valorPeca - ((orcamentoPeca.valorPeca * orcamentoPeca.valorDesconto) / 100);
        orcamentoPeca.valorTotal = orcamentoPeca.valorPecaDesconto * orcamentoPeca.quantidade;
        orcamentoPeca.ipiIncluido = ((orcamentoPeca.valorPeca * orcamentoPeca.percIpi) / 100) * orcamentoPeca.quantidade;

        this._orcamentoPecasEspecService.atualizar(orcamentoPeca).subscribe();

        this.orcamento.valorTotal = _.sum(this.dataSourceData.items.map(d => d.valorTotal));
        this._osBancadaPecaOrcamentoService.atualizar({ ...this.orcamento, ...{ valorTotal: this.orcamento.valorTotal } }).subscribe();
    }

    paginar() {
        this.obterDados();
    }
}