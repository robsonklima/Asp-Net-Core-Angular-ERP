import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ContratoSLAData, ContratoSLAParameters } from '../types/contrato-sla.types';

@Injectable({
  providedIn: 'root'
})
export class ContratoSLAService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: ContratoSLAParameters): Observable<ContratoSLAData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/ContratoSLA`, { params: params }).pipe(
      map((data: ContratoSLAData) => data)
    )
  }
}
