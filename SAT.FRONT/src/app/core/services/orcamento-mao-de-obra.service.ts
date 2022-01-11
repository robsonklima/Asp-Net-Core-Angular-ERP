import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrcamentoMaoDeObra } from '../types/orcamento.types';

@Injectable({
    providedIn: 'root'
})
export class OrcamentoMaoDeObraService
{
    constructor (private http: HttpClient) { }

    criar(maoDeObra: OrcamentoMaoDeObra): Observable<OrcamentoMaoDeObra>
    {
        return this.http.post<OrcamentoMaoDeObra>(`${c.api}/OrcamentoMaoDeObra`, maoDeObra).pipe(
            map((obj) => obj)
        );
    }

    atualizar(maoDeObra: OrcamentoMaoDeObra): Observable<OrcamentoMaoDeObra>
    {
        const url = `${c.api}/OrcamentoMaoDeObra`;

        return this.http.put<OrcamentoMaoDeObra>(url, maoDeObra).pipe(
            map((obj) => obj)
        );
    }
}