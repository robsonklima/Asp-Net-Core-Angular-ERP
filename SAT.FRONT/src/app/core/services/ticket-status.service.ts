import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { appConfig as c } from 'app/core/config/app.config';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { TicketStatus, TicketStatusData, TicketStatusParameters } from '../types/ticket.types';

@Injectable({
  providedIn: 'root'
})
export class TicketStatusService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: TicketStatusParameters): Observable<TicketStatusData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/TicketStatus`, { params: params }).pipe(
      map((data: TicketStatusData) => data)
    )
  }

  obterPorCodigo(codStatus: number): Observable<TicketStatus> {
    const url = `${c.api}/TicketStatus/${codStatus}`;
    return this.http.get<TicketStatus>(url).pipe(
      map((obj) => obj)
    );  
}

  criar(ticketStatus: TicketStatus): Observable<TicketStatus> {
    return this.http.post<TicketStatus>(`${c.api}/TicketStatus`, 
      ticketStatus).pipe(
      map((obj) => obj)
    );
  }

  atualizar(ticketStatus: TicketStatus): Observable<TicketStatus> {
    const url = `${c.api}/TicketStatus`;
    return this.http.put<TicketStatus>(url, ticketStatus).pipe(
      map((obj) => obj)
    );
  }
}
