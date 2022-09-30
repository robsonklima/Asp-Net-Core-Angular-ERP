import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrdemServicoSTN, OrdemServicoSTNData, OrdemServicoSTNParameters } from '../types/ordem-servico-stn.types';

@Injectable({
    providedIn: 'root'
})
export class OrdemServicoSTNService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: OrdemServicoSTNParameters): Observable<OrdemServicoSTNData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/OrdemServicoSTN`, { params: params }).pipe(
            map((data: OrdemServicoSTNData) => data)
        )
    }

    obterPorCodigo(codAtendimento: number): Observable<OrdemServicoSTN> {
        const url = `${c.api}/OrdemServicoSTN/${codAtendimento}`;
        return this.http.get<OrdemServicoSTN>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(ordem: OrdemServicoSTN): Observable<OrdemServicoSTN> {
        return this.http.post<OrdemServicoSTN>(`${c.api}/OrdemServicoSTN`, ordem).pipe(
            map((obj) => obj)
        );
    }

    atualizar(ordem: OrdemServicoSTN): Observable<OrdemServicoSTN> {
        const url = `${c.api}/OrdemServicoSTN`;

        return this.http.put<OrdemServicoSTN>(url, ordem).pipe(
            map((obj) => obj)
        );
    }

    deletar(codAtendimento: number): Observable<OrdemServicoSTN> {
        const url = `${c.api}/OrdemServicoSTN/${codAtendimento}`;

        return this.http.delete<OrdemServicoSTN>(url).pipe(
            map((obj) => obj)
        );
    }
}