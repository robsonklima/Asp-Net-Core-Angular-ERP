import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { CheckListPOS, CheckListPOSData, CheckListPOSParameters } from '../types/checkListPOS.types';



@Injectable({
  providedIn: 'root'
})
export class CheckListPOSService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: CheckListPOSParameters): Observable<CheckListPOSData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/CheckListPOS`, { params: params }).pipe(
      map((data: CheckListPOSData) => data)
    )
  }

  obterPorCodigo(codCheckListPOS: number): Observable<CheckListPOS> {
    const url = `${c.api}/CheckListPOS/${codCheckListPOS}`;
    return this.http.get<CheckListPOS>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(checkListPOS: CheckListPOS): Observable<CheckListPOS> {
    return this.http.post<CheckListPOS>(`${c.api}/CheckListPOS`, checkListPOS).pipe(
      map((obj) => obj)
    );
  }

  atualizar(checkListPOS: CheckListPOS): Observable<CheckListPOS> {
    const url = `${c.api}/CheckListPOS`;

    return this.http.put<CheckListPOS>(url, checkListPOS).pipe(
      map((obj) => obj)
    );
  }

  deletar(codCheckListPOS: number): Observable<CheckListPOS> {
    const url = `${c.api}/CheckListPOS/${codCheckListPOS}`;

    return this.http.delete<CheckListPOS>(url).pipe(
      map((obj) => obj)
    );
  }
}
