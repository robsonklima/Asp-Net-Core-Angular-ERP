import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrcamentoOutroServico } from '../types/orcamento.types';

@Injectable({
    providedIn: 'root'
})
export class OrcamentoOutroServicoService
{
    constructor (private http: HttpClient) { }

    criar(servico: OrcamentoOutroServico): Observable<OrcamentoOutroServico>
    {
        return this.http.post<OrcamentoOutroServico>(`${c.api}/OrcamentoOutroServico`, servico).pipe(map((obj) => obj));
    }

    atualizar(servico: OrcamentoOutroServico): Observable<OrcamentoOutroServico>
    {
        const url = `${c.api}/OrcamentoOutroServico`;
        return this.http.put<OrcamentoOutroServico>(url, servico).pipe(map((obj) => obj));
    }
}