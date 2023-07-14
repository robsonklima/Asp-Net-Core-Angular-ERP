import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';
import packageInfo from '../../../../../package.json';
import { DocumentoSistemaService } from 'app/core/services/documentos-sistema.service';

@Component({
    selector: 'version',
    templateUrl: './version.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    exportAs: 'version'
})
export class VersionComponent implements OnInit {
    versao: string = packageInfo.version;

    constructor(
        private _docSistemaService: DocumentoSistemaService
    ) { }

    async ngOnInit() {
        const data = await this._docSistemaService.obterPorParametros({}).toPromise();
    }
}
