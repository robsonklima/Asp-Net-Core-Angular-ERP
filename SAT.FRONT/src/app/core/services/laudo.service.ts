import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Laudo, LaudoData, LaudoParameters } from '../types/laudo.types';

@Injectable({
  providedIn: 'root'
})
export class LaudoService
{
  constructor (
    private http: HttpClient
  ) { }

  obterPorParametros(parameters: LaudoParameters): Observable<LaudoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Laudo`, { params: params }).pipe(
      map((data: LaudoData) => data)
    )
  }

  obterPorCodigo(codLaudo: number): Observable<Laudo>
  {
    const url = `${c.api}/Laudo/${codLaudo}`;

    return this.http.get<Laudo>(url).pipe(
      map((obj) => obj)
    );
  }

  atualizar(laudo: Laudo): Observable<Laudo> {
    const url = `${c.api}/Laudo`;

    return this.http.put<Laudo>(url, laudo).pipe(
      map((obj) => obj)
    );
  }

}
