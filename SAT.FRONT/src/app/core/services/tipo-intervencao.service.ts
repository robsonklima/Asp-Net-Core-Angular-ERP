import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { TipoIntervencao, TipoIntervencaoData, TipoIntervencaoParameters } from '../types/tipo-intervencao.types';

@Injectable({
  providedIn: 'root'
})
export class TipoIntervencaoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: TipoIntervencaoParameters): Observable<TipoIntervencaoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/TipoIntervencao`, { params: params }).pipe(
      map((data: TipoIntervencaoData) => data)
    )
  }

  obterPorCodigo(codTipoIntervencao: number): Observable<TipoIntervencao> {
    const url = `${c.api}/TipoIntervencao/${codTipoIntervencao}`;
    return this.http.get<TipoIntervencao>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(tipoIntervencao: TipoIntervencao): Observable<TipoIntervencao> {
    return this.http.post<TipoIntervencao>(`${c.api}/TipoIntervencao`, tipoIntervencao).pipe(
        map((obj) => obj)
    );
}

atualizar(tipoIntervencao: TipoIntervencao): Observable<TipoIntervencao> {
    const url = `${c.api}/TipoIntervencao`;
    
    return this.http.put<TipoIntervencao>(url, tipoIntervencao).pipe(
        map((obj) => obj)
    );
}

deletar(codTipoIntervencao: number): Observable<TipoIntervencao> {
    const url = `${c.api}/Feriado/${codTipoIntervencao}`;
    
    return this.http.delete<TipoIntervencao>(url).pipe(
      map((obj) => obj)
    );
  }
}
