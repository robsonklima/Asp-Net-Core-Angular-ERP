import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { appConfig as c } from 'app/core/config/app.config';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { TicketPrioridade, TicketPrioridadeData, TicketPrioridadeParameters } from '../types/ticket.types';

@Injectable({
  providedIn: 'root'
})
export class TicketPrioridadeService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: TicketPrioridadeParameters): Observable<TicketPrioridadeData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
        if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/TicketPrioridade`, { params: params }).pipe(
        map((data: TicketPrioridadeData) => data)
    )
}

  obterPorCodigo(codPrioridade: number): Observable<TicketPrioridade> {
    const url = `${c.api}/TicketPrioridade/${ codPrioridade }`;
    return this.http.get<TicketPrioridade>(url).pipe(
      map((obj) => obj)
    );
  }

  atualizar(prioridade: TicketPrioridade): Observable<TicketPrioridade> {
    const url = `${c.api}/TicketPrioridade`;
    return this.http.put<TicketPrioridade>(url, prioridade).pipe(
      map((obj) => obj)
    );
  }
}
