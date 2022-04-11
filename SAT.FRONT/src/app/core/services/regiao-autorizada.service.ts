import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { RegiaoAutorizada, RegiaoAutorizadaData,
         RegiaoAutorizadaParameters } from '../types/regiao-autorizada.types';

@Injectable({
    providedIn: 'root'
})
export class RegiaoAutorizadaService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: RegiaoAutorizadaParameters): Observable<RegiaoAutorizadaData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/RegiaoAutorizada`, { params: params }).pipe(
          map((data: RegiaoAutorizadaData) => data)
        )
    }

    obterPorCodigo(codRegiao: number, codAutorizada: number, codFilial: number): Observable<RegiaoAutorizada> {
        const url = `${c.api}/RegiaoAutorizada/${codRegiao}/${codAutorizada}/${codFilial}`;
        return this.http.get<RegiaoAutorizada>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(regiaoAutorizada: RegiaoAutorizada): Observable<RegiaoAutorizada> {
        return this.http.post<RegiaoAutorizada>(`${c.api}/RegiaoAutorizada`, regiaoAutorizada).pipe(
            map((obj) => obj)
        );
    }

    atualizar(regiaoAutorizada: RegiaoAutorizada, codRegiao: number, codAutorizada: number, codFilial: number): Observable<RegiaoAutorizada> {
        const url = `${c.api}/RegiaoAutorizada/${codRegiao}/${codAutorizada}/${codFilial}`;
        
        return this.http.put<RegiaoAutorizada>(url, regiaoAutorizada).pipe(
            map((obj) => obj)
        );
    }

    deletar(codRegiao: number, codAutorizada: number, codFilial: number): Observable<RegiaoAutorizada> {
        const url = `${c.api}/RegiaoAutorizada/${codRegiao}/${codAutorizada}/${codFilial}`;
        
        return this.http.delete<RegiaoAutorizada>(url).pipe(
          map((obj) => obj)
        );
    }
}