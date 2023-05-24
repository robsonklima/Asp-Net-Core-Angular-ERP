import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { MRPLogix, MRPLogixData, MRPLogixParameters } from '../types/mrp-logix.types';

@Injectable({
    providedIn: 'root'
})
export class MRPLogixService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: MRPLogixParameters): Observable<MRPLogixData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get<MRPLogixData>(`${c.api}/MRPLogix`, { params: params }).pipe(
            map((data) => data)
        )
    }

    obterPorCodigo(codMRPLogix: number): Observable<MRPLogix> {
        const url = `${c.api}/MRPLogix/${codMRPLogix}`;
        return this.http.get<MRPLogix>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(mrpLogix: MRPLogix): Observable<MRPLogix> {
        return this.http.post<MRPLogix>(`${c.api}/MRPLogix`, mrpLogix).pipe(
            map((obj) => obj)
        );
    }

    atualizar(mrpLogix: MRPLogix): Observable<MRPLogix> {
        const url = `${c.api}/MRPLogix`;
        return this.http.put<MRPLogix>(url, mrpLogix).pipe(
                ((obj) => obj)
        );
    }

    deletar(codMRPLogix: number): Observable<MRPLogix> {
        const url = `${c.api}/MRPLogix/${codMRPLogix}`;
        return this.http.delete<MRPLogix>(url).pipe(
            map((obj) => obj)
        );
    }
}