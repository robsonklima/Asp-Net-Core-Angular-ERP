import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ItemSolucaoParameters, ItemSolucaoData, ItemSolucao } from '../types/item-Solucao.types';

@Injectable({
    providedIn: 'root'
})
export class ORService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: ItemSolucaoParameters): Observable<ItemSolucaoData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/ItemSolucao`, { params: params }).pipe(
          map((data: ItemSolucaoData) => data)
        )
    }

    obterPorCodigo(codItemSolucao: number): Observable<ItemSolucao> {
        const url = `${c.api}/ItemSolucao/${codItemSolucao}`;
        return this.http.get<ItemSolucao>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(itemSolucao: ItemSolucao): Observable<ItemSolucao> {
        return this.http.post<ItemSolucao>(`${c.api}/ItemSolucao`, itemSolucao).pipe(
            map((obj) => obj)
        );
    }

    atualizar(itemSolucao: ItemSolucao): Observable<ItemSolucao> {
        const url = `${c.api}/ItemSolucao`;
        
        return this.http.put<ItemSolucao>(url, itemSolucao).pipe(
            map((obj) => obj)
        );
    }

    deletar(codItemSolucao: number): Observable<ItemSolucao> {
        const url = `${c.api}/ItemSolucao/${codItemSolucao}`;
        
        return this.http.delete<ItemSolucao>(url).pipe(
          map((obj) => obj)
        );
    }
}