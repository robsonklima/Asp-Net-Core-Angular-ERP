import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { NavegacaoConfiguracao, NavegacaoConfiguracaoData, NavegacaoConfiguracaoParameters } from '../types/navegacao-configuracao.types';



@Injectable({
  providedIn: 'root'
})
export class NavegacaoConfiguracaoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: NavegacaoConfiguracaoParameters): Observable<NavegacaoConfiguracaoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/NavegacaoConfiguracao`, { params: params }).pipe(
      map((data: NavegacaoConfiguracaoData) => data)
    )
  }

  obterPorCodigo(codNavegacaoConfiguracao: number): Observable<NavegacaoConfiguracao> {
    const url = `${c.api}/NavegacaoConfiguracao/${codNavegacaoConfiguracao}`;
    return this.http.get<NavegacaoConfiguracao>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(navegacaoConfiguracao: NavegacaoConfiguracao): Observable<NavegacaoConfiguracao> {
    return this.http.post<NavegacaoConfiguracao>(`${c.api}/NavegacaoConfiguracao`, navegacaoConfiguracao).pipe(
      map((obj) => obj)
    );
  }

  atualizar(navegacaoConfiguracao: NavegacaoConfiguracao): Observable<NavegacaoConfiguracao> {
    const url = `${c.api}/NavegacaoConfiguracao`;

    return this.http.put<NavegacaoConfiguracao>(url, navegacaoConfiguracao).pipe(
      map((obj) => obj)
    );
  }

  deletar(codNavegacaoConfiguracao: number): Observable<NavegacaoConfiguracao> {
    const url = `${c.api}/NavegacaoConfiguracao/${codNavegacaoConfiguracao}`;

    return this.http.delete<NavegacaoConfiguracao>(url).pipe(
      map((obj) => obj)
    );
  }
}
