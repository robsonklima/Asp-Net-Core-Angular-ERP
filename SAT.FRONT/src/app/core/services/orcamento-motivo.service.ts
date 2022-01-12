import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrcamentoMotivoData, OrcamentoMotivoParameters } from '../types/orcamento.types';

@Injectable({
    providedIn: 'root'
})
export class OrcamentoMotivoService
{
    constructor (private http: HttpClient) { }

    obterPorParametros(parameters: OrcamentoMotivoParameters): Observable<OrcamentoMotivoData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/OrcamentoMotivo`, { params: params }).pipe(
            map((data: OrcamentoMotivoData) => data)
        )
    }
}