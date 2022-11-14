import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { TicketAnexo, TicketAnexoData, TicketAnexoParameters } from '../types/ticket-anexo.types';

@Injectable({
    providedIn: 'root'
})
export class TicketAnexoService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: TicketAnexoParameters): Observable<TicketAnexoData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/TicketAnexo`, { params: params }).pipe(

            map((data: TicketAnexoData) => data)
        )
    }

    obterPorCodigo(cod: number): Observable<TicketAnexo> {
        const url = `${c.api}/TicketAnexo/${cod}`;
        return this.http.get<TicketAnexo>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(anexo: TicketAnexo): Observable<TicketAnexo> {
        return this.http.post<TicketAnexo>(`${c.api}/TicketAnexo`, anexo).pipe(
            map((obj) => obj)
        );
    }

    atualizar(anexo: TicketAnexo): Observable<TicketAnexo> {
        const url = `${c.api}/TicketAnexo`;

        return this.http.put<TicketAnexo>(url, anexo).pipe(
            map((obj) => obj)
        );
    }

    deletar(cod: number): Observable<TicketAnexo> {
        const url = `${c.api}/TicketAnexo/${cod}`;

        return this.http.delete<TicketAnexo>(url).pipe(
            map((obj) => obj)
        );
    }
}