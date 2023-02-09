import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { PecaRE5114, PecaRE5114Data, PecaRE5114Parameters } from '../types/pecaRE5114.types';

@Injectable({
    providedIn: 'root'
})
export class PecaRE5114Service {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: PecaRE5114Parameters): Observable<PecaRE5114Data> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get<PecaRE5114Data>(`${c.api}/PecaRE5114`, { params: params }).pipe(
            map((data) => data)
        )
    }

    obterPorCodigo(codPecaRE5114: number): Observable<PecaRE5114> {
        const url = `${c.api}/PecaRE5114/${codPecaRE5114}`;
        return this.http.get<PecaRE5114>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(pecaRE5114: PecaRE5114): Observable<PecaRE5114> {
        return this.http.post<PecaRE5114>(`${c.api}/PecaRE5114`, pecaRE5114).pipe(
            map((obj) => obj)
        );
    }

    atualizar(pecaRE5114: PecaRE5114): Observable<PecaRE5114> {
        const url = `${c.api}/PecaRE5114`;
        return this.http.put<PecaRE5114>(url, pecaRE5114).pipe(
                ((obj) => obj)
        );
    }

    deletar(codPecaRE5114: number): Observable<PecaRE5114> {
        const url = `${c.api}/PecaRE5114/${codPecaRE5114}`;
        return this.http.delete<PecaRE5114>(url).pipe(
            map((obj) => obj)
        );
    }
}