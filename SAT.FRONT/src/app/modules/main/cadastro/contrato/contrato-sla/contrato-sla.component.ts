import { AcordoNivelServico } from 'app/core/types/acordo-nivel-servico.types';
import { AcordoNivelServicoService } from './../../../../../core/services/acordo-nivel-servico.service';
import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import Enumerable from 'linq';
import { MatDialog } from '@angular/material/dialog';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { ContratoEquipamentoService } from 'app/core/services/contrato-equipamento.service';
import { ContratoEquipamentoData, ContratoEquipamento } from 'app/core/types/contrato-equipamento.types';
import { Contrato, ContratoParameters } from 'app/core/types/contrato.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { Subject } from 'rxjs';
import { FormControl } from '@angular/forms';

@Component({
	selector: 'app-contrato-sla',
	templateUrl: './contrato-sla.component.html',
	styles: [`
        .list-grid-contrato-sla {
            grid-template-columns: auto 250px 200px 200px;
            
            /* @screen sm {
                grid-template-columns: 48px 250px 150px auto 120px;
            }
        
            @screen md{
                grid-template-columns: 48px 250px 150px auto 120px;
            }
        
            @screen lg {
                grid-template-columns: 48px 250px 150px auto 120px;
            } */
        }
    `]
})

export class ContratoSlaComponent implements OnInit {
	//copia velha
	@ViewChild('sidenav') sidenav: MatSidenav;
    @ViewChild(MatSort) private sort: MatSort;
    userSession: UserSession;
    dataSourceData: ContratoEquipamentoData;
    selectedItem: Contrato | null = null;
    codContrato: number;
    filtro: any;
    isLoading: boolean = false;
    protected _onDestroy = new Subject<void>();
	slasFiltro: FormControl = new FormControl();

	//cod novo
	public slas: AcordoNivelServico[] = [];

    constructor(
        private _cdr: ChangeDetectorRef,
        private _contratoEquipService: ContratoEquipamentoService,
        private _userService: UserService,
        private _route: ActivatedRoute,
        public _dialog: MatDialog,
		private _slaService: AcordoNivelServicoService

    ) {

        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void {

    }

    ngAfterViewInit(): void {
        this.codContrato = +this._route.snapshot.paramMap.get('codContrato');

        this.obterDados();
        this._cdr.detectChanges();
    }

    async obterDados() {
        this.isLoading = true;
		this.slas = (await this._slaService.obterPorParametros({}).toPromise()).items;

        this.isLoading = false;
    }

    excluir(ce: ContratoEquipamento) {
        const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
            data: {
                titulo: 'Confirmação',
                message: 'Deseja excluir este modelo?',
                buttonText: {
                    ok: 'Sim',
                    cancel: 'Não'
                }
            }
        });

        dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
            if (confirmacao) {
                this._contratoEquipService.deletar(ce.codContrato, ce.codEquip).subscribe();
            }
            
            this.obterDados();
        });
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}
