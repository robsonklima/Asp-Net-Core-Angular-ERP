import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ORDefeitoParameters, ORDefeitoData, ORDefeito } from '../types/or-defeito.types';

@Injectable({
    providedIn: 'root'
})
export class ORDefeitoService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: ORDefeitoParameters): Observable<ORDefeitoData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/ORDefeito`, { params: params }).pipe(
          map((data: ORDefeitoData) => data)
        )
    }

    obterPorCodigo(codDefeito: number): Observable<ORDefeito> {
        const url = `${c.api}/ORDefeito/${codDefeito}`;
        return this.http.get<ORDefeito>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(orDefeito: ORDefeito): Observable<ORDefeito> {
        return this.http.post<ORDefeito>(`${c.api}/ORDefeito`, orDefeito).pipe(
            map((obj) => obj)
        );
    }

    atualizar(orDefeito: ORDefeito): Observable<ORDefeito> {
        const url = `${c.api}/ORDefeito`;
        
        return this.http.put<ORDefeito>(url, orDefeito).pipe(
            map((obj) => obj)
        );
    }

    deletar(codDefeito: number): Observable<ORDefeito> {
        const url = `${c.api}/ORDefeito/${codDefeito}`;
        
        return this.http.delete<ORDefeito>(url).pipe(
          map((obj) => obj)
        );
    }
}