import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { NLogParameters, NLogRegistro } from '../types/n-log.types';

@Injectable({
    providedIn: 'root'
})
export class NLogService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: NLogParameters): Observable<NLogRegistro[]> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null)
                params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/NLog`, { params: params })
            .pipe(map((data: NLogRegistro[]) => data));
    }

    criar(notificacao: NLogRegistro): Observable<NLogRegistro> {
        return this.http.post<NLogRegistro>(`${c.api}/NLog`, notificacao).pipe(
            map((obj) => obj)
        );
    }
}