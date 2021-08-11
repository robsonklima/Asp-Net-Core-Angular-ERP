import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { TipoRota, TipoRotaData, TipoRotaParameters } from '../types/tipo-rota.types';

@Injectable({
    providedIn: 'root'
})
export class TipoRotaService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: TipoRotaParameters): Observable<TipoRotaData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/TipoRota`, { params: params }).pipe(
          map((data: TipoRotaData) => data)
        )
    }

    obterPorCodigo(codTipoRota: number): Observable<TipoRota> {
        const url = `${c.api}/TipoRota/${codTipoRota}`;
        return this.http.get<TipoRota>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(tipoRota: TipoRota): Observable<TipoRota> {
        return this.http.post<TipoRota>(`${c.api}/TipoRota`, tipoRota).pipe(
            map((obj) => obj)
        );
    }

    atualizar(tipoRota: TipoRota): Observable<TipoRota> {
        const url = `${c.api}/TipoRota`;
        
        return this.http.put<TipoRota>(url, tipoRota).pipe(
            map((obj) => obj)
        );
    }

    deletar(codTipoRota: number): Observable<TipoRota> {
        const url = `${c.api}/TipoRota/${codTipoRota}`;
        
        return this.http.delete<TipoRota>(url).pipe(
          map((obj) => obj)
        );
    }
}