import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { MotivoComunicacao, MotivoComunicacaoData, MotivoComunicacaoParameters } from 'app/core/types/motivo-comunicacao.types'

@Injectable({
    providedIn: 'root'
})
export class MotivoComunicacaoService {
    constructor(private http: HttpClient) { }

    criar(op: MotivoComunicacao): Observable<MotivoComunicacao> {
        return this.http.post<MotivoComunicacao>(`${c.api}/MotivoComunicacao`, op).pipe(
            map((obj) => obj)
        );
    }

    obterPorCodigo(cod: number): Observable<MotivoComunicacao> {
        const url = `${c.api}/MotivoComunicacao/${cod}`;
        return this.http.get<MotivoComunicacao>(url).pipe(
            map((obj) => obj)
        );
    }

    obterPorParametros(parameters: MotivoComunicacaoParameters): Observable<MotivoComunicacaoData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/MotivoComunicacao`, { params: params }).pipe(
            map((data: MotivoComunicacaoData) => data)
        )
    }

    atualizar(op: MotivoComunicacao): Observable<MotivoComunicacao> {
        const url = `${c.api}/MotivoComunicacao`;
        return this.http.put<MotivoComunicacao>(url, op).pipe(
            map((obj) => obj)
        );
    }

    deletar(cod: number): Observable<MotivoComunicacao> {
        const url = `${c.api}/MotivoComunicacao/${cod}`;

        return this.http.delete<MotivoComunicacao>(url).pipe(
            map((obj) => obj)
        );
    }
}