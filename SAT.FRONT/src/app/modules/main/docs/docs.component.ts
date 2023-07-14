import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { DocumentoSistemaService } from 'app/core/services/documentos-sistema.service';
import { MatDialog } from '@angular/material/dialog';
import { DocumentoSistemaFormDialogComponent } from './documento-sistema-form-dialog/documento-sistema-form-dialog.component';
import { DocumentoSistema, DocumentoSistemaData, documentoCategoriasConst } from 'app/core/types/documento-sistema.types';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { toastTypesConst } from 'app/core/types/generic.types';
import { MatPaginator } from '@angular/material/paginator';

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
        private _dialog: MatDialog,
        private _snack: CustomSnackbarService,
        private _cdr: ChangeDetectorRef
    ) { }

    async ngOnInit() {
        this.obterDados();
        this.categorias = documentoCategoriasConst;
    }

    onDocumentoSistema(documento: DocumentoSistema = null) {
        const dialogRef = this._dialog.open(DocumentoSistemaFormDialogComponent, {
            data: {
                documento: documento
            }
        });

        dialogRef.afterClosed().subscribe(async (data: any) => {
            if (data) {

            }
        });
    }

    public paginar() { this.obterDados(); }

    obterDados(query: string = '') {
        this._docSistemaService
            .obterPorParametros({
                filter: query,
                pageNumber: this.paginator?.pageIndex + 1,
                sortActive: 'codDocumentoSistema',
                sortDirection: 'desc',
                pageSize: this.paginator?.pageSize,
            })
            .subscribe((data) => {
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

