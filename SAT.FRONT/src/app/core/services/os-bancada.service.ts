import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OSBancada, OSBancadaData, OSBancadaParameters } from '../types/os-bancada.types';

@Injectable({
    providedIn: 'root'
})
export class OSBancadaService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: OSBancadaParameters): Observable<OSBancadaData> {

        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/OSBancada`, { params: params }).pipe(
            map((data: OSBancadaData) => data)
        )
    }

    obterPorCodigo(codOsbancada: number): Observable<OSBancada> {
        const url = `${c.api}/OSBancada/${codOsbancada}`;
        return this.http.get<OSBancada>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(osBancada: OSBancada): Observable<OSBancada> {
        return this.http.post<OSBancada>(`${c.api}/OSBancada`, osBancada).pipe(
            map((obj) => obj)
        );
    }

    atualizar(osBancada: OSBancada): Observable<OSBancada> {
        const url = `${c.api}/OSBancada`;

        return this.http.put<OSBancada>(url, osBancada).pipe(
            map((obj) => obj)
        );
    }

    deletar(codOsbancada: number): Observable<OSBancada> {
        const url = `${c.api}/OSBancada/${codOsbancada}`;

        return this.http.delete<OSBancada>(url).pipe(
            map((obj) => obj)
        );
    }
}