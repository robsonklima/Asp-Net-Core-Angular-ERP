import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { InstalacaoPleito, InstalacaoPleitoData, InstalacaoPleitoParameters } from '../types/instalacao-pleito.types';

@Injectable({
  providedIn: 'root'
})
export class InstalacaoPleitoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: InstalacaoPleitoParameters): Observable<InstalacaoPleitoData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/InstalacaoPleito`, { params: params }).pipe(
      map((data: InstalacaoPleitoData) => data)
    )
  }

  obterPorCodigo(codInstalacaoPleito: number): Observable<InstalacaoPleito> {
    const url = `${c.api}/InstalacaoPleito/${codInstalacaoPleito}`;
    return this.http.get<InstalacaoPleito>(url).pipe(
      map((obj) => obj)
    );
}

  criar(instalacaoPleito: InstalacaoPleito): Observable<InstalacaoPleito> {
    return this.http.post<InstalacaoPleito>(`${c.api}/InstalacaoPleito`, instalacaoPleito).pipe(
      map((obj) => obj)
    );
  }

  atualizar(instalacaoPleito: InstalacaoPleito): Observable<InstalacaoPleito> {
    const url = `${c.api}/InstalacaoPleito`;

    return this.http.put<InstalacaoPleito>(url, instalacaoPleito).pipe(
      map((obj) => obj)
    );
  }

  deletar(codInstalacaoPleito: number): Observable<InstalacaoPleito> {
    const url = `${c.api}/InstalacaoPleito/${codInstalacaoPleito}`;

    return this.http.delete<InstalacaoPleito>(url).pipe(
      map((obj) => obj)
    );
  }
}
