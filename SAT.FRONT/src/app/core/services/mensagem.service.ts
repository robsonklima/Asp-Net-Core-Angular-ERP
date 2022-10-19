import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Mensagem, MensagemData, MensagemParameters } from '../types/mensagem.types';

@Injectable({
    providedIn: 'root'
})
export class MensagemService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: MensagemParameters): Observable<MensagemData> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });
    
        return this.http.get(`${c.api}/Mensagem`, { params: params }).pipe(
          map((data: MensagemData) => data)
        )
    }

    obterPorCodigo(codMsg: number): Observable<Mensagem> {
        const url = `${c.api}/Mensagem/${codMsg}`;
        return this.http.get<Mensagem>(url).pipe(
          map((obj) => obj)
        );
    }

    criar(mensagem: Mensagem): Observable<Mensagem> {
        return this.http.post<Mensagem>(`${c.api}/Mensagem`, mensagem).pipe(
            map((obj) => obj)
        );
    }

    atualizar(mensagem: Mensagem): Observable<Mensagem> {
        const url = `${c.api}/Mensagem`;
        
        return this.http.put<Mensagem>(url, mensagem).pipe(
            map((obj) => obj)
        );
    }

    deletar(codMsg: number): Observable<Mensagem> {
        const url = `${c.api}/Mensagem/${codMsg}`;
        
        return this.http.delete<Mensagem>(url).pipe(
          map((obj) => obj)
        );
    }
}