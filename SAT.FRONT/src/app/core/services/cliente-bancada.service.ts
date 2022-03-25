import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ClienteBancada, ClienteBancadaData, ClienteBancadaParameters } from '../types/cliente-bancada.types';

@Injectable({
    providedIn: 'root'
})
export class ClienteBancadaService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: ClienteBancadaParameters): Observable<ClienteBancadaData> {

        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/ClienteBancada`, { params: params }).pipe(
            map((data: ClienteBancadaData) => data)
        )
    }

    obterPorCodigo(codClienteBancada: number): Observable<ClienteBancada> {
        const url = `${c.api}/ClienteBancada/${codClienteBancada}`;
        return this.http.get<ClienteBancada>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(clienteBancada: ClienteBancada): Observable<ClienteBancada> {
        return this.http.post<ClienteBancada>(`${c.api}/ClienteBancada`, clienteBancada).pipe(
            map((obj) => obj)
        );
    }

    atualizar(clienteBancada: ClienteBancada): Observable<ClienteBancada> {
        const url = `${c.api}/ClienteBancada`;

        return this.http.put<ClienteBancada>(url, clienteBancada).pipe(
            map((obj) => obj)
        );
    }

    deletar(codClienteBancada: number): Observable<ClienteBancada> {
        const url = `${c.api}/ClienteBancada/${codClienteBancada}`;

        return this.http.delete<ClienteBancada>(url).pipe(
            map((obj) => obj)
        );
    }
}