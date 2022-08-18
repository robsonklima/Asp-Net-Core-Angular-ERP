import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrcamentoFaturamentoData } from '../types/orcamento.types';
import { OrcamentoFaturamentoParameters } from '../types/orcamento-faturamento.types';

@Injectable({
    providedIn: 'root'
})
export class OrcamentoFaturamentoService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: OrcamentoFaturamentoParameters): Observable<OrcamentoFaturamentoData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/OrcamentoFaturamento`, { params: params }).pipe(
            map((data: OrcamentoFaturamentoData) => data)
        )
    }
}