import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ANS, ANSData, ANSParameters } from '../types/ans.types';

@Injectable({
  providedIn: 'root'
})
export class ANSService
{
  constructor (
    private http: HttpClient
  ) { }

  obterPorParametros(parameters: ANSParameters): Observable<ANSData>
  {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key =>
    {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/ANS`, { params: params }).pipe(
      map((data: ANSData) => data)
    )
  }

  obterPorCodigo(cod: number): Observable<ANS>
  {
    const url = `${c.api}/ANS/${cod}`;

    return this.http.get<ANS>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(ANS: ANS): Observable<ANS>
  {
    return this.http.post<ANS>(`${c.api}/ANS`, ANS).pipe(
      map((obj) => obj)
    );
  }

  atualizar(ANS: ANS): Observable<ANS>
  {
    const url = `${c.api}/ANS`;

    return this.http.put<ANS>(url, ANS).pipe(
      map((obj) => obj)
    );
  }

  deletar(codOS: number): Observable<ANS>
  {
    const url = `${c.api}/ANS/${codOS}`;

    return this.http.delete<ANS>(url).pipe(
      map((obj) => obj)
    );
  }
}
