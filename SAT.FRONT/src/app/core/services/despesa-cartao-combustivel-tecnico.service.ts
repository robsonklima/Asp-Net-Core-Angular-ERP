import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DespesaCartaoCombustivelTecnicoData, DespesaCartaoCombustivelTecnicoParameters } from '../types/despesa-cartao-combustivel.types';

@Injectable({
  providedIn: 'root'
})
export class DespesaCartaoCombustivelTecnicoService
{
  constructor (private http: HttpClient) { }

  obterPorParametros(parameters: DespesaCartaoCombustivelTecnicoParameters): Observable<DespesaCartaoCombustivelTecnicoData>
  {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key =>
    {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/DespesaCartaoCombustivelTecnico`, { params: params }).pipe(
      map((data: DespesaCartaoCombustivelTecnicoData) => data)
    )
  }
}
