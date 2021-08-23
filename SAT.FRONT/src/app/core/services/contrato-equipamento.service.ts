import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Acao, AcaoData, AcaoParameters } from '../types/acao.types';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class ContratoEquipamentoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: AcaoParameters): Observable<AcaoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Acao`, { params: params }).pipe(
      map((data: AcaoData) => data)
    )
  }
}
