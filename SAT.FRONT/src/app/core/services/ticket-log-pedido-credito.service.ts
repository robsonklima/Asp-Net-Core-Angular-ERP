import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { appConfig as c } from 'app/core/config/app.config';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { TicketLogPedidoCreditoData, TicketLogPedidoCreditoParameters } from '../types/ticket-log-pedido-credito.types';
import { TicketLogPedidoCredito } from '../types/ticketlog-types';

@Injectable({
    providedIn: 'root'
})
export class TicketLogPedidoCreditoService
{
    constructor (private http: HttpClient) { }

    criar(pedidoCredito: TicketLogPedidoCredito): Observable<TicketLogPedidoCredito>
    {
        return this.http.post<TicketLogPedidoCredito>(
            `${c.api}/TicketLogPedidoCredito`, pedidoCredito)
            .pipe(map((obj) => obj));
    }

    obterPorParametros(parameters: TicketLogPedidoCreditoParameters): Observable<TicketLogPedidoCreditoData> {
		let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
          });

		return this.http.get(`${c.api}/TicketLogPedidoCredito`, { params: params }).pipe(
			map((data: TicketLogPedidoCreditoData) => data)
		)
	}

    deletar(cod: number): Observable<TicketLogPedidoCredito> {
        const url = `${c.api}/TicketLogPedidoCredito/${cod}`;

        return this.http.delete<TicketLogPedidoCredito>(url).pipe(
            map((obj) => obj)
        );
    }
}