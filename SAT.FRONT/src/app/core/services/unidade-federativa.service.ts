import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import {
    UnidadeFederativa, UnidadeFederativaData,
    UnidadeFederativaParameters
} from '../types/unidade-federativa.types';

@Injectable({
    providedIn: 'root'
})
export class UnidadeFederativaService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: UnidadeFederativaParameters): Observable<UnidadeFederativaData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/UnidadeFederativa`, { params: params }).pipe(
            map((data: UnidadeFederativaData) => data)
        )
    }

    criar(uf: UnidadeFederativa): Observable<UnidadeFederativa> {
        return this.http.post<UnidadeFederativa>(`${c.api}/UnidadeFederativa`, uf).pipe(
            map((obj) => obj)
        );
    }

    obterPorCodigo(codUF: number): Observable<UnidadeFederativa> {
        const url = `${c.api}/UnidadeFederativa/${codUF}`;
        return this.http.get<UnidadeFederativa>(url).pipe(
            map((obj) => obj)
        );
    }

    atualizar(uf: UnidadeFederativa): Observable<UnidadeFederativa> {
        const url = `${c.api}/UnidadeFederativa`;

        return this.http.put<UnidadeFederativa>(url, uf).pipe(
            map((obj) => obj)
        );
    }

    deletar(codUF: number): Observable<UnidadeFederativa> {
        const url = `${c.api}/UnidadeFederativa/${codUF}`;

        return this.http.delete<UnidadeFederativa>(url).pipe(
            map((obj) => obj)
        );
    }

    async obterUnidadesFederativas(codPais: number): Promise<UnidadeFederativa[]> {
        const params: UnidadeFederativaParameters = {
            sortActive: 'nomeUF',
            sortDirection: 'asc',
            codPais: codPais,
            pageSize: 50
        }
        return (await this.obterPorParametros(params).toPromise()).items;
    }
}