import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { PontoPeriodoUsuario, PontoPeriodoUsuarioData, PontoPeriodoUsuarioParameters } from '../types/ponto-periodo-usuario.types';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class PontoPeriodoUsuarioService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: PontoPeriodoUsuarioParameters): Observable<PontoPeriodoUsuarioData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/PontoPeriodoUsuario`, { params: params }).pipe(
      map((data: PontoPeriodoUsuarioData) => data)
    )
  }

  obterPorCodigo(codPontoPeriodoUsuario: number): Observable<PontoPeriodoUsuario> {
    const url = `${c.api}/PontoPeriodoUsuario/${codPontoPeriodoUsuario}`;

    return this.http.get<PontoPeriodoUsuario>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(pontoPeriodoUsuario: PontoPeriodoUsuario): Observable<PontoPeriodoUsuario> {
    return this.http.post<PontoPeriodoUsuario>(`${c.api}/PontoPeriodoUsuario`, pontoPeriodoUsuario).pipe(
      map((obj) => obj)
    );
  }

  atualizar(pontoPeriodoUsuario: PontoPeriodoUsuario): Observable<PontoPeriodoUsuario> {
    const url = `${c.api}/PontoPeriodoUsuario`;
    return this.http.put<PontoPeriodoUsuario>(url, pontoPeriodoUsuario).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPontoPeriodoUsuario: number): Observable<PontoPeriodoUsuario> {
    const url = `${c.api}/PontoPeriodoUsuario/${codPontoPeriodoUsuario}`;
    
    return this.http.delete<PontoPeriodoUsuario>(url).pipe(
      map((obj) => obj)
    );
  }
}
