import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { PontoUsuario, PontoUsuarioDt, PontoUsuarioParameters } from '../types/ponto-usuario.types';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class PontoUsuarioService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: PontoUsuarioParameters): Observable<PontoUsuarioDt> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/PontoUsuario`, { params: params }).pipe(
      map((data: PontoUsuarioDt) => data)
    )
  }

  obterPorCodigo(codPontoUsuario: number): Observable<PontoUsuario> {
    const url = `${c.api}/PontoUsuario/${codPontoUsuario}`;

    return this.http.get<PontoUsuario>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(pontoUsuario: PontoUsuario): Observable<PontoUsuario> {
    return this.http.post<PontoUsuario>(`${c.api}/PontoUsuario`, pontoUsuario).pipe(
      map((obj) => obj)
    );
  }

  atualizar(pontoUsuario: PontoUsuario): Observable<PontoUsuario> {
    const url = `${c.api}/PontoUsuario`;
    return this.http.put<PontoUsuario>(url, pontoUsuario).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPontoUsuario: number): Observable<PontoUsuario> {
    const url = `${c.api}/PontoUsuario/${codPontoUsuario}`;
    
    return this.http.delete<PontoUsuario>(url).pipe(
      map((obj) => obj)
    );
  }
}
