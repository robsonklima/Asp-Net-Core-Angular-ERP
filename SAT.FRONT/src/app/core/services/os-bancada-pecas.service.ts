import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OSBancadaPecasParameters, OSBancadaPecasData, OSBancadaPecas } from '../types/os-bancada-pecas.types';


@Injectable({
    providedIn: 'root'
})
export class OSBancadaPecasService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: OSBancadaPecasParameters): Observable<OSBancadaPecasData> {

        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/OSBancadaPecas`, { params: params }).pipe(
            map((data: OSBancadaPecasData) => data)
        )
    }

    criar(osBancada: OSBancadaPecas): Observable<OSBancadaPecas> {
        return this.http.post<OSBancadaPecas>(`${c.api}/OSBancadaPecas`, osBancada).pipe(
            map((obj) => obj)
        );
    }

    atualizar(osBancada: OSBancadaPecas): Observable<OSBancadaPecas> {
        const url = `${c.api}/OSBancadaPecas`;

        return this.http.put<OSBancadaPecas>(url, osBancada).pipe(
            map((obj) => obj)
        );
    }

    deletar(codOsbancada: number, codFilialRe5114: number): Observable<OSBancadaPecas> {
        const url = `${c.api}/OSBancadaPecas/${codOsbancada}/${codFilialRe5114}`;

        return this.http.delete<OSBancadaPecas>(url).pipe(
            map((obj) => obj)
        );
    }
}
