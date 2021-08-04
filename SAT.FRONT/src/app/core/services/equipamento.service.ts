import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Equipamento, EquipamentoData, EquipamentoParameters } from '../types/equipamento.types';

@Injectable({
  providedIn: 'root'
})
export class EquipamentoService {
  constructor(private http: HttpClient) {}

  criar(equipamento: Equipamento): Observable<Equipamento> {
    return this.http.post<Equipamento>(`${c.api}/Equipamento`, equipamento).pipe(
      map((obj) => obj)
    );
  }

  obterPorCodigo(codEquip: number): Observable<Equipamento> {
    const url = `${c.api}/Equipamento/${codEquip}`;
    return this.http.get<Equipamento>(url).pipe(
      map((obj) => obj)
    );
  }

  obterPorParametros(parameters: EquipamentoParameters): Observable<EquipamentoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Equipamento`, { params: params }).pipe(
      map((data: EquipamentoData) => data)
    )
  }

  atualizar(equipamento: Equipamento): Observable<Equipamento> {
    const url = `${c.api}/Equipamento`;
    return this.http.put<Equipamento>(url, equipamento).pipe(
      map((obj) => obj)
    );
  }

  deletar(codEquip: number): Observable<Equipamento> {
    const url = `${c.api}/Equipamento/${codEquip}`;
    
    return this.http.delete<Equipamento>(url).pipe(
      map((obj) => obj)
    );
  }
}
