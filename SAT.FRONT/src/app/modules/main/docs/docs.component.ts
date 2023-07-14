import { ChangeDetectorRef, Component, ElementRef, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { DocumentoSistemaData, documentoCategoriasConst } from 'app/core/types/documento-sistema.types';
import { DocumentoSistemaService } from 'app/core/services/documentos-sistema.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { toastTypesConst } from 'app/core/types/generic.types';
import { MatPaginator } from '@angular/material/paginator';
import { Subject, fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, takeUntil } from 'rxjs/operators';

@Component({
    selector: 'app-docs',
    templateUrl: './docs.component.html',
    encapsulation: ViewEncapsulation.None,
})
export class DocsComponent implements OnInit, OnDestroy {
    @ViewChild(MatPaginator) paginator: MatPaginator;
    dataSource: DocumentoSistemaData;
    categorias: string[] = [];
    loading: boolean = true;
    @ViewChild('query') query: ElementRef;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _docSistemaService: DocumentoSistemaService,
        private _snack: CustomSnackbarService,
        private _cdr: ChangeDetectorRef
    ) {
        this.categorias = documentoCategoriasConst;
    }

    async ngOnInit() {
        this.obterDados();

        fromEvent(this.query.nativeElement, 'keyup').pipe(
            map((event: any) => {
                return event.target.value;
            })
            , debounceTime(1000)
            , distinctUntilChanged()
            , takeUntil(this._unsubscribeAll)
        ).subscribe((query: string) => {
            this.paginator.pageIndex = 0;
            this.obterDados(query);
        });
    }

    public paginar() { this.obterDados(); }

    obterDados(query: string = '') {
        this._docSistemaService
            .obterPorParametros({
                filter: query,
                pageNumber: this.paginator?.pageIndex || 0 + 1,
                sortActive: 'codDocumentoSistema',
                sortDirection: 'desc',
                pageSize: this.paginator?.pageSize || 10,
            })
            .subscribe((data) => {
                this._cdr.detectChanges();
                this.dataSource = data;
                this.loading = false;
            }, e => {
                this._snack.exibirToast(e.message, toastTypesConst.ERROR)
                this.loading = false;
            });
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}

