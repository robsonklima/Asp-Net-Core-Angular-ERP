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

    criar(material: OrcamentoMaoDeObra): Observable<OrcamentoMaoDeObra>
    {
        return this.http.post<OrcamentoMaoDeObra>(`${c.api}/OrcamentoMaoDeObra`, material).pipe(
            map((obj) => obj)
        );
    }
}