import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrcDadosBancariosData, OrcDadosBancariosParameters } from '../types/orcamento-dados-bancarios.types';

@Injectable({
    providedIn: 'root'
})
export class OrcDadosBancariosService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: OrcDadosBancariosParameters): Observable<OrcDadosBancariosData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/OrcDadosBancarios`, { params: params }).pipe(
            map((data: OrcDadosBancariosData) => data)
        )
    }
}