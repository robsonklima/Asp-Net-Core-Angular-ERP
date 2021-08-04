import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { EquipamentoContrato, EquipamentoContratoData,
         EquipamentoContratoParameters } from '../types/equipamento-contrato.types';

@Injectable({
  providedIn: 'root'
})
export class EquipamentoContratoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: EquipamentoContratoParameters): Observable<EquipamentoContratoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/EquipamentoContrato`, { params: params }).pipe(
      map((data: EquipamentoContratoData) => data)
    )
  }

  obterPorCodigo(codEquipContrato: number): Observable<EquipamentoContrato> {
    const url = `${c.api}/EquipamentoContrato/${codEquipContrato}`;
    return this.http.get<EquipamentoContrato>(url).pipe(
      map((obj) => obj)
    );
}

  criar(equipamentoContrato: EquipamentoContrato): Observable<EquipamentoContrato> {
    return this.http.post<EquipamentoContrato>(`${c.api}/EquipamentoContrato`, 
      equipamentoContrato).pipe(
      map((obj) => obj)
    );
  }

  atualizar(equipamentoContrato: EquipamentoContrato): Observable<EquipamentoContrato> {
    const url = `${c.api}/EquipamentoContrato`;
    return this.http.put<EquipamentoContrato>(url, equipamentoContrato).pipe(
      map((obj) => obj)
    );
  }

  deletar(codEquipContrato: number): Observable<EquipamentoContrato> {
    const url = `${c.api}/EquipamentoContrato/${codEquipContrato}`;
    
    return this.http.delete<EquipamentoContrato>(url).pipe(
      map((obj) => obj)
    );
  }
}
