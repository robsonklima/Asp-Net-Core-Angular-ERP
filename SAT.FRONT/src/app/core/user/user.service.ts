import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { User } from 'app/core/user/user.types';
import { AppConfig, appConfig as c } from 'app/core/config/app.config'
import { HttpClient, HttpParams } from '@angular/common/http';
import { Usuario, UsuarioData, UsuarioParameters } from '../types/usuario.types';
import { Navegacao } from '../types/navegacao.types';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private _user: ReplaySubject<User> = new ReplaySubject<User>(1);

  constructor(private http: HttpClient) { }

  set user(value: User) {
    this._user.next(value);
  }

  set userSession(session: string) {
    localStorage.setItem('userSession', JSON.stringify(session));
  }

  get userSession(): string {
    return localStorage.getItem('userSession') ?? '';
  }

  get user$(): Observable<User> {
    return this._user.asObservable();
  }

  get(): Observable<User> {
    return JSON.parse(this.userSession).usuario;
  }

  obterPorParametros(parameters: UsuarioParameters): Observable<UsuarioData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) {
        params = params.append(key, String(parameters[key]));
      }
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

  registrarUsuario(usuario: Usuario): void {
    localStorage.setItem("usuario", JSON.stringify(usuario));
  }

  registrarToken(token: string): void {
    localStorage.setItem("token", JSON.stringify(token));
  }

  registrarFiltro(filtro: any): void {
    let filtros = JSON.parse(localStorage.getItem('filtros')) || [];
    filtros = filtros.filter(f => f.nome !== filtro.nome);
    filtros.push(filtro);
    localStorage.setItem("filtros", JSON.stringify(filtros));
  }

  removerFiltro(filtro: any): void {
    let filtros = JSON.parse(localStorage.getItem('filtros')) || [];
    filtros = filtros.filter(f => f.nome !== filtro.nome);
    localStorage.setItem("filtros", JSON.stringify(filtros));
  }

  obterFiltro(nome: string): any {
    let filtros: any[] = JSON.parse(localStorage.getItem("filtros")) || [];
    return filtros.filter(f => f.nome === nome).shift();
  }

  registrarNavegacoes(navegacoes: Navegacao[]): void {
    localStorage.setItem("navegacoes", JSON.stringify(navegacoes));
  }

  registrarConfiguracoes(config: AppConfig): void {
    localStorage.setItem("config", JSON.stringify(config));
  }

  obterConfiguracoes(): AppConfig {
    return JSON.parse(localStorage.getItem("config"));
  }

  logout() {
    localStorage.removeItem("usuario");
    localStorage.removeItem("token");
  }
}
