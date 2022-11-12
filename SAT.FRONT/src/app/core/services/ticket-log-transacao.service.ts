import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { appConfig as c } from 'app/core/config/app.config';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { TicketLogTransacaoData, TicketLogTransacaoParameters } from '../types/ticket-log-transacao.types';


@Injectable({
  providedIn: 'root'
})
export class TicketLogTransacaoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: TicketLogTransacaoParameters): Observable<TicketLogTransacaoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/TicketLogTransacao`, { params: params }).pipe(
      map((data: TicketLogTransacaoData) => data)
    )
  }
}
