import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrcamentoDeslocamento } from '../types/orcamento.types';

@Injectable({
    providedIn: 'root'
})
export class OrcamentoDeslocamentoService
{
    constructor (private http: HttpClient) { }

    criar(deslocamento: OrcamentoDeslocamento): Observable<OrcamentoDeslocamento>
    {
        return this.http.post<OrcamentoDeslocamento>(`${c.api}/OrcamentoDeslocamento`, deslocamento).pipe(
            map((obj) => obj)
        );
    }
}