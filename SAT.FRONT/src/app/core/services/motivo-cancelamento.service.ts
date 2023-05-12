import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { MotivoCancelamento, MotivoCancelamentoData, MotivoCancelamentoParameters } from 'app/core/types/motivo-cancelamento.types'

@Injectable({
    providedIn: 'root'
})
export class MotivoCancelamentoService {
    constructor(private http: HttpClient) { }

    criar(op: MotivoCancelamento): Observable<MotivoCancelamento> {
        return this.http.post<MotivoCancelamento>(`${c.api}/MotivoCancelamento`, op).pipe(
            map((obj) => obj)
        );
    }

    obterPorCodigo(cod: number): Observable<MotivoCancelamento> {
        const url = `${c.api}/MotivoCancelamento/${cod}`;
        return this.http.get<MotivoCancelamento>(url).pipe(
            map((obj) => obj)
        );
    }

    obterPorParametros(parameters: MotivoCancelamentoParameters): Observable<MotivoCancelamentoData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/MotivoCancelamento`, { params: params }).pipe(
            map((data: MotivoCancelamentoData) => data)
        )
    }

    atualizar(op: MotivoCancelamento): Observable<MotivoCancelamento> {
        const url = `${c.api}/MotivoCancelamento`;
        return this.http.put<MotivoCancelamento>(url, op).pipe(
            map((obj) => obj)
        );
    }

    deletar(cod: number): Observable<MotivoCancelamento> {
        const url = `${c.api}/MotivoCancelamento/${cod}`;

        return this.http.delete<MotivoCancelamento>(url).pipe(
            map((obj) => obj)
        );
    }
}