import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { TipoChamadoSTNParameters, TipoChamadoSTNData, TipoChamadoSTN } from '../types/tipo-chamado-stn.types';


@Injectable({
  providedIn: 'root'
})
export class TipoChamadoSTNService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: TipoChamadoSTNParameters): Observable<TipoChamadoSTNData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/TipoChamadoSTN`, { params: params }).pipe(
      map((data: TipoChamadoSTNData) => data)
    )
  }

  obterPorCodigo(codTipoChamadoSTN: number): Observable<TipoChamadoSTN> {
    const url = `${c.api}/TipoChamadoSTN/${codTipoChamadoSTN}`;
    return this.http.get<TipoChamadoSTN>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(tipoChamadoSTN: TipoChamadoSTN): Observable<TipoChamadoSTN> {
    return this.http.post<TipoChamadoSTN>(`${c.api}/TipoChamadoSTN`, tipoChamadoSTN).pipe(
      map((obj) => obj)
    );
  }

  atualizar(tipoChamadoSTN: TipoChamadoSTN): Observable<TipoChamadoSTN> {
    const url = `${c.api}/TipoChamadoSTN`;

    return this.http.put<TipoChamadoSTN>(url, tipoChamadoSTN).pipe(
      map((obj) => obj)
    );
  }

  deletar(codTipoChamadoSTN: number): Observable<TipoChamadoSTN> {
    const url = `${c.api}/TipoChamadoSTN/${codTipoChamadoSTN}`;

    return this.http.delete<TipoChamadoSTN>(url).pipe(
      map((obj) => obj)
    );
  }
}
