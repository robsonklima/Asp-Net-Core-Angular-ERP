import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ItemDefeitoParameters, ItemDefeitoData, ItemDefeito } from '../types/item-defeito.types';

@Injectable({
    providedIn: 'root'
})
export class ORService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: ItemDefeitoParameters): Observable<ItemDefeitoData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/ItemDefeito`, { params: params }).pipe(
          map((data: ItemDefeitoData) => data)
        )
    }

    obterPorCodigo(codItemDefeito: number): Observable<ItemDefeito> {
        const url = `${c.api}/ItemDefeito/${codItemDefeito}`;
        return this.http.get<ItemDefeito>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(itemDefeito: ItemDefeito): Observable<ItemDefeito> {
        return this.http.post<ItemDefeito>(`${c.api}/ItemDefeito`, itemDefeito).pipe(
            map((obj) => obj)
        );
    }

    atualizar(itemDefeito: ItemDefeito): Observable<ItemDefeito> {
        const url = `${c.api}/ItemDefeito`;
        
        return this.http.put<ItemDefeito>(url, itemDefeito).pipe(
            map((obj) => obj)
        );
    }

    deletar(codItemDefeito: number): Observable<ItemDefeito> {
        const url = `${c.api}/ItemDefeito/${codItemDefeito}`;
        
        return this.http.delete<ItemDefeito>(url).pipe(
          map((obj) => obj)
        );
    }
}