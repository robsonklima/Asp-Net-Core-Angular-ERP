import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { PontoPeriodoStatus, PontoPeriodoStatusData, PontoPeriodoStatusParameters } from '../types/ponto-periodo-status.types';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class PontoPeriodoStatusService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: PontoPeriodoStatusParameters): Observable<PontoPeriodoStatusData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/PontoPeriodoStatus`, { params: params }).pipe(
      map((data: PontoPeriodoStatusData) => data)
    )
  }

  obterPorCodigo(codPontoPeriodoStatus: number): Observable<PontoPeriodoStatus> {
    const url = `${c.api}/PontoPeriodoStatus/${codPontoPeriodoStatus}`;

    return this.http.get<PontoPeriodoStatus>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(pontoPeriodoStatus: PontoPeriodoStatus): Observable<PontoPeriodoStatus> {
    return this.http.post<PontoPeriodoStatus>(`${c.api}/PontoPeriodoStatus`, pontoPeriodoStatus).pipe(
      map((obj) => obj)
    );
  }

  atualizar(pontoPeriodoStatus: PontoPeriodoStatus): Observable<PontoPeriodoStatus> {
    const url = `${c.api}/PontoPeriodoStatus`;
    return this.http.put<PontoPeriodoStatus>(url, pontoPeriodoStatus).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPontoPeriodoStatus: number): Observable<PontoPeriodoStatus> {
    const url = `${c.api}/PontoPeriodoStatus/${codPontoPeriodoStatus}`;
    
    return this.http.delete<PontoPeriodoStatus>(url).pipe(
      map((obj) => obj)
    );
  }
}
