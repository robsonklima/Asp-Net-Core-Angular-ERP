import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DespesaPeriodo, DespesaPeriodoData, DespesaPeriodoParameters } from '../types/despesa-periodo.types';

@Injectable({
    providedIn: 'root'
})
export class DespesaPeriodoService
{
    constructor (private http: HttpClient) { }

    obterPorParametros(parameters: DespesaPeriodoParameters): Observable<DespesaPeriodoData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/DespesaPeriodo`, { params: params })
            .pipe(map((data: DespesaPeriodoData) => data));
    }

    obterPorCodigo(codDespesaPeriodo: number): Observable<DespesaPeriodo>
    {
        return this.http.get<DespesaPeriodo>(
            `${c.api}/DespesaPeriodo/${codDespesaPeriodo}`)
            .pipe(map((obj) => obj));
    }

    criar(despesaPeriodo: DespesaPeriodo): Observable<DespesaPeriodo>
    {
        return this.http.post<DespesaPeriodo>(
            `${c.api}/DespesaPeriodo`, despesaPeriodo)
            .pipe(map((obj) => obj));
    }

    atualizar(despesaPeriodo: DespesaPeriodo): Observable<DespesaPeriodo>
    {
        return this.http.put<DespesaPeriodo>(
            `${c.api}/DespesaPeriodo`, despesaPeriodo)
            .pipe(map((obj) => obj));
    }

    deletar(codDespesaPeriodo: number): Observable<DespesaPeriodo>
    {
        return this.http.delete<DespesaPeriodo>(
            `${c.api}/DespesaPeriodo/${codDespesaPeriodo}`)
            .pipe(map((obj) => obj));
    }
}