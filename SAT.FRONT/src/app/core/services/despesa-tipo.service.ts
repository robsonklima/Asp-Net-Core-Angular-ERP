import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DespesaTipo, DespesaTipoData, DespesaTipoParameters } from '../types/despesa.types';

@Injectable({
    providedIn: 'root'
})
export class DespesaTipoService
{
    constructor (private http: HttpClient) { }

    obterPorParametros(parameters: DespesaTipoParameters): Observable<DespesaTipoData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null)
                params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/DespesaTipo`, { params: params })
            .pipe(map((data: DespesaTipoData) => data));
    }

    obterPorCodigo(codDespesaTipo: number): Observable<DespesaTipo>
    {
        return this.http.get<DespesaTipo>(
            `${c.api}/DespesaTipo/${codDespesaTipo}`)
            .pipe(map((obj) => obj));
    }

    criar(tipo: DespesaTipo): Observable<DespesaTipo>
    {
        return this.http.post<DespesaTipo>(
            `${c.api}/DespesaTipo`, tipo)
            .pipe(map((obj) => obj));
    }

    atualizar(tipo: DespesaTipo): Observable<DespesaTipo>
    {
        return this.http.put<DespesaTipo>(
            `${c.api}/DespesaTipo`, tipo)
            .pipe(map((obj) => obj));
    }

    deletar(codDespesaTipo: number): Observable<DespesaTipo>
    {
        return this.http.delete<DespesaTipo>(
            `${c.api}/DespesaTipo/${codDespesaTipo}`)
            .pipe(map((obj) => obj));
    }
}