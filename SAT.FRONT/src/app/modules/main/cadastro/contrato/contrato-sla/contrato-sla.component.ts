import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ContratoSLAService } from 'app/core/services/contrato-sla.service';
import { ContratoSLA } from './../../../../../core/types/contrato-sla.types';
import { AcordoNivelServico } from 'app/core/types/acordo-nivel-servico.types';
import { AcordoNivelServicoService } from './../../../../../core/services/acordo-nivel-servico.service';
import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { Subject } from 'rxjs';
import { FormControl } from '@angular/forms';
import { debounceTime, delay, filter, map, takeUntil, tap } from 'rxjs/operators';
import moment from 'moment';
import Enumerable from 'linq';

@Component({
    selector: 'app-contrato-sla',
    templateUrl: './contrato-sla.component.html',
    styles: [`
        .list-grid-contrato-sla {
            grid-template-columns: 100px auto 150px;
            
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
    
    filtro: any;

    //cod novo
    
    protected _onDestroy = new Subject<void>();
    public slas: AcordoNivelServico[] = [];
    public contratoSlas: ContratoSLA[] = [];
    userSession: UserSession;
    codContrato: number;
    slasFiltro: FormControl = new FormControl();
    isLoading: boolean = false;
    searching: boolean = false;

    constructor(
        private _cdr: ChangeDetectorRef,
        private _userService: UserService,
        private _route: ActivatedRoute,
        public _dialog: MatDialog,
        private _snack: CustomSnackbarService,
        private _slaService: AcordoNivelServicoService,
        private _contratoSlaService: ContratoSLAService
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
        this.contratoSlas = (await this._contratoSlaService.obterPorParametros({ codContrato: this.codContrato }).toPromise()).items

        this.slasFiltro.valueChanges
			.pipe(filter(query => !!query),
				tap(() => this.searching = true),
				takeUntil(this._onDestroy),
				debounceTime(700),
				map(async query => {
					const data = await this._slaService.obterPorParametros({
						sortActive: 'NomeSLA',
						sortDirection: 'asc',
						filter: query,
						pageSize: 100,
					}).toPromise();

					return data.items.slice();
				}),
				delay(500),
				takeUntil(this._onDestroy)
			)
			.subscribe(async data => {
				this.searching = false;
				this.slas = await data;
			},
				() => {
					this.searching = false;
				}
			);

        this.isLoading = false;
    }

    salvar(sla: AcordoNivelServico) {

        let obj: ContratoSLA = {
            codContrato: this.codContrato,
            codSLA: sla.codSLA,
            dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
            codUsuarioCad: this.userSession.usuario?.codUsuario
        }
        this._contratoSlaService.criar(obj).subscribe(() => {
            this._snack.exibirToast("SLA inserido com sucesso!", "success");
        })

        this.obterDados();
    }

    excluir(csla: ContratoSLA) {
        const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
            data: {
                titulo: 'Confirmação',
                message: 'Deseja excluir este SLA?',
                buttonText: {
                    ok: 'Sim',
                    cancel: 'Não'
                }
            }
        });
        
        dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
            if (confirmacao) {
                this._contratoSlaService.deletar(csla.codContrato, csla.codSLA).subscribe(() =>{
                    this._snack.exibirToast("SLA excluída com sucesso!", "success");
                });

                this.obterDados();
            }

        });
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}
