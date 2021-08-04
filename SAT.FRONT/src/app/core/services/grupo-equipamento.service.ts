import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { GrupoEquipamento, GrupoEquipamentoData, GrupoEquipamentoParameters } from '../types/grupo-equipamento.types';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class GrupoEquipamentoService {
  constructor(private http: HttpClient) {}

  criar(equipamento: GrupoEquipamento): Observable<GrupoEquipamento> {
    return this.http.post<GrupoEquipamento>(`${c.api}/GrupoEquipamento`, equipamento).pipe(
      map((obj) => obj)
    );
  }

  obterPorCodigo(codGrupoEquip: number): Observable<GrupoEquipamento> {
    const url = `${c.api}/GrupoEquipamento/${codGrupoEquip}`;
    return this.http.get<GrupoEquipamento>(url).pipe(
      map((obj) => obj)
    );
  }

  obterPorParametros(parameters: GrupoEquipamentoParameters): Observable<GrupoEquipamentoData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/GrupoEquipamento`, { params: params }).pipe(
      map((data: GrupoEquipamentoData) => data)
    )
  }

  atualizar(grupoEquipamento: GrupoEquipamento): Observable<GrupoEquipamento> {
    const url = `${c.api}/GrupoEquipamento`;
    return this.http.put<GrupoEquipamento>(url, grupoEquipamento).pipe(
      map((obj) => obj)
    );
  }

  deletar(codGrupoEquipamento: number): Observable<GrupoEquipamento> {
    const url = `${c.api}/GrupoEquipamento/${codGrupoEquipamento}`;

    return this.http.delete<GrupoEquipamento>(url).pipe(
      map((obj) => obj)
    );
  }
}
