import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { PosVenda, PosVendaData, PosVendaParameters } from '../types/pos-venda.types';

@Injectable({
    providedIn: 'root'
})
export class PosVendaService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: PosVendaParameters): Observable<PosVendaData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/PosVenda`, { params: params }).pipe(
            map((data: PosVendaData) => data)
        )
    }

    obterPorCodigo(codPosVenda: number): Observable<PosVenda> {
        const url = `${c.api}/PosVenda/${codPosVenda}`;
        return this.http.get<PosVenda>(url).pipe(
            map((obj) => obj)
        );
    }
}