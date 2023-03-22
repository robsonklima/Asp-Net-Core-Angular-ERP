import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { OrdemServico, OrdemServicoData, OrdemServicoParameters } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { UserService } from 'app/core/user/user.service';
import _ from 'lodash';

@Component({
    selector: 'app-partes-pecas-controle-lista',
    templateUrl: './partes-pecas-controle-lista.component.html',

    styles: [
        /* language=SCSS */
        `.list-grid-partes {
            grid-template-columns: 120px 120px 100px 200px 80px 200px 200px 400px auto;
        }`
    ],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class PartesPecasControleListaComponent extends Filterable implements OnInit, AfterViewInit, IFilterable {

    @ViewChild('sidenav') public sidenav: MatSidenav;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    dataSourceData: OrdemServicoData;
    rat: RelatorioAtendimento;
    isLoading: boolean = false;
    @ViewChild('searchInputControl') searchInputControl: ElementRef;

    constructor(
        protected _userService: UserService,
        private _cdr: ChangeDetectorRef,
        private _osService: OrdemServicoService
    ) {
        super(_userService, 'partes-pecas');
        this.userSession = JSON.parse(this._userService.userSession);
    }

    registerEmitters(): void {
        this.sidenav.closedStart.subscribe(() => {
            this.onSidenavClosed();
            this.obterOS();
        });
    }

    ngOnInit(): void {
    }

    async ngAfterViewInit() {
        await this.obterOS();
        this.registerEmitters();

        if (this.sort && this.paginator) {
            this.sort.disableClear = true;
            this._cdr.markForCheck();

            this.sort.sortChange.subscribe(() => {
                this.paginator.pageIndex = 0;
                this.obterOS();
            });
        }
        this._cdr.detectChanges();
    }

    async obterOS(filtro: string = '') {
        this.isLoading = true;

        const parametros: OrdemServicoParameters = {
            ...{
                pageNumber: this.paginator?.pageIndex + 1,
                sortActive: 'codOS',
                sortDirection: 'desc',
                pageSize: this.paginator?.pageSize,
                filter: filtro,
                codStatusServicos: "9,10,11,6,7",
            },
           ...this.filter?.parametros
        }
        console.log(parametros);
        

        const data = await this._osService.obterPorParametros(parametros).toPromise();
        this.dataSourceData = data;

        this.isLoading = false;
        this._cdr.detectChanges();
    }

    isUltimaRAT(os: OrdemServico) {
        var count = os?.relatoriosAtendimento.length - 1;
        this.rat = os?.relatoriosAtendimento[count];
        return this.rat?.codRAT;
    }

    paginar() {
        this.obterOS();
    }
}
