import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Autorizada, AutorizadaData, AutorizadaParameters } from '../types/autorizada.types';
import { statusConst } from '../types/status-types';

@Injectable({
  providedIn: 'root'
})
export class AutorizadaService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: AutorizadaParameters): Observable<AutorizadaData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Autorizada`, { params: params }).pipe(
      map((data: AutorizadaData) => data)
    )
  }

  obterPorCodigo(codAutorizada: number): Observable<Autorizada> {
    const url = `${c.api}/Autorizada/${codAutorizada}`;
    return this.http.get<Autorizada>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(acordoNivelServico: Autorizada): Observable<Autorizada> {
    return this.http.post<Autorizada>(`${c.api}/Autorizada`, acordoNivelServico).pipe(
      map((obj) => obj)
    );
  }

  atualizar(autorizada: Autorizada): Observable<Autorizada> {
    const url = `${c.api}/Autorizada`;
    return this.http.put<Autorizada>(url, autorizada).pipe(
      map((obj) => obj)
    );
  }

  deletar(codAutorizada: number): Observable<Autorizada> {
    const url = `${c.api}/Autorizada/${codAutorizada}`;

    return this.http.delete<Autorizada>(url).pipe(
      map((obj) => obj)
    );
  }

  async obterAutorizadas(codFilial?: number, filtro: string = ''): Promise<Autorizada[]> {

    const params: AutorizadaParameters = {
      sortActive: 'razaoSocial',
      sortDirection: 'asc',
      codFilial: codFilial,
      pageSize: 1000,
      indAtivo: statusConst.ATIVO,
      filter: filtro
    }
    return (await this.obterPorParametros(params).toPromise()).items;
  }
}
