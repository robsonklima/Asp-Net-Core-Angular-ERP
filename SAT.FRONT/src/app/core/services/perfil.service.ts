import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Perfil, PerfilData, PerfilParameters } from '../types/perfil.types';

@Injectable({
  providedIn: 'root'
})
export class PerfilService {
  constructor(private http: HttpClient) { }

 
  obterPorParametros(parameters: PerfilParameters): Observable<PerfilData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Perfil`, { params: params }).pipe(
      map((data: PerfilData) => data)
    )
  }

  obterPorCodigo(codPerfil: number): Observable<Perfil> {
    const url = `${c.api}/Perfil/${codPerfil}`;
    return this.http.get<Perfil>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(perfil: Perfil): Observable<Perfil> {
    return this.http.post<Perfil>(`${c.api}/Perfil`, perfil).pipe(
      map((obj) => obj)
    );
  }

  atualizar(perfil: Perfil): Observable<Perfil> {
    const url = `${c.api}/Perfil`;
    return this.http.put<Perfil>(url, perfil).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPerfil: number): Observable<Perfil> {
    const url = `${c.api}/Perfil/${codPerfil}`;

    return this.http.delete<Perfil>(url).pipe(
      map((obj) => obj)
    );
  }
}
