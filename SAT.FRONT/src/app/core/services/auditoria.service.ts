import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Auditoria, AuditoriaData, AuditoriaParameters } from '../types/auditoria.types';


@Injectable({
  providedIn: 'root'
})
export class AuditoriaService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: AuditoriaParameters): Observable<AuditoriaData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Auditoria`, { params: params }).pipe(
      map((data: AuditoriaData) => data)
    )
  }

  obterPorCodigo(codAuditoria: number): Observable<Auditoria> {
    const url = `${c.api}/Auditoria/${codAuditoria}`;
    return this.http.get<Auditoria>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(auditoria: Auditoria): Observable<Auditoria> {
    return this.http.post<Auditoria>(`${c.api}/Auditoria`, auditoria).pipe(
      map((obj) => obj)
    );
  }

  atualizar(auditoria: Auditoria): Observable<Auditoria> {
    const url = `${c.api}/Auditoria`;

    return this.http.put<Auditoria>(url, auditoria).pipe(
      map((obj) => obj)
    );
  }

  deletar(codAuditoria: number): Observable<Auditoria> {
    const url = `${c.api}/Auditoria/${codAuditoria}`;

    return this.http.delete<Auditoria>(url).pipe(
      map((obj) => obj)
    );
  }
}
