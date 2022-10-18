import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ORTipo, ORTipoData, ORTipoParameters } from '../types/or-tipo.types';

@Injectable({
    providedIn: 'root'
})
export class ORTipoService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: ORTipoParameters): Observable<ORTipoData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/ORTipo`, { params: params }).pipe(
          map((data: ORTipoData) => data)
        )
    }

    obterPorCodigo(codOR: number): Observable<ORTipo> {
        const url = `${c.api}/ORTipo/${codOR}`;
        return this.http.get<ORTipo>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(orTipo: ORTipo): Observable<ORTipo> {
        return this.http.post<ORTipo>(`${c.api}/ORTipo`, orTipo).pipe(
            map((obj) => obj)
        );
    }

    atualizar(orTipo: ORTipo): Observable<ORTipo> {
        const url = `${c.api}/ORTipo`;
        
        return this.http.put<ORTipo>(url, orTipo).pipe(
            map((obj) => obj)
        );
    }

    deletar(codOR: number): Observable<ORTipo> {
        const url = `${c.api}/ORTipo/${codOR}`;
        
        return this.http.delete<ORTipo>(url).pipe(
          map((obj) => obj)
        );
    }
}