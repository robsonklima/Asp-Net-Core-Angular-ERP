import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { AcordoNivelServico, AcordoNivelServicoData, AcordoNivelServicoParameters } from '../types/acordo-nivel-servico.types';

@Injectable({
  providedIn: 'root'
})
export class AcordoNivelServicoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: AcordoNivelServicoParameters): Observable<AcordoNivelServicoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/AcordoNivelServico`, { params: params }).pipe(
      map((data: AcordoNivelServicoData) => data)
    )
  }

  obterPorCodigo(codSLA: number): Observable<AcordoNivelServico> {
    const url = `${c.api}/AcordoNivelServico/${codSLA}`;
    return this.http.get<AcordoNivelServico>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(acordoNivelServico: AcordoNivelServico): Observable<AcordoNivelServico> {
    return this.http.post<AcordoNivelServico>(`${c.api}/AcordoNivelServico`, 
      acordoNivelServico).pipe(
      map((obj) => obj)
    );
  }

  atualizar(acordoNivelServico: AcordoNivelServico): Observable<AcordoNivelServico> {
    const url = `${c.api}/AcordoNivelServico`;
    return this.http.put<AcordoNivelServico>(url, acordoNivelServico).pipe(
      map((obj) => obj)
    );
  }

  deletar(codSLA: number): Observable<AcordoNivelServico> {
    const url = `${c.api}/AcordoNivelServico/${codSLA}`;
    
    return this.http.delete<AcordoNivelServico>(url).pipe(
      map((obj) => obj)
    );
  }
}
