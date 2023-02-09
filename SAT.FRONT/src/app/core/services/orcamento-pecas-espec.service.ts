import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrcamentoPecasEspecParameters, OrcamentoPecasEspecData, OrcamentoPecasEspec } from '../types/orcamento-pecas-espec.types';


@Injectable({
    providedIn: 'root'
})
export class OrcamentoPecasEspecService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: OrcamentoPecasEspecParameters): Observable<OrcamentoPecasEspecData> {

        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/OrcamentoPecasEspec`, { params: params }).pipe(
            map((data: OrcamentoPecasEspecData) => data)
        )
    }

    obterPorCodigo(codOrcamentoPecasEspec: number): Observable<OrcamentoPecasEspec> {
        const url = `${c.api}/OrcamentoPecasEspec/${codOrcamentoPecasEspec}`;
        return this.http.get<OrcamentoPecasEspec>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(orcamentoPecasEspec: OrcamentoPecasEspec): Observable<OrcamentoPecasEspec> {
        return this.http.post<OrcamentoPecasEspec>(`${c.api}/OrcamentoPecasEspec`, orcamentoPecasEspec).pipe(
            map((obj) => obj)
        );
    }

    atualizar(orcamentoPecasEspec: OrcamentoPecasEspec): Observable<OrcamentoPecasEspec> {
        const url = `${c.api}/OrcamentoPecasEspec`;

        return this.http.put<OrcamentoPecasEspec>(url, orcamentoPecasEspec).pipe(
            map((obj) => obj)
        );
    }

    deletar(codOrcamentoPecasEspec: number): Observable<OrcamentoPecasEspec> {
        const url = `${c.api}/OrcamentoPecasEspec/${codOrcamentoPecasEspec}`;

        return this.http.delete<OrcamentoPecasEspec>(url).pipe(
            map((obj) => obj)
        );
    }
}