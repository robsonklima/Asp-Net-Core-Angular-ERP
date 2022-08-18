import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { DespesaConfiguracaoCombustivelService } from 'app/core/services/despesa-configuracao-combustivel.service';
import { DespesaConfiguracaoCombustivelData, DespesaConfiguracaoCombustivelParameters } from 'app/core/types/despesa-configuracao-combustivel.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
    selector: 'app-valores-combustivel-lista',
    templateUrl: './valores-combustivel-lista.component.html',

    styles: [
        /* language=SCSS */
        `.list-grid-u {
            grid-template-columns: 3% 5% 5% 10% 15% 10% 15% auto;
        }`
    ],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class ValoresCombustivelListaComponent extends Filterable implements OnInit, AfterViewInit, IFilterable {

    @ViewChild('sidenav') public sidenav: MatSidenav;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    dataSourceData: DespesaConfiguracaoCombustivelData;
    isLoading: boolean = false;
    @ViewChild('searchInputControl') searchInputControl: ElementRef;

    constructor(
        protected _userService: UserService,
        private _cdr: ChangeDetectorRef,
        private _despesaConfiguracaoCombustivelService: DespesaConfiguracaoCombustivelService
        //private _exportacaoService: ExportacaoService
    ) {
        super(_userService, 'despesaConfiguracaoCombustivel')
        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void {
    }

    registerEmitters(): void {
        // this.sidenav.closedStart.subscribe(() => {
        //     this.onSidenavClosed();
        //     this.obterDados();
        // })
    }

    async ngAfterViewInit() {
        this.registerEmitters();
        this.obterDados();

        if (this.sort && this.paginator) {
            fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
                map((event: any) => {
                    return event.target.value;
                })
                , debounceTime(700)
                , distinctUntilChanged()
            ).subscribe((text: string) => {
                this.paginator.pageIndex = 0;
                this.searchInputControl.nativeElement.val = text;
                this.obterDados(text);
            });

            this.sort.disableClear = true;
            this._cdr.markForCheck();

            this.sort.sortChange.subscribe(() => {
                this.paginator.pageIndex = 0;
                this.obterDados();
            });
        }

        this._cdr.detectChanges();
    }

    async obterDados(filtro: string = '') {
        this.isLoading = true;

        const parametros: DespesaConfiguracaoCombustivelParameters = {
            ...{
                pageNumber: this.paginator?.pageIndex + 1,
                sortActive: this.sort?.active,
                sortDirection: this.sort?.direction,
                pageSize: this.paginator?.pageSize,
                filter: filtro
            },
            ...this.filter?.parametros
        }
        const data = await this._despesaConfiguracaoCombustivelService.obterPorParametros(parametros).toPromise();
        this.dataSourceData = data;
        this.isLoading = false;
        this._cdr.detectChanges();
    }
    
    paginar() {
        this.obterDados();
    }
}
