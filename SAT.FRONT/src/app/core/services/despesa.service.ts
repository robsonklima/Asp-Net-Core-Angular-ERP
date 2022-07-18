import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DespesaParameters, DespesaData, Despesa, ViewDespesaImpressaoItem } from '../types/despesa.types';

@Injectable({
    providedIn: 'root'
})
export class DespesaService
{
    constructor (private http: HttpClient) { }

    obterPorParametros(parameters: DespesaParameters): Observable<DespesaData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null)
                params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/Despesa`, { params: params })
            .pipe(map((data: DespesaData) => data));
    }

    obterPorCodigo(codDespesa: number): Observable<Despesa>
    {
        return this.http.get<Despesa>(
            `${c.api}/Despesa/${codDespesa}`)
            .pipe(map((obj) => obj));
    }

    criar(despesa: Despesa): Observable<Despesa>
    {
        return this.http.post<Despesa>(
            `${c.api}/Despesa`, despesa)
            .pipe(map((obj) => obj));
    }

    atualizar(despesa: Despesa): Observable<Despesa>
    {
        return this.http.put<Despesa>(
            `${c.api}/Despesa`, despesa)
            .pipe(map((obj) => obj));
    }

    deletar(codDespesa: number): Observable<Despesa>
    {
        return this.http.delete<Despesa>(
            `${c.api}/Despesa/${codDespesa}`)
            .pipe(map((obj) => obj));
    }

    impressao(parameters: DespesaParameters): Observable<ViewDespesaImpressaoItem[]>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null)
                params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/Despesa/Impressao`, { params: params })
            .pipe(map((data: ViewDespesaImpressaoItem[]) => data));
    }
}