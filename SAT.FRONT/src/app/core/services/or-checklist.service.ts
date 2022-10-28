import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ORCheckList, ORCheckListData, ORCheckListParameters } from '../types/or-checklist.types';

@Injectable({
    providedIn: 'root'
})
export class ORCheckListService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: ORCheckListParameters): Observable<ORCheckListData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/ORCheckList`, { params: params }).pipe(
            map((data: ORCheckListData) => data)
        )
    }

    obterPorCodigo(cod: number): Observable<ORCheckList> {
        const url = `${c.api}/ORCheckList/${cod}`;
        return this.http.get<ORCheckList>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(checklist: ORCheckList): Observable<ORCheckList> {
        return this.http.post<ORCheckList>(`${c.api}/ORCheckList`, checklist).pipe(
            map((obj) => obj)
        );
    }

    atualizar(checklist: ORCheckList): Observable<ORCheckList> {
        const url = `${c.api}/ORCheckList`;

        return this.http.put<ORCheckList>(url, checklist).pipe(
            map((obj) => obj)
        );
    }

    deletar(cod: number): Observable<ORCheckList> {
        const url = `${c.api}/ORCheckList/${cod}`;

        return this.http.delete<ORCheckList>(url).pipe(
            map((obj) => obj)
        );
    }
}