import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { appConfig as c } from 'app/core/config/app.config';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { TicketLogPedidoCreditoData, TicketLogPedidoCreditoParameters } from '../types/ticket-log-pedido-credito.types';
import { TicketPrioridade } from '../types/ticket.types';

@Injectable({
  providedIn: 'root'
})
export class TicketPrioridadeService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: TicketLogPedidoCreditoParameters): Observable<TicketLogPedidoCreditoData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
        if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/TipoContrato`, { params: params }).pipe(
        map((data: TicketLogPedidoCreditoData) => data)
    )
}

  obterPorCodigo(codPrioridade: number): Observable<TicketPrioridade> {
    const url = `${c.api}/TicketPrioridade/${codPrioridade}`;
    return this.http.get<TicketPrioridade>(url).pipe(
      map((obj) => obj)
    );
  }

  atualizar(ticketPrioridade: TicketPrioridade): Observable<TicketPrioridade> {
    const url = `${c.api}/TicketPrioridade`;
    return this.http.put<TicketPrioridade>(url, ticketPrioridade).pipe(
      map((obj) => obj)
    );
  }
}
