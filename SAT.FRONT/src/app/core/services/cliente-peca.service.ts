import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ClientePeca, ClientePecaData, ClientePecaParameters } from '../types/cliente-peca.types';

@Injectable({
    providedIn: 'root'
})
export class ClientePecaService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: ClientePecaParameters): Observable<ClientePecaData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get<ClientePecaData>(`${c.api}/ClientePeca`, { params: params }).pipe(
            map((data) => data)
        )
    }

    obterPorCodigo(codClientePeca: number): Observable<ClientePeca> {
        const url = `${c.api}/ClientePeca/${codClientePeca}`;
        return this.http.get<ClientePeca>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(peca: ClientePeca): Observable<ClientePeca> {
        return this.http.post<ClientePeca>(`${c.api}/ClientePeca`, peca).pipe(
            map((obj) => obj)
        );
    }

    atualizar(peca: ClientePeca): Observable<ClientePeca> {
        const url = `${c.api}/ClientePeca`;
        return this.http.put<ClientePeca>(url, peca).pipe(
            ((obj) => obj)
        );
    }

    deletar(codClientePeca: number): Observable<ClientePeca> {
        const url = `${c.api}/ClientePeca/${codClientePeca}`;
        return this.http.delete<ClientePeca>(url).pipe(
            map((obj) => obj)
        );
    }
}