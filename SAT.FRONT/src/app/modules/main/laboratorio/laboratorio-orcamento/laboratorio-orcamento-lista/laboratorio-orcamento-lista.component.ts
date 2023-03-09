import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { OsBancadaPecasOrcamentoService } from 'app/core/services/os-bancada-pecas-orcamento.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
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
        `.list-grid-orc {
            grid-template-columns: 60px 80px 70px 70px 80px auto 200px 80px 80px 120px;
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
        private _snack: CustomSnackbarService,
        private _exportacaoService: ExportacaoService
    ) {
        super(_userService, 'laboratorio-orcamento');
        this.userSession = JSON.parse(this._userService.userSession);
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

    registerEmitters(): void {
        this.sidenav.closedStart.subscribe(() => {
            this.onSidenavClosed();
            this.obterDados();
          });
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
        
        this.isLoading = false;
        this._cdr.detectChanges();
    }

    exportar(orcamento) {
        let exportacaoParam: Exportacao = {
          formatoArquivo: ExportacaoFormatoEnum.PDF,
          tipoArquivo: ExportacaoTipoEnum.ORC_BANCADA,
          entityParameters: {
            codOrcamento: orcamento.codOrcamento
          }
        }
    
        this._exportacaoService
          .exportar(FileMime.PDF, exportacaoParam)
          .catch(e => { this._snack.exibirToast(`Não foi possível realizar o download ${e.message}`) });
      }

    paginar() {
        this.obterDados();
    }
}
