import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { TipoFreteData, TipoFreteParameters } from '../types/tipo-frete.types';

@Injectable({
    providedIn: 'root'
})
export class TipoFreteService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: TipoFreteParameters): Observable<TipoFreteData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/TipoFrete`, { params: params }).pipe(
            map((data: TipoFreteData) => data)
        )
    }
}