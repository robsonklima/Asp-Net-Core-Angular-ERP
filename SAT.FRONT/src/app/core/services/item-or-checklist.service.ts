import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ItemXORCheckList, ItemXORCheckListData, ItemXORCheckListParameters } from '../types/item-or-checklist.types';

@Injectable({
    providedIn: 'root'
})
export class ItemXORCheckListService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: ItemXORCheckListParameters): Observable<ItemXORCheckListData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/ItemXORCheckList`, { params: params }).pipe(
          map((data: ItemXORCheckListData) => data)
        )
    }

    obterPorCodigo(CodItemChecklist: number): Observable<ItemXORCheckList> {
        const url = `${c.api}/ItemXORCheckList/${CodItemChecklist}`;
        return this.http.get<ItemXORCheckList>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(itemXORCheckList: ItemXORCheckList): Observable<ItemXORCheckList> {
        return this.http.post<ItemXORCheckList>(`${c.api}/ItemXORCheckList`, itemXORCheckList).pipe(
            map((obj) => obj)
        );
    }

    atualizar(itemXORCheckList: ItemXORCheckList): Observable<ItemXORCheckList> {
        const url = `${c.api}/ItemXORCheckList`;
        
        return this.http.put<ItemXORCheckList>(url, itemXORCheckList).pipe(
            map((obj) => obj)
        );
    }

    deletar(codItemChecklist: number): Observable<ItemXORCheckList> {
        const url = `${c.api}/ItemXORCheckList/${codItemChecklist}`;
        
        return this.http.delete<ItemXORCheckList>(url).pipe(
          map((obj) => obj)
        );
    }
}