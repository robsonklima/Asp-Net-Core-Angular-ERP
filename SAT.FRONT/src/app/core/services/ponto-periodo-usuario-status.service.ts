import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { PontoPeriodoUsuarioStatus, PontoPeriodoUsuarioStatusData, PontoPeriodoUsuarioStatusParameters } from '../types/ponto-periodo-usuario-status.types';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class PontoPeriodoUsuarioStatusService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: PontoPeriodoUsuarioStatusParameters): Observable<PontoPeriodoUsuarioStatusData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/PontoPeriodoUsuarioStatus`, { params: params }).pipe(
      map((data: PontoPeriodoUsuarioStatusData) => data)
    )
  }

  obterPorCodigo(codPontoPeriodoUsuarioStatus: number): Observable<PontoPeriodoUsuarioStatus> {
    const url = `${c.api}/PontoPeriodoUsuarioStatus/${codPontoPeriodoUsuarioStatus}`;

    return this.http.get<PontoPeriodoUsuarioStatus>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(pontoPeriodoUsuarioStatus: PontoPeriodoUsuarioStatus): Observable<PontoPeriodoUsuarioStatus> {
    return this.http.post<PontoPeriodoUsuarioStatus>(`${c.api}/PontoPeriodoUsuarioStatus`, pontoPeriodoUsuarioStatus).pipe(
      map((obj) => obj)
    );
  }

  atualizar(pontoPeriodoUsuarioStatus: PontoPeriodoUsuarioStatus): Observable<PontoPeriodoUsuarioStatus> {
    const url = `${c.api}/PontoPeriodoUsuarioStatus`;
    return this.http.put<PontoPeriodoUsuarioStatus>(url, pontoPeriodoUsuarioStatus).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPontoPeriodoUsuarioStatus: number): Observable<PontoPeriodoUsuarioStatus> {
    const url = `${c.api}/PontoPeriodoUsuarioStatus/${codPontoPeriodoUsuarioStatus}`;
    
    return this.http.delete<PontoPeriodoUsuarioStatus>(url).pipe(
      map((obj) => obj)
    );
  }
}
