import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Tecnico, TecnicoData, TecnicoParameters, ViewTecnicoDeslocamentoData } from '../types/tecnico.types';

@Injectable({
    providedIn: 'root'
})
export class TecnicoService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: TecnicoParameters): Observable<TecnicoData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/Tecnico`, { params: params }).pipe(

            map((data: TecnicoData) => data)
        )
    }

    obterDeslocamentos(parameters: TecnicoParameters): Observable<ViewTecnicoDeslocamentoData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/Tecnico/Deslocamentos`, { params: params }).pipe(

            map((data: ViewTecnicoDeslocamentoData) => data)
        )
    }

    obterPorCodigo(codTecnico: number): Observable<Tecnico> {
        const url = `${c.api}/Tecnico/${codTecnico}`;
        return this.http.get<Tecnico>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(tecnico: Tecnico): Observable<Tecnico> {
        return this.http.post<Tecnico>(`${c.api}/Tecnico`, tecnico).pipe(
            map((obj) => obj)
        );
    }

    atualizar(tecnico: Tecnico): Observable<Tecnico> {
        const url = `${c.api}/Tecnico`;

        return this.http.put<Tecnico>(url, tecnico).pipe(
            map((obj) => obj)
        );
    }

    deletar(codTecnico: number): Observable<Tecnico> {
        const url = `${c.api}/Tecnico/${codTecnico}`;

        return this.http.delete<Tecnico>(url).pipe(
            map((obj) => obj)
        );
    }
}