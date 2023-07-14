import { ChangeDetectionStrategy, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { DocumentoSistemaService } from 'app/core/services/documentos-sistema.service';
import { MatDialog } from '@angular/material/dialog';
import { DocumentoSistemaFormDialogComponent } from './documento-sistema-form-dialog/documento-sistema-form-dialog.component';
import { DocumentoSistema, documentoCategoriasConst } from 'app/core/types/documento-sistema.types';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { toastTypesConst } from 'app/core/types/generic.types';

@Component({
    selector: 'app-docs',
    templateUrl: './docs.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class DocsComponent implements OnInit, OnDestroy {
    documentos: DocumentoSistema[];
    categorias: string[] = [];
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    loading: boolean = true;

    constructor(
        private _docSistemaService: DocumentoSistemaService,
        private _dialog: MatDialog,
        private _snack: CustomSnackbarService
    ) { }

    async ngOnInit() {
        this.obterDocumentos();

        this.categorias = documentoCategoriasConst;
    }

    onDocumentoSistema() {
        this._dialog.open(DocumentoSistemaFormDialogComponent);
    }

    async obterDocumentos(query: string = '') {
        await this._docSistemaService
            .obterPorParametros({ filter: query, })
            .subscribe((data) => {
                this.documentos = data?.items;

                this.loading = false;
            }, e => {
                this._snack.exibirToast(e, toastTypesConst.ERROR)

                this.loading = false;
            });

        console.log(this.documentos);

    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}

