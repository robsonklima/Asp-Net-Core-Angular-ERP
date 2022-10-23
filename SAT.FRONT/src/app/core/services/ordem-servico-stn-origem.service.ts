import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrdemServicoSTNOrigem, OrdemServicoSTNOrigemData, OrdemServicoSTNOrigemParameters } from '../types/ordem-servico-stn-origem.types';

@Injectable({
    providedIn: 'root'
})
export class OrdemServicoSTNOrigemService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: OrdemServicoSTNOrigemParameters): Observable<OrdemServicoSTNOrigemData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/OrdemServicoSTNOrigem`, { params: params }).pipe(
            map((data: OrdemServicoSTNOrigemData) => data)
        )
    }

    obterPorCodigo(CodOrigemChamadoSTN: number): Observable<OrdemServicoSTNOrigem> {
        const url = `${c.api}/OrdemServicoSTNOrigem/${CodOrigemChamadoSTN}`;
        return this.http.get<OrdemServicoSTNOrigem>(url).pipe(
            map((obj) => obj)
        );
    }
}