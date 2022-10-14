import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrcamentoParameters, OrcamentoData, Orcamento, OrcamentoAprovacao, ViewOrcamentoLista, ViewOrcamentoListaData } from '../types/orcamento.types';
import Enumerable from 'linq';
import _ from 'lodash';

@Injectable({
    providedIn: 'root'
})
export class OrcamentoService
{
    constructor (private http: HttpClient) { }

    obterPorView(parameters: OrcamentoParameters): Observable<ViewOrcamentoListaData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/Orcamento/View`, { params: params }).pipe(
            map((data: ViewOrcamentoListaData) => data)
        )
    }

    obterPorParametros(parameters: OrcamentoParameters): Observable<OrcamentoData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/Orcamento`, { params: params }).pipe(
            map((data: OrcamentoData) => data)
        )
    }

    obterPorCodigo(codOrcamento: number): Observable<Orcamento>
    {
        const url = `${c.api}/Orcamento/${codOrcamento}`;
        return this.http.get<Orcamento>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(orcamento: Orcamento): Observable<Orcamento>
    {
        return this.http.post<Orcamento>(`${c.api}/Orcamento`, orcamento).pipe(
            map((obj) => obj)
        );
    }

    atualizar(orcamento: Orcamento): Observable<Orcamento>
    {
        const url = `${c.api}/Orcamento`;

        return this.http.put<Orcamento>(url, orcamento).pipe(
            map((obj) => obj)
        );
    }

    deletar(codOrcamento: number): Observable<Orcamento>
    {
        const url = `${c.api}/Orcamento/${codOrcamento}`;

        return this.http.delete<Orcamento>(url).pipe(
            map((obj) => obj));
    }

    atualizarTotalizacao(codOrcamento: number)
    {
        this.obterPorCodigo(codOrcamento).subscribe(orc =>
        {
            orc = this.calculaTotalizacao(orc);
            this.atualizar(orc).toPromise();
        });
    }

    aprovar(aprovacao: OrcamentoAprovacao): Observable<OrcamentoAprovacao>
    {
        return this.http.post<OrcamentoAprovacao>(`${c.api}/Orcamento/AprovacaoCliente`, aprovacao).pipe(
            map((obj) => obj)
        );
    }

    calculaTotalizacao(orcamento: Orcamento): Orcamento {  
        const valorMateriais = _.sumBy(orcamento?.materiais, (material) => { return material?.valorTotal; });
        const valorOutrosServicos = _.sumBy(orcamento?.outrosServicos, (servico) => { return servico?.valorTotal; });
        const valorKmDeslocamento = orcamento?.orcamentoDeslocamento?.valorTotalKmDeslocamento;
        const valorKmRodado = orcamento?.orcamentoDeslocamento?.valorTotalKmRodado;
        const valorMaoDeObra = orcamento?.maoDeObra?.valorTotal;
        const valorDescontos = _.sumBy(orcamento?.descontos, (desconto) => { return desconto?.valorTotal; });
        const valorDescontosMateriais = _.sumBy(orcamento?.materiais, (desconto) => { return desconto?.valorDesconto; });
        
        orcamento.valorTotal = (valorMateriais + valorOutrosServicos + valorKmDeslocamento + valorKmRodado + valorMaoDeObra) - valorDescontos;
        orcamento.valorTotalDesconto = valorDescontos + valorDescontosMateriais;

        if (orcamento.valorTotal)
            orcamento.valorTotal = parseFloat(orcamento.valorTotal.toString());

        if (orcamento.valorTotalDesconto)
        orcamento.valorTotalDescontoDesconto = parseFloat(orcamento.valorTotalDesconto.toString());

        if (isNaN(orcamento.valorTotal))
            orcamento.valorTotal = 0;

        if (isNaN(orcamento.valorTotalDesconto))
            orcamento.valorTotalDesconto = 0;
        
        return orcamento;
    }
}