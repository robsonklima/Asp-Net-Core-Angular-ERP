import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { TipoServico, TipoServicoData, TipoServicoParameters } from '../types/tipo-servico.types';

@Injectable({
  providedIn: 'root'
})
export class TipoServicoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: TipoServicoParameters): Observable<TipoServicoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/TipoServico`, { params: params }).pipe(
      map((data: TipoServicoData) => data)
    )
  }

  obterPorCodigo(codServico: number): Observable<TipoServico> {
    const url = `${c.api}/TipoServico/${codServico}`;
    return this.http.get<TipoServico>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(tipoServico: TipoServico): Observable<TipoServico> {
    return this.http.post<TipoServico>(`${c.api}/TipoServico`, tipoServico).pipe(
      map((obj) => obj)
    );
  }

  atualizar(tipoServico: TipoServico): Observable<TipoServico> {
    const url = `${c.api}/TipoServico`;

    return this.http.put<TipoServico>(url, tipoServico).pipe(
      map((obj) => obj)
    );
  }

  deletar(codServico: number): Observable<TipoServico> {
    const url = `${c.api}/TipoServico/${codServico}`;

    return this.http.delete<TipoServico>(url).pipe(
      map((obj) => obj)
    );
  }
}
