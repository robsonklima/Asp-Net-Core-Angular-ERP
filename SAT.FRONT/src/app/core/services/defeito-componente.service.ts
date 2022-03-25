import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config';
import { DefeitoComponente, DefeitoComponenteData, DefeitoComponenteParameters } from '../types/defeito-componente.types';

@Injectable({
  providedIn: 'root'
})
export class DefeitoComponenteService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: DefeitoComponenteParameters): Observable<DefeitoComponenteData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/DefeitoComponente`, { params: params }).pipe(
      map((data: DefeitoComponenteData) => data)
    )
  }

  obterPorCodigo(codDefeito: number): Observable<DefeitoComponente> {
    const url = `${c.api}/DefeitoComponente/${codDefeito}`;

    return this.http.get<DefeitoComponente>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(defeito: DefeitoComponente): Observable<DefeitoComponente> {
    return this.http.post<DefeitoComponente>(`${c.api}/DefeitoComponente`, defeito).pipe(
      map((obj) => obj)
    );
  }

  atualizar(defeito: DefeitoComponente): Observable<DefeitoComponente> {
    const url = `${c.api}/DefeitoComponente`;
    return this.http.put<DefeitoComponente>(url, defeito).pipe(
      map((obj) => obj)
    );
  }

  deletar(codDefeito: number): Observable<DefeitoComponente> {
    const url = `${c.api}/DefeitoComponente/${codDefeito}`;

    return this.http.delete<DefeitoComponente>(url).pipe(
      map((obj) => obj)
    );
  }
}
