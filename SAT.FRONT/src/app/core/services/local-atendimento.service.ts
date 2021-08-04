import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { LocalAtendimento, LocalAtendimentoData, LocalAtendimentoParameters } from '../types/local-atendimento.types';

@Injectable({
  providedIn: 'root'
})
export class LocalAtendimentoService {
  constructor(private http: HttpClient) {}

  criar(localAtendimento: LocalAtendimento): Observable<LocalAtendimento> {
    return this.http.post<LocalAtendimento>(`${c.api}/LocalAtendimento`, localAtendimento).pipe(
      map((obj) => obj)
    );
  }

  obterPorCodigo(codPosto: number): Observable<LocalAtendimento> {
    const url = `${c.api}/LocalAtendimento/${codPosto}`;
    return this.http.get<LocalAtendimento>(url).pipe(
      map((obj) => obj)
    );
  }

  obterPorParametros(parameters: LocalAtendimentoParameters): Observable<LocalAtendimentoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/LocalAtendimento`, { params: params }).pipe(
      map((data: LocalAtendimentoData) => data)
    )
  }

  atualizar(localAtendimento: LocalAtendimento): Observable<LocalAtendimento> {
    const url = `${c.api}/LocalAtendimento`;
    return this.http.put<LocalAtendimento>(url, localAtendimento).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPosto: number): Observable<LocalAtendimento> {
    const url = `${c.api}/LocalAtendimento/${codPosto}`;
    
    return this.http.delete<LocalAtendimento>(url).pipe(
      map((obj) => obj)
    );
  }
}
