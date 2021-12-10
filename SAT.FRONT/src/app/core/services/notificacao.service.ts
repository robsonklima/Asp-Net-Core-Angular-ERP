import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Notificacao, NotificacaoData, NotificacaoParameters } from '../types/notificacao.types';

@Injectable({
    providedIn: 'root'
})
export class NotificacaoService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: NotificacaoParameters): Observable<NotificacaoData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/Notificacao`, { params: params }).pipe(
          map((data: NotificacaoData) => data)
        )
    }

    obterPorCodigo(codNotificacao: number): Observable<Notificacao> {
        const url = `${c.api}/Notificacao/${codNotificacao}`;
        return this.http.get<Notificacao>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(notificacao: Notificacao): Observable<Notificacao> {
        return this.http.post<Notificacao>(`${c.api}/Notificacao`, notificacao).pipe(
            map((obj) => obj)
        );
    }

    atualizar(notificacao: Notificacao): Observable<Notificacao> {
        const url = `${c.api}/Notificacao`;
        
        return this.http.put<Notificacao>(url, notificacao).pipe(
            map((obj) => obj)
        );
    }

    deletar(codNotificacao: number): Observable<Notificacao> {
        const url = `${c.api}/Notificacao/${codNotificacao}`;
        
        return this.http.delete<Notificacao>(url).pipe(
          map((obj) => obj)
        );
    }
}