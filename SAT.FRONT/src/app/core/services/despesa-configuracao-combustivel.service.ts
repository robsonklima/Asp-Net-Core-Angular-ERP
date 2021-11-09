import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DespesaConfiguracaoCombustivelData, DespesaConfiguracaoCombustivelParameters } from '../types/despesa-configuracao-combustivel.types';

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
}
