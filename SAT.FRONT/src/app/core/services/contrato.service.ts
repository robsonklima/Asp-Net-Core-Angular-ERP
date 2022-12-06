import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Contrato, ContratoData, ContratoParameters } from '../types/contrato.types';

@Injectable({
  providedIn: 'root'
})
export class ContratoService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: ContratoParameters): Observable<ContratoData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Contrato`, { params: params }).pipe(
      map((data: ContratoData) => data)
    )
  }

  obterPorCodigo(codContrato: number): Observable<Contrato> {
    const url = `${c.api}/Contrato/${codContrato}`;
    return this.http.get<Contrato>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(contrato: Contrato): Observable<Contrato> {
    return this.http.post<Contrato>(`${c.api}/Contrato`, contrato).pipe(
      map((obj) => obj)
    );
  }

  atualizar(contrato: Contrato): Observable<Contrato> {
    const url = `${c.api}/Contrato`;

    return this.http.put<Contrato>(url, contrato).pipe(
      map((obj) => obj)
    );
  }

  deletar(codContrato: number): Observable<Contrato> {
    const url = `${c.api}/Contrato/${codContrato}`;

    return this.http.delete<Contrato>(url).pipe(
      map((obj) => obj)
    );
  }
}
