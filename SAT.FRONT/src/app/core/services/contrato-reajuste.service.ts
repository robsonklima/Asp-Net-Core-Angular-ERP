import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ContratoReajusteParameters, ContratoReajusteData, ContratoReajuste } from '../types/contrato-reajuste.types';

@Injectable({
  providedIn: 'root'
})
export class ContratoReajusteService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: ContratoReajusteParameters): Observable<ContratoReajusteData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/ContratoReajuste`, { params: params }).pipe(
      map((data: ContratoReajusteData) => data)
    )
  }

  obterPorCodigo(codContratoReajuste: number): Observable<ContratoReajuste> {
    const url = `${c.api}/ContratoReajuste/${codContratoReajuste}`;
    return this.http.get<ContratoReajuste>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(contrato: ContratoReajuste): Observable<ContratoReajuste> {
    return this.http.post<ContratoReajuste>(`${c.api}/ContratoReajuste`, contrato).pipe(
      map((obj) => obj)
    );
  }

  atualizar(contrato: ContratoReajuste): Observable<ContratoReajuste> {
    const url = `${c.api}/ContratoReajuste`;

    return this.http.put<ContratoReajuste>(url, contrato).pipe(
      map((obj) => obj)
    );
  }

  deletar(codContratoReajuste: number): Observable<ContratoReajuste> {
    const url = `${c.api}/ContratoReajuste/${codContratoReajuste}`;

    return this.http.delete<ContratoReajuste>(url).pipe(
      map((obj) => obj)
    );
  }
}
