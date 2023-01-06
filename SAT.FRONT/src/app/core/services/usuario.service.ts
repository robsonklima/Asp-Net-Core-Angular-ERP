import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Tecnico, TecnicoData, TecnicoParameters } from '../types/tecnico.types';
import { Usuario, UsuarioData, UsuarioParameters } from '../types/usuario.types';

@Injectable({
    providedIn: 'root'
})
export class UsuarioService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: UsuarioParameters): Observable<UsuarioData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/Usuario`, { params: params }).pipe(

            map((data: UsuarioData) => data)
        )
    }

    obterPorCodigo(codUsuario: string): Observable<Usuario> {
        const url = `${c.api}/Usuario/${codUsuario}`;
        return this.http.get<Usuario>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(tecnico: Usuario): Observable<Usuario> {
        return this.http.post<Usuario>(`${c.api}/Usuario`, tecnico).pipe(
            map((obj) => obj)
        );
    }

    atualizar(tecnico: Usuario): Observable<Usuario> {
        const url = `${c.api}/Usuario`;

        return this.http.put<Usuario>(url, tecnico).pipe(
            map((obj) => obj)
        );
    }

    deletar(codUsuario: number): Observable<Usuario> {
        const url = `${c.api}/Usuario/${codUsuario}`;

        return this.http.delete<Usuario>(url).pipe(
            map((obj) => obj)
        );
    }
}