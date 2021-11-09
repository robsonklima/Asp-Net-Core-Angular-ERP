import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { PontoUsuarioData, PontoUsuarioDataData, PontoUsuarioDataParameters } from '../types/ponto-usuario-data.types';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class PontoUsuarioDataService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: PontoUsuarioDataParameters): Observable<PontoUsuarioDataData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/PontoUsuarioData`, { params: params }).pipe(
      map((data: PontoUsuarioDataData) => data)
    )
  }

  obterPorCodigo(codPontoUsuarioData: number): Observable<PontoUsuarioData> {
    const url = `${c.api}/PontoUsuarioData/${codPontoUsuarioData}`;

    return this.http.get<PontoUsuarioData>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(pontoUsuarioData: PontoUsuarioData): Observable<PontoUsuarioData> {
    return this.http.post<PontoUsuarioData>(`${c.api}/PontoUsuarioData`, pontoUsuarioData).pipe(
      map((obj) => obj)
    );
  }

  atualizar(pontoUsuarioData: PontoUsuarioData): Observable<PontoUsuarioData> {
    const url = `${c.api}/PontoUsuarioData`;
    return this.http.put<PontoUsuarioData>(url, pontoUsuarioData).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPontoUsuarioData: number): Observable<PontoUsuarioData> {
    const url = `${c.api}/PontoUsuarioData/${codPontoUsuarioData}`;
    
    return this.http.delete<PontoUsuarioData>(url).pipe(
      map((obj) => obj)
    );
  }
}
