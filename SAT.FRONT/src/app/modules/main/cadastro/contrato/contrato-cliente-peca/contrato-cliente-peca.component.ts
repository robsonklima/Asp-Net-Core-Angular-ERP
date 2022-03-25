import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { Contrato } from 'app/core/types/contrato.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { Subject } from 'rxjs';
import { ClientePecaService } from 'app/core/services/cliente-peca.service';
import { ClientePecaData, ClientePecaParameters } from 'app/core/types/cliente-peca.types';

@Component({
    selector: 'app-contrato-cliente-peca',
    templateUrl: './contrato-cliente-peca.component.html',
    styles: [`
      .list-grid-contrato-equipamento {
          grid-template-columns: 30% 30% 30% auto;
          
          @screen sm {
            grid-template-columns: 30% 30% 30% auto;
          }
      }
  `],
})

export class ContratoClientePecaComponent implements OnInit {
    userSession: UserSession;
    @ViewChild(MatSort) private sort: MatSort;
    dataSourceData: ClientePecaData;
    selectedItem: Contrato | null = null;
    codContrato: number;
    filtro: any;
    isLoading: boolean = false;
    protected _onDestroy = new Subject<void>();

    constructor(
        private _cdr: ChangeDetectorRef,
        private _equipamentoContratoService: ClientePecaService,
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

        this.obterContratos();
        this._cdr.detectChanges();

    }

    async obterContratos() {

        const params: ClientePecaParameters = {
            codContrato: this.codContrato,
            sortActive: this.sort?.active || 'codPeca',
            sortDirection: this.sort?.direction || 'asc',
            //filter: this.searchInputControl.nativeElement.val
        };

        const data = await this._equipamentoContratoService
            .obterPorParametros(params)
            .toPromise();

        this.dataSourceData = data;
        this.isLoading = false;

    }

    excluir(ce) {
        const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
            data: {
                titulo: 'Confirmação',
                message: 'Deseja excluir este equipamento?',
                buttonText: {
                    ok: 'Sim',
                    cancel: 'Não'
                }
            }
        });

        dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
            if (confirmacao) {

            }

            this.obterContratos();
        });
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}
