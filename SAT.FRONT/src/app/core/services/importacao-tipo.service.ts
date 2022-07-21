import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ImportacaoTipo, ImportacaoTipoData, ImportacaoTipoParameters } from '../types/importacao-configuracao.type';

@Injectable({
  providedIn: 'root'
})
export class ImportacaoTipoService
{
  constructor (
    private http: HttpClient
  ) { }

  obterPorParametros(parameters: ImportacaoTipoParameters): Observable<ImportacaoTipoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/ImportacaoTipo`, { params: params }).pipe(
      map((data: ImportacaoTipoData) => data)
    )
  }

  obterPorCodigo(codImportacaoTipo: number): Observable<ImportacaoTipo> {
    const url = `${c.api}/ImportacaoTipo/${codImportacaoTipo}`;
    return this.http.get<ImportacaoTipo>(url).pipe(
      map((obj) => obj)
    );
}

  criar(importacaoTipo: ImportacaoTipo): Observable<ImportacaoTipo> {
    return this.http.post<ImportacaoTipo>(`${c.api}/ImportacaoTipo`, 
    importacaoTipo).pipe(
      map((obj) => obj)
    );
  }

  atualizar(importacaoTipo: ImportacaoTipo): Observable<ImportacaoTipo> {
    const url = `${c.api}/ImportacaoTipo`;
    return this.http.put<ImportacaoTipo>(url, importacaoTipo).pipe(
      map((obj) => obj)
    );
  }

  deletar(codImportacaoTipo: number): Observable<ImportacaoTipo> {
    const url = `${c.api}/ImportacaoTipo/${codImportacaoTipo}`;
    
    return this.http.delete<ImportacaoTipo>(url).pipe(
      map((obj) => obj)
    );
  }
}
