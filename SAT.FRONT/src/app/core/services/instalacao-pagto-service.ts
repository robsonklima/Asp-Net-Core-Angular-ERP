import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { InstalacaoPagto, InstalacaoPagtoData, InstalacaoPagtoParameters } from '../types/instalacao-pagto.types';

@Injectable({
  providedIn: 'root'
})
export class InstalacaoPagtoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: InstalacaoPagtoParameters): Observable<InstalacaoPagtoData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/InstalacaoPagto`, { params: params }).pipe(
      map((data: InstalacaoPagtoData) => data)
    )
  }

  obterPorCodigo(codInstalPagto: number): Observable<InstalacaoPagto> {
    const url = `${c.api}/InstalacaoPagto/${codInstalPagto}`;
    return this.http.get<InstalacaoPagto>(url).pipe(
      map((obj) => obj)
    );
}

  criar(instalacaoPagto: InstalacaoPagto): Observable<InstalacaoPagto> {
    return this.http.post<InstalacaoPagto>(`${c.api}/InstalacaoPagto`, instalacaoPagto).pipe(
      map((obj) => obj)
    );
  }

  atualizar(instalacaoPagto: InstalacaoPagto): Observable<InstalacaoPagto> {
    const url = `${c.api}/InstalacaoPagto`;

    return this.http.put<InstalacaoPagto>(url, instalacaoPagto).pipe(
      map((obj) => obj)
    );
  }

  deletar(codInstalPagto: number): Observable<InstalacaoPagto> {
    const url = `${c.api}/InstalacaoPagto/${codInstalPagto}`;

    return this.http.delete<InstalacaoPagto>(url).pipe(
      map((obj) => obj)
    );
  }
}
