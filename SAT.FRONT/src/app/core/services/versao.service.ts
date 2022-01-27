import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Versao, VersaoData, VersaoParameters } from '../types/versao.types';

@Injectable({
    providedIn: 'root'
})
export class VersaoService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: VersaoParameters): Observable<VersaoData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/Versao`, { params: params }).pipe(
            map((data: VersaoData) => data)
        )
    }

    obterPorCodigo(codVersao: number): Observable<Versao> {
        const url = `${c.api}/Versao/${codVersao}`;
        return this.http.get<Versao>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(versao: Versao): Observable<Versao> {
        return this.http.post<Versao>(`${c.api}/Versao`, versao).pipe(
            map((obj) => obj)
        );
    }

    atualizar(versao: Versao): Observable<Versao> {
        const url = `${c.api}/Versao`;

        return this.http.put<Versao>(url, versao).pipe(
            map((obj) => obj)
        );
    }

    deletar(codVersao: number): Observable<Versao> {
        const url = `${c.api}/Versao/${codVersao}`;

        return this.http.delete<Versao>(url).pipe(
            map((obj) => obj)
        );
    }
}