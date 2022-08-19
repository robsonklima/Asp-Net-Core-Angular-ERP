import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DespesaConfiguracaoCombustivel, DespesaConfiguracaoCombustivelData, DespesaConfiguracaoCombustivelParameters } from '../types/despesa-configuracao-combustivel.types';

@Injectable({
  providedIn: 'root'
})
export class DespesaConfiguracaoCombustivelService
{
  constructor (private http: HttpClient) { }

  obterPorParametros(parameters: DespesaConfiguracaoCombustivelParameters): Observable<DespesaConfiguracaoCombustivelData>
  {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key =>
    {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/DespesaConfiguracaoCombustivel`, { params: params }).pipe(
      map((data: DespesaConfiguracaoCombustivelData) => data)
    )
  }

  obterPorCodigo(codDespesaConfiguracaoCombustivel: number): Observable<DespesaConfiguracaoCombustivel> {
    const url = `${c.api}/DespesaConfiguracaoCombustivel/${codDespesaConfiguracaoCombustivel}`;
    return this.http.get<DespesaConfiguracaoCombustivel>(url).pipe(
        map((obj) => obj)
    );
  }

  criar(despesaConfiguracaoCombustivel: DespesaConfiguracaoCombustivel): Observable<DespesaConfiguracaoCombustivel> {
    return this.http.post<DespesaConfiguracaoCombustivel>(`${c.api}/DespesaConfiguracaoCombustivel`, despesaConfiguracaoCombustivel).pipe(
        map((obj) => obj)
    );
}

atualizar(despesaConfiguracaoCombustivel: DespesaConfiguracaoCombustivel): Observable<DespesaConfiguracaoCombustivel> {
    const url = `${c.api}/DespesaConfiguracaoCombustivel`;

    return this.http.put<DespesaConfiguracaoCombustivel>(url, despesaConfiguracaoCombustivel).pipe(
        map((obj) => obj)
    );
}

deletar(codDespesaConfiguracaoCombustivel: number): Observable<DespesaConfiguracaoCombustivel> {
    const url = `${c.api}/DespesaConfiguracaoCombustivel/${codDespesaConfiguracaoCombustivel}`;

    return this.http.delete<DespesaConfiguracaoCombustivel>(url).pipe(
        map((obj) => obj)
    );
}
}
