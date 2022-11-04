import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ORTempoReparo, ORTempoReparoData, ORTempoReparoParameters } from '../types/or-tempo-reparo.types';


@Injectable({
  providedIn: 'root'
})
export class ORTempoReparoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: ORTempoReparoParameters): Observable<ORTempoReparoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/ORTempoReparo`, { params: params }).pipe(
      map((data: ORTempoReparoData) => data)
    )
  }

  obterPorCodigo(codORTempoReparo: number): Observable<ORTempoReparo> {
    const url = `${c.api}/ORTempoReparo/${codORTempoReparo}`;
    return this.http.get<ORTempoReparo>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(tr: ORTempoReparo): Observable<ORTempoReparo> {
    return this.http.post<ORTempoReparo>(`${c.api}/ORTempoReparo`, tr).pipe(
      map((obj) => obj)
    );
  }

  atualizar(tr: ORTempoReparo): Observable<ORTempoReparo> {
    const url = `${c.api}/ORTempoReparo`;

    return this.http.put<ORTempoReparo>(url, tr).pipe(
      map((obj) => obj)
    );
  }

  deletar(codORTempoReparo: number): Observable<ORTempoReparo> {
    const url = `${c.api}/ORTempoReparo/${codORTempoReparo}`;

    return this.http.delete<ORTempoReparo>(url).pipe(
      map((obj) => obj)
    );
  }
}
