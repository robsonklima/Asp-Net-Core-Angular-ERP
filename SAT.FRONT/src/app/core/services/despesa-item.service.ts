import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DespesaItem, DespesaItemData, DespesaItemParameters } from '../types/despesa.types';

@Injectable({
    providedIn: 'root'
})
export class DespesaItemService
{
    constructor (private http: HttpClient) { }

    obterPorParametros(parameters: DespesaItemParameters): Observable<DespesaItemData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null)
                params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/DespesaItem`, { params: params })
            .pipe(map((data: DespesaItemData) => data));
    }

    obterPorCodigo(codDespesaItem: number): Observable<DespesaItem>
    {
        return this.http.get<DespesaItem>(
            `${c.api}/DespesaItem/${codDespesaItem}`)
            .pipe(map((obj) => obj));
    }

    criar(item: DespesaItem): Observable<DespesaItem>
    {
        return this.http.post<DespesaItem>(
            `${c.api}/DespesaItem`, item)
            .pipe(map((obj) => obj));
    }

    atualizar(item: DespesaItem): Observable<DespesaItem>
    {
        return this.http.put<DespesaItem>(
            `${c.api}/DespesaItem`, item)
            .pipe(map((obj) => obj));
    }

    deletar(codDespesaItem: number): Observable<DespesaItem>
    {
        return this.http.delete<DespesaItem>(
            `${c.api}/DespesaItem/${codDespesaItem}`)
            .pipe(map((obj) => obj));
    }
}