import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { appConfig as c } from 'app/core/config/app.config'
import { BancadaLaboratorio, BancadaLaboratorioData, BancadaLaboratorioParameters, ViewLaboratorioTecnicoBancada } from '../types/bancada-laboratorio.types';
import { map } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class BancadaLaboratorioService {
  constructor(
    private http: HttpClient
  ) { }

  obterPorView(parameters: BancadaLaboratorioParameters): Observable<ViewLaboratorioTecnicoBancada[]> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/BancadaLaboratorio/TecnicosBancada`, { params: params }).pipe(
      map((data: ViewLaboratorioTecnicoBancada[]) => data)
    )
  }

  obterPorParametros(parameters: BancadaLaboratorioParameters): Observable<BancadaLaboratorioData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/BancadaLaboratorio`, { params: params }).pipe(
      map((data: BancadaLaboratorioData) => data)
    )
  }

  obterPorCodigo(codBancadaLaboratorio: string): Observable<BancadaLaboratorio> {
    const url = `${c.api}/BancadaLaboratorio/${codBancadaLaboratorio}`;
    return this.http.get<BancadaLaboratorio>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(bancadaLaboratorio: BancadaLaboratorio): Observable<BancadaLaboratorio> {
    return this.http.post<BancadaLaboratorio>(`${c.api}/BancadaLaboratorio`, bancadaLaboratorio).pipe(
      map((obj) => obj)
    );
  }

  atualizar(bancadaLaboratorio: BancadaLaboratorio): Observable<BancadaLaboratorio> {
    const url = `${c.api}/BancadaLaboratorio`;

    return this.http.put<BancadaLaboratorio>(url, bancadaLaboratorio).pipe(
      map((obj) => obj)
    );
  }

  deletar(codBancadaLaboratorio: number): Observable<BancadaLaboratorio> {
    const url = `${c.api}/BancadaLaboratorio/${codBancadaLaboratorio}`;

    return this.http.delete<BancadaLaboratorio>(url).pipe(
      map((obj) => obj)
    );
  }
}
