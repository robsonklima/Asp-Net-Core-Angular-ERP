import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { TipoContrato, TipoContratoData, TipoContratoParameters } from '../types/tipo-contrato.types';

@Injectable({
    providedIn: 'root'
})
export class TipoContratoService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: TipoContratoParameters): Observable<TipoContratoData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/TipoContrato`, { params: params }).pipe(
            map((data: TipoContratoData) => data)
        )
    }

    obterPorCodigo(codTipoContrato: number): Observable<TipoContrato> {
        const url = `${c.api}/TipoContrato/${codTipoContrato}`;
        return this.http.get<TipoContrato>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(tipoCausa: TipoContrato): Observable<TipoContrato> {
        return this.http.post<TipoContrato>(`${c.api}/TipoContrato`, tipoCausa).pipe(
            map((obj) => obj)
        );
    }

    atualizar(tipoCausa: TipoContrato): Observable<TipoContrato> {
        const url = `${c.api}/TipoContrato`;

        return this.http.put<TipoContrato>(url, tipoCausa).pipe(
            map((obj) => obj)
        );
    }

    deletar(codTipoContrato: number): Observable<TipoContrato> {
        const url = `${c.api}/TipoContrato/${codTipoContrato}`;

        return this.http.delete<TipoContrato>(url).pipe(
            map((obj) => obj)
        );
    }
}