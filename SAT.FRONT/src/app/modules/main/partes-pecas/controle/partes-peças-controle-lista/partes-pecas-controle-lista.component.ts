import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { OSBancadaPecasService } from 'app/core/services/os-bancada-pecas.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { OSBancadaPecas, OSBancadaPecasData, OSBancadaPecasParameters } from 'app/core/types/os-bancada-pecas.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
    selector: 'app-partes-pecas-controle-lista',
    templateUrl: './partes-pecas-controle-lista.component.html',

    styles: [
        /* language=SCSS */
        `.list-grid-partes {
            grid-template-columns: 150px;
        }`
    ],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class PartesPecasControleListaComponent extends Filterable implements OnInit, AfterViewInit, IFilterable {

    @ViewChild('sidenav') public sidenav: MatSidenav;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    dataSourceData: OSBancadaPecasData;
    isLoading: boolean = false;
    @ViewChild('searchInputControl') searchInputControl: ElementRef;

    constructor(
        protected _userService: UserService,
        private _cdr: ChangeDetectorRef,
        private _osBancadaPecasService: OSBancadaPecasService
    ) {
        super(_userService, 'partes-pecas');
        this.userSession = JSON.parse(this._userService.userSession);
    }
    
    registerEmitters(): void {
        this.sidenav.closedStart.subscribe(() => {
            this.onSidenavClosed();
            this.obterDados();
          });
    }

    ngOnInit(): void {
    }

    async ngAfterViewInit() {
        await this.obterDados();
        this.registerEmitters();

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

        const parametros: OSBancadaPecasParameters = {
            ...{
                pageNumber: this.paginator?.pageIndex + 1,
                sortActive: 'codOsbancada',
                sortDirection: 'desc',
                pageSize: this.paginator?.pageSize,
                filter: filtro
            },
            ...this.filter?.parametros
        }

        const data = await this._osBancadaPecasService.obterPorParametros(parametros).toPromise();
        this.dataSourceData = data;
        
        this.isLoading = false;
        this._cdr.detectChanges();
    }

    paginar() {
        this.obterDados();
    }
}
