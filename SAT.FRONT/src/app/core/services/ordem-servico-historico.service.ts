import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrdemServicoHistoricoData, OrdemServicoHistoricoParameters } from '../types/ordem-servico-historico.types';

@Injectable({
    providedIn: 'root'
})
export class OrdemServicoHistoricoService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: OrdemServicoHistoricoParameters): Observable<OrdemServicoHistoricoData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/OrdemServicoHistorico`, { params: params }).pipe(
          map((data: OrdemServicoHistoricoData) => data)
        )
    }
}