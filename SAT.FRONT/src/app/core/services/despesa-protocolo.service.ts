import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DespesaProtocoloParameters, DespesaProtocoloData } from '../types/despesa-protocolo.types';

@Injectable({
    providedIn: 'root'
})
export class DespesaProtocoloService
{
    constructor (private http: HttpClient) { }

    obterPorParametros(parameters: DespesaProtocoloParameters): Observable<DespesaProtocoloData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/DespesaProtocolo`, { params: params })
            .pipe(map((data: DespesaProtocoloData) => data));
    }
}