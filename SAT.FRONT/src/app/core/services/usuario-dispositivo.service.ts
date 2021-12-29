import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { UsuarioDispositivo, UsuarioDispositivoData, UsuarioDispositivoParameters } from '../types/usuario-dispositivo.types';

@Injectable({
    providedIn: 'root'
})
export class UsuarioDispositivoService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: UsuarioDispositivoParameters): Observable<UsuarioDispositivoData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/UsuarioDispositivo`, { params: params }).pipe(
            map((data: UsuarioDispositivoData) => data)
        )
    }

    obterPorCodigo(cod: number): Observable<UsuarioDispositivo> {
        const url = `${c.api}/UsuarioDispositivo/${cod}`;
        return this.http.get<UsuarioDispositivo>(url).pipe(
            map((obj) => obj)
        );
    }

    atualizar(usuarioDispositivo: UsuarioDispositivo): Observable<UsuarioDispositivo> {
        const url = `${c.api}/UsuarioDispositivo`;
    
        return this.http.put<UsuarioDispositivo>(url, usuarioDispositivo).pipe(
          map((obj) => obj)
        );
      }

    criar(disp: UsuarioDispositivo): Observable<UsuarioDispositivo> {
        return this.http.post<UsuarioDispositivo>(`${c.api}/UsuarioDispositivo`, disp).pipe(
            map((obj) => obj)
        );
    }
}