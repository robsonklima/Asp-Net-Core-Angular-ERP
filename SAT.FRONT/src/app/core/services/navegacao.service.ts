import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { NavegacaoParameters, NavegacaoData, Navegacao } from '../types/navegacao.types';



@Injectable({
  providedIn: 'root'
})
export class NavegacaoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: NavegacaoParameters): Observable<NavegacaoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Navegacao`, { params: params }).pipe(
      map((data: NavegacaoData) => data)
    )
  }

  obterPorCodigo(codNavegacao: number): Observable<Navegacao> {
    const url = `${c.api}/Navegacao/${codNavegacao}`;
    return this.http.get<Navegacao>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(navegacao: Navegacao): Observable<Navegacao> {
    return this.http.post<Navegacao>(`${c.api}/Navegacao`, navegacao).pipe(
      map((obj) => obj)
    );
  }

  atualizar(navegacao: Navegacao): Observable<Navegacao> {
    const url = `${c.api}/Navegacao`;

    return this.http.put<Navegacao>(url, navegacao).pipe(
      map((obj) => obj)
    );
  }

  deletar(codNavegacao: number): Observable<Navegacao> {
    const url = `${c.api}/Navegacao/${codNavegacao}`;

    return this.http.delete<Navegacao>(url).pipe(
      map((obj) => obj)
    );
  }
}
