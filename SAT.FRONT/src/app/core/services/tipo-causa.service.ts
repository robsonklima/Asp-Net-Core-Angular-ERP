import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { TipoCausa, TipoCausaData, TipoCausaParameters } from '../types/tipo-causa.types';

@Injectable({
    providedIn: 'root'
})
export class TipoCausaService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: TipoCausaParameters): Observable<TipoCausaData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/TipoCausa`, { params: params }).pipe(
            map((data: TipoCausaData) => data)
        )
    }

    obterPorCodigo(codTipoCausa: number): Observable<TipoCausa> {
        const url = `${c.api}/TipoCausa/${codTipoCausa}`;
        return this.http.get<TipoCausa>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(tipoCausa: TipoCausa): Observable<TipoCausa> {
        return this.http.post<TipoCausa>(`${c.api}/TipoCausa`, tipoCausa).pipe(
            map((obj) => obj)
        );
    }

    atualizar(tipoCausa: TipoCausa): Observable<TipoCausa> {
        const url = `${c.api}/TipoCausa`;

        return this.http.put<TipoCausa>(url, tipoCausa).pipe(
            map((obj) => obj)
        );
    }

    deletar(codTipoCausa: number): Observable<TipoCausa> {
        const url = `${c.api}/TipoCausa/${codTipoCausa}`;

        return this.http.delete<TipoCausa>(url).pipe(
            map((obj) => obj)
        );
    }
}