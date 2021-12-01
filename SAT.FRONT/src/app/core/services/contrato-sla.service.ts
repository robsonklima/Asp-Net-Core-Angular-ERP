import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ContratoSLA, ContratoSLAData, ContratoSLAParameters } from '../types/contrato-sla.types';

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

  criar(contratoSLA: ContratoSLA): Observable<ContratoSLA> {
    return this.http.post<ContratoSLA>(`${c.api}/ContratoSLA`, contratoSLA).pipe(
      map((obj) => obj)
    );
  }

  atualizar(contratoSLA: ContratoSLA): Observable<ContratoSLA> {
    const url = `${c.api}/ContratoSLA`;

    return this.http.put<ContratoSLA>(url, contratoSLA).pipe(
      map((obj) => obj)
    );
  }

  deletar(codContrato: number, codSLA: number): Observable<ContratoSLA> {
    const url = `${c.api}/ContratoSLA/${codContrato}/${codSLA}`;

    return this.http.delete<ContratoSLA>(url).pipe(
      map((obj) => obj)
    );
  }
}
