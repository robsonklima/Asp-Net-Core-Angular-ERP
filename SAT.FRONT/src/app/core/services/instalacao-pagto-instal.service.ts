import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { InstalacaoPagtoInstalParameters, InstalacaoPagtoInstalData, InstalacaoPagtoInstal } from '../types/instalacao-pagto-instal.types';


@Injectable({
  providedIn: 'root'
})
export class InstalacaoPagtoInstalService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: InstalacaoPagtoInstalParameters): Observable<InstalacaoPagtoInstalData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/InstalacaoPagtoInstal`, { params: params }).pipe(
      map((data: InstalacaoPagtoInstalData) => data)
    )
  }

  obterPorCodigo(codInstalacao, codInstalPagto, codInstalTipoParcela: number): Observable<InstalacaoPagtoInstal> {
    const url = `${c.api}/InstalacaoPagtoInstal/${codInstalacao}/${codInstalPagto}/${codInstalTipoParcela}`;
    return this.http.get<InstalacaoPagtoInstal>(url).pipe(
      map((obj) => obj)
    );
}

  criar(instalacaoPagtoInstal: InstalacaoPagtoInstal): Observable<InstalacaoPagtoInstal> {
    return this.http.post<InstalacaoPagtoInstal>(`${c.api}/InstalacaoPagtoInstal`, instalacaoPagtoInstal).pipe(
      map((obj) => obj)
    );
  }

  atualizar(instalacaoPagtoInstal: InstalacaoPagtoInstal): Observable<InstalacaoPagtoInstal> {
    const url = `${c.api}/InstalacaoPagtoInstal`;

    return this.http.put<InstalacaoPagtoInstal>(url, instalacaoPagtoInstal).pipe(
      map((obj) => obj)
    );
  }

  deletar(codInstalacao, codInstalPagto, codInstalTipoParcela: number): Observable<InstalacaoPagtoInstal> {
    const url = `${c.api}/InstalacaoPagtoInstal/${codInstalacao}/${codInstalPagto}/${codInstalTipoParcela}`;

    return this.http.delete<InstalacaoPagtoInstal>(url).pipe(
      map((obj) => obj)
    );
  }
}
