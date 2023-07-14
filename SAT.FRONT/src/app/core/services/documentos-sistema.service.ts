import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config';
import { DocumentoSistema, DocumentoSistemaData, DocumentoSistemaParameters } from '../types/documento-sistema.types';

@Injectable({
  providedIn: 'root'
})
export class DocumentoSistemaService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: DocumentoSistemaParameters): Observable<DocumentoSistemaData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/DocumentoSistema`, { params: params }).pipe(
      map((data: DocumentoSistemaData) => data)
    )
  }

  obterPorCodigo(codAcao: number): Observable<DocumentoSistema> {
    const url = `${c.api}/DocumentoSistema/${codAcao}`;

    return this.http.get<DocumentoSistema>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(acao: DocumentoSistema): Observable<DocumentoSistema> {
    return this.http.post<DocumentoSistema>(`${c.api}/DocumentoSistema`, acao).pipe(
      map((obj) => obj)
    );
  }

  atualizar(acao: DocumentoSistema): Observable<DocumentoSistema> {
    const url = `${c.api}/DocumentoSistema`;
    return this.http.put<DocumentoSistema>(url, acao).pipe(
      map((obj) => obj)
    );
  }

  deletar(codAcao: number): Observable<DocumentoSistema> {
    const url = `${c.api}/DocumentoSistema/${codAcao}`;

    return this.http.delete<DocumentoSistema>(url).pipe(
      map((obj) => obj)
    );
  }
}
