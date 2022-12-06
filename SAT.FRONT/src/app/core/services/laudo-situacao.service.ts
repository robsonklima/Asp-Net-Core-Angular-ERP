import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { LaudoSituacaoParameters, LaudoSituacaoData, LaudoSituacao } from '../types/laudo-situacao.types';

@Injectable({
  providedIn: 'root'
})
export class LaudoSituacaoService
{
  constructor (
    private http: HttpClient
  ) { }

  obterPorParametros(parameters: LaudoSituacaoParameters): Observable<LaudoSituacaoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/LaudoSituacao`, { params: params }).pipe(
      map((data: LaudoSituacaoData) => data)
    )
  }

  obterPorCodigo(codLaudoSituacao: number): Observable<LaudoSituacao>
  {
    const url = `${c.api}/LaudoSituacao/${codLaudoSituacao}`;

    return this.http.get<LaudoSituacao>(url).pipe(
      map((obj) => obj)
    );
  }

  atualizar(laudoSituacao: LaudoSituacao): Observable<LaudoSituacao> {
    const url = `${c.api}/LaudoSituacao`;

    return this.http.put<LaudoSituacao>(url, laudoSituacao).pipe(
      map((obj) => obj)
    );
  }

  criar(laudoSituacao: LaudoSituacao): Observable<LaudoSituacao>
  {
    return this.http.post<LaudoSituacao>(`${c.api}/LaudoSituacao`, laudoSituacao).pipe(
      map((obj) => obj)
    );
  }

}
