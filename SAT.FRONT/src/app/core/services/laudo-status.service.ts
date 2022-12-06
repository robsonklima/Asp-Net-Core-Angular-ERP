import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { LaudoStatusParameters, LaudoStatusData, LaudoStatus } from '../types/laudo-Status.types';

@Injectable({
  providedIn: 'root'
})
export class LaudoStatusService
{
  constructor (
    private http: HttpClient
  ) { }

  obterPorParametros(parameters: LaudoStatusParameters): Observable<LaudoStatusData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/LaudoStatus`, { params: params }).pipe(
      map((data: LaudoStatusData) => data)
    )
  }

  obterPorCodigo(codLaudoStatus: number): Observable<LaudoStatus>
  {
    const url = `${c.api}/LaudoStatus/${codLaudoStatus}`;

    return this.http.get<LaudoStatus>(url).pipe(
      map((obj) => obj)
    );
  }

}
