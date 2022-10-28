import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ORCheckListItem, ORCheckListItemData, ORCheckListItemParameters } from '../types/or-checklist-item.types';

@Injectable({
    providedIn: 'root'
})
export class ORCheckListItemService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: ORCheckListItemParameters): Observable<ORCheckListItemData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/ORCheckListItem`, { params: params }).pipe(
            map((data: ORCheckListItemData) => data)
        )
    }

    obterPorCodigo(cod: number): Observable<ORCheckListItem> {
        const url = `${c.api}/ORCheckListItem/${cod}`;
        return this.http.get<ORCheckListItem>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(item: ORCheckListItem): Observable<ORCheckListItem> {
        return this.http.post<ORCheckListItem>(`${c.api}/ORCheckListItem`, item).pipe(
            map((obj) => obj)
        );
    }

    atualizar(item: ORCheckListItem): Observable<ORCheckListItem> {
        const url = `${c.api}/ORCheckListItem`;

        return this.http.put<ORCheckListItem>(url, item).pipe(
            map((obj) => obj)
        );
    }

    deletar(cod: number): Observable<ORCheckListItem> {
        const url = `${c.api}/ORCheckListItem/${cod}`;

        return this.http.delete<ORCheckListItem>(url).pipe(
            map((obj) => obj)
        );
    }
}