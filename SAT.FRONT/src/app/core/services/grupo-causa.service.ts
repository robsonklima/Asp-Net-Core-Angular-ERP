import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { GrupoCausa, GrupoCausaData, GrupoCausaParameters } from '../types/grupo-causa.types';

@Injectable({
    providedIn: 'root'
})
export class GrupoCausaService {
    constructor(private http: HttpClient) {}
    
    obterPorParametros(parameters: GrupoCausaParameters): Observable<GrupoCausaData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/GrupoCausa`, { params: params }).pipe(
            map((data: GrupoCausaData) => data)
        )
    }

    obterPorCodigo(codGrupoCausa: number): Observable<GrupoCausa> {
        const url = `${c.api}/Causa/${codGrupoCausa}`;
        return this.http.get<GrupoCausa>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(grupoCausa: GrupoCausa): Observable<GrupoCausa> {
        return this.http.post<GrupoCausa>(`${c.api}/GrupoCausa`, grupoCausa).pipe(
            map((obj) => obj)
        );
    }

    atualizar(grupoCausa: GrupoCausa): Observable<GrupoCausa> {
        const url = `${c.api}/GrupoCausa`;

        return this.http.put<GrupoCausa>(url, grupoCausa).pipe(
            map((obj) => obj)
        );
    }

    deletar(codGrupoCausa: number): Observable<GrupoCausa> {
        const url = `${c.api}/GrupoCausa/${codGrupoCausa}`;

        return this.http.delete<GrupoCausa>(url).pipe(
            map((obj) => obj)
        );
    }
}