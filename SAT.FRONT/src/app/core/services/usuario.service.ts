import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Usuario, UsuarioData, UsuarioParameters } from "../types/usuario.types";
import { Navegacao } from "../types/navegacao.types";

@Injectable({
  providedIn: "root"
})
export class UsuarioService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: UsuarioParameters): Observable<UsuarioData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Usuario`, { params: params, headers: this.headers }).pipe(
      map((data: UsuarioData) => data)
    )
  }

  obterPorCodigo(codUsuario: string): Observable<Usuario> {
    const url = `${c.api}/Usuario/${codUsuario}`;
    return this.http.get<Usuario>(url).pipe(
      map((obj) => obj)
    );
  }

  login(usuario: Usuario): Observable<any> {
    return this.http
      .post<Usuario>(`${c.api}/Usuario/Login/${usuario.codUsuario}`, usuario)
      .pipe(map((obj) => obj));
  }

  registrarUsuario(usuario: Usuario): void {
    localStorage.setItem("usuario", JSON.stringify(usuario));
  }

  registrarToken(token: string): void {
    localStorage.setItem("token", JSON.stringify(token));
  }

  registrarFiltros(filtros: any[]): void {
    localStorage.setItem("filtros", JSON.stringify(filtros));
  }

  registrarNavegacoes(navegacoes: Navegacao[]): void {
    localStorage.setItem("navegacoes", JSON.stringify(navegacoes));
  }

  logout() {
    localStorage.removeItem("usuario");
    localStorage.removeItem("token");
  }
}