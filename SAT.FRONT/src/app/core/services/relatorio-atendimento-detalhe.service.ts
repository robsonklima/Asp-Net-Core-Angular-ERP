import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { RelatorioAtendimentoDetalhe } from '../types/relatorio-atendimento-detalhe.type';

@Injectable({
  providedIn: 'root'
})
export class RelatorioAtendimentoDetalheService {
  constructor(private http: HttpClient) {}

  criar(relatorioAtendimentoDetalhe: RelatorioAtendimentoDetalhe): Observable<RelatorioAtendimentoDetalhe> {
    return this.http.post<RelatorioAtendimentoDetalhe>(`${c.api}/RelatorioAtendimentoDetalhe`, relatorioAtendimentoDetalhe).pipe(
      map((obj) => obj)
    );
  }

  deletar(codRATDetalhe: number): Observable<void> {
    const url = `${c.api}/RelatorioAtendimentoDetalhe/${codRATDetalhe}`;

    return this.http.delete<void>(url).pipe(
      map((obj) => obj)
    );
  }

  atualizar(relatorioAtendimentoDetalhe: RelatorioAtendimentoDetalhe): Observable<RelatorioAtendimentoDetalhe> {
    const url = `${c.api}/RelatorioAtendimentoDetalhe`;

    return this.http.put<RelatorioAtendimentoDetalhe>(url, relatorioAtendimentoDetalhe).pipe(
      map((obj) => obj)
    );
  }
}
