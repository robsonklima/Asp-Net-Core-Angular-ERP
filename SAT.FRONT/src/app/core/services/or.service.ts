import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OR, ORData, ORParameters } from '../types/OR.types';

@Injectable({
    providedIn: 'root'
})
export class ORService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: ORParameters): Observable<ORData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/OR`, { params: params }).pipe(
          map((data: ORData) => data)
        )
    }

    obterPorCodigo(codOR: number): Observable<OR> {
        const url = `${c.api}/OR/${codOR}`;
        return this.http.get<OR>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(or: OR): Observable<OR> {
        return this.http.post<OR>(`${c.api}/OR`, or).pipe(
            map((obj) => obj)
        );
    }

    atualizar(or: OR): Observable<OR> {
        const url = `${c.api}/OR`;
        
        return this.http.put<OR>(url, or).pipe(
            map((obj) => obj)
        );
    }

    deletar(codOR: number): Observable<OR> {
        const url = `${c.api}/OR/${codOR}`;
        
        return this.http.delete<OR>(url).pipe(
          map((obj) => obj)
        );
    }
}