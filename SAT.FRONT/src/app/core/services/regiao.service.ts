import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Regiao, RegiaoData, RegiaoParameters } from '../types/regiao.types';

@Injectable({
  providedIn: 'root'
})
export class RegiaoService {
  constructor(private http: HttpClient) {}

  criar(regiao: Regiao): Observable<Regiao> {
    return this.http.post<Regiao>(`${c.api}/Regiao`, regiao).pipe(
      map((obj) => obj)
    );
  }

  obterPorParametros(parameters: RegiaoParameters): Observable<RegiaoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Regiao`, { params: params }).pipe(
      map((data: RegiaoData) => data)
    )
  }

  obterPorCodigo(codRegiao: number): Observable<Regiao> {
    const url = `${c.api}/Regiao/${codRegiao}`;
    return this.http.get<Regiao>(url).pipe(
      map((obj) => obj)
    );
  }

  atualizar(regiao: Regiao): Observable<Regiao> {
    const url = `${c.api}/Regiao`;
    return this.http.put<Regiao>(url, regiao).pipe(
      map((obj) => obj)
    );
  }

  deletar(codRegiao: number): Observable<Regiao> {
    const url = `${c.api}/Regiao/${codRegiao}`;
    
    return this.http.delete<Regiao>(url).pipe(
      map((obj) => obj)
    );
  }
}
