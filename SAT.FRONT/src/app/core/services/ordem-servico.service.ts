import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrdemServico, OrdemServicoData, OrdemServicoParameters } from '../types/ordem-servico.types';

@Injectable({
  providedIn: 'root'
})
export class OrdemServicoService
{
  constructor (
    private http: HttpClient
  ) { }

  obterPorParametros(parameters: OrdemServicoParameters): Observable<OrdemServicoData>
  {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key =>
    {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/OrdemServico`, { params: params }).pipe(
      map((data: OrdemServicoData) => data)
    )
  }

  obterPorCodigo(codOS: number): Observable<OrdemServico>
  {
    const url = `${c.api}/OrdemServico/${codOS}`;

    return this.http.get<OrdemServico>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(ordemServico: OrdemServico): Observable<OrdemServico>
  {
    return this.http.post<OrdemServico>(`${c.api}/OrdemServico`, ordemServico).pipe(
      map((obj) => obj)
    );
  }

  atualizar(ordemServico: OrdemServico): Observable<OrdemServico>
  {
    const url = `${c.api}/OrdemServico`;

    return this.http.put<OrdemServico>(url, ordemServico).pipe(
      map((obj) => obj)
    );
  }

  deletar(codOS: number): Observable<OrdemServico>
  {
    const url = `${c.api}/OrdemServico/${codOS}`;

    return this.http.delete<OrdemServico>(url).pipe(
      map((obj) => obj)
    );
  }
}
