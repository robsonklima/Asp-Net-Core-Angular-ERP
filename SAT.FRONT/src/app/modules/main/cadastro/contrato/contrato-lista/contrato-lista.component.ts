import { IFilterable } from 'app/core/types/filtro.types';
import { Filterable } from './../../../../../core/filters/filterable';
import { MatSidenav } from '@angular/material/sidenav';
import { ContratoParameters } from './../../../../../core/types/contrato.types';
import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ContratoService } from 'app/core/services/contrato.service';
import { ContratoData, Contrato } from 'app/core/types/contrato.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent, interval, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, startWith, takeUntil } from 'rxjs/operators';
import { FileMime } from 'app/core/types/file.types';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';

@Component({
    selector: 'app-contrato-lista',
    templateUrl: './contrato-lista.component.html',
    styles: [`
        .list-grid-contrato {
            grid-template-columns: 48px 250px 150px auto 160px 30px;
            
            /* @screen sm {
               grid-template-columns: 48px 250px 150px auto 120px 30px;
            }
        
            @screen md{
                grid-template-columns: 48px 250px 150px auto 120px 30px;
            }
        
            @screen lg {
                grid-template-columns: 48px 250px 150px auto 120px 30px;
            }  */
        }
    `],
})
export class ContratoListaComponent extends Filterable implements AfterViewInit, IFilterable {
    @ViewChild('sidenav') sidenav: MatSidenav;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    @ViewChild('searchInputControl') searchInputControl: ElementRef;

    dataSourceData: ContratoData;
    selectedItem: Contrato | null = null;
    isLoading: boolean = false;
    protected _onDestroy = new Subject<void>();

    constructor(
        private _cdr: ChangeDetectorRef,
        private _contratoService: ContratoService,
        private _exportacaoService: ExportacaoService,
        protected _userService: UserService,
    ) {

        super(_userService, 'contrato');
    }


    registerEmitters(): void {
        this.sidenav.closedStart.subscribe(() => {
            this.onSidenavClosed();
            this.obterDados();
        })
    }

    loadFilter(): void {
        super.loadFilter();
    }

    onSidenavClosed(): void {
        if (this.paginator) this.paginator.pageIndex = 0;
        this.loadFilter();
        this.obterDados();
    }


    ngOnInit(): void {
    }

    ngAfterViewInit(): void {

        this.obterContratos();
        this.registerEmitters();
        this._cdr.detectChanges();
    }

    public async obterContratos(filter: string = '') {
        this.isLoading = true;

        fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
            map((event: any) => {
                return event.target.value;
            })
            , debounceTime(1000)
            , distinctUntilChanged()
        ).subscribe((text: string) => {
            this.paginator.pageIndex = 0;
            this.obterContratos(text);
        });

        const params: ContratoParameters = {
            pageNumber: this.paginator.pageIndex + 1,
            sortActive: this.sort.active || 'codContrato',
            sortDirection: this.sort.direction || 'desc',
            pageSize: this.paginator?.pageSize,
            filter: filter
        };

        const data: ContratoData = await this._contratoService
            .obterPorParametros({
                ...params,
                codClientes: this.filter?.parametros.codClientes,
                codTipoContrato: this.filter?.parametros.codTipoContrato
            })
            .toPromise();

        this.dataSourceData = data;
        this.isLoading = false;
    }

    async obterDados(filtro: string = '') {
        this.isLoading = true;

        const params: ContratoParameters = {
            pageNumber: this.paginator?.pageIndex + 1,
            sortActive: this.sort?.active,
            sortDirection: this.sort?.direction || 'asc',
            pageSize: this.paginator?.pageSize,
            filter: filtro
        };

        const data: ContratoData = await this._contratoService.obterPorParametros({
            ...params,
            ...this.filter?.parametros
        }).toPromise();
        this.dataSourceData = data;
        this.isLoading = false;
        this._cdr.detectChanges();
    }


    public async exportar() {
        this.isLoading = true;

        let exportacaoParam: Exportacao = {
            formatoArquivo: ExportacaoFormatoEnum.EXCEL,
            tipoArquivo: ExportacaoTipoEnum.CONTRATO,
            entityParameters: {}
        }

        await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
        this.isLoading = false;
    }

    paginar() {
        this.onPaginationChanged()
        this.obterContratos();
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}
