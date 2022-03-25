import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { LiderTecnico, LiderTecnicoData, LiderTecnicoParameters } from '../types/lider-tecnico.types';

@Injectable({
    providedIn: 'root'
})
export class LiderTecnicoService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: LiderTecnicoParameters): Observable<LiderTecnicoData> {

        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/LiderTecnico`, { params: params }).pipe(
            map((data: LiderTecnicoData) => data)
        )
    }

    obterPorCodigo(codLiderTecnico: number): Observable<LiderTecnico> {
        const url = `${c.api}/LiderTecnico/${codLiderTecnico}`;
        return this.http.get<LiderTecnico>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(liderTecnico: LiderTecnico): Observable<LiderTecnico> {
        return this.http.post<LiderTecnico>(`${c.api}/LiderTecnico`, liderTecnico).pipe(
            map((obj) => obj)
        );
    }

    atualizar(liderTecnico: LiderTecnico): Observable<LiderTecnico> {
        const url = `${c.api}/LiderTecnico`;

        return this.http.put<LiderTecnico>(url, liderTecnico).pipe(
            map((obj) => obj)
        );
    }

    deletar(codLiderTecnico: number): Observable<LiderTecnico> {
        const url = `${c.api}/LiderTecnico/${codLiderTecnico}`;

        return this.http.delete<LiderTecnico>(url).pipe(
            map((obj) => obj)
        );
    }
}