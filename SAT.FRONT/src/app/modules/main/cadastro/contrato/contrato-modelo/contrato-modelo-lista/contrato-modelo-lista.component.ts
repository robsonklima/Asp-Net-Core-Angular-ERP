import { ContratoEquipamentoService } from 'app/core/services/contrato-equipamento.service';
import { ContratoParameters } from '../../../../../../core/types/contrato.types';
import { ChangeDetectorRef, Component, OnInit, ViewChild, ViewEncapsulation, AfterViewChecked, AfterViewInit } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Contrato } from 'app/core/types/contrato.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { Subject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ContratoEquipamentoData } from 'app/core/types/contrato-equipamento.types';


@Component({
    selector: 'app-contrato-modelo-lista',
    templateUrl: './contrato-modelo-lista.component.html',
    styles: [`
        .list-grid-contrato-modelo {
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
    `],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class ContratoModeloListaComponent implements AfterViewInit {
    @ViewChild('sidenav') sidenav: MatSidenav;
    userSession: UserSession;
    @ViewChild(MatSort) private sort: MatSort;
    dataSourceData: ContratoEquipamentoData;
    selectedItem: Contrato | null = null;
    codContrato: number;
    filtro: any;
    isLoading: boolean = false;
    protected _onDestroy = new Subject<void>();

    constructor(
        private _cdr: ChangeDetectorRef,
        private _contratoEquipService: ContratoEquipamentoService,
        private _userService: UserService,
        private _route: ActivatedRoute,
    ) {

        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void {

    }

    ngAfterViewInit(): void {
        this.codContrato = +this._route.snapshot.paramMap.get('codContrato');

        this.obterContratos();
        this._cdr.detectChanges();
    }

    async obterContratos() {
        this.isLoading = true;

        const params: ContratoParameters = {
            codContrato: this.codContrato,           
        };

        const data: ContratoEquipamentoData = await this._contratoEquipService
            .obterPorParametros({
                ...params,
            })
            .toPromise();

        this.dataSourceData = data;
        console.log(data);
        
        this.isLoading = false;
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}
