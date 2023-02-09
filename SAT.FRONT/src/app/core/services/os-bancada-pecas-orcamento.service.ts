import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OsBancadaPecasOrcamentoParameters, OsBancadaPecasOrcamentoData, OsBancadaPecasOrcamento } from '../types/os-bancada-pecas-orcamento.types';


@Injectable({
    providedIn: 'root'
})
export class OsBancadaPecasOrcamentoService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: OsBancadaPecasOrcamentoParameters): Observable<OsBancadaPecasOrcamentoData> {

        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/OsbancadaPecasOrcamento`, { params: params }).pipe(
            map((data: OsBancadaPecasOrcamentoData) => data)
        )
    }

    obterPorCodigo(codOrcamento: number): Observable<OsBancadaPecasOrcamento> {
        const url = `${c.api}/OsbancadaPecasOrcamento/${codOrcamento}`;
        return this.http.get<OsBancadaPecasOrcamento>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(osBancadaPecasOrcamento: OsBancadaPecasOrcamento): Observable<OsBancadaPecasOrcamento> {
        return this.http.post<OsBancadaPecasOrcamento>(`${c.api}/OsbancadaPecasOrcamento`, osBancadaPecasOrcamento).pipe(
            map((obj) => obj)
        );
    }

    atualizar(osBancadaPecasOrcamento: OsBancadaPecasOrcamento): Observable<OsBancadaPecasOrcamento> {
        const url = `${c.api}/OsbancadaPecasOrcamento`;

        return this.http.put<OsBancadaPecasOrcamento>(url, osBancadaPecasOrcamento).pipe(
            map((obj) => obj)
        );
    }

    deletar(codOrcamento: number): Observable<OsBancadaPecasOrcamento> {
        const url = `${c.api}/OsbancadaPecasOrcamento/${codOrcamento}`;

        return this.http.delete<OsBancadaPecasOrcamento>(url).pipe(
            map((obj) => obj)
        );
    }
}