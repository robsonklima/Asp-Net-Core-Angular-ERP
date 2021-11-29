import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ContratoSLAData, ContratoSLAParameters } from '../types/contrato-sla.types';
import { SLAData, SLAParameters } from '../types/sla.types';

@Injectable({
  providedIn: 'root'
})
export class SLAService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: SLAParameters): Observable<SLAData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/SLA`, { params: params }).pipe(
      map((data: SLAData) => data)
    )
  }
}
