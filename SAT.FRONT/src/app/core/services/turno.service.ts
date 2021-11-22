import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Turno, TurnoData, TurnoParameters } from '../types/turno.types';

@Injectable({
    providedIn: 'root'
})
export class TurnoService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: TurnoParameters): Observable<TurnoData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/Turno`, { params: params }).pipe(
            map((data: TurnoData) => data)
        )
    }

    obterPorCodigo(codTurno: number): Observable<Turno> {
        const url = `${c.api}/Turno/${codTurno}`;
        return this.http.get<Turno>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(turno: Turno): Observable<Turno> {
        return this.http.post<Turno>(`${c.api}/Turno`, turno).pipe(
            map((obj) => obj)
        );
    }

    atualizar(turno: Turno): Observable<Turno> {
        const url = `${c.api}/Turno`;

        return this.http.put<Turno>(url, turno).pipe(
            map((obj) => obj)
        );
    }

    deletar(codTurno: number): Observable<Turno> {
        const url = `${c.api}/Turno/${codTurno}`;

        return this.http.delete<Turno>(url).pipe(
            map((obj) => obj)
        );
    }
}