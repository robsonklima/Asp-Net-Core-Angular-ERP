import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ImprodutividadeParameters, ImprodutividadeData, Improdutividade } from '../types/improdutividade.types';

@Injectable({
  providedIn: 'root'
})
export class ImprodutividadeService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: ImprodutividadeParameters): Observable<ImprodutividadeData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Improdutividade`, { params: params }).pipe(
      map((data: ImprodutividadeData) => data)
    )
  }

  obterPorCodigo(codImprodutividade: number): Observable<Improdutividade> {
    const url = `${c.api}/Improdutividade/${codImprodutividade}`;
    return this.http.get<Improdutividade>(url).pipe(
      map((obj) => obj)
    );
  }
}
