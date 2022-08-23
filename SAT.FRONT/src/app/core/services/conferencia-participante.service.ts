import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { ConferenciaParticipante, ConferenciaParticipanteData, ConferenciaParticipanteParameters } from '../types/conferencia-participante.types';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class ConferenciaParticipanteService {
  constructor(private http: HttpClient) { }

  obterTodos(): Observable<ConferenciaParticipante[]> {
    return this.http.get<ConferenciaParticipante[]>(`${c.api}/ConferenciaParticipante`).pipe(
      map((obj) => obj)
    );
  }

  obterPorParametros(parameters: ConferenciaParticipanteParameters): Observable<ConferenciaParticipanteData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));

    });
    
    return this.http.get(`${c.api}/ConferenciaParticipante`, { params: params }).pipe(
      map((data: ConferenciaParticipanteData) => data)
    )
  }

  obterPorCodigo(cod: number): Observable<ConferenciaParticipante> {
    const url = `${c.api}/ConferenciaParticipante/${cod}`;
    return this.http.get<ConferenciaParticipante>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(participante: ConferenciaParticipante): Observable<ConferenciaParticipante> {
    return this.http.post<ConferenciaParticipante>(`${c.api}/ConferenciaParticipante`, participante).pipe(
      map((obj) => obj)
    );
  }

  deletar(cod: number): Observable<ConferenciaParticipante> {
    const url = `${c.api}/ConferenciaParticipante/${cod}`;

    return this.http.delete<ConferenciaParticipante>(url).pipe(
      map((obj) => obj)
    );
  }
}
