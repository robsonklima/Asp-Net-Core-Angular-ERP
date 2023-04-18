import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { AdiantamentoRDsPendentesViewData, DespesaAdiantamento, DespesaAdiantamentoData, DespesaAdiantamentoParameters, DespesaAdiantamentoSolicitacao, ViewMediaDespesasAdiantamento } from '../types/despesa-adiantamento.types';

@Injectable({
    providedIn: 'root'
})
export class DespesaAdiantamentoService
{
    constructor (private http: HttpClient) { }

    obterPorParametros(parameters: DespesaAdiantamentoParameters): Observable<DespesaAdiantamentoData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/DespesaAdiantamento`, { params: params })
            .pipe(map((data: DespesaAdiantamentoData) => data));
    }

    obterPorView(parameters: DespesaAdiantamentoParameters): Observable<AdiantamentoRDsPendentesViewData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/DespesaAdiantamento/Pendentes`, { params: params })
            .pipe(map((data: AdiantamentoRDsPendentesViewData) => data));
    }

    obterPorCodigo(codDespesaAdiantamento: number): Observable<DespesaAdiantamento>
    {
        return this.http.get<DespesaAdiantamento>(
            `${c.api}/DespesaAdiantamento/${codDespesaAdiantamento}`)
            .pipe(map((obj) => obj));
    }

    criar(despesaAdiantamento: DespesaAdiantamento): Observable<DespesaAdiantamento>
    {
        return this.http.post<DespesaAdiantamento>(
            `${c.api}/DespesaAdiantamento`, despesaAdiantamento)
            .pipe(map((obj) => obj));
    }

    criarSolicitacao(solicitacao: DespesaAdiantamentoSolicitacao): Observable<DespesaAdiantamentoSolicitacao>
    {
        return this.http.post<DespesaAdiantamentoSolicitacao>(
            `${c.api}/DespesaAdiantamento/Solicitacao`, solicitacao)
            .pipe(map((obj) => obj));
    }

    atualizar(despesaAdiantamento: DespesaAdiantamento): Observable<DespesaAdiantamento>
    {
        return this.http.put<DespesaAdiantamento>(
            `${c.api}/DespesaAdiantamento`, despesaAdiantamento)
            .pipe(map((obj) => obj));
    }

    obterMedia(codTecnico: number): Observable<ViewMediaDespesasAdiantamento[]>
    {
        return this.http.get<ViewMediaDespesasAdiantamento[]>(
            `${c.api}/DespesaAdiantamento/Media/${codTecnico}`)
            .pipe(map((obj) => obj));
    }
}