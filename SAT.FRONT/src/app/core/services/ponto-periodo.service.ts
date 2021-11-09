import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { PontoPeriodo, PontoPeriodoData, PontoPeriodoParameters } from '../types/ponto-periodo.types';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class PontoPeriodoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: PontoPeriodoParameters): Observable<PontoPeriodoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/PontoPeriodo`, { params: params }).pipe(
      map((data: PontoPeriodoData) => data)
    )
  }

  obterPorCodigo(codPontoPeriodo: number): Observable<PontoPeriodo> {
    const url = `${c.api}/PontoPeriodo/${codPontoPeriodo}`;

    return this.http.get<PontoPeriodo>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(pontoPeriodo: PontoPeriodo): Observable<PontoPeriodo> {
    return this.http.post<PontoPeriodo>(`${c.api}/PontoPeriodo`, pontoPeriodo).pipe(
      map((obj) => obj)
    );
  }

  atualizar(pontoPeriodo: PontoPeriodo): Observable<PontoPeriodo> {
    const url = `${c.api}/PontoPeriodo`;
    return this.http.put<PontoPeriodo>(url, pontoPeriodo).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPontoPeriodo: number): Observable<PontoPeriodo> {
    const url = `${c.api}/PontoPeriodo/${codPontoPeriodo}`;
    
    return this.http.delete<PontoPeriodo>(url).pipe(
      map((obj) => obj)
    );
  }
}
