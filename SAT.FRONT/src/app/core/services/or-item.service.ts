import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ORItem, ORItemData, ORItemParameters } from '../types/or-item.types';

@Injectable({
    providedIn: 'root'
})
export class ORItemService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: ORItemParameters): Observable<ORItemData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/ORItem`, { params: params }).pipe(
          map((data: ORItemData) => data)
        )
    }

    obterPorCodigo(codOR: number): Observable<ORItem> {
        const url = `${c.api}/ORItem/${codOR}`;
        return this.http.get<ORItem>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(orItem: ORItem): Observable<ORItem> {
        return this.http.post<ORItem>(`${c.api}/ORItem`, orItem).pipe(
            map((obj) => obj)
        );
    }

    atualizar(orItem: ORItem): Observable<ORItem> {
        const url = `${c.api}/ORItem`;
        
        return this.http.put<ORItem>(url, orItem).pipe(
            map((obj) => obj)
        );
    }

    deletar(codOR: number): Observable<ORItem> {
        const url = `${c.api}/ORItem/${codOR}`;
        
        return this.http.delete<ORItem>(url).pipe(
          map((obj) => obj)
        );
    }
}