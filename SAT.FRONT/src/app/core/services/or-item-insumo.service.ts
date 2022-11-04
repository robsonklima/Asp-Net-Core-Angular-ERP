import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ORItemInsumo, ORItemInsumoData, ORItemInsumoParameters } from '../types/or-item-insumo.types';

@Injectable({
    providedIn: 'root'
})
export class ORItemInsumoService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: ORItemInsumoParameters): Observable<ORItemInsumoData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/ORItemInsumo`, { params: params }).pipe(
          map((data: ORItemInsumoData) => data)
        )
    }

    obterPorCodigo(codORItemInsumo: number): Observable<ORItemInsumo> {
        const url = `${c.api}/ORItemInsumo/${codORItemInsumo}`;
        return this.http.get<ORItemInsumo>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(insumo: ORItemInsumo): Observable<ORItemInsumo> {
        return this.http.post<ORItemInsumo>(`${c.api}/ORItemInsumo`, insumo).pipe(
            map((obj) => obj)
        );
    }

    atualizar(insumo: ORItemInsumo): Observable<ORItemInsumo> {
        const url = `${c.api}/ORItemInsumo`;
        
        return this.http.put<ORItemInsumo>(url, insumo).pipe(
            map((obj) => obj)
        );
    }

    deletar(codORItemInsumo: number): Observable<ORItemInsumo> {
        const url = `${c.api}/ORItemInsumo/${codORItemInsumo}`;
        
        return this.http.delete<ORItemInsumo>(url).pipe(
          map((obj) => obj)
        );
    }
}