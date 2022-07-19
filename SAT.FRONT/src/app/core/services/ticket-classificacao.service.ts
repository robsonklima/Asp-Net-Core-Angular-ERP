import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { TicketClassificacao, TicketClassificacaoData, TicketClassificacaoParameters} from '../types/ticket.types';

@Injectable({
  providedIn: 'root'
})
export class TicketClassificacaoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: TicketClassificacaoParameters): Observable<TicketClassificacaoData> {
    let params = new HttpParams();

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

//   criar(equipamentoContrato: EquipamentoContrato): Observable<EquipamentoContrato> {
//     return this.http.post<EquipamentoContrato>(`${c.api}/EquipamentoContrato`, 
//       equipamentoContrato).pipe(
//       map((obj) => obj)
//     );
//   }

  atualizar(ticketClassificacao: TicketClassificacao): Observable<TicketClassificacao> {
    const url = `${c.api}/TicketClassificacao`;
    return this.http.put<TicketClassificacao>(url, ticketClassificacao).pipe(
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
