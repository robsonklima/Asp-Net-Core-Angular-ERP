import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { BancadaListaData, BancadaListaParameters } from '../types/bancada-lista.types';

@Injectable({
    providedIn: 'root'
})
export class BancadaListaService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: BancadaListaParameters): Observable<BancadaListaData> {

        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/BancadaLista`, { params: params }).pipe(
            map((data: BancadaListaData) => data)
        )
    }
}