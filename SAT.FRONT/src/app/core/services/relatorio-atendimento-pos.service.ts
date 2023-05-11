import { RelatorioAtendimentoPOS, RelatorioAtendimentoPOSData, RelatorioAtendimentoPOSParameters } from '../types/relatorio-atendimento-pos.types';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { appConfig as c } from 'app/core/config/app.config';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class RelatorioAtendimentoPOSService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: RelatorioAtendimentoPOSParameters): Observable<RelatorioAtendimentoPOSData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/RelatorioAtendimentoPOS`, { params: params }).pipe(
      map((data: RelatorioAtendimentoPOSData) => data)
    )
  }

  obterPorCodigo(codigo: number): Observable<RelatorioAtendimentoPOS> {
    const url = `${c.api}/RelatorioAtendimentoPOS/${codigo}`;
    return this.http.get<RelatorioAtendimentoPOS>(url).pipe(
      map((obj) => obj)
    );  
}

  criar(relatorio: RelatorioAtendimentoPOS): Observable<RelatorioAtendimentoPOS> {
    return this.http.post<RelatorioAtendimentoPOS>(`${c.api}/RelatorioAtendimentoPOS`, relatorio).pipe(
      map((obj) => obj)
    );
  }

  atualizar(relatorio: RelatorioAtendimentoPOS): Observable<RelatorioAtendimentoPOS> {
    const url = `${c.api}/RelatorioAtendimentoPOS`;
    return this.http.put<RelatorioAtendimentoPOS>(url, relatorio).pipe(
      map((obj) => obj)
    );
  }

  deletar(codigo: number): Observable<RelatorioAtendimentoPOS> {
    const url = `${c.api}/RelatorioAtendimentoPOS/${codigo}`;

    return this.http.delete<RelatorioAtendimentoPOS>(url).pipe(
      map((obj) => obj)
    );
  }
}
