import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { PerfilSetorParameters, PerfilSetorData, PerfilSetor } from '../types/perfil-setor.types';


@Injectable({
  providedIn: 'root'
})
export class PerfilSetorService {
  constructor(private http: HttpClient) { }

 
  obterPorParametros(parameters: PerfilSetorParameters): Observable<PerfilSetorData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/PerfilSetor`, { params: params }).pipe(
      map((data: PerfilSetorData) => data)
    )
  }

  obterPorCodigo(codPerfilSetor: number): Observable<PerfilSetor> {
    const url = `${c.api}/PerfilSetor/${codPerfilSetor}`;
    return this.http.get<PerfilSetor>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(perfilSetor: PerfilSetor): Observable<PerfilSetor> {
    return this.http.post<PerfilSetor>(`${c.api}/PerfilSetor`, perfilSetor).pipe(
      map((obj) => obj)
    );
  }

  atualizar(perfilSetor: PerfilSetor): Observable<PerfilSetor> {
    const url = `${c.api}/PerfilSetor`;
    return this.http.put<PerfilSetor>(url, perfilSetor).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPerfilSetor: number): Observable<PerfilSetor> {
    const url = `${c.api}/PerfilSetor/${codPerfilSetor}`;

    return this.http.delete<PerfilSetor>(url).pipe(
      map((obj) => obj)
    );
  }
}
