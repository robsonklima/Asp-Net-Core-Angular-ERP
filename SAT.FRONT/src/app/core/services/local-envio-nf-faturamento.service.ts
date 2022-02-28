import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { LocalEnvioNFFaturamento, LocalEnvioNFFaturamentoData, LocalEnvioNFFaturamentoParameters } from '../types/local-envio-nf-faturamento.types';

@Injectable({
  providedIn: 'root'
})
export class LocalEnvioNFFaturamentoService
{
  constructor (
    private http: HttpClient
  ) { }


  criar(localEnvioNFFaturamento: LocalEnvioNFFaturamento): Observable<LocalEnvioNFFaturamento> {
    return this.http.post<LocalEnvioNFFaturamento>(`${c.api}/LocalEnvioNFFaturamento`, localEnvioNFFaturamento).pipe(
      map((obj) => obj)
    );
  }

  obterPorCodigo(codPosto: number): Observable<LocalEnvioNFFaturamento> {
    const url = `${c.api}/LocalEnvioNFFaturamento/${codPosto}`;
    return this.http.get<LocalEnvioNFFaturamento>(url).pipe(
      map((obj) => obj)
    );
  }

  obterPorParametros(parameters: LocalEnvioNFFaturamentoParameters): Observable<LocalEnvioNFFaturamentoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/LocalEnvioNFFaturamento`, { params: params }).pipe(
      map((data: LocalEnvioNFFaturamentoData) => data)
    )
  }

  atualizar(localEnvioNFFaturamento: LocalEnvioNFFaturamento): Observable<LocalEnvioNFFaturamento> {
    const url = `${c.api}/LocalEnvioNFFaturamento`;
    return this.http.put<LocalEnvioNFFaturamento>(url, localEnvioNFFaturamento).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPosto: number): Observable<LocalEnvioNFFaturamento> {
    const url = `${c.api}/LocalEnvioNFFaturamento/${codPosto}`;
    
    return this.http.delete<LocalEnvioNFFaturamento>(url).pipe(
      map((obj) => obj)
    );
  }
}
