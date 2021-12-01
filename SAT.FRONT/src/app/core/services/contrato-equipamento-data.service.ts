import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ContratoEquipamentoDataData, ContratoEquipamentoDataParameters } from '../types/contrato-equipamento-data.types';



@Injectable({
    providedIn: 'root'
})
export class ContratoEquipamentoDataService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: ContratoEquipamentoDataParameters): Observable<ContratoEquipamentoDataData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/ContratoEquipamentoData`, { params: params }).pipe(
            map((data: ContratoEquipamentoDataData) => data)
        )
    }
}