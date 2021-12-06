import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Instalacao, InstalacaoData, InstalacaoParameters } from '../types/instalacao.types';

@Injectable({
  providedIn: 'root'
})
export class InstalacaoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: InstalacaoParameters): Observable<InstalacaoData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Instalacao`, { params: params }).pipe(
      map((data: InstalacaoData) => data)
    )
  }

  obterPorCodigo(codInstalacao: number): Observable<Instalacao> {
    const url = `${c.api}/Instalacao/${codInstalacao}`;
    return this.http.get<Instalacao>(url).pipe(
      map((obj) => obj)
    );
}

  criar(instalacao: Instalacao): Observable<Instalacao> {
    return this.http.post<Instalacao>(`${c.api}/Instalacao`, instalacao).pipe(
      map((obj) => obj)
    );
  }

  atualizar(instalacao: Instalacao): Observable<Instalacao> {
    const url = `${c.api}/Instalacao`;

    return this.http.put<Instalacao>(url, instalacao).pipe(
      map((obj) => obj)
    );
  }

  deletar(codInstalacao: number): Observable<Instalacao> {
    const url = `${c.api}/Instalacao/${codInstalacao}`;

    return this.http.delete<Instalacao>(url).pipe(
      map((obj) => obj)
    );
  }
}
