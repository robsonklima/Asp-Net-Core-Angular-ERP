import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DespesaItemAlertaParameters, DespesaItemAlertaData } from '../types/despesa.types';

@Injectable({
    providedIn: 'root'
})
export class DespesaItemAlertaService
{
    constructor (private http: HttpClient) { }

    obterPorParametros(parameters: DespesaItemAlertaParameters): Observable<DespesaItemAlertaData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null)
                params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/DespesaItemAlerta`, { params: params })
            .pipe(map((data: DespesaItemAlertaData) => data));
    }
}