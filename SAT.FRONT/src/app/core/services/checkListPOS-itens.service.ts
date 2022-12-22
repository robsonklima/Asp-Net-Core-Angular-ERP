import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { CheckListPOSItensParameters, CheckListPOSItensData, CheckListPOSItens } from '../types/checkListPOS-itens.types';



@Injectable({
  providedIn: 'root'
})
export class CheckListPOSItensService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: CheckListPOSItensParameters): Observable<CheckListPOSItensData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/CheckListPOSItens`, { params: params }).pipe(
      map((data: CheckListPOSItensData) => data)
    )
  }

  obterPorCodigo(codCheckListPOSItens: number): Observable<CheckListPOSItens> {
    const url = `${c.api}/CheckListPOSItens/${codCheckListPOSItens}`;
    return this.http.get<CheckListPOSItens>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(checkListPOSItens: CheckListPOSItens): Observable<CheckListPOSItens> {
    return this.http.post<CheckListPOSItens>(`${c.api}/CheckListPOSItens`, checkListPOSItens).pipe(
      map((obj) => obj)
    );
  }

  atualizar(checkListPOSItens: CheckListPOSItens): Observable<CheckListPOSItens> {
    const url = `${c.api}/CheckListPOSItens`;

    return this.http.put<CheckListPOSItens>(url, checkListPOSItens).pipe(
      map((obj) => obj)
    );
  }

  deletar(codCheckListPOSItens: number): Observable<CheckListPOSItens> {
    const url = `${c.api}/CheckListPOSItens/${codCheckListPOSItens}`;

    return this.http.delete<CheckListPOSItens>(url).pipe(
      map((obj) => obj)
    );
  }
}
