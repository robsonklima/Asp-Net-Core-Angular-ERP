import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { MotivoAgendamento, MotivoAgendamentoData, MotivoAgendamentoParameters } from '../types/motivo-agendamento.types';

@Injectable({
  providedIn: 'root'
})
export class MotivoAgendamentoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: MotivoAgendamentoParameters): Observable<MotivoAgendamentoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/MotivoAgendamento`, { params: params }).pipe(
      map((data: MotivoAgendamentoData) => data)
    )
  }

  obterPorCodigo(codMotivoAgendamento: number): Observable<MotivoAgendamento> {
    const url = `${c.api}/MotivoAgendamento/${codMotivoAgendamento}`;

    return this.http.get<MotivoAgendamento>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(motivoAgendamento: MotivoAgendamento): Observable<MotivoAgendamento> {
    return this.http.post<MotivoAgendamento>(`${c.api}/MotivoAgendamento`, motivoAgendamento).pipe(
      map((obj) => obj)
    );
  }

  atualizar(motivoAgendamento: MotivoAgendamento): Observable<MotivoAgendamento> {
    const url = `${c.api}/MotivoAgendamento`;
    return this.http.put<MotivoAgendamento>(url, motivoAgendamento).pipe(
      map((obj) => obj)
    );
  }

  deletar(codMotivoAgendamento: number): Observable<MotivoAgendamento> {
    const url = `${c.api}/MotivoAgendamento/${codMotivoAgendamento}`;
    
    return this.http.delete<MotivoAgendamento>(url).pipe(
      map((obj) => obj)
    );
  }
}
