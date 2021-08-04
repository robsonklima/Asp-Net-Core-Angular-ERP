import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Transportadora, TransportadoraData, TransportadoraParameters } from '../types/transportadora.types';

@Injectable({
    providedIn: 'root'
})
export class TransportadoraService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: TransportadoraParameters): Observable<TransportadoraData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/Transportadora`, { params: params }).pipe(
            map((data: TransportadoraData) => data)
        )
    }

    obterPorCodigo(codTransportadora: number): Observable<Transportadora> {
        const url = `${c.api}/Transportadora/${codTransportadora}`;
        return this.http.get<Transportadora>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(transportadora: Transportadora): Observable<Transportadora> {
        return this.http.post<Transportadora>(`${c.api}/Transportadora`, transportadora).pipe(
            map((obj) => obj)
        );
    }

    atualizar(transportadora: Transportadora): Observable<Transportadora> {
        const url = `${c.api}/Transportadora`;

        return this.http.put<Transportadora>(url, transportadora).pipe(
            map((obj) => obj)
        );
    }

    deletar(codTransportadora: number): Observable<Transportadora> {
        const url = `${c.api}/Transportadora/${codTransportadora}`;

        return this.http.delete<Transportadora>(url).pipe(
            map((obj) => obj)
        );
    }
}