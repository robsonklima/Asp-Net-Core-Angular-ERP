import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { MensagemTecnico, MensagemTecnicoData, MensagemTecnicoParameters } from '../types/mensagem-tecnico.types';

@Injectable({
  providedIn: 'root'
})
export class MensagemTecnicoService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: MensagemTecnicoParameters): Observable<MensagemTecnicoData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/MensagemTecnico`, { params: params }).pipe(
      map((data: MensagemTecnicoData) => data)
    )
  }

  obterPorCodigo(codMensagemTecnico: number): Observable<MensagemTecnico> {
    const url = `${c.api}/MensagemTecnico/${codMensagemTecnico}`;

    return this.http.get<MensagemTecnico>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(mensagemTecnico: MensagemTecnico): Observable<MensagemTecnico> {
    return this.http.post<MensagemTecnico>(`${c.api}/MensagemTecnico`, mensagemTecnico).pipe(
      map((obj) => obj)
    );
  }

  atualizar(mensagemTecnico: MensagemTecnico): Observable<MensagemTecnico> {
    const url = `${c.api}/MensagemTecnico`;
    return this.http.put<MensagemTecnico>(url, mensagemTecnico).pipe(
      map((obj) => obj)
    );
  }

  deletar(codMensagemTecnico: number): Observable<MensagemTecnico> {
    const url = `${c.api}/MensagemTecnico/${codMensagemTecnico}`;
    
    return this.http.delete<MensagemTecnico>(url).pipe(
      map((obj) => obj)
    );
  }
}
