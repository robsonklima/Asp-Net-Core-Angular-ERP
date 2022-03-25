import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ClientePecaGenerica, ClientePecaGenericaData, ClientePecaGenericaParameters } from '../types/cliente-peca-generica.types';

@Injectable({
    providedIn: 'root'
})
export class ClientePecaGenericaService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: ClientePecaGenericaParameters): Observable<ClientePecaGenericaData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get<ClientePecaGenericaData>(`${c.api}/ClientePecaGenerica`, { params: params }).pipe(
            map((data) => data)
        )
    }

    obterPorCodigo(codClientePecaGenerica: number): Observable<ClientePecaGenerica> {
        const url = `${c.api}/ClientePecaGenerica/${codClientePecaGenerica}`;
        return this.http.get<ClientePecaGenerica>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(peca: ClientePecaGenerica): Observable<ClientePecaGenerica> {
        return this.http.post<ClientePecaGenerica>(`${c.api}/ClientePecaGenerica`, peca).pipe(
            map((obj) => obj)
        );
    }

    atualizar(peca: ClientePecaGenerica): Observable<ClientePecaGenerica> {
        const url = `${c.api}/ClientePecaGenerica`;
        return this.http.put<ClientePecaGenerica>(url, peca).pipe(
            ((obj) => obj)
        );
    }

    deletar(codClientePecaGenerica: number): Observable<ClientePecaGenerica> {
        const url = `${c.api}/ClientePecaGenerica/${codClientePecaGenerica}`;
        return this.http.delete<ClientePecaGenerica>(url).pipe(
            map((obj) => obj)
        );
    }
}