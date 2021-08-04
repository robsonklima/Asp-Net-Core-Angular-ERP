import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { StatusServico, StatusServicoData, StatusServicoParameters } from '../types/status-servico.types';

@Injectable({
  providedIn: 'root'
})
export class StatusServicoService {
  constructor(private http: HttpClient) {}

  criar(statusServico: StatusServico): Observable<StatusServico> {
    return this.http.post<StatusServico>(`${c.api}/StatusServico`, statusServico).pipe(
      map((obj) => obj)
    );
  }

  obterPorCodigo(codStatusServico: number): Observable<StatusServico> {
    const url = `${c.api}/StatusServico/${codStatusServico}`;
    return this.http.get<StatusServico>(url).pipe(
      map((obj) => obj)
    );
  }

  obterPorParametros(parameters: StatusServicoParameters): Observable<StatusServicoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/StatusServico`, { params: params }).pipe(
      map((data: StatusServicoData) => data)
    )
  }

  atualizar(statusServico: StatusServico): Observable<StatusServico> {
    const url = `${c.api}/StatusServico`;
    return this.http.put<StatusServico>(url, statusServico).pipe(
      map((obj) => obj)
    );
  }

  deletar(codStatusServico: number): Observable<StatusServico> {
    const url = `${c.api}/StatusServico/${codStatusServico}`;
    
    return this.http.delete<StatusServico>(url).pipe(
      map((obj) => obj)
    );
  }
}
