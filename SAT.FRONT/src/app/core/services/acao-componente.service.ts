import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config';
import { AcaoComponente, AcaoComponenteData, AcaoComponenteParameters } from '../types/acao-componente.types';

@Injectable({
  providedIn: 'root'
})
export class AcaoComponenteService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: AcaoComponenteParameters): Observable<AcaoComponenteData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/AcaoComponente`, { params: params }).pipe(
      map((data: AcaoComponenteData) => data)
    )
  }

  obterPorCodigo(codAcao: number): Observable<AcaoComponente> {
    const url = `${c.api}/AcaoComponente/${codAcao}`;

    return this.http.get<AcaoComponente>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(acao: AcaoComponente): Observable<AcaoComponente> {
    return this.http.post<AcaoComponente>(`${c.api}/AcaoComponente`, acao).pipe(
      map((obj) => obj)
    );
  }

  atualizar(acao: AcaoComponente): Observable<AcaoComponente> {
    const url = `${c.api}/AcaoComponente`;
    return this.http.put<AcaoComponente>(url, acao).pipe(
      map((obj) => obj)
    );
  }

  deletar(codAcao: number): Observable<AcaoComponente> {
    const url = `${c.api}/AcaoComponente/${codAcao}`;

    return this.http.delete<AcaoComponente>(url).pipe(
      map((obj) => obj)
    );
  }
}
