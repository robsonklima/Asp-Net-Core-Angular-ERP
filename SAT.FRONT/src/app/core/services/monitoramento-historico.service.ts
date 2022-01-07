import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { MonitoramentoHistoricoData } from '../types/monitoramento-historico.types';
import { MonitoramentoParameters } from '../types/monitoramento.types';

@Injectable({
  providedIn: 'root'
})
export class MonitoramentoHistoricoService
{
  constructor (private http: HttpClient) { }

  obterPorParametros(parameters: MonitoramentoParameters): Observable<MonitoramentoHistoricoData>
  {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key =>
    {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/MonitoramentoHistorico`, { params: params }).pipe(
      map((data: MonitoramentoHistoricoData) => data)
    )
  }
}