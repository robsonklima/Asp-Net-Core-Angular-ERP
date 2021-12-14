import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { FiltroUsuarioData } from '../types/filtro.types';

@Injectable({
  providedIn: 'root'
})
export class FiltroService {
  constructor(private http: HttpClient) { }

  criar(filtroUsuario: FiltroUsuarioData): Observable<FiltroUsuarioData> {
    return this.http.post<FiltroUsuarioData>(`${c.api}/Filtro`, filtroUsuario).pipe(
      map((obj) => obj)
    );
  }

  atualizar(filtroUsuario: FiltroUsuarioData): Observable<FiltroUsuarioData> {
    const url = `${c.api}/Filtro`;
    return this.http.put<FiltroUsuarioData>(url, filtroUsuario).pipe(
      map((obj) => obj)
    );
  }

  deletar(codFiltroUsuario: number): Observable<FiltroUsuarioData> {
    const url = `${c.api}/Filtro/${codFiltroUsuario}`;
    return this.http.delete<FiltroUsuarioData>(url).pipe(
      map((obj) => obj)
    );
  }
}
