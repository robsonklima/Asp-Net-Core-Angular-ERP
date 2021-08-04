import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Cidade, CidadeData, CidadeParameters } from '../types/cidade.types';

@Injectable({
    providedIn: 'root'
})
export class CidadeService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: CidadeParameters): Observable<CidadeData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/Cidade`, { params: params }).pipe(
            map((data: CidadeData) => data)
        )
    }

    obterPorCodigo(codCidade: number): Observable<Cidade> {
        const url = `${c.api}/Cidade/${codCidade}`;
        return this.http.get<Cidade>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(cidade: Cidade): Observable<Cidade> {
        return this.http.post<Cidade>(`${c.api}/Cidade`, cidade).pipe(
            map((obj) => obj)
        );
    }

    atualizar(cidade: Cidade): Observable<Cidade> {
        const url = `${c.api}/Cidade`;

        return this.http.put<Cidade>(url, cidade).pipe(
            map((obj) => obj)
        );
    }

    deletar(codCidade: number): Observable<Cidade> {
        const url = `${c.api}/Cidade/${codCidade}`;

        return this.http.delete<Cidade>(url).pipe(
            map((obj) => obj)
        );
    }
}