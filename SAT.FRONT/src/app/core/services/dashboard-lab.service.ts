import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

import { appConfig as c } from 'app/core/config/app.config'
import { DashboardLabParameters, ViewDashboardLabRecebidosReparados } from '../types/dashboard-lab.types';

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
}