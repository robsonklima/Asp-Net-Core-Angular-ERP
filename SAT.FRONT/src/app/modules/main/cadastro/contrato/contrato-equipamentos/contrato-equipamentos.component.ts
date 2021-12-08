import { EquipamentoContratoService } from './../../../../../core/services/equipamento-contrato.service';
import { EquipamentoContratoParameters } from './../../../../../core/types/equipamento-contrato.types';
import { EquipamentoContratoData } from 'app/core/types/equipamento-contrato.types';
import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { ContratoEquipamentoService } from 'app/core/services/contrato-equipamento.service';
import { ContratoEquipamentoData } from 'app/core/types/contrato-equipamento.types';
import { Contrato, ContratoParameters } from 'app/core/types/contrato.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { Subject } from 'rxjs';

@Component({
    selector: 'app-contrato-equipamentos',
    templateUrl: './contrato-equipamentos.component.html',
    styles: [`
      .list-grid-contrato-equipamento {
          grid-template-columns: 72px auto 100px 60px 100px 100px 80px 80px 50px;
          
          @screen sm {
            grid-template-columns: 72px auto 75px 60px 40px 100px 80px 80px 80px 50px;
          }
      }
  `],
})

export class ContratoEquipamentosComponent implements OnInit {
    userSession: UserSession;
    @ViewChild(MatSort) private sort: MatSort;
    dataSourceData: EquipamentoContratoData;
    selectedItem: Contrato | null = null;
    codContrato: number;
    filtro: any;
    isLoading: boolean = false;
    protected _onDestroy = new Subject<void>();

    constructor(
        private _cdr: ChangeDetectorRef,
        private _equipamentoContratoService: EquipamentoContratoService,
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

        const params: EquipamentoContratoParameters = {
            codContrato: this.codContrato,
            sortActive: this.sort?.active || 'numSerie',
            sortDirection: this.sort?.direction || 'asc',
            //filter: this.searchInputControl.nativeElement.val
        };

        const data = await this._equipamentoContratoService
            .obterPorParametros(params)
            .toPromise();

        console.log(data);
        
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
                console.log('Not Implemented hihi ');
                
            }

            this.obterContratos();
        });
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}
