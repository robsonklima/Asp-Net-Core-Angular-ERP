import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Causa, CausaData, CausaParameters } from '../types/causa.types';

@Injectable({
    providedIn: 'root'
})
export class CausaService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: CausaParameters): Observable<CausaData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/Causa`, { params: params }).pipe(
            map((data: CausaData) => data)
        )
    }

    obterPorCodigo(codCausa: number): Observable<Causa> {
        const url = `${c.api}/Causa/${codCausa}`;
        return this.http.get<Causa>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(causa: Causa): Observable<Causa> {
        return this.http.post<Causa>(`${c.api}/Causa`, causa).pipe(
            map((obj) => obj)
        );
    }

    atualizar(causa: Causa): Observable<Causa> {
        const url = `${c.api}/Causa`;

        return this.http.put<Causa>(url, causa).pipe(
            map((obj) => obj)
        );
    }

    deletar(codCausa: number): Observable<Causa> {
        const url = `${c.api}/Causa/${codCausa}`;

        return this.http.delete<Causa>(url).pipe(
            map((obj) => obj)
        );
    }
}