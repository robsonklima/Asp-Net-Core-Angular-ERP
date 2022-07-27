import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Peca, PecaData, PecaParameters, PecaStatus, PecaStatusData, PecaStatusParameters } from '../types/peca.types';

@Injectable({
    providedIn: 'root'
})
export class PecaStatusService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: PecaStatusParameters): Observable<PecaStatusData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get<PecaStatusData>(`${c.api}/PecaStatus`, { params: params }).pipe(
            map((data) => data)
        )
    }

    obterPorCodigo(codPecaStatus: number): Observable<PecaStatus> {
        const url = `${c.api}/PecaStatus/${codPecaStatus}`;
        return this.http.get<PecaStatus>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(pecaStatus: PecaStatus): Observable<PecaStatus> {
        return this.http.post<PecaStatus>(`${c.api}/PecaStatus`, pecaStatus).pipe(
            map((obj) => obj)
        );
    }

    atualizar(pecaStatus: PecaStatus): Observable<PecaStatus> {
        const url = `${c.api}/PecaStatus`;
        return this.http.put<PecaStatus>(url, pecaStatus).pipe(
                ((obj) => obj)
        );
    }

    deletar(codPecaStatus: number): Observable<PecaStatus> {
        const url = `${c.api}/PecaStatus/${codPecaStatus}`;
        return this.http.delete<PecaStatus>(url).pipe(
            map((obj) => obj)
        );
    }
}