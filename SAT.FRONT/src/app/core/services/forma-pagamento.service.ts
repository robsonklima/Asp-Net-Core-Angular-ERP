import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { FormaPagamento, FormaPagamentoData, FormaPagamentoParameters } from '../types/forma-pagamento.types';

@Injectable({
    providedIn: 'root'
})
export class FormaPagamentoService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: FormaPagamentoParameters): Observable<FormaPagamentoData> {

        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/FormaPagamento`, { params: params }).pipe(
            map((data: FormaPagamentoData) => data)
        )
    }

    obterPorCodigo(codFormaPagto: number): Observable<FormaPagamento> {
        const url = `${c.api}/FormaPagamento/${codFormaPagto}`;
        return this.http.get<FormaPagamento>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(formaPagamento: FormaPagamento): Observable<FormaPagamento> {
        return this.http.post<FormaPagamento>(`${c.api}/FormaPagamento`, formaPagamento).pipe(
            map((obj) => obj)
        );
    }

    atualizar(formaPagamento: FormaPagamento): Observable<FormaPagamento> {
        const url = `${c.api}/FormaPagamento`;

        return this.http.put<FormaPagamento>(url, formaPagamento).pipe(
            map((obj) => obj)
        );
    }

    deletar(codFormaPagto: number): Observable<FormaPagamento> {
        const url = `${c.api}/FormaPagamento/${codFormaPagto}`;

        return this.http.delete<FormaPagamento>(url).pipe(
            map((obj) => obj)
        );
    }
}