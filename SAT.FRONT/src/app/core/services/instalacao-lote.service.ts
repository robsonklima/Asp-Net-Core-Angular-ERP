import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { InstalacaoLote, InstalacaoLoteData, InstalacaoLoteParameters } from '../types/instalacao-lote.types';

@Injectable({
  providedIn: 'root'
})
export class InstalacaoLoteService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: InstalacaoLoteParameters): Observable<InstalacaoLoteData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/InstalacaoLote`, { params: params }).pipe(
      map((data: InstalacaoLoteData) => data)
    )
  }

  obterPorCodigo(codInstalacaoLote: number): Observable<InstalacaoLote> {
    const url = `${c.api}/InstalacaoLote/${codInstalacaoLote}`;
    return this.http.get<InstalacaoLote>(url).pipe(
      map((obj) => obj)
    );
}

  criar(instalLote: InstalacaoLote): Observable<InstalacaoLote> {
    return this.http.post<InstalacaoLote>(`${c.api}/InstalacaoLote`, instalLote).pipe(
      map((obj) => obj)
    );
  }

  atualizar(instalLote: InstalacaoLote): Observable<InstalacaoLote> {
    const url = `${c.api}/InstalacaoLote`;

    return this.http.put<InstalacaoLote>(url, instalLote).pipe(
      map((obj) => obj)
    );
  }

  deletar(codInstalacaoLote: number): Observable<InstalacaoLote> {
    const url = `${c.api}/InstalacaoLote/${codInstalacaoLote}`;

    return this.http.delete<InstalacaoLote>(url).pipe(
      map((obj) => obj)
    );
  }
}
