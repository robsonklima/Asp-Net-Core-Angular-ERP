import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Ticket, TicketAtendimento, TicketAtendimentoData, TicketAtendimentoParameters, TicketData, TicketParameters } from '../types/ticket.types';

@Injectable({
  providedIn: 'root'
})
export class TicketAtendimentoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: TicketAtendimentoParameters): Observable<TicketAtendimentoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/TicketAtendimento`, { params: params }).pipe(
      map((data: TicketAtendimentoData) => data)
    )
  }

  obterPorCodigo(codTicketAtendimento: number): Observable<TicketAtendimento> {

    const url = `${c.api}/TicketAtendimento/${codTicketAtendimento}`;
    return this.http.get<TicketAtendimento>(url).pipe(
      map((obj) => obj)
      
    );
}

  criar(atendimento: TicketAtendimento): Observable<TicketAtendimento> {
    return this.http.post<TicketAtendimento>(`${c.api}/TicketAtendimento`, atendimento).pipe(
      map((obj) => obj)
    );
  }

  atualizar(atendimento: TicketAtendimento): Observable<TicketAtendimento> {
    const url = `${c.api}/TicketAtendimento`;
    return this.http.put<TicketAtendimento>(url, atendimento).pipe(
      map((obj) => obj)
    );
  }

  deletar(cod: number): Observable<TicketAtendimento> {
    const url = `${c.api}/TicketAtendimento/${cod}`;
    
    return this.http.delete<TicketAtendimento>(url).pipe(
      map((obj) => obj)
    );
}
}
