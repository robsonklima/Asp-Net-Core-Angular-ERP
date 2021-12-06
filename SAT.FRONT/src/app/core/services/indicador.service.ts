import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Indicador, IndicadorParameters } from '../types/indicador.types';
import { DashboardTecnicoDisponibilidadeTecnicoViewModel } from '../types/tecnico.types';

@Injectable({
  providedIn: 'root'
})
export class IndicadorService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: IndicadorParameters): Observable<Indicador[]> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Indicador`, { params: params }).pipe(
      map((data: Indicador[]) => data)
    )
  }

  obterIndicadoresFiliais(): Observable<Indicador[]> {
    const url = `${c.api}/Indicador/IndicadoresFiliais`;
    return this.http.get<Indicador[]>(url).pipe(
      map((obj) => obj)
    );
  }

  obterIndicadoresDisponibilidadeTecnicos(parameters: IndicadorParameters): Observable<DashboardTecnicoDisponibilidadeTecnicoViewModel[]> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Indicador/DisponibilidadeTecnicos`, { params: params }).pipe(
      map((data: DashboardTecnicoDisponibilidadeTecnicoViewModel[]) => data)
    );
  }
}
