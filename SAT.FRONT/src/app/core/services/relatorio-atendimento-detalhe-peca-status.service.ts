import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { RelatorioAtendimentoDetalhePecaStatus, RelatorioAtendimentoDetalhePecaStatusData, RelatorioAtendimentoDetalhePecaStatusParameters } from '../types/relatorio-atendimento-detalhe-peca-status.types';



@Injectable({
  providedIn: 'root'
})
export class RelatorioAtendimentoDetalhePecaStatusService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: RelatorioAtendimentoDetalhePecaStatusParameters): Observable<RelatorioAtendimentoDetalhePecaStatusData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/RelatorioAtendimentoDetalhePecaStatus`, { params: params }).pipe(
      map((data: RelatorioAtendimentoDetalhePecaStatusData) => data)
    )
  }

  obterPorCodigo(codRATDetalhesPecasStatus: number): Observable<RelatorioAtendimentoDetalhePecaStatus> {
    const url = `${c.api}/RelatorioAtendimentoDetalhePecaStatus/${codRATDetalhesPecasStatus}`;
    return this.http.get<RelatorioAtendimentoDetalhePecaStatus>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(relatorioAtendimentoDetalhePecaStatus: RelatorioAtendimentoDetalhePecaStatus): Observable<RelatorioAtendimentoDetalhePecaStatus> {
    return this.http.post<RelatorioAtendimentoDetalhePecaStatus>(`${c.api}/RelatorioAtendimentoDetalhePecaStatus`, relatorioAtendimentoDetalhePecaStatus).pipe(
      map((obj) => obj)
    );
  }

  atualizar(relatorioAtendimentoDetalhePecaStatus: RelatorioAtendimentoDetalhePecaStatus): Observable<RelatorioAtendimentoDetalhePecaStatus> {
    const url = `${c.api}/RelatorioAtendimentoDetalhePecaStatus`;

    return this.http.put<RelatorioAtendimentoDetalhePecaStatus>(url, relatorioAtendimentoDetalhePecaStatus).pipe(
      map((obj) => obj)
    );
  }

  deletar(codRATDetalhesPecasStatus: number): Observable<RelatorioAtendimentoDetalhePecaStatus> {
    const url = `${c.api}/RelatorioAtendimentoDetalhePecaStatus/${codRATDetalhesPecasStatus}`;

    return this.http.delete<RelatorioAtendimentoDetalhePecaStatus>(url).pipe(
      map((obj) => obj)
    );
  }
}
