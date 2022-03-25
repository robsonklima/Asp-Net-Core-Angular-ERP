import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { FerramentaTecnico, FerramentaTecnicoData, FerramentaTecnicoParameters } from '../types/ferramenta-tecnico.types';

@Injectable({
    providedIn: 'root'
})
export class FerramentaTecnicoService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: FerramentaTecnicoParameters): Observable<FerramentaTecnicoData> {

        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/FerramentaTecnico`, { params: params }).pipe(
            map((data: FerramentaTecnicoData) => data)
        )
    }

    obterPorCodigo(codFerramentaTecnico: number): Observable<FerramentaTecnico> {
        const url = `${c.api}/FerramentaTecnico/${codFerramentaTecnico}`;
        return this.http.get<FerramentaTecnico>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(ferramentaTecnico: FerramentaTecnico): Observable<FerramentaTecnico> {
        return this.http.post<FerramentaTecnico>(`${c.api}/FerramentaTecnico`, ferramentaTecnico).pipe(
            map((obj) => obj)
        );
    }

    atualizar(ferramentaTecnico: FerramentaTecnico): Observable<FerramentaTecnico> {
        const url = `${c.api}/FerramentaTecnico`;

        return this.http.put<FerramentaTecnico>(url, ferramentaTecnico).pipe(
            map((obj) => obj)
        );
    }

    deletar(codFerramentaTecnico: number): Observable<FerramentaTecnico> {
        const url = `${c.api}/FerramentaTecnico/${codFerramentaTecnico}`;

        return this.http.delete<FerramentaTecnico>(url).pipe(
            map((obj) => obj)
        );
    }
}