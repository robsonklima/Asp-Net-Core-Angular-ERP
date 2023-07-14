import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { DocumentoSistemaData, documentoCategoriasConst } from 'app/core/types/documento-sistema.types';
import { DocumentoSistemaService } from 'app/core/services/documentos-sistema.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { toastTypesConst } from 'app/core/types/generic.types';
import { MatPaginator } from '@angular/material/paginator';
import { Subject } from 'rxjs';

@Component({
    selector: 'app-docs',
    templateUrl: './docs.component.html',
    encapsulation: ViewEncapsulation.None,
})
export class DocsComponent implements OnInit, OnDestroy {
    @ViewChild(MatPaginator) paginator: MatPaginator;
    dataSource: DocumentoSistemaData;
    categorias: string[] = [];
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    loading: boolean = true;

    constructor(
        private _docSistemaService: DocumentoSistemaService,
        private _snack: CustomSnackbarService,
        private _cdr: ChangeDetectorRef
    ) { }

    async ngOnInit() {
        this.obterDados();
        this.categorias = documentoCategoriasConst;
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
                this._snack.exibirToast(e, toastTypesConst.ERROR)
                this.loading = false;
            });
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}

