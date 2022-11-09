import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { TicketClassificacao, TicketClassificacaoData, TicketClassificacaoParameters } from '../types/ticket.types';

@Injectable({
  providedIn: 'root'
})
export class TicketClassificacaoService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: TicketClassificacaoParameters): Observable<TicketClassificacaoData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/TicketClassificacao`, { params: params }).pipe(
      map((data: TicketClassificacaoData) => data)
    )
  }

  obterPorCodigo(codClassificacao: number): Observable<TicketClassificacao> {
    const url = `${c.api}/TicketClassificacao/${codClassificacao}`;
    return this.http.get<TicketClassificacao>(url).pipe(
      map((obj) => obj)
    );
  }

  atualizar(ticketClassificacao: TicketClassificacao): Observable<TicketClassificacao> {
    const url = `${c.api}/TicketClassificacao`;
    return this.http.put<TicketClassificacao>(url, ticketClassificacao).pipe(
      map((obj) => obj)
    );
  }
}
