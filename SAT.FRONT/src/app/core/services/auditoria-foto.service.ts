import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { AuditoriaFoto, AuditoriaFotoData, AuditoriaFotoParameters } from '../types/auditoria-foto.types';


@Injectable({
  providedIn: 'root'
})
export class AuditoriaFotoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: AuditoriaFotoParameters): Observable<AuditoriaFotoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/AuditoriaFoto`, { params: params }).pipe(
      map((data: AuditoriaFotoData) => data)
    )
  }

  obterPorCodigo(codAuditoriaFoto: number): Observable<AuditoriaFoto> {
    const url = `${c.api}/AuditoriaFoto/${codAuditoriaFoto}`;
    return this.http.get<AuditoriaFoto>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(auditoriaFoto: AuditoriaFoto): Observable<AuditoriaFoto> {
    return this.http.post<AuditoriaFoto>(`${c.api}/AuditoriaFoto`, auditoriaFoto).pipe(
      map((obj) => obj)
    );
  }

  atualizar(auditoriaFoto: AuditoriaFoto): Observable<AuditoriaFoto> {
    const url = `${c.api}/AuditoriaFoto`;

    return this.http.put<AuditoriaFoto>(url, auditoriaFoto).pipe(
      map((obj) => obj)
    );
  }

  deletar(codAuditoriaFoto: number): Observable<AuditoriaFoto> {
    const url = `${c.api}/AuditoriaFoto/${codAuditoriaFoto}`;

    return this.http.delete<AuditoriaFoto>(url).pipe(
      map((obj) => obj)
    );
  }
}
