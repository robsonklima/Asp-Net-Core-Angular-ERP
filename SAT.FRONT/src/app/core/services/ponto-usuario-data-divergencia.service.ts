import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { PontoUsuarioDataDivergencia, PontoUsuarioDataDivergenciaData, PontoUsuarioDataDivergenciaParameters } from '../types/ponto-usuario-data-divergencia.types';

@Injectable({
  providedIn: 'root'
})
export class PontoUsuarioDataDivergenciaService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: PontoUsuarioDataDivergenciaParameters): Observable<PontoUsuarioDataDivergenciaData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/PontoUsuarioDataDivergencia`, { params: params }).pipe(
      map((data: PontoUsuarioDataDivergenciaData) => data)
    )
  }

  obterPorCodigo(codPontoUsuarioDataDivergencia: number): Observable<PontoUsuarioDataDivergencia> {
    const url = `${c.api}/PontoUsuarioDataDivergencia/${codPontoUsuarioDataDivergencia}`;

    return this.http.get<PontoUsuarioDataDivergencia>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(pontoUsuarioDataDivergencia: PontoUsuarioDataDivergencia): Observable<PontoUsuarioDataDivergencia> {
    return this.http.post<PontoUsuarioDataDivergencia>(`${c.api}/PontoUsuarioDataDivergencia`, pontoUsuarioDataDivergencia).pipe(
      map((obj) => obj)
    );
  }

  atualizar(pontoUsuarioDataDivergencia: PontoUsuarioDataDivergencia): Observable<PontoUsuarioDataDivergencia> {
    const url = `${c.api}/PontoUsuarioDataDivergencia`;
    return this.http.put<PontoUsuarioDataDivergencia>(url, pontoUsuarioDataDivergencia).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPontoUsuarioDataDivergencia: number): Observable<PontoUsuarioDataDivergencia> {
    const url = `${c.api}/PontoUsuarioDataDivergencia/${codPontoUsuarioDataDivergencia}`;
    
    return this.http.delete<PontoUsuarioDataDivergencia>(url).pipe(
      map((obj) => obj)
    );
  }
}
