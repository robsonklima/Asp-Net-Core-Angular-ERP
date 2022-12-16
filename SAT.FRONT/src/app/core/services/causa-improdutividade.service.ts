import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { CausaImprodutividadeParameters, CausaImprodutividadeData, CausaImprodutividade } from '../types/causa-improdutividade.types';




@Injectable({
  providedIn: 'root'
})
export class CausaImprodutividadeService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: CausaImprodutividadeParameters): Observable<CausaImprodutividadeData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/CausaImprodutividade`, { params: params }).pipe(
      map((data: CausaImprodutividadeData) => data)
    )
  }

  obterPorCodigo(codCausaImprodutividade: number): Observable<CausaImprodutividade> {
    const url = `${c.api}/CausaImprodutividade/${codCausaImprodutividade}`;
    return this.http.get<CausaImprodutividade>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(causaImprodutividade: CausaImprodutividade): Observable<CausaImprodutividade> {
    return this.http.post<CausaImprodutividade>(`${c.api}/CausaImprodutividade`, causaImprodutividade).pipe(
      map((obj) => obj)
    );
  }

  atualizar(causaImprodutividade: CausaImprodutividade): Observable<CausaImprodutividade> {
    const url = `${c.api}/CausaImprodutividade`;

    return this.http.put<CausaImprodutividade>(url, causaImprodutividade).pipe(
      map((obj) => obj)
    );
  }

  deletar(codCausaImprodutividade: number): Observable<CausaImprodutividade> {
    const url = `${c.api}/CausaImprodutividade/${codCausaImprodutividade}`;

    return this.http.delete<CausaImprodutividade>(url).pipe(
      map((obj) => obj)
    );
  }
}
