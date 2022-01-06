import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { MonitoramentoData, MonitoramentoParameters } from '../types/monitoramento.types';

@Injectable({
    providedIn: 'root'
})
export class MonitoramentoService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: MonitoramentoParameters): Observable<MonitoramentoData> {
        let params = new HttpParams();
    
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/Monitoramento`, { params: params }).pipe(
          map((data: MonitoramentoData) => data)
        )
      }
}
