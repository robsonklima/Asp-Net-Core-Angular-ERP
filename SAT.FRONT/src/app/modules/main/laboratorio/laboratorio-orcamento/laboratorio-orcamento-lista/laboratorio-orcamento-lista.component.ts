import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { OsBancadaPecasOrcamentoService } from 'app/core/services/os-bancada-pecas-orcamento.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { OsBancadaPecasOrcamentoData, OsBancadaPecasOrcamentoParameters } from 'app/core/types/os-bancada-pecas-orcamento.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
    selector: 'app-laboratorio-orcamento-lista',
    templateUrl: './laboratorio-orcamento-lista.component.html',

    styles: [
        /* language=SCSS */
        `.list-grid-u {
            grid-template-columns: 60px 80px 70px 70px 80px auto 200px 80px 120px;
        }`
    ],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class LaboratorioOrcamentoListaComponent extends Filterable implements OnInit, IFilterable {

    @ViewChild('sidenav') public sidenav: MatSidenav;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    dataSourceData: OsBancadaPecasOrcamentoData;
    isLoading: boolean = false;
    @ViewChild('searchInputControl') searchInputControl: ElementRef;

    constructor(
        protected _userService: UserService,
        private _cdr: ChangeDetectorRef,
        private _osBancadaPecasOrcService: OsBancadaPecasOrcamentoService,
    ) {
        super(_userService, 'orcamento-bancada');
        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void {
    }

    async ngAfterViewInit() {
        await this.obterDados();

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

    registerEmitters(): void {
   }

    async obterDados(filtro: string = '') {
        this.isLoading = true;

    const parametros: OsBancadaPecasOrcamentoParameters = {
            ...{
                pageNumber: this.paginator?.pageIndex + 1,
                sortActive: 'codPecaRe5114',
                sortDirection: 'desc',
                pageSize: this.paginator?.pageSize,
                filter: filtro
            },
            ...this.filter?.parametros
        }

        const data = await this._osBancadaPecasOrcService.obterPorParametros(parametros).toPromise();
        this.dataSourceData = data;
        console.log(data.items);
        
        
        this.isLoading = false;
        this._cdr.detectChanges();
    }

    paginar() {
        this.obterDados();
    }
}
