import { AfterViewInit, ChangeDetectorRef, Component, Input, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { fuseAnimations } from '@fuse/animations';
import { OSBancadaPecasService } from 'app/core/services/os-bancada-pecas.service';
import { LaboratorioOSBancadaPecaRE5114DialogComponent } from '../laboratorio-os-bancada-pecare5114-dialog/laboratorio-os-bancada-pecare5114-dialog.component';
import { OSBancadaPecas, OSBancadaPecasData, OSBancadaPecasParameters } from 'app/core/types/os-bancada-pecas.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import _ from 'lodash';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { PecaRE5114Service } from 'app/core/services/pecaRE5114.service';

@Component({
    selector: 'app-laboratorio-os-bancada-pecare5114-lista',
    templateUrl: './laboratorio-os-bancada-pecare5114-lista.component.html',
    styles: [
        /* language=SCSS */
        `.list-grid-u {
            grid-template-columns: 10% 10% 40% 8% 15% 10%;
        }`
    ],
    animations: fuseAnimations,
    encapsulation: ViewEncapsulation.None,
})
export class LaboratorioOSBancadaPecaRE5114ListaComponent implements AfterViewInit {
    @Input() codOsbancada: number;
    dataSourceData: OSBancadaPecasData;
    userSession: UsuarioSessao;
    isLoading: boolean = false;

    constructor(
        private _userService: UserService,
        private _cdr: ChangeDetectorRef,
        private _osBancadaPecasService: OSBancadaPecasService,
        private _pecaRE5114Service: PecaRE5114Service,
        private _dialog: MatDialog,
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngAfterViewInit(): void {
        this.obterDados();

        this._cdr.detectChanges();
    }

    private async obterDados() {
        const parametros: OSBancadaPecasParameters = {
            ...{
                sortActive: 'codPecaRe5114',
                sortDirection: 'desc',
                codOsbancadas: this.codOsbancada.toString()
            }
        }

        const data = await this._osBancadaPecasService.obterPorParametros(parametros).toPromise();
        this.dataSourceData = data;

        this.isLoading = false;
        this._cdr.detectChanges();
    }

    editarPeca(osBancadaPecas: OSBancadaPecas) {
        const dialogRef = this._dialog.open(LaboratorioOSBancadaPecaRE5114DialogComponent, {
            data: { osBancadaPecas: osBancadaPecas }
        });

        dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
            if (confirmacao) {
                this.obterDados();
            }
        });
    }

    validarStatus(oSBancadaPecas: OSBancadaPecas) {
        var osBancadaPecas = oSBancadaPecas;

        if (osBancadaPecas.indPecaDevolvida == 1)
            return "Devolvida";

        else if (osBancadaPecas.indPecaLiberada == 1)
            return "Liberada";

        return "Transferido";
    }

    async removerPeca(osBancadaPecas: OSBancadaPecas) {
        const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: `Deseja remover o atendimento?`,
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
            if (confirmacao) {
                await this._osBancadaPecasService.deletar(osBancadaPecas.codOsbancada, osBancadaPecas.codPecaRe5114).toPromise();
                await this._pecaRE5114Service.deletar(osBancadaPecas.codPecaRe5114).toPromise();
                
                this.ngAfterViewInit();
            }
        });
    }

    async onChange($event: MatSlideToggleChange, osBancadaPecas: OSBancadaPecas) {
        if ($event.checked && osBancadaPecas.indPecaLiberada == 1)
            osBancadaPecas.indPecaDevolvida = 1;

        else {
            osBancadaPecas.indPecaDevolvida = 0;
            osBancadaPecas.indImpressao = 0;
        }

        this._osBancadaPecasService.atualizar(osBancadaPecas).subscribe();
    }

    paginar() {
        this.obterDados();
    }
}