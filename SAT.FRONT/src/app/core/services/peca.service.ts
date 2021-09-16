import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Peca, PecaData, PecaParameters } from '../types/peca.types';

@Injectable({
    providedIn: 'root'
})
export class PecaService {
    constructor(private http: HttpClient) {}

    requestOptions: Object = {
        responseType: 'blob'
    }

    obterPorParametros(parameters: PecaParameters): Observable<PecaData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get<PecaData>(`${c.api}/Peca`, { params: params }).pipe(
            map((data) => data)
        )
    }

    obterPorCodigo(codPeca: number): Observable<Peca> {
        const url = `${c.api}/Peca/${codPeca}`;
        return this.http.get<Peca>(url).pipe(
            map((obj) => obj)
        );
    }
    
    exportarExcel(): Promise<ArrayBuffer> {
        return this.http.get<any>(`${c.api}/Peca/export`, this.requestOptions).pipe(map((file:ArrayBuffer) => {
            return file;
          })).toPromise();
    }

    criar(peca: Peca): Observable<Peca> {
        return this.http.post<Peca>(`${c.api}/Peca`, peca).pipe(
            map((obj) => obj)
        );
    }

    atualizar(peca: Peca): Observable<Peca> {
        const url = `${c.api}/Peca`;
        return this.http.put<Peca>(url, peca).pipe(
                ((obj) => obj)
        );
    }

    deletar(codPeca: number): Observable<Peca> {
        const url = `${c.api}/Peca/${codPeca}`;
        return this.http.delete<Peca>(url).pipe(
            map((obj) => obj)
        );
    }
}