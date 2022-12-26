import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ProtocoloChamadoSTNParameters, ProtocoloChamadoSTNData, ProtocoloChamadoSTN } from '../types/protocolo-chamado-stn.types';



@Injectable({
  providedIn: 'root'
})
export class ProtocoloChamadoSTNService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: ProtocoloChamadoSTNParameters): Observable<ProtocoloChamadoSTNData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/ProtocoloChamadoSTN`, { params: params }).pipe(
      map((data: ProtocoloChamadoSTNData) => data)
    )
  }

  obterPorCodigo(codProtocoloChamadoSTN: number): Observable<ProtocoloChamadoSTN> {
    const url = `${c.api}/ProtocoloChamadoSTN/${codProtocoloChamadoSTN}`;
    return this.http.get<ProtocoloChamadoSTN>(url).pipe(
      map((obj) => obj)
    );
  }
  
  criar(protocoloChamadoSTN: ProtocoloChamadoSTN): Observable<ProtocoloChamadoSTN> {
    return this.http.post<ProtocoloChamadoSTN>(`${c.api}/ProtocoloChamadoSTN`, protocoloChamadoSTN).pipe(
      map((obj) => obj)
    );
  }

  atualizar(protocoloChamadoSTN: ProtocoloChamadoSTN): Observable<ProtocoloChamadoSTN> {
    const url = `${c.api}/ProtocoloChamadoSTN`;

    return this.http.put<ProtocoloChamadoSTN>(url, protocoloChamadoSTN).pipe(
      map((obj) => obj)
    );
  }

  deletar(codProtocoloChamadoSTN: number): Observable<ProtocoloChamadoSTN> {
    const url = `${c.api}/ProtocoloChamadoSTN/${codProtocoloChamadoSTN}`;

    return this.http.delete<ProtocoloChamadoSTN>(url).pipe(
      map((obj) => obj)
    );
  }
}
