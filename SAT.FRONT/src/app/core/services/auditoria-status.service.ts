import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { AuditoriaStatus, AuditoriaStatusData, AuditoriaStatusParameters } from '../types/auditoria-status.types';


@Injectable({
  providedIn: 'root'
})
export class AuditoriaStatusService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: AuditoriaStatusParameters): Observable<AuditoriaStatusData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/AuditoriaStatus`, { params: params }).pipe(
      map((data: AuditoriaStatusData) => data)
    )
  }

  obterPorCodigo(codAuditoriaStatus: number): Observable<AuditoriaStatus> {
    const url = `${c.api}/AuditoriaStatus/${codAuditoriaStatus}`;
    return this.http.get<AuditoriaStatus>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(auditoriaStatus: AuditoriaStatus): Observable<AuditoriaStatus> {
    return this.http.post<AuditoriaStatus>(`${c.api}/AuditoriaStatus`, auditoriaStatus).pipe(
      map((obj) => obj)
    );
  }

  atualizar(auditoriaStatus: AuditoriaStatus): Observable<AuditoriaStatus> {
    const url = `${c.api}/AuditoriaStatus`;

    return this.http.put<AuditoriaStatus>(url, auditoriaStatus).pipe(
      map((obj) => obj)
    );
  }

  deletar(codAuditoriaStatus: number): Observable<AuditoriaStatus> {
    const url = `${c.api}/AuditoriaStatus/${codAuditoriaStatus}`;

    return this.http.delete<AuditoriaStatus>(url).pipe(
      map((obj) => obj)
    );
  }
}
