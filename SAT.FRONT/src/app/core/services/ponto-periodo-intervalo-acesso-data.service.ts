import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { PontoPeriodoIntervaloAcessoData, PontoPeriodoIntervaloAcessoDataData, PontoPeriodoIntervaloAcessoDataParameters } from '../types/ponto-periodo-intervalo-acesso-data.types';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class PontoPeriodoIntervaloAcessoDataIntervaloAcessoDataService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: PontoPeriodoIntervaloAcessoDataParameters): Observable<PontoPeriodoIntervaloAcessoDataData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/PontoPeriodoIntervaloAcessoData`, { params: params }).pipe(
      map((data: PontoPeriodoIntervaloAcessoDataData) => data)
    )
  }

  obterPorCodigo(codPontoPeriodoIntervaloAcessoData: number): Observable<PontoPeriodoIntervaloAcessoData> {
    const url = `${c.api}/PontoPeriodoIntervaloAcessoData/${codPontoPeriodoIntervaloAcessoData}`;

    return this.http.get<PontoPeriodoIntervaloAcessoData>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(pontoPeriodoIntervaloAcessoData: PontoPeriodoIntervaloAcessoData): Observable<PontoPeriodoIntervaloAcessoData> {
    return this.http.post<PontoPeriodoIntervaloAcessoData>(`${c.api}/PontoPeriodoIntervaloAcessoData`, pontoPeriodoIntervaloAcessoData).pipe(
      map((obj) => obj)
    );
  }

  atualizar(pontoPeriodoIntervaloAcessoData: PontoPeriodoIntervaloAcessoData): Observable<PontoPeriodoIntervaloAcessoData> {
    const url = `${c.api}/PontoPeriodoIntervaloAcessoData`;
    return this.http.put<PontoPeriodoIntervaloAcessoData>(url, pontoPeriodoIntervaloAcessoData).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPontoPeriodoIntervaloAcessoData: number): Observable<PontoPeriodoIntervaloAcessoData> {
    const url = `${c.api}/PontoPeriodoIntervaloAcessoData/${codPontoPeriodoIntervaloAcessoData}`;
    
    return this.http.delete<PontoPeriodoIntervaloAcessoData>(url).pipe(
      map((obj) => obj)
    );
  }
}
