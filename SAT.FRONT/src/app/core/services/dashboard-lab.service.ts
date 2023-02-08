import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DashboardLabParameters, ViewDashboardLabProdutividadeTecnica, ViewDashboardLabRecebidosReparados, ViewDashboardLabTopFaltantes, ViewDashboardLabTopTempoMedioReparo } from '../types/dashboard-lab.types';

@Injectable({
  providedIn: 'root'
})
export class DashboardLabService {
  constructor(private http: HttpClient) {}

  obterRecebidosReparados(parameters: DashboardLabParameters): Observable<ViewDashboardLabRecebidosReparados[]> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/DashboardLab/RecebidosReparados`, { params: params }).pipe(
      map((data: ViewDashboardLabRecebidosReparados[]) => data)
    )
  }

  obterTopFaltantes(parameters: DashboardLabParameters): Observable<ViewDashboardLabTopFaltantes[]> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/DashboardLab/TopFaltantes`, { params: params }).pipe(
      map((data: ViewDashboardLabTopFaltantes[]) => data)
    )
  }

  obterTempoMedioReparo(parameters: DashboardLabParameters): Observable<ViewDashboardLabTopTempoMedioReparo[]> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/DashboardLab/TempoMedioReparo`, { params: params }).pipe(
      map((data: ViewDashboardLabTopTempoMedioReparo[]) => data)
    )
  }

  obterProdutividadeTecnica(parameters: DashboardLabParameters): Observable<ViewDashboardLabProdutividadeTecnica[]> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/DashboardLab/ProdutividadeTecnica`, { params: params }).pipe(
      map((data: ViewDashboardLabProdutividadeTecnica[]) => data)
    )
  }
}
