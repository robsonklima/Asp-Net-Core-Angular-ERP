import { ChangeDetectionStrategy, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { DocumentoSistemaService } from 'app/core/services/documentos-sistema.service';
import { MatDialog } from '@angular/material/dialog';
import { DocumentoSistemaFormDialogComponent } from './documento-sistema-form-dialog/documento-sistema-form-dialog.component';
import { DocumentoSistema } from 'app/core/types/documento-sistema.types';

@Component({
    selector: 'app-docs',
    templateUrl: './docs.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class DocsComponent implements OnInit, OnDestroy {
    documentos: DocumentoSistema[];
    categorias: string[] = ['MANUAL', 'SISTEMA'];
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _docSistemaService: DocumentoSistemaService,
        private _dialog: MatDialog
    ) { }

    async ngOnInit() {
        this.obterDocumentos();
    }

    onDocumentoSistema() {
        this._dialog.open(DocumentoSistemaFormDialogComponent);
    }

    async obterDocumentos(query: string = '') {
        this.documentos = (await this._docSistemaService.obterPorParametros({
            filter: query,
        }).toPromise()).items;
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}

