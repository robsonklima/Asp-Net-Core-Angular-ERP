import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Defeito, DefeitoData, DefeitoParameters } from '../types/defeito.types';

@Injectable({
  providedIn: 'root'
})
export class DefeitoService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: DefeitoParameters): Observable<DefeitoData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get<DefeitoData>(`${c.api}/Defeito`, { params: params }).pipe(
      map((data) => data)
    )
  }

  obterPorCodigo(codDefeito: number): Observable<Defeito> {
    const url = `${c.api}/Defeito/${codDefeito}`;

    return this.http.get<Defeito>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(defeito: Defeito): Observable<Defeito> {
    return this.http.post<Defeito>(`${c.api}/Defeito`, defeito).pipe(
      map((obj) => obj)
    );
  }

  atualizar(defeito: Defeito): Observable<Defeito> {
    const url = `${c.api}/Defeito`;

    return this.http.put<Defeito>(url, defeito).pipe(
      map((obj) => obj)
    );
  }

  deletar(codDefeito: number): Observable<Defeito> {
    const url = `${c.api}/Defeito/${codDefeito}`;

    return this.http.delete<Defeito>(url).pipe(
      map((obj) => obj)
    );
  }
}
