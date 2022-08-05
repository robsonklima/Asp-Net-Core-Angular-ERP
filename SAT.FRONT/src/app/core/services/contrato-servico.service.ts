import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ContratoServico } from '../types/contrato.types';
import { ContratoServicoData, ContratoServicoParameters } from '../types/contrato-servico.types';

@Injectable({
  providedIn: 'root'
})
export class ContratoServicoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: ContratoServicoParameters): Observable<ContratoServicoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/ContratoServico`, { params: params }).pipe(
      map((data: ContratoServicoData) => data)
    )
  }

  criar(contratoServico: ContratoServico): Observable<ContratoServico> {
    return this.http.post<ContratoServico>(`${c.api}/ContratoServico`, contratoServico).pipe(
      map((obj) => obj)
    );
  }

  atualizar(contratoServico: ContratoServico): Observable<ContratoServico> {
    const url = `${c.api}/ContratoServico`;

    return this.http.put<ContratoServico>(url, contratoServico).pipe(
      map((obj) => obj)
    );
  }

  deletar(codContrato: number, codContratoServico: number): Observable<ContratoServico> {
    const url = `${c.api}/ContratoServico/${codContrato}/${codContratoServico}`;

    return this.http.delete<ContratoServico>(url).pipe(
      map((obj) => obj)
    );
  }
}
