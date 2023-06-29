import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { FeriadoService } from 'app/core/services/feriado.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FeriadoData, FeriadoParameters } from 'app/core/types/feriado.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
    selector: 'app-feriado-lista',
    templateUrl: './feriado-lista.component.html',
    styles: [`
        .list-grid-u {
              grid-template-columns: 25% 25% 25% auto;
        }`
    ],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class FeriadoListaComponent extends Filterable implements AfterViewInit, IFilterable {

    @ViewChild('sidenav') public sidenav: MatSidenav;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    dataSourceData: FeriadoData;
    isLoading: boolean = false;
    @ViewChild('searchInputControl') searchInputControl: ElementRef;

    constructor(
        protected _userService: UserService,
        private _cdr: ChangeDetectorRef,
        private _feriadoService: FeriadoService,
        private _exportacaoService: ExportacaoService
    ) {
        super(_userService, 'feriado')
        this.userSession = JSON.parse(this._userService.userSession);
    }

    registerEmitters(): void {
        this.sidenav.closedStart.subscribe(() => {
            this.onSidenavClosed();
            this.obterDados();
        })
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

    const parametros: FeriadoParameters = {
        ...{
            pageNumber: this.paginator?.pageIndex + 1,
            sortActive: this.sort?.active || 'data',
            sortDirection: this.sort?.direction || 'desc',
            pageSize: this.paginator?.pageSize,
            filter: filtro
        },
        ...this.filter?.parametros
    }
    const data = await this._feriadoService.obterPorParametros(parametros).toPromise();
    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
}
async exportar(){
    this.isLoading = true;
    
    let exportacaoParam: Exportacao = {
        formatoArquivo: ExportacaoFormatoEnum.EXCEL,
        tipoArquivo: ExportacaoTipoEnum.FERIADO,
        entityParameters: this.filter?.parametros
    }

    await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);

    this.isLoading = false;
}

paginar() {
    this.obterDados();
}
}
