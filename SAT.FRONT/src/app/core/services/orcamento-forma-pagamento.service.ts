import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrcFormaPagamentoData, OrcFormaPagamentoParameters } from '../types/orcamento-forma-pagamento.types';

@Injectable({
    providedIn: 'root'
})
export class OrcFormaPagamentoService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: OrcFormaPagamentoParameters): Observable<OrcFormaPagamentoData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/OrcFormaPagamento`, { params: params }).pipe(
            map((data: OrcFormaPagamentoData) => data)
        )
    }
}