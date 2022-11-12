import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OR, ORData, ORParameters } from '../types/OR.types';
import { ORSolucaoParameters, ORSolucaoData, ORSolucao } from '../types/or-solucao.types';

@Injectable({
    providedIn: 'root'
})
export class ORSolucaoService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: ORSolucaoParameters): Observable<ORSolucaoData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/ORSolucao`, { params: params }).pipe(
          map((data: ORSolucaoData) => data)
        )
    }

    obterPorCodigo(codSolucao: number): Observable<ORSolucao> {
        const url = `${c.api}/ORSolucao/${codSolucao}`;
        return this.http.get<ORSolucao>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(orSolucao: ORSolucao): Observable<ORSolucao> {
        return this.http.post<ORSolucao>(`${c.api}/ORSolucao`, orSolucao).pipe(
            map((obj) => obj)
        );
    }

    atualizar(orSolucao: ORSolucao): Observable<ORSolucao> {
        const url = `${c.api}/ORSolucao`;
        
        return this.http.put<ORSolucao>(url, orSolucao).pipe(
            map((obj) => obj)
        );
    }

    deletar(codSolucao: number): Observable<ORSolucao> {
        const url = `${c.api}/ORSolucao/${codSolucao}`;
        
        return this.http.delete<ORSolucao>(url).pipe(
          map((obj) => obj)
        );
    }
}