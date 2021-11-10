import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DespesaCartaoCombustivel, DespesaCartaoCombustivelTecnicoParameters, DespesaCartaoCombustivelData } from '../types/despesa-cartao-combustivel.types';

@Injectable({
  providedIn: 'root'
})
export class DespesaCartaoCombustivelService
{
  constructor (private http: HttpClient) { }

  obterPorParametros(parameters: DespesaCartaoCombustivelTecnicoParameters): Observable<DespesaCartaoCombustivelData>
  {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key =>
    {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/DespesaCartaoCombustivel`, { params: params }).pipe(
      map((data: DespesaCartaoCombustivelData) => data)
    )
  }

  obterPorCodigo(codDespesaCartaoCombustivel: number): Observable<DespesaCartaoCombustivel>
  {
    return this.http.get<DespesaCartaoCombustivel>(
      `${c.api}/DespesaCartaoCombustivel/${codDespesaCartaoCombustivel}`)
      .pipe(map((obj) => obj));
  }
}
