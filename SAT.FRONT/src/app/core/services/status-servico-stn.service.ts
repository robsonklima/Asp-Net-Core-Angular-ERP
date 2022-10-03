import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { StatusServicoSTN, StatusServicoSTNData, StatusServicoSTNParameters } from '../types/status-servico-stn.types';

@Injectable({
  providedIn: 'root'
})
export class StatusServicoSTNService {
  constructor(private http: HttpClient) {}

  criar(status: StatusServicoSTN): Observable<StatusServicoSTN> {
    return this.http.post<StatusServicoSTN>(`${c.api}/StatusServicoSTN`, status).pipe(
      map((obj) => obj)
    );
  }

  obterPorCodigo(codStatus: number): Observable<StatusServicoSTN> {
    const url = `${c.api}/StatusServicoSTN/${codStatus}`;
    return this.http.get<StatusServicoSTN>(url).pipe(
      map((obj) => obj)
    );
  }

  obterPorParametros(parameters: StatusServicoSTNParameters): Observable<StatusServicoSTNData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/StatusServicoSTN`, { params: params }).pipe(
      map((data: StatusServicoSTNData) => data)
    )
  }

  atualizar(status: StatusServicoSTN): Observable<StatusServicoSTN> {
    const url = `${c.api}/StatusServicoSTN`;
    return this.http.put<StatusServicoSTN>(url, status).pipe(
      map((obj) => obj)
    );
  }

  deletar(codStatusServicoSTN: number): Observable<StatusServicoSTN> {
    const url = `${c.api}/StatusServicoSTN/${codStatusServicoSTN}`;
    
    return this.http.delete<StatusServicoSTN>(url).pipe(
      map((obj) => obj)
    );
  }
}
