import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { RedeBanrisul, RedeBanrisulData, RedeBanrisulParameters } from 'app/core/types/rede-banrisul.types'

@Injectable({
    providedIn: 'root'
})
export class RedeBanrisulService {
    constructor(private http: HttpClient) { }

    criar(rede: RedeBanrisul): Observable<RedeBanrisul> {
        return this.http.post<RedeBanrisul>(`${c.api}/RedeBanrisul`, rede).pipe(
            map((obj) => obj)
        );
    }

    obterPorCodigo(cod: number): Observable<RedeBanrisul> {
        const url = `${c.api}/RedeBanrisul/${cod}`;
        return this.http.get<RedeBanrisul>(url).pipe(
            map((obj) => obj)
        );
    }

    obterPorParametros(parameters: RedeBanrisulParameters): Observable<RedeBanrisulData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/RedeBanrisul`, { params: params }).pipe(
            map((data: RedeBanrisulData) => data)
        )
    }

    atualizar(rede: RedeBanrisul): Observable<RedeBanrisul> {
        const url = `${c.api}/RedeBanrisul`;
        return this.http.put<RedeBanrisul>(url, rede).pipe(
            map((obj) => obj)
        );
    }

    deletar(cod: number): Observable<RedeBanrisul> {
        const url = `${c.api}/RedeBanrisul/${cod}`;

        return this.http.delete<RedeBanrisul>(url).pipe(
            map((obj) => obj)
        );
    }
}