import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { UsuarioDispositivo } from '../types/usuario-dispositivo.types';

@Injectable({
    providedIn: 'root'
})
export class UsuarioDispositivoService {
    constructor(private http: HttpClient) {}

    obterPorUsuarioEHash(codUsuario: string, hash: string): Observable<UsuarioDispositivo> {
        const url = `${c.api}/UsuarioDispositivo/${codUsuario}/${hash}`;
        return this.http.get<UsuarioDispositivo>(url).pipe(
            map((obj) => obj)
        );
    }

    atualizar(relatorioAtendimento: UsuarioDispositivo): Observable<UsuarioDispositivo> {
        const url = `${c.api}/UsuarioDispositivo`;
    
        return this.http.put<UsuarioDispositivo>(url, relatorioAtendimento).pipe(
          map((obj) => obj)
        );
      }

    criar(hash: UsuarioDispositivo): Observable<UsuarioDispositivo> {
        return this.http.post<UsuarioDispositivo>(`${c.api}/UsuarioDispositivo`, hash).pipe(
            map((obj) => obj)
        );
    }
}