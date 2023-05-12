import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OperadoraTelefonia, OperadoraTelefoniaData, OperadoraTelefoniaParameters } from 'app/core/types/operadora-telefonia.types'

@Injectable({
    providedIn: 'root'
})
export class OperadoraTelefoniaService {
    constructor(private http: HttpClient) { }

    criar(op: OperadoraTelefonia): Observable<OperadoraTelefonia> {
        return this.http.post<OperadoraTelefonia>(`${c.api}/OperadoraTelefonia`, op).pipe(
            map((obj) => obj)
        );
    }

    obterPorCodigo(cod: number): Observable<OperadoraTelefonia> {
        const url = `${c.api}/OperadoraTelefonia/${cod}`;
        return this.http.get<OperadoraTelefonia>(url).pipe(
            map((obj) => obj)
        );
    }

    obterPorParametros(parameters: OperadoraTelefoniaParameters): Observable<OperadoraTelefoniaData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/OperadoraTelefonia`, { params: params }).pipe(
            map((data: OperadoraTelefoniaData) => data)
        )
    }

    atualizar(op: OperadoraTelefonia): Observable<OperadoraTelefonia> {
        const url = `${c.api}/OperadoraTelefonia`;
        return this.http.put<OperadoraTelefonia>(url, op).pipe(
            map((obj) => obj)
        );
    }

    deletar(cod: number): Observable<OperadoraTelefonia> {
        const url = `${c.api}/OperadoraTelefonia/${cod}`;

        return this.http.delete<OperadoraTelefonia>(url).pipe(
            map((obj) => obj)
        );
    }
}