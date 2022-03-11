import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ImportacaoAberturaOrdemServico } from '../types/importacao.types';
import { ImportacaoConfiguracao, ImportacaoConfiguracaoData, ImportacaoConfiguracaoParameters } from '../types/importacao-configuracao.type';

@Injectable({
  providedIn: 'root'
})
export class ImportacaoConfiguracaoService
{
  constructor (
    private http: HttpClient
  ) { }

  obterPorParametros(parameters: ImportacaoConfiguracaoParameters): Observable<ImportacaoConfiguracaoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/ImportacaoConfiguracao`, { params: params }).pipe(
      map((data: ImportacaoConfiguracaoData) => data)
    )
  }

  obterPorCodigo(codImportacaoConf: number): Observable<ImportacaoConfiguracao> {
    const url = `${c.api}/ImportacaoConfiguracao/${codImportacaoConf}`;
    return this.http.get<ImportacaoConfiguracao>(url).pipe(
      map((obj) => obj)
    );
}

  criar(importacaoConfigucarao: ImportacaoConfiguracao): Observable<ImportacaoConfiguracao> {
    return this.http.post<ImportacaoConfiguracao>(`${c.api}/ImportacaoConfiguracao`, 
    importacaoConfigucarao).pipe(
      map((obj) => obj)
    );
  }

  atualizar(importacaoConfigucarao: ImportacaoConfiguracao): Observable<ImportacaoConfiguracao> {
    const url = `${c.api}/ImportacaoConfiguracao`;
    return this.http.put<ImportacaoConfiguracao>(url, importacaoConfigucarao).pipe(
      map((obj) => obj)
    );
  }

  deletar(codImportacaoConf: number): Observable<ImportacaoConfiguracao> {
    const url = `${c.api}/ImportacaoConfiguracao/${codImportacaoConf}`;
    
    return this.http.delete<ImportacaoConfiguracao>(url).pipe(
      map((obj) => obj)
    );
  }
  
}
