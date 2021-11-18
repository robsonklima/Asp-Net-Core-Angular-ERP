import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DespesaPeriodoTecnico, DespesaPeriodoTecnicoData, DespesaPeriodoTecnicoParameters } from '../types/despesa-periodo.types';
import { DespesaPeriodoTecnicoAtendimentoData } from '../types/despesa-adiantamento.types';

@Injectable({
    providedIn: 'root'
})
export class DespesaPeriodoTecnicoService
{
    constructor (private http: HttpClient) { }

    obterAtendimentos(parameters: DespesaPeriodoTecnicoParameters): Observable<DespesaPeriodoTecnicoAtendimentoData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/DespesaPeriodoTecnico/Atendimentos`, { params: params })
            .pipe(map((data: DespesaPeriodoTecnicoAtendimentoData) => data));
    }

    obterPorParametros(parameters: DespesaPeriodoTecnicoParameters): Observable<DespesaPeriodoTecnicoData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/DespesaPeriodoTecnico`, { params: params })
            .pipe(map((data: DespesaPeriodoTecnicoData) => data));
    }

    obterPorCodigo(codDespesaPeriodoTecnico: number): Observable<DespesaPeriodoTecnico>
    {
        return this.http.get<DespesaPeriodoTecnico>(
            `${c.api}/DespesaPeriodoTecnico/${codDespesaPeriodoTecnico}`)
            .pipe(map((obj) => obj));
    }

    criar(despesaPeriodoTecnico: DespesaPeriodoTecnico): Observable<DespesaPeriodoTecnico>
    {
        return this.http.post<DespesaPeriodoTecnico>(
            `${c.api}/DespesaPeriodoTecnico`, despesaPeriodoTecnico)
            .pipe(map((obj) => obj));
    }

    atualizar(despesaPeriodoTecnico: DespesaPeriodoTecnico): Observable<DespesaPeriodoTecnico>
    {
        return this.http.put<DespesaPeriodoTecnico>(
            `${c.api}/DespesaPeriodoTecnico`, despesaPeriodoTecnico)
            .pipe(map((obj) => obj));
    }

    deletar(codDespesaPeriodoTecnico: number): Observable<DespesaPeriodoTecnico>
    {
        return this.http.delete<DespesaPeriodoTecnico>(
            `${c.api}/DespesaPeriodoTecnico/${codDespesaPeriodoTecnico}`)
            .pipe(map((obj) => obj));
    }
}