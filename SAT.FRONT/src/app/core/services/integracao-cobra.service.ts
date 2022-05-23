import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { IntegracaoCobra, IntegracaoCobraData, IntegracaoCobraParameters } from '../types/IntegracaoCobra.types';

@Injectable({
  providedIn: 'root'
})
export class IntegracaoCobraService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: IntegracaoCobraParameters): Observable<IntegracaoCobraData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/IntegracaoCobra`, { params: params }).pipe(
      map((data: IntegracaoCobraData) => data)
    )
  }

  obterPorCodigo(codIntegracaoCobra: number): Observable<IntegracaoCobra> {
    const url = `${c.api}/IntegracaoCobra/${codIntegracaoCobra}`;
    return this.http.get<IntegracaoCobra>(url).pipe(
      map((obj) => obj)
    );
}

  criar(instalLote: IntegracaoCobra): Observable<IntegracaoCobra> {
    return this.http.post<IntegracaoCobra>(`${c.api}/IntegracaoCobra`, instalLote).pipe(
      map((obj) => obj)
    );
  }

  atualizar(instalLote: IntegracaoCobra): Observable<IntegracaoCobra> {
    const url = `${c.api}/IntegracaoCobra`;

    return this.http.put<IntegracaoCobra>(url, instalLote).pipe(
      map((obj) => obj)
    );
  }

  deletar(codIntegracaoCobra: number): Observable<IntegracaoCobra> {
    const url = `${c.api}/IntegracaoCobra/${codIntegracaoCobra}`;

    return this.http.delete<IntegracaoCobra>(url).pipe(
      map((obj) => obj)
    );
  }
}
