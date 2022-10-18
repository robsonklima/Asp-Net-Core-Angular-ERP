import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ORStatus, ORStatusData, ORStatusParameters } from '../types/or-status.types';

@Injectable({
    providedIn: 'root'
})
export class ORStatusService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: ORStatusParameters): Observable<ORStatusData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/ORStatus`, { params: params }).pipe(
          map((data: ORStatusData) => data)
        )
    }

    obterPorCodigo(codOR: number): Observable<ORStatus> {
        const url = `${c.api}/ORStatus/${codOR}`;
        return this.http.get<ORStatus>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(orStatus: ORStatus): Observable<ORStatus> {
        return this.http.post<ORStatus>(`${c.api}/ORStatus`, orStatus).pipe(
            map((obj) => obj)
        );
    }

    atualizar(orStatus: ORStatus): Observable<ORStatus> {
        const url = `${c.api}/ORStatus`;
        
        return this.http.put<ORStatus>(url, orStatus).pipe(
            map((obj) => obj)
        );
    }

    deletar(codOR: number): Observable<ORStatus> {
        const url = `${c.api}/ORStatus/${codOR}`;
        
        return this.http.delete<ORStatus>(url).pipe(
          map((obj) => obj)
        );
    }
}