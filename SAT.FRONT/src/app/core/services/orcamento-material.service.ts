import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrcamentoMaterial } from '../types/orcamento.types';

@Injectable({
    providedIn: 'root'
})
export class OrcamentoMaterialService
{
    constructor (private http: HttpClient) { }

    criar(material: OrcamentoMaterial): Observable<OrcamentoMaterial>
    {
        return this.http.post<OrcamentoMaterial>(`${c.api}/OrcamentoMaterial`, material).pipe(
            map((obj) => obj)
        );
    }
}