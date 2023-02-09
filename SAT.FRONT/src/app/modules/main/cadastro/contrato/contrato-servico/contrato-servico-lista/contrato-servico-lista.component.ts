import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { ContratoServicoService } from 'app/core/services/contrato-servico.service';
import { ContratoServico, ContratoServicoData, ContratoServicoParameters } from 'app/core/types/contrato-servico.types';
import { Contrato } from 'app/core/types/contrato.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

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
    contratoServicoData: ContratoServicoData;
    selectedItem: Contrato | null = null;
    codContrato: number;
    filtro: any;
    isLoading: boolean = false;
    @ViewChild('searchInputControl') searchInputControl: ElementRef;
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

    ngAfterViewInit(): void {
        this.isLoading = true;
        this.codContrato = +this._route.snapshot.paramMap.get('codContrato');
        this.obterContratosServico();
        this.registrarEmitters();
        this._cdr.detectChanges();
    }

    registrarEmitters() {
        fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
			map((event: any) => {
				return event.target.value;
			})
			, debounceTime(1000)
			, distinctUntilChanged()
		).subscribe((text: string) => {
            this.obterContratosServico(text);
		});
    }

    async obterContratosServico(text: string='') {
        this.isLoading = true;
        const params: ContratoServicoParameters = { 
            codContrato: this.codContrato,
            filter: text
        };

        const data = await this._contratoServicoService
            .obterPorParametros(params)
            .toPromise();

        this.contratoServicoData = data;
        this.isLoading = false;
    }

    async excluir(codigoServico: number) {
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
                await this._contratoServicoService.deletar(codigoServico).toPromise();
                this.obterContratosServico();
            }
        });
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}
