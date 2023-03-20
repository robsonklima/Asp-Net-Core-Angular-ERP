import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { InstalacaoTipoParcela, InstalacaoTipoParcelaData, InstalacaoTipoParcelaParameters } from '../types/instalacao-tipo-parcela.types';

@Injectable({
  providedIn: 'root'
})
export class InstalacaoTipoParcelaService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: InstalacaoTipoParcelaParameters): Observable<InstalacaoTipoParcelaData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/InstalacaoTipoParcela`, { params: params }).pipe(
      map((data: InstalacaoTipoParcelaData) => data)
    )
  }

  obterPorCodigo(codInstalacaoTipoParcela: number): Observable<InstalacaoTipoParcela> {
    const url = `${c.api}/InstalacaoTipoParcela/${codInstalacaoTipoParcela}`;
    return this.http.get<InstalacaoTipoParcela>(url).pipe(
      map((obj) => obj)
    );
}

  criar(instalLote: InstalacaoTipoParcela): Observable<InstalacaoTipoParcela> {
    return this.http.post<InstalacaoTipoParcela>(`${c.api}/InstalacaoTipoParcela`, instalLote).pipe(
      map((obj) => obj)
    );
  }

  atualizar(instalLote: InstalacaoTipoParcela): Observable<InstalacaoTipoParcela> {
    const url = `${c.api}/InstalacaoTipoParcela`;

    return this.http.put<InstalacaoTipoParcela>(url, instalLote).pipe(
      map((obj) => obj)
    );
  }

  deletar(codInstalacaoTipoParcela: number): Observable<InstalacaoTipoParcela> {
    const url = `${c.api}/InstalacaoTipoParcela/${codInstalacaoTipoParcela}`;

    return this.http.delete<InstalacaoTipoParcela>(url).pipe(
      map((obj) => obj)
    );
  }
}
