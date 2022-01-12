import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Pais, PaisData, PaisParameters } from '../types/pais.types';

@Injectable({
    providedIn: 'root'
})
export class PaisService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: PaisParameters): Observable<PaisData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/Pais`, { params: params }).pipe(
            map((data: PaisData) => data)
        )
    }

    obterPorCodigo(codPais: number): Observable<Pais> {
        const url = `${c.api}/Pais/${codPais}`;
        return this.http.get<Pais>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(pais: Pais): Observable<Pais> {
        return this.http.post<Pais>(`${c.api}/Pais`, pais).pipe(
            map((obj) => obj)
        );
    }

    atualizar(pais: Pais): Observable<Pais> {
        const url = `${c.api}/Pais`;

        return this.http.put<Pais>(url, pais).pipe(
            map((obj) => obj)
        );
    }

    deletar(codPais: number): Observable<Pais> {
        const url = `${c.api}/Pais/${codPais}`;

        return this.http.delete<Pais>(url).pipe(
            map((obj) => obj)
        );
    }

    async obterPaises(): Promise<Pais[]> {
        const params: PaisParameters = {
            sortActive: 'nomePais',
            sortDirection: 'asc',
            pageSize: 200
        }
        return (await this.obterPorParametros(params).toPromise()).items;
    }
}