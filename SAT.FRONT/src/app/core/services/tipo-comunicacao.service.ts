import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { TipoComunicacao, TipoComunicacaoData, TipoComunicacaoParameters } from 'app/core/types/tipo-comunicacao.types'

@Injectable({
    providedIn: 'root'
})
export class TipoComunicacaoService {
    constructor(private http: HttpClient) { }

    criar(tipo: TipoComunicacao): Observable<TipoComunicacao> {
        return this.http.post<TipoComunicacao>(`${c.api}/TipoComunicacao`, tipo).pipe(
            map((obj) => obj)
        );
    }

    obterPorCodigo(cod: number): Observable<TipoComunicacao> {
        const url = `${c.api}/TipoComunicacao/${cod}`;
        return this.http.get<TipoComunicacao>(url).pipe(
            map((obj) => obj)
        );
    }

    obterPorParametros(parameters: TipoComunicacaoParameters): Observable<TipoComunicacaoData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/TipoComunicacao`, { params: params }).pipe(
            map((data: TipoComunicacaoData) => data)
        )
    }

    atualizar(tipo: TipoComunicacao): Observable<TipoComunicacao> {
        const url = `${c.api}/TipoComunicacao`;
        return this.http.put<TipoComunicacao>(url, tipo).pipe(
            map((obj) => obj)
        );
    }

    deletar(cod: number): Observable<TipoComunicacao> {
        const url = `${c.api}/TipoComunicacao/${cod}`;

        return this.http.delete<TipoComunicacao>(url).pipe(
            map((obj) => obj)
        );
    }
}