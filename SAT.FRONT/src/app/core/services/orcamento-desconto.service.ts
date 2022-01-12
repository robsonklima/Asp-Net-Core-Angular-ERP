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

    criar(servico: OrcamentoDesconto): Observable<OrcamentoDesconto>
    {
        return this.http.post<OrcamentoDesconto>(`${c.api}/OrcamentoDesconto`, servico).pipe(map((obj) => obj));
    }

    atualizar(servico: OrcamentoDesconto): Observable<OrcamentoDesconto>
    {
        const url = `${c.api}/OrcamentoDesconto`;
        return this.http.put<OrcamentoDesconto>(url, servico).pipe(map((obj) => obj));
    }
}