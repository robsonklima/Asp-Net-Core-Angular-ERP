import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { InstalacaoPleitoInstal, InstalacaoPleitoInstalData, InstalacaoPleitoInstalParameters } from '../types/instalacao-pleito-instal.types';

@Injectable({
  providedIn: 'root'
})
export class InstalacaoPleitoInstalService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: InstalacaoPleitoInstalParameters): Observable<InstalacaoPleitoInstalData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/InstalacaoPleitoInstal`, { params: params }).pipe(
      map((data: InstalacaoPleitoInstalData) => data)
    )
  }

  obterPorCodigo(codInstalPleitoInstal: number): Observable<InstalacaoPleitoInstal> {
    const url = `${c.api}/InstalacaoPleitoInstal/${codInstalPleitoInstal}`;
    return this.http.get<InstalacaoPleitoInstal>(url).pipe(
      map((obj) => obj)
    );
}

  criar(instalacaoPleitoInstal: InstalacaoPleitoInstal): Observable<InstalacaoPleitoInstal> {
    return this.http.post<InstalacaoPleitoInstal>(`${c.api}/InstalacaoPleitoInstal`, instalacaoPleitoInstal).pipe(
      map((obj) => obj)
    );
  }

  atualizar(instalacaoPleitoInstal: InstalacaoPleitoInstal): Observable<InstalacaoPleitoInstal> {
    const url = `${c.api}/InstalacaoPleitoInstal`;

    return this.http.put<InstalacaoPleitoInstal>(url, instalacaoPleitoInstal).pipe(
      map((obj) => obj)
    );
  }

  deletar(codInstalPleitoInstal: number): Observable<InstalacaoPleitoInstal> {
    const url = `${c.api}/InstalacaoPleitoInstal/${codInstalPleitoInstal}`;

    return this.http.delete<InstalacaoPleitoInstal>(url).pipe(
      map((obj) => obj)
    );
  }
}
