import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { RelatorioAtendimentoDetalhePeca } from '../types/relatorio-atendimento-detalhe-peca.type';

@Injectable({
  providedIn: 'root'
})
export class RelatorioAtendimentoDetalhePecaService {
  constructor(private http: HttpClient) {}

  criar(relatorioAtendimentoDetalhePeca: RelatorioAtendimentoDetalhePeca): Observable<RelatorioAtendimentoDetalhePeca> {
    return this.http.post<RelatorioAtendimentoDetalhePeca>(`${c.api}/RelatorioAtendimentoDetalhePeca`, relatorioAtendimentoDetalhePeca).pipe(
      map((obj) => obj)
    );
  }

  deletar(codRATDetalhe: number): Observable<void> {
    const url = `${c.api}/RelatorioAtendimentoDetalhePeca/${codRATDetalhe}`;

    return this.http.delete<void>(url).pipe(
      map((obj) => obj)
    );
  }

  atualizar(relatorioAtendimentoDetalhePeca: RelatorioAtendimentoDetalhePeca): Observable<RelatorioAtendimentoDetalhePeca> {
    const url = `${c.api}/RelatorioAtendimentoDetalhePeca`;

    return this.http.put<RelatorioAtendimentoDetalhePeca>(url, relatorioAtendimentoDetalhePeca).pipe(
      map((obj) => obj)
    );
  }
}
