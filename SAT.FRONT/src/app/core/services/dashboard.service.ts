import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ViewDadosDashboard, ViewDadosDashboardParameters } from '../types/dashboard.types';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  constructor(private http: HttpClient) { }

  obterViewPorParametros(parameters: ViewDadosDashboardParameters): Observable<ViewDadosDashboard> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Dashboard`, { params: params }).pipe(
      map((data: ViewDadosDashboard) => data)
    )
  }
}
