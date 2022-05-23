import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DispBBBloqueioOS, DispBBBloqueioOSData, DispBBBloqueioOSParameters } from '../types/DispBBBloqueioOS.types';

@Injectable({
  providedIn: 'root'
})
export class DispBBBloqueioOSService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: DispBBBloqueioOSParameters): Observable<DispBBBloqueioOSData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/DispBBBloqueioOS`, { params: params }).pipe(
      map((data: DispBBBloqueioOSData) => data)
    )
  }

  obterPorCodigo(codDispBBBloqueioOS: number): Observable<DispBBBloqueioOS> {
    const url = `${c.api}/DispBBBloqueioOS/${codDispBBBloqueioOS}`;
    return this.http.get<DispBBBloqueioOS>(url).pipe(
      map((obj) => obj)
    );
}

  criar(instalLote: DispBBBloqueioOS): Observable<DispBBBloqueioOS> {
    return this.http.post<DispBBBloqueioOS>(`${c.api}/DispBBBloqueioOS`, instalLote).pipe(
      map((obj) => obj)
    );
  }

  atualizar(instalLote: DispBBBloqueioOS): Observable<DispBBBloqueioOS> {
    const url = `${c.api}/DispBBBloqueioOS`;

    return this.http.put<DispBBBloqueioOS>(url, instalLote).pipe(
      map((obj) => obj)
    );
  }

  deletar(codDispBBBloqueioOS: number): Observable<DispBBBloqueioOS> {
    const url = `${c.api}/DispBBBloqueioOS/${codDispBBBloqueioOS}`;

    return this.http.delete<DispBBBloqueioOS>(url).pipe(
      map((obj) => obj)
    );
  }
}
