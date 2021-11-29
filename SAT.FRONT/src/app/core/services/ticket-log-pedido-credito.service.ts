import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
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
}