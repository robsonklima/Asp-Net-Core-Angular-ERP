import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { RelatorioAtendimentoPecaStatusParameters, RelatorioAtendimentoPecaStatusData, RelatorioAtendimentoPecaStatus } from '../types/relatorio-atendimento-peca-status.types';



@Injectable({
  providedIn: 'root'
})
export class RelatorioAtendimentoPecaStatusService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: RelatorioAtendimentoPecaStatusParameters): Observable<RelatorioAtendimentoPecaStatusData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/RelatorioAtendimentoPecaStatus`, { params: params }).pipe(
      map((data: RelatorioAtendimentoPecaStatusData) => data)
    )
  }

  obterPorCodigo(codRatpecasStatus: number): Observable<RelatorioAtendimentoPecaStatus> {
    const url = `${c.api}/RelatorioAtendimentoPecaStatus/${codRatpecasStatus}`;
    return this.http.get<RelatorioAtendimentoPecaStatus>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(relatorioAtendimentoPecaStatus: RelatorioAtendimentoPecaStatus): Observable<RelatorioAtendimentoPecaStatus> {
    return this.http.post<RelatorioAtendimentoPecaStatus>(`${c.api}/RelatorioAtendimentoPecaStatus`, relatorioAtendimentoPecaStatus).pipe(
      map((obj) => obj)
    );
  }

  atualizar(relatorioAtendimentoPecaStatus: RelatorioAtendimentoPecaStatus): Observable<RelatorioAtendimentoPecaStatus> {
    const url = `${c.api}/RelatorioAtendimentoPecaStatus`;

    return this.http.put<RelatorioAtendimentoPecaStatus>(url, relatorioAtendimentoPecaStatus).pipe(
      map((obj) => obj)
    );
  }

  deletar(codRatpecasStatus: number): Observable<RelatorioAtendimentoPecaStatus> {
    const url = `${c.api}/RelatorioAtendimentoPecaStatus/${codRatpecasStatus}`;

    return this.http.delete<RelatorioAtendimentoPecaStatus>(url).pipe(
      map((obj) => obj)
    );
  }
}
