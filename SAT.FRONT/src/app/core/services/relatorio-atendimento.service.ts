import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { RelatorioAtendimento, RelatorioAtendimentoData, RelatorioAtendimentoParameters } from '../types/relatorio-atendimento.types';

@Injectable({
  providedIn: 'root'
})
export class RelatorioAtendimentoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: RelatorioAtendimentoParameters): Observable<RelatorioAtendimentoData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/RelatorioAtendimento`, { params: params }).pipe(
      map((data: RelatorioAtendimentoData) => data)
    )
  }

  obterPorCodigo(codRAT: number): Observable<RelatorioAtendimento> {
    const url = `${c.api}/RelatorioAtendimento/${codRAT}`;
    return this.http.get<RelatorioAtendimento>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(relatorioAtendimento: RelatorioAtendimento): Observable<RelatorioAtendimento> {
    return this.http.post<RelatorioAtendimento>(`${c.api}/RelatorioAtendimento`, relatorioAtendimento).pipe(
      map((obj) => obj)
    );
  }

  atualizar(relatorioAtendimento: RelatorioAtendimento): Observable<RelatorioAtendimento> {
    const url = `${c.api}/RelatorioAtendimento`;

    return this.http.put<RelatorioAtendimento>(url, relatorioAtendimento).pipe(
      map((obj) => obj)
    );
  }

  deletar(codRAT: number): Observable<RelatorioAtendimento> {
    const url = `${c.api}/RelatorioAtendimento/${codRAT}`;

    return this.http.delete<RelatorioAtendimento>(url).pipe(
      map((obj) => obj)
    );
  }
}
