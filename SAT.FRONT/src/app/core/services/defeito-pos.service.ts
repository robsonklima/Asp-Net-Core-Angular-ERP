import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DefeitoPOS, DefeitoPOSData, DefeitoPOSParameters } from '../types/defeito-pos.types';

@Injectable({
  providedIn: 'root'
})
export class DefeitoPOSService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: DefeitoPOSParameters): Observable<DefeitoPOSData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get<DefeitoPOSData>(`${c.api}/DefeitoPOS`, { params: params }).pipe(
      map((data) => data)
    )
  }

  obterPorCodigo(codDefeitoPOS: number): Observable<DefeitoPOS> {
    const url = `${c.api}/DefeitoPOS/${codDefeitoPOS}`;

    return this.http.get<DefeitoPOS>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(d: DefeitoPOS): Observable<DefeitoPOS> {
    return this.http.post<DefeitoPOS>(`${c.api}/DefeitoPOS`, d).pipe(
      map((obj) => obj)
    );
  }

  atualizar(d: DefeitoPOS): Observable<DefeitoPOS> {
    const url = `${c.api}/DefeitoPOS`;

    return this.http.put<DefeitoPOS>(url, d).pipe(
      map((obj) => obj)
    );
  }

  deletar(codDefeitoPOS: number): Observable<DefeitoPOS> {
    const url = `${c.api}/DefeitoPOS/${codDefeitoPOS}`;

    return this.http.delete<DefeitoPOS>(url).pipe(
      map((obj) => obj)
    );
  }
}
