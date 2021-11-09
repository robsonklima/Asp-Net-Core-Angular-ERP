import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { PontoPeriodoModoAprovacao, PontoPeriodoModoAprovacaoData, PontoPeriodoModoAprovacaoParameters } from '../types/ponto-periodo-modo-aprovacao.types';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class PontoPeriodoModoAprovacaoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: PontoPeriodoModoAprovacaoParameters): Observable<PontoPeriodoModoAprovacaoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/PontoPeriodoModoAprovacao`, { params: params }).pipe(
      map((data: PontoPeriodoModoAprovacaoData) => data)
    )
  }

  obterPorCodigo(codPontoPeriodoModoAprovacao: number): Observable<PontoPeriodoModoAprovacao> {
    const url = `${c.api}/PontoPeriodoModoAprovacao/${codPontoPeriodoModoAprovacao}`;

    return this.http.get<PontoPeriodoModoAprovacao>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(pontoPeriodoModoAprovacao: PontoPeriodoModoAprovacao): Observable<PontoPeriodoModoAprovacao> {
    return this.http.post<PontoPeriodoModoAprovacao>(`${c.api}/PontoPeriodoModoAprovacao`, pontoPeriodoModoAprovacao).pipe(
      map((obj) => obj)
    );
  }

  atualizar(pontoPeriodoModoAprovacao: PontoPeriodoModoAprovacao): Observable<PontoPeriodoModoAprovacao> {
    const url = `${c.api}/PontoPeriodoModoAprovacao`;
    return this.http.put<PontoPeriodoModoAprovacao>(url, pontoPeriodoModoAprovacao).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPontoPeriodoModoAprovacao: number): Observable<PontoPeriodoModoAprovacao> {
    const url = `${c.api}/PontoPeriodoModoAprovacao/${codPontoPeriodoModoAprovacao}`;
    
    return this.http.delete<PontoPeriodoModoAprovacao>(url).pipe(
      map((obj) => obj)
    );
  }
}
