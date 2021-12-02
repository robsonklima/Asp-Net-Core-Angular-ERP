import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DespesaProtocoloPeriodoTecnico } from '../types/despesa-protocolo.types';

@Injectable({
    providedIn: 'root'
})
export class DespesaProtocoloPeriodoTecnicoService
{
    constructor (private http: HttpClient) { }

    criar(item: DespesaProtocoloPeriodoTecnico): Observable<DespesaProtocoloPeriodoTecnico>
    {
        return this.http.post<DespesaProtocoloPeriodoTecnico>(
            `${c.api}/DespesaProtocoloPeriodoTecnico`, item)
            .pipe(map((obj) => obj));
    }
}