import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { InstalacaoMotivoMulta, InstalacaoMotivoMultaData, InstalacaoMotivoMultaParameters } from '../types/instalacao-motivo-multa.types';

@Injectable({
  providedIn: 'root'
})
export class InstalacaoMotivoMultaService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: InstalacaoMotivoMultaParameters): Observable<InstalacaoMotivoMultaData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/InstalacaoMotivoMulta`, { params: params }).pipe(
      map((data: InstalacaoMotivoMultaData) => data)
    )
  }

  obterPorCodigo(codInstalMotivoMulta: number): Observable<InstalacaoMotivoMulta> {
    const url = `${c.api}/InstalacaoMotivoMulta/${codInstalMotivoMulta}`;
    return this.http.get<InstalacaoMotivoMulta>(url).pipe(
      map((obj) => obj)
    );
}

  criar(instalLote: InstalacaoMotivoMulta): Observable<InstalacaoMotivoMulta> {
    return this.http.post<InstalacaoMotivoMulta>(`${c.api}/InstalacaoMotivoMulta`, instalLote).pipe(
      map((obj) => obj)
    );
  }

  atualizar(instalLote: InstalacaoMotivoMulta): Observable<InstalacaoMotivoMulta> {
    const url = `${c.api}/InstalacaoMotivoMulta`;

    return this.http.put<InstalacaoMotivoMulta>(url, instalLote).pipe(
      map((obj) => obj)
    );
  }

  deletar(codInstalMotivoMulta: number): Observable<InstalacaoMotivoMulta> {
    const url = `${c.api}/InstalacaoMotivoMulta/${codInstalMotivoMulta}`;

    return this.http.delete<InstalacaoMotivoMulta>(url).pipe(
      map((obj) => obj)
    );
  }
}
