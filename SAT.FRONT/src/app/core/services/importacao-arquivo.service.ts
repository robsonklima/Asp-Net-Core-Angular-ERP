import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ImportacaoArquivo } from '../types/importacao-arquivo.types';

@Injectable({
  providedIn: 'root'
})
export class ImportacaoArquivoService {
  constructor(private http: HttpClient) { }

  criar(importacaoArquivo: ImportacaoArquivo): Observable<ImportacaoArquivo> {
    return this.http.post<ImportacaoArquivo>(`${c.api}/ImportacaoArquivo`, importacaoArquivo).pipe(
      map((obj) => obj)
    );
  }
}
