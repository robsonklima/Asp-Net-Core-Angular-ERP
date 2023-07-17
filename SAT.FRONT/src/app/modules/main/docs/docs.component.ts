import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnDestroy, ViewChild, ViewEncapsulation } from '@angular/core';
import { DocumentoSistemaData, documentoCategoriasConst } from 'app/core/types/documento-sistema.types';
import { DocumentoSistemaService } from 'app/core/services/documentos-sistema.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { toastTypesConst } from 'app/core/types/generic.types';
import { Subject, fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
    selector: 'app-docs',
    templateUrl: './docs.component.html',
    encapsulation: ViewEncapsulation.None,
})
export class DocsComponent implements AfterViewInit, OnDestroy {
    @ViewChild('query') query: ElementRef;
    dataSource: DocumentoSistemaData;
    categorias: string[] = [];
    categoriaSelecionada: string;
    loading: boolean = true;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _docSistemaService: DocumentoSistemaService,
        private _snack: CustomSnackbarService,
        private _cdr: ChangeDetectorRef
    ) {
        this.categorias = documentoCategoriasConst;
    }

    async ngAfterViewInit() {
        this.obterDados();

        fromEvent(this.query.nativeElement, 'keyup').pipe(
            map((event: any) => {
                return event.target.value;
            })
            , debounceTime(1000)
            , distinctUntilChanged()
        ).subscribe((query: string) => {
            this.obterDados(query);
        });
    }

    public paginar() { this.obterDados(); }

    aplicarFiltroCategoria(e: any) {
        if (e.value == 'TODOS')
            this.categoriaSelecionada = null;
        else
            this.categoriaSelecionada = e.value;

        this.obterDados();
    }

    obterDocumentosPorCategoria(categoria: string) {
        return this.dataSource.items.filter(d => d.categoria == categoria);
    }

    obterDados(query: string = '') {
        this.loading = true;

        this._docSistemaService
            .obterPorParametros({
                filter: query,
                categoria: this.categoriaSelecionada,
                sortActive: 'codDocumentoSistema',
                sortDirection: 'desc',
                pageSize: 24,
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

