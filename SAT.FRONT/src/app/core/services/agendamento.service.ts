import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { EMPTY, Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Agendamento, AgendamentoData, AgendamentoParameters } from '../types/Agendamento.types';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class AgendamentoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: AgendamentoParameters): Observable<AgendamentoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Agendamento`, { params: params }).pipe(
      map((data: AgendamentoData) => data)
    )
  }

  obterPorCodigo(codAgendamento: number): Observable<Agendamento> {
    const url = `${c.api}/Agendamento/${codAgendamento}`;

    return this.http.get<Agendamento>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(agendamento: Agendamento): Observable<Agendamento> {
    return this.http.post<Agendamento>(`${c.api}/Agendamento`, agendamento).pipe(
      map((obj) => obj)
    );
  }

  atualizar(agendamento: Agendamento): Observable<Agendamento> {
    const url = `${c.api}/Agendamento`;
    return this.http.put<Agendamento>(url, agendamento).pipe(
      map((obj) => obj)
    );
  }

  deletar(codAgendamento: number): Observable<Agendamento> {
    const url = `${c.api}/Agendamento/${codAgendamento}`;
    
    return this.http.delete<Agendamento>(url).pipe(
      map((obj) => obj)
    );
  }
}
