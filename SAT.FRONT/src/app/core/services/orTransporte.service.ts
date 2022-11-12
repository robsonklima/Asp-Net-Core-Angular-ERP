import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ORTransporte, ORTransporteData, ORTransporteParameters } from '../types/ORTransporte.types';

@Injectable({
    providedIn: 'root'
})
export class ORTransporteService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: ORTransporteParameters): Observable<ORTransporteData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/ORTransporte`, { params: params }).pipe(
          map((data: ORTransporteData) => data)
        )
    }

    obterPorCodigo(codTransportadora: number): Observable<ORTransporte> {
        const url = `${c.api}/ORTransporte/${codTransportadora}`;
        return this.http.get<ORTransporte>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(ORTransporte: ORTransporte): Observable<ORTransporte> {
        return this.http.post<ORTransporte>(`${c.api}/ORTransporte`, ORTransporte).pipe(
            map((obj) => obj)
        );
    }

    atualizar(ORTransporte: ORTransporte): Observable<ORTransporte> {
        const url = `${c.api}/ORTransporte`;
        
        return this.http.put<ORTransporte>(url, ORTransporte).pipe(
            map((obj) => obj)
        );
    }

    deletar(codTransportadora: number): Observable<ORTransporte> {
        const url = `${c.api}/ORTransporte/${codTransportadora}`;
        
        return this.http.delete<ORTransporte>(url).pipe(
          map((obj) => obj)
        );
    }
}