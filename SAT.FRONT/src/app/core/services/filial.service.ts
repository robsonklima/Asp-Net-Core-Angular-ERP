import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Filial, FilialData, FilialParameters } from '../types/filial.types';

@Injectable({
  providedIn: 'root'
})
export class FilialService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: FilialParameters): Observable<FilialData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Filial`, { params: params }).pipe(
      map((data: FilialData) => data)
    )
  }

  obterPorCodigo(codFilial: number): Observable<Filial> {
    const url = `${c.api}/Filial/${codFilial}`;
    return this.http.get<Filial>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(filial: Filial): Observable<Filial> {
    return this.http.post<Filial>(`${c.api}/Filial`, filial).pipe(
      map((obj) => obj)
    );
  }

  atualizar(filial: Filial): Observable<Filial> {
    const url = `${c.api}/Filial`;

    return this.http.put<Filial>(url, filial).pipe(
      map((obj) => obj)
    );
  }

  deletar(codFilial: number): Observable<Filial> {
    const url = `${c.api}/Filial/${codFilial}`;

    return this.http.delete<Filial>(url).pipe(
      map((obj) => obj)
    );
  }
}
