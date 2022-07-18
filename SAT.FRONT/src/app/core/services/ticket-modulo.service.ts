import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Ticket, TicketData, TicketModulo, TicketModuloData, TicketModuloParameters, TicketParameters } from '../types/ticket.types';

@Injectable({
  providedIn: 'root'
})
export class TicketModuloService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: TicketModuloParameters): Observable<TicketModuloData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/TicketModulo`, { params: params }).pipe(
      map((data: TicketModuloData) => data)
    )
  }

  obterPorCodigo(codModulo: number): Observable<TicketModulo> {
    const url = `${c.api}/TicketModulo/${codModulo}`;
    return this.http.get<TicketModulo>(url).pipe(
      map((obj) => obj)
    );  
}

  criar(TicketModulo: TicketModulo): Observable<TicketModulo> {
    return this.http.post<TicketModulo>(`${c.api}/TicketModulo`, 
      TicketModulo).pipe(
      map((obj) => obj)
    );
  }

  atualizar(ticketModulo: TicketModulo): Observable<TicketModulo> {
    const url = `${c.api}/TicketModulo`;
    return this.http.put<TicketModulo>(url, ticketModulo).pipe(
      map((obj) => obj)
    );
  }

//   deletar(codEquipContrato: number): Observable<EquipamentoContrato> {
//     const url = `${c.api}/EquipamentoContrato/${codEquipContrato}`;
    
//     return this.http.delete<EquipamentoContrato>(url).pipe(
//       map((obj) => obj)
//     );
// }
}
