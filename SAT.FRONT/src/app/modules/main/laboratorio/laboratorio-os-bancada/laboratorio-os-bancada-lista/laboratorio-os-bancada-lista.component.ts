import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { OSBancadaService } from 'app/core/services/os-bancada.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { OSBancadaData, OSBancadaParameters } from 'app/core/types/os-bancada.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
    selector: 'app-laboratorio-os-bancada-lista',
    templateUrl: './laboratorio-os-bancada-lista.component.html',

    styles: [
        /* language=SCSS */
        `.list-grid-u {
            grid-template-columns: 10% 20% 10% 10% 10% 5% 10%;
        }`
    ],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class LaboratorioOSBancadaListaComponent extends Filterable implements OnInit, AfterViewInit, IFilterable {

    @ViewChild('sidenav') public sidenav: MatSidenav;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    dataSourceData: OSBancadaData;
    isLoading: boolean = false;
    @ViewChild('searchInputControl') searchInputControl: ElementRef;

    constructor(
        protected _userService: UserService,
        private _cdr: ChangeDetectorRef,
        private _osBancadaService: OSBancadaService,
        private _exportacaoService: ExportacaoService
    ) {
        super(_userService, 'os-bancada');
        this.userSession = JSON.parse(this._userService.userSession);
    }
    registerEmitters(): void {
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

    async obterDados(filtro: string = '') {
        this.isLoading = true;

        const parametros: OSBancadaParameters = {
            ...{
                pageNumber: this.paginator?.pageIndex + 1,
                sortActive: 'codOsbancada',
                sortDirection: 'desc',
                pageSize: this.paginator?.pageSize,
                filter: filtro
            },
            ...this.filter?.parametros
        }

        const data = await this._osBancadaService.obterPorParametros(parametros).toPromise();
        this.dataSourceData = data;
        
        this.isLoading = false;
        this._cdr.detectChanges();
    }

    exportar() {

        // let params: Exportacao = {
        //     formatoArquivo: ExportacaoFormatoEnum.EXCEL,
        //     tipoArquivo: ExportacaoTipoEnum.VALOR_COMBUSTIVEL,
        //     entityParameters: {}
        // }

        // this._exportacaoService.exportar(FileMime.Excel,params);
    }

    paginar() {
        this.obterDados();
    }
}
