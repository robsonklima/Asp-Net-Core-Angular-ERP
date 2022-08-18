import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { AuditoriaVeiculoAcessorio, AuditoriaVeiculoAcessorioData, AuditoriaVeiculoAcessorioParameters } from '../types/auditoria-veiculo-acessorio.types';


@Injectable({
  providedIn: 'root'
})
export class AuditoriaVeiculoAcessorioService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: AuditoriaVeiculoAcessorioParameters): Observable<AuditoriaVeiculoAcessorioData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/AuditoriaVeiculoAcessorio`, { params: params }).pipe(
      map((data: AuditoriaVeiculoAcessorioData) => data)
    )
  }

  obterPorCodigo(codAuditoriaVeiculoAcessorio: number): Observable<AuditoriaVeiculoAcessorio> {
    const url = `${c.api}/AuditoriaVeiculoAcessorio/${codAuditoriaVeiculoAcessorio}`;
    return this.http.get<AuditoriaVeiculoAcessorio>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(auditoriaVeiculoAcessorio: AuditoriaVeiculoAcessorio): Observable<AuditoriaVeiculoAcessorio> {
    return this.http.post<AuditoriaVeiculoAcessorio>(`${c.api}/AuditoriaVeiculoAcessorio`, auditoriaVeiculoAcessorio).pipe(
      map((obj) => obj)
    );
  }

  atualizar(auditoriaVeiculoAcessorio: AuditoriaVeiculoAcessorio): Observable<AuditoriaVeiculoAcessorio> {
    const url = `${c.api}/AuditoriaVeiculoAcessorio`;

    return this.http.put<AuditoriaVeiculoAcessorio>(url, auditoriaVeiculoAcessorio).pipe(
      map((obj) => obj)
    );
  }

  deletar(codAuditoriaVeiculoAcessorio: number): Observable<AuditoriaVeiculoAcessorio> {
    const url = `${c.api}/AuditoriaVeiculoAcessorio/${codAuditoriaVeiculoAcessorio}`;

    return this.http.delete<AuditoriaVeiculoAcessorio>(url).pipe(
      map((obj) => obj)
    );
  }
}
