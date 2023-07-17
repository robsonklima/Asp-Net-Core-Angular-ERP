import { Component, OnInit } from '@angular/core';
import { DocumentoSistemaService } from 'app/core/services/documentos-sistema.service';
import { DocumentoSistema } from 'app/core/types/documento-sistema.types';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-docs-detalhe',
  templateUrl: './docs-detalhe.component.html'
})
export class DocsDetalheComponent implements OnInit {
  codDocumentoSistema: number;
  documento: DocumentoSistema;

  constructor(
    private _route: ActivatedRoute,
    private _docSistemaService: DocumentoSistemaService
  ) { }

  ngOnInit(): void {
    this.codDocumentoSistema = +this._route.snapshot.paramMap.get('codDocumentoSistema');

    if (this.codDocumentoSistema) {
      this._docSistemaService
        .obterPorCodigo(this.codDocumentoSistema)
        .subscribe(data => {
          this.documento = data;
        });
    }
  }
}
