import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { ChangeDetectorRef, Component, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Contrato } from 'app/core/types/contrato.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { Subject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ContratoServico, ContratoServicoData, ContratoServicoParameters } from 'app/core/types/contrato-servico.types';
import { ContratoServicoService } from 'app/core/services/contrato-servico.service';

@Component({
    selector: 'app-contrato-servico-lista',
    templateUrl: './contrato-servico-lista.component.html',
    styles: [`
        .list-grid-contrato-servico {
            grid-template-columns: auto 75px auto 75px 75px;
            
            @screen sm {
                grid-template-columns: 100px auto 250px 200px 200px;
            }
        }
    `],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class ContratoServicoListaComponent implements AfterViewInit {
   
    userSession: UserSession;
    @ViewChild(MatSort) private sort: MatSort;
    contratoServicoData: ContratoServicoData;
    selectedItem: Contrato | null = null;
    codContrato: number;
    filtro: any;
    isLoading: boolean = false;
    protected _onDestroy = new Subject<void>();

    constructor(
        private _cdr: ChangeDetectorRef,
        private _contratoServicoService: ContratoServicoService,
        private _userService: UserService,
        private _route: ActivatedRoute,
        public _dialog: MatDialog

    ) {

        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void {

    }

    ngAfterViewInit(): void {
        this.isLoading = true;
        this.codContrato = +this._route.snapshot.paramMap.get('codContrato');
        this.obterContratosServico();
        this._cdr.detectChanges();
    }

    async obterContratosServico() {

        const params: ContratoServicoParameters = { codContrato: this.codContrato };

        const data = await this._contratoServicoService
            .obterPorParametros(params)
            .toPromise();

        this.contratoServicoData = data;
        this.isLoading = false;
    }

    async excluir(contratoServico: ContratoServico) {
        const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
            data: {
                titulo: 'Confirmação',
                message: 'Deseja excluir este servico?',
                buttonText: {
                    ok: 'Sim',
                    cancel: 'Não'
                }
            }
        });

        dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
            if (confirmacao) {
                await this._contratoServicoService.deletar(contratoServico.codContratoServico).toPromise();
                this.obterContratosServico();
            }
        });
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}
