import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Feriado, FeriadoData, FeriadoParameters } from '../types/feriado.types';

@Injectable({
    providedIn: 'root'
})
export class FeriadoService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: FeriadoParameters): Observable<FeriadoData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/Feriado`, { params: params }).pipe(
            map((data: FeriadoData) => data)
        )
    }

    obterDiasUteis(parameters: FeriadoParameters): Observable<number> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/Feriado/getDiasUteis`, { params: params }).pipe(
            map((data: number) => data)
        )
    }

    obterPorCodigo(codFeriado: number): Observable<Feriado> {
        const url = `${c.api}/Feriado/${codFeriado}`;
        return this.http.get<Feriado>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(feriado: Feriado): Observable<Feriado> {
        return this.http.post<Feriado>(`${c.api}/Feriado`, feriado).pipe(
            map((obj) => obj)
        );
    }

    atualizar(feriado: Feriado): Observable<Feriado> {
        const url = `${c.api}/Feriado`;

        return this.http.put<Feriado>(url, feriado).pipe(
            map((obj) => obj)
        );
    }

    deletar(codFeriado: number): Observable<Feriado> {
        const url = `${c.api}/Feriado/${codFeriado}`;

        return this.http.delete<Feriado>(url).pipe(
            map((obj) => obj)
        );
    }
}