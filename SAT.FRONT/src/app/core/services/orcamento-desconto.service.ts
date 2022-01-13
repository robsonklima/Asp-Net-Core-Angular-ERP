import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrcamentoDesconto } from '../types/orcamento.types';

@Injectable({
    providedIn: 'root'
})
export class OrcamentoDescontoService
{
    constructor (private http: HttpClient) { }

    criar(desconto: OrcamentoDesconto): Observable<OrcamentoDesconto>
    {
        return this.http.post<OrcamentoDesconto>(`${c.api}/OrcamentoDesconto`, desconto).pipe(map((obj) => obj));
    }

    atualizar(desconto: OrcamentoDesconto): Observable<OrcamentoDesconto>
    {
        const url = `${c.api}/OrcamentoDesconto`;
        return this.http.put<OrcamentoDesconto>(url, desconto).pipe(map((obj) => obj));
    }

    deletar(codOrcamentoDesc: number): Observable<OrcamentoDesconto>
    {
        const url = `${c.api}/OrcamentoDesconto/${codOrcamentoDesc}`;
        return this.http.delete<OrcamentoDesconto>(url).pipe(map((obj) => obj));
    }
}