import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { UsuarioLogin, UsuarioLoginData, UsuarioLoginParameters } from '../types/usuario-login.types';

@Injectable({
    providedIn: 'root'
})
export class UsuarioLoginService {
    constructor(private http: HttpClient) {}

    obterPorParametros(parameters: UsuarioLoginParameters): Observable<UsuarioLoginData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/UsuarioLogin`, { params: params }).pipe(
            map((data: UsuarioLoginData) => data)
        )
    }

    criar(login: UsuarioLogin): Observable<UsuarioLogin> {
        return this.http.post<UsuarioLogin>(`${c.api}/UsuarioLogin`, login).pipe(
            map((obj) => obj)
        );
    }
}