import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { AuditoriaVeiculo, AuditoriaVeiculoData, AuditoriaVeiculoParameters } from '../types/auditoria-veiculo.types';


@Injectable({
  providedIn: 'root'
})
export class AuditoriaVeiculoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: AuditoriaVeiculoParameters): Observable<AuditoriaVeiculoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/AuditoriaVeiculo`, { params: params }).pipe(
      map((data: AuditoriaVeiculoData) => data)
    )
  }

  obterPorCodigo(codAuditoriaVeiculo: number): Observable<AuditoriaVeiculo> {
    const url = `${c.api}/AuditoriaVeiculo/${codAuditoriaVeiculo}`;
    return this.http.get<AuditoriaVeiculo>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(auditoriaVeiculo: AuditoriaVeiculo): Observable<AuditoriaVeiculo> {
    return this.http.post<AuditoriaVeiculo>(`${c.api}/AuditoriaVeiculo`, auditoriaVeiculo).pipe(
      map((obj) => obj)
    );
  }

  atualizar(auditoriaVeiculo: AuditoriaVeiculo): Observable<AuditoriaVeiculo> {
    const url = `${c.api}/AuditoriaVeiculo`;

    return this.http.put<AuditoriaVeiculo>(url, auditoriaVeiculo).pipe(
      map((obj) => obj)
    );
  }

  deletar(codAuditoriaVeiculo: number): Observable<AuditoriaVeiculo> {
    const url = `${c.api}/AuditoriaVeiculo/${codAuditoriaVeiculo}`;

    return this.http.delete<AuditoriaVeiculo>(url).pipe(
      map((obj) => obj)
    );
  }
}
