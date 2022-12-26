import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { InstalacaoTipoPleito, InstalacaoTipoPleitoData, InstalacaoTipoPleitoParameters } from '../types/instalacao-tipo-pleito.types';

@Injectable({
  providedIn: 'root'
})
export class InstalacaoTipoPleitoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: InstalacaoTipoPleitoParameters): Observable<InstalacaoTipoPleitoData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/InstalacaoTipoPleito`, { params: params }).pipe(
      map((data: InstalacaoTipoPleitoData) => data)
    )
  }

  obterPorCodigo(codInstalacaoTipoPleito: number): Observable<InstalacaoTipoPleito> {
    const url = `${c.api}/InstalacaoTipoPleito/${codInstalacaoTipoPleito}`;
    return this.http.get<InstalacaoTipoPleito>(url).pipe(
      map((obj) => obj)
    );
}

  criar(instalacaoTipoPleito: InstalacaoTipoPleito): Observable<InstalacaoTipoPleito> {
    return this.http.post<InstalacaoTipoPleito>(`${c.api}/InstalacaoTipoPleito`, instalacaoTipoPleito).pipe(
      map((obj) => obj)
    );
  }

  atualizar(instalacaoTipoPleito: InstalacaoTipoPleito): Observable<InstalacaoTipoPleito> {
    const url = `${c.api}/InstalacaoTipoPleito`;

    return this.http.put<InstalacaoTipoPleito>(url, instalacaoTipoPleito).pipe(
      map((obj) => obj)
    );
  }

  deletar(codInstalacaoTipoPleito: number): Observable<InstalacaoTipoPleito> {
    const url = `${c.api}/InstalacaoTipoPleito/${codInstalacaoTipoPleito}`;

    return this.http.delete<InstalacaoTipoPleito>(url).pipe(
      map((obj) => obj)
    );
  }
}
