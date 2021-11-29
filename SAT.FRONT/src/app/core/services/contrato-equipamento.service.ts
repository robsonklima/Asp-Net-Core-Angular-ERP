import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ContratoEquipamento, ContratoEquipamentoData, ContratoEquipamentoParameters } from '../types/contrato-equipamento.types';

@Injectable({
  providedIn: 'root'
})
export class ContratoEquipamentoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: ContratoEquipamentoParameters): Observable<ContratoEquipamentoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/ContratoEquipamento`, { params: params }).pipe(
      map((data: ContratoEquipamentoData) => data)
    )
  }

  obterPorCodigo(codContratoEquip: number, codEquip: number): Observable<ContratoEquipamento> {
    const url = `${c.api}/ContratoEquipamento/${codContratoEquip}/${codEquip}`;
    return this.http.get<ContratoEquipamento>(url).pipe(
      map((obj) => obj)
    );
  }

  
  criar(contratoEquipamento: ContratoEquipamento): Observable<ContratoEquipamento> {
    return this.http.post<ContratoEquipamento>(`${c.api}/ContratoEquipamento`, contratoEquipamento).pipe(
      map((obj) => obj)
    );
  }

  atualizar(contratoEquipamento: ContratoEquipamento): Observable<ContratoEquipamento> {
    const url = `${c.api}/ContratoEquipamento`;

    return this.http.put<ContratoEquipamento>(url, contratoEquipamento).pipe(
      map((obj) => obj)
    );
  }

  deletar(codContratoEquipamento: number): Observable<ContratoEquipamento> {
    const url = `${c.api}/ContratoEquipamento/${codContratoEquipamento}`;

    return this.http.delete<ContratoEquipamento>(url).pipe(
      map((obj) => obj)
    );
  }

}
