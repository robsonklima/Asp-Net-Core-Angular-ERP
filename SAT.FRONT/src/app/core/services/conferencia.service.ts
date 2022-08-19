import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Conferencia, ConferenciaData, ConferenciaParameters } from '../types/conferencia.types';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class ConferenciaService {
  constructor(private http: HttpClient) { }

  obterTodos(): Observable<Conferencia[]> {
    return this.http.get<Conferencia[]>(`${c.api}/Conferencia`).pipe(
      map((obj) => obj)
    );
  }

  obterPorParametros(parameters: ConferenciaParameters): Observable<ConferenciaData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));

    });
    
    return this.http.get(`${c.api}/Conferencia`, { params: params }).pipe(
      map((data: ConferenciaData) => data)
    )
  }

  obterPorCodigo(codConferencia: number): Observable<Conferencia> {
    const url = `${c.api}/Conferencia/${codConferencia}`;
    return this.http.get<Conferencia>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(conferencia: Conferencia): Observable<Conferencia> {
    return this.http.post<Conferencia>(`${c.api}/Conferencia`, conferencia).pipe(
      map((obj) => obj)
    );
  }

  atualizar(conferencia: Conferencia): Observable<Conferencia> {
    const url = `${c.api}/Conferencia`;

    return this.http.put<Conferencia>(url, conferencia).pipe(
      map((obj) => obj)
    );
  }

  deletar(codConferencia: number): Observable<Conferencia> {
    const url = `${c.api}/Conferencia/${codConferencia}`;

    return this.http.delete<Conferencia>(url).pipe(
      map((obj) => obj)
    );
  }
}
