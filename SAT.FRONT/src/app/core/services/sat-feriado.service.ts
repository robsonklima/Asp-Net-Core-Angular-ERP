import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { SATFeriado, SATFeriadoData, SATFeriadoParameters } from '../types/sat-feriado.types';

@Injectable({
    providedIn: 'root'
})
export class SATFeriadoService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: SATFeriadoParameters): Observable<SATFeriadoData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/SATFeriado`, { params: params }).pipe(
            map((data: SATFeriadoData) => data)
        )
    }

    obterPorCodigo(codSATFeriado: number): Observable<SATFeriado> {
        const url = `${c.api}/SATFeriado/${codSATFeriado}`;
        return this.http.get<SATFeriado>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(SATFeriado: SATFeriado): Observable<SATFeriado> {
        return this.http.post<SATFeriado>(`${c.api}/SATFeriado`, SATFeriado).pipe(
            map((obj) => obj)
        );
    }

    atualizar(SATFeriado: SATFeriado): Observable<SATFeriado> {
        const url = `${c.api}/SATFeriado`;

        return this.http.put<SATFeriado>(url, SATFeriado).pipe(
            map((obj) => obj)
        );
    }

    deletar(codSATFeriado: number): Observable<SATFeriado> {
        const url = `${c.api}/SATFeriado/${codSATFeriado}`;

        return this.http.delete<SATFeriado>(url).pipe(
            map((obj) => obj)
        );
    }
}