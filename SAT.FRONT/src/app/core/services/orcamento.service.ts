import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Orcamento, OrcamentoData, OrcamentoParameters } from '../types/orcamento.types';

@Injectable({
    providedIn: 'root'
})
export class OrcamentoService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: OrcamentoParameters): Observable<OrcamentoData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/Orcamento`, { params: params }).pipe(
          map((data: OrcamentoData) => data)
        )
    }

    obterPorCodigo(codOrcamento: number): Observable<Orcamento> {
        const url = `${c.api}/Orcamento/${codOrcamento}`;
        return this.http.get<Orcamento>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(orcamento: Orcamento): Observable<Orcamento> {
        return this.http.post<Orcamento>(`${c.api}/Orcamento`, orcamento).pipe(
            map((obj) => obj)
        );
    }

    atualizar(orcamento: Orcamento): Observable<Orcamento> {
        const url = `${c.api}/Orcamento`;
        
        return this.http.put<Orcamento>(url, orcamento).pipe(
            map((obj) => obj)
        );
    }

    deletar(codOrcamento: number): Observable<Orcamento> {
        const url = `${c.api}/Orcamento/${codOrcamento}`;
        
        return this.http.delete<Orcamento>(url).pipe(
          map((obj) => obj)
        );
    }
}