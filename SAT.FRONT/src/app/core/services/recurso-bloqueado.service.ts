import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { RecursoBloqueadoParameters, RecursoBloqueadoData, RecursoBloqueado } from '../types/recurso-bloqueado.types';




@Injectable({
  providedIn: 'root'
})
export class RecursoBloqueadoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: RecursoBloqueadoParameters): Observable<RecursoBloqueadoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/RecursoBloqueado`, { params: params }).pipe(
      map((data: RecursoBloqueadoData) => data)
    )
  }

  obterPorCodigo(codRecursoBloqueado: number): Observable<RecursoBloqueado> {
    const url = `${c.api}/RecursoBloqueado/${codRecursoBloqueado}`;
    return this.http.get<RecursoBloqueado>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(recursoBloqueado: RecursoBloqueado): Observable<RecursoBloqueado> {
    return this.http.post<RecursoBloqueado>(`${c.api}/RecursoBloqueado`, recursoBloqueado).pipe(
      map((obj) => obj)
    );
  }

  atualizar(recursoBloqueado: RecursoBloqueado): Observable<RecursoBloqueado> {
    const url = `${c.api}/RecursoBloqueado`;

    return this.http.put<RecursoBloqueado>(url, recursoBloqueado).pipe(
      map((obj) => obj)
    );
  }

  deletar(codRecursoBloqueado: number): Observable<RecursoBloqueado> {
    const url = `${c.api}/RecursoBloqueado/${codRecursoBloqueado}`;

    return this.http.delete<RecursoBloqueado>(url).pipe(
      map((obj) => obj)
    );
  }
}
