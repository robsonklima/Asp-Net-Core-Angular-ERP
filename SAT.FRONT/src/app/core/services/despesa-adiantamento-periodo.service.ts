import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DespesaAdiantamentoPeriodo, DespesaAdiantamentoPeriodoData, DespesaAdiantamentoPeriodoParameters, DespesaPeriodo } from '../types/despesa-atendimento.types';

@Injectable({
    providedIn: 'root'
})
export class DespesaAdiantamentoPeriodoService
{
    constructor (private http: HttpClient) { }

    obterPorParametros(parameters: DespesaAdiantamentoPeriodoParameters): Observable<DespesaAdiantamentoPeriodoData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/DespesaAdiantamentoPeriodo`, { params: params })
            .pipe(map((data: DespesaAdiantamentoPeriodoData) => data));
    }

    obterPorCodigo(codDespesaAdiantamentoPeriodo: number): Observable<DespesaAdiantamentoPeriodo>
    {
        return this.http.get<DespesaAdiantamentoPeriodo>(
            `${c.api}/DespesaAdiantamentoPeriodo/${codDespesaAdiantamentoPeriodo}`)
            .pipe(map((obj) => obj));
    }

    criar(codDespesaAdiantamentoPeriodo: DespesaAdiantamentoPeriodo): Observable<DespesaAdiantamentoPeriodo>
    {
        return this.http.post<DespesaAdiantamentoPeriodo>(
            `${c.api}/DespesaPeriodo`, codDespesaAdiantamentoPeriodo)
            .pipe(map((obj) => obj));
    }

    atualizar(codDespesaAdiantamentoPeriodo: DespesaAdiantamentoPeriodo): Observable<DespesaAdiantamentoPeriodo>
    {
        return this.http.put<DespesaAdiantamentoPeriodo>(
            `${c.api}/DespesaAdiantamentoPeriodo`, codDespesaAdiantamentoPeriodo)
            .pipe(map((obj) => obj));
    }

    deletar(codDespesaAdiantamentoPeriodo: number): Observable<DespesaAdiantamentoPeriodo>
    {
        return this.http.delete<DespesaAdiantamentoPeriodo>(
            `${c.api}/DespesaAdiantamentoPeriodo/${codDespesaAdiantamentoPeriodo}`)
            .pipe(map((obj) => obj));
    }
}