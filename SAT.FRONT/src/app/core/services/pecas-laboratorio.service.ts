import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { PecasLaboratorioData, PecasLaboratorioParameters } from '../types/pecas-laboratorio.types';

@Injectable({
    providedIn: 'root'
})
export class PecasLaboratorioService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: PecasLaboratorioParameters): Observable<PecasLaboratorioData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get<PecasLaboratorioData>(`${c.api}/PecasLaboratorio`, { params: params }).pipe(
            map((data) => data)
        )
    }
}