import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config';
import { TipoEquipamento, TipoEquipamentoData,
         TipoEquipamentoParameters } from '../types/tipo-equipamento.types';

@Injectable({
  providedIn: 'root'
})
export class TipoEquipamentoService {
  constructor(private http: HttpClient) {}

  criar(tipoEquipamento: TipoEquipamento): Observable<TipoEquipamento> {
    return this.http.post<TipoEquipamento>(`${c.api}/TipoEquipamento`, tipoEquipamento).pipe(
      map((obj) => obj)
    );
  }

  obterPorCodigo(codTipoEquipamento: number): Observable<TipoEquipamento> {
    const url = `${c.api}/TipoEquipamento/${codTipoEquipamento}`;
    return this.http.get<TipoEquipamento>(url).pipe(
      map((obj) => obj)
    );
  }

  obterPorParametros(parameters: TipoEquipamentoParameters): Observable<TipoEquipamentoData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/TipoEquipamento`, { params: params }).pipe(
      map((data: TipoEquipamentoData) => data)
    )
  }

  atualizar(tipoEquipamento: TipoEquipamento): Observable<TipoEquipamento> {
    const url = `${c.api}/TipoEquipamento`;
    return this.http.put<TipoEquipamento>(url, tipoEquipamento).pipe(
      map((obj) => obj)
    );
  }

  deletar(codTipoEquipamento: number): Observable<TipoEquipamento> {
    const url = `${c.api}/TipoEquipamento/${codTipoEquipamento}`;

    return this.http.delete<TipoEquipamento>(url).pipe(
      map((obj) => obj)
    );
  }
}
