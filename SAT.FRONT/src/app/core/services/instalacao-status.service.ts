import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { InstalacaoStatus, InstalacaoStatusData, InstalacaoStatusParameters } from '../types/instalacao-status.types';

@Injectable({
  providedIn: 'root'
})
export class InstalacaoStatusService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: InstalacaoStatusParameters): Observable<InstalacaoStatusData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/InstalacaoStatus`, { params: params }).pipe(
      map((data: InstalacaoStatusData) => data)
    )
  }

  obterPorCodigo(codInstalStatus: number): Observable<InstalacaoStatus> {
    const url = `${c.api}/InstalacaoStatus/${codInstalStatus}`;
    return this.http.get<InstalacaoStatus>(url).pipe(
      map((obj) => obj)
    );
}

  criar(instalacaoStatus: InstalacaoStatus): Observable<InstalacaoStatus> {
    return this.http.post<InstalacaoStatus>(`${c.api}/InstalacaoStatus`, instalacaoStatus).pipe(
      map((obj) => obj)
    );
  }

  atualizar(instalacaoStatus: InstalacaoStatus): Observable<InstalacaoStatus> {
    const url = `${c.api}/InstalacaoStatus`;

    return this.http.put<InstalacaoStatus>(url, instalacaoStatus).pipe(
      map((obj) => obj)
    );
  }

  deletar(codInstalStatus: number): Observable<InstalacaoStatus> {
    const url = `${c.api}/InstalacaoStatus/${codInstalStatus}`;

    return this.http.delete<InstalacaoStatus>(url).pipe(
      map((obj) => obj)
    );
  }
}
