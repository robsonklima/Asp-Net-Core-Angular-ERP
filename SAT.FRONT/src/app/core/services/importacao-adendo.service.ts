import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ImportacaoAdendoParameters, ImportacaoAdendoData, ImportacaoAdendo } from '../types/importacao-adendo.types';



@Injectable({
  providedIn: 'root'
})
export class ImportacaoAdendoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: ImportacaoAdendoParameters): Observable<ImportacaoAdendoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/ImportacaoAdendo`, { params: params }).pipe(
      map((data: ImportacaoAdendoData) => data)
    )
  }

  obterPorCodigo(codAdendo: number): Observable<ImportacaoAdendo> {
    const url = `${c.api}/ImportacaoAdendo/${codAdendo}`;
    return this.http.get<ImportacaoAdendo>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(adendo: ImportacaoAdendo): Observable<ImportacaoAdendo> {
    return this.http.post<ImportacaoAdendo>(`${c.api}/ImportacaoAdendo`, adendo).pipe(
      map((obj) => obj)
    );
  }

  atualizar(adendo: ImportacaoAdendo): Observable<ImportacaoAdendo> {
    const url = `${c.api}/ImportacaoAdendo`;

    return this.http.put<ImportacaoAdendo>(url, adendo).pipe(
      map((obj) => obj)
    );
  }

  deletar(codAdendo: number): Observable<ImportacaoAdendo> {
    const url = `${c.api}/ImportacaoAdendo/${codAdendo}`;

    return this.http.delete<ImportacaoAdendo>(url).pipe(
      map((obj) => obj)
    );
  }
}
