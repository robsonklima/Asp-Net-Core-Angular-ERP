import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { TipoIndiceReajuste, TipoIndiceReajusteData, TipoIndiceReajusteParameters } from '../types/tipo-indice-reajuste.types';

@Injectable({
    providedIn: 'root'
})
export class TipoIndiceReajusteService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: TipoIndiceReajusteParameters): Observable<TipoIndiceReajusteData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/TipoIndiceReajuste`, { params: params }).pipe(
            map((data: TipoIndiceReajusteData) => data)
        )
    }

    obterPorCodigo(codTipoIndiceReajuste: number): Observable<TipoIndiceReajuste> {
        const url = `${c.api}/TipoIndiceReajuste/${codTipoIndiceReajuste}`;
        return this.http.get<TipoIndiceReajuste>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(tipoCausa: TipoIndiceReajuste): Observable<TipoIndiceReajuste> {
        return this.http.post<TipoIndiceReajuste>(`${c.api}/TipoIndiceReajuste`, tipoCausa).pipe(
            map((obj) => obj)
        );
    }

    atualizar(tipoCausa: TipoIndiceReajuste): Observable<TipoIndiceReajuste> {
        const url = `${c.api}/TipoIndiceReajuste`;

        return this.http.put<TipoIndiceReajuste>(url, tipoCausa).pipe(
            map((obj) => obj)
        );
    }

    deletar(codTipoIndiceReajuste: number): Observable<TipoIndiceReajuste> {
        const url = `${c.api}/TipoIndiceReajuste/${codTipoIndiceReajuste}`;

        return this.http.delete<TipoIndiceReajuste>(url).pipe(
            map((obj) => obj)
        );
    }
}