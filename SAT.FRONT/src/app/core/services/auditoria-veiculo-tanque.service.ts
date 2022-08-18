import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { AuditoriaVeiculoTanque, AuditoriaVeiculoTanqueData, AuditoriaVeiculoTanqueParameters } from '../types/auditoria-veiculo-Tanque.types';


@Injectable({
  providedIn: 'root'
})
export class AuditoriaVeiculoTanqueService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: AuditoriaVeiculoTanqueParameters): Observable<AuditoriaVeiculoTanqueData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/AuditoriaVeiculoTanque`, { params: params }).pipe(
      map((data: AuditoriaVeiculoTanqueData) => data)
    )
  }

  obterPorCodigo(codAuditoriaVeiculoTanque: number): Observable<AuditoriaVeiculoTanque> {
    const url = `${c.api}/AuditoriaVeiculoTanque/${codAuditoriaVeiculoTanque}`;
    return this.http.get<AuditoriaVeiculoTanque>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(auditoriaVeiculoTanque: AuditoriaVeiculoTanque): Observable<AuditoriaVeiculoTanque> {
    return this.http.post<AuditoriaVeiculoTanque>(`${c.api}/AuditoriaVeiculoTanque`, auditoriaVeiculoTanque).pipe(
      map((obj) => obj)
    );
  }

  atualizar(auditoriaVeiculoTanque: AuditoriaVeiculoTanque): Observable<AuditoriaVeiculoTanque> {
    const url = `${c.api}/AuditoriaVeiculoTanque`;

    return this.http.put<AuditoriaVeiculoTanque>(url, auditoriaVeiculoTanque).pipe(
      map((obj) => obj)
    );
  }

  deletar(codAuditoriaVeiculoTanque: number): Observable<AuditoriaVeiculoTanque> {
    const url = `${c.api}/AuditoriaVeiculoTanque/${codAuditoriaVeiculoTanque}`;

    return this.http.delete<AuditoriaVeiculoTanque>(url).pipe(
      map((obj) => obj)
    );
  }
}
