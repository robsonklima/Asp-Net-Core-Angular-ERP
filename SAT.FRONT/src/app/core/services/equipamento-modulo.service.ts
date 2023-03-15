import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config';
import { EquipamentoModulo, EquipamentoModuloData, EquipamentoModuloParameters } from '../types/equipamento-modulo.types';

@Injectable({
  providedIn: 'root'
})
export class EquipamentoModuloService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: EquipamentoModuloParameters): Observable<EquipamentoModuloData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/EquipamentoModulo`, { params: params }).pipe(
      map((data: EquipamentoModuloData) => data)
    )
  }

  obterPorCodigo(codAcao: number): Observable<EquipamentoModulo> {
    const url = `${c.api}/EquipamentoModulo/${codAcao}`;

    return this.http.get<EquipamentoModulo>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(acao: EquipamentoModulo): Observable<EquipamentoModulo> {
    return this.http.post<EquipamentoModulo>(`${c.api}/EquipamentoModulo`, acao).pipe(
      map((obj) => obj)
    );
  }

  atualizar(acao: EquipamentoModulo): Observable<EquipamentoModulo> {
    const url = `${c.api}/EquipamentoModulo`;
    return this.http.put<EquipamentoModulo>(url, acao).pipe(
      map((obj) => obj)
    );
  }

  deletar(codConfigEquipModulos: number): Observable<EquipamentoModulo> {
    const url = `${c.api}/EquipamentoModulo/${codConfigEquipModulos}`;

    return this.http.delete<EquipamentoModulo>(url).pipe(
      map((obj) => obj)
    );
  }
}
