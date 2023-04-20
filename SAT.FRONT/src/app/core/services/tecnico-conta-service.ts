import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { TecnicoConta, TecnicoContaData, TecnicoContaParameters } from '../types/tecnico.types';

@Injectable({
    providedIn: 'root'
})
export class TecnicoContaService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: TecnicoContaParameters): Observable<TecnicoContaData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/TecnicoConta`, { params: params }).pipe(

            map((data: TecnicoContaData) => data)
        )
    }
    
    obterPorCodigo(cod: number): Observable<TecnicoConta> {
        const url = `${c.api}/TecnicoConta/${cod}`;
        return this.http.get<TecnicoConta>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(tecnicoConta: TecnicoConta): Observable<TecnicoConta> {
        return this.http.post<TecnicoConta>(`${c.api}/TecnicoConta`, tecnicoConta).pipe(
            map((obj) => obj)
        );
    }

    atualizar(TecnicoConta: TecnicoConta): Observable<TecnicoConta> {
        const url = `${c.api}/TecnicoConta`;

        return this.http.put<TecnicoConta>(url, TecnicoConta).pipe(
            map((obj) => obj)
        );
    }

    deletar(cod: number): Observable<TecnicoConta> {
        const url = `${c.api}/TecnicoConta/${cod}`;

        return this.http.delete<TecnicoConta>(url).pipe(
            map((obj) => obj)
        );
    }
}