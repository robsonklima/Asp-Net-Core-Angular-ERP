import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DespesaConfiguracao, DespesaConfiguracaoData, DespesaConfiguracaoParameters } from '../types/despesa.types';

@Injectable({
    providedIn: 'root'
})
export class DespesaConfiguracaoService
{
    constructor (private http: HttpClient) { }

    obterPorParametros(parameters: DespesaConfiguracaoParameters): Observable<DespesaConfiguracaoData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null)
                params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/DespesaConfiguracao`, { params: params })
            .pipe(map((data: DespesaConfiguracaoData) => data));
    }

    obterPorCodigo(codDespesaConfiguracao: number): Observable<DespesaConfiguracao>
    {
        return this.http.get<DespesaConfiguracao>(
            `${c.api}/DespesaConfiguracao/${codDespesaConfiguracao}`)
            .pipe(map((obj) => obj));
    }
}