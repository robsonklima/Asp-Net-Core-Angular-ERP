import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { SegurancaUsuarioModel, User } from 'app/core/user/user.types';
import { AppConfig, appConfig as c } from 'app/core/config/app.config'
import { HttpClient, HttpParams } from '@angular/common/http';
import { Usuario, UsuarioData, UsuarioParameters, UsuariosLogados } from '../types/usuario.types';
import { Navegacao } from '../types/navegacao.types';
import { map } from 'rxjs/operators';
import { PerfilEnum } from '../types/perfil.types';
import { SetorEnum } from '../types/setor.types';

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

  get isCustomer(): boolean {
    var session = JSON.parse(localStorage.getItem('userSession'));
    var user = session.usuario;
    if (user.codPerfil == PerfilEnum.CLIENTE_BASICO ||
        user.codPerfil == PerfilEnum.CLIENTE_INTERMEDIARIO ||
        user.codPerfil == PerfilEnum.CLIENTE_AVANÇADO
        ) {
      return true;
    }

    return false;
  }

  get isOpenOS(): boolean {
    var session = JSON.parse(localStorage.getItem('userSession'));
    var user = session.usuario.perfil;
    var setor = session.usuario.setor;
    if (user.indAbreChamado == 1 || setor.codSetor == SetorEnum.COORDENACAO_DE_CONTRATOS) {
      return true;
    }

    return false;
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

  atualizaFiltrosUsuario(usuario: User): Observable<Usuario> {
    return this.obterPorCodigo(usuario.codUsuario);
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

  registrarUsuarioFiltro(codUsuarioFiltro: number): void {
    localStorage.setItem("filtro-selecionado", JSON.stringify(codUsuarioFiltro));
  }

  obterUsuarioFiltro(): number {
    let filtro: number = JSON.parse(localStorage.getItem("filtro-selecionado")) || 0;
    return filtro;
  }

  removerFiltro(nome: any): void {
    let filtros = JSON.parse(localStorage.getItem('filtros')) || [];
    filtros = filtros.filter(f => f.nome !== nome);
    localStorage.setItem("filtros", JSON.stringify(filtros));
  }

  obterFiltro(nome: string): any {
    let filtros: any[] = JSON.parse(localStorage.getItem("filtros")) || [];
    return filtros.filter(f => f.nome === nome).shift();
  }

  atualizarPropriedade(filterName: string, propertyName: string, propertyValue: any) {
    let filtros: any[] = JSON.parse(localStorage.getItem("filtros")) || [];
    var filtro = filtros.filter(f => f.nome === filterName).shift();
    if (filtro) {
      filtro.parametros[`${propertyName}`] = propertyValue;
      this.registrarFiltro(filtro);
    }
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

  atualizar(usuario: Usuario): Observable<Usuario> {
    const url = `${c.api}/Usuario`;
    return this.http.put<Usuario>(url, usuario).pipe(
      map((obj) => obj)
    );
  }

  alterarSenha(segurancaUsuarioModel: SegurancaUsuarioModel): Observable<SegurancaUsuarioModel> {
    const url = `${c.api}/Usuario/AlterarSenha`;
    return this.http.put<SegurancaUsuarioModel>(url, segurancaUsuarioModel).pipe(
      map((obj) => obj)
    );
  }

  verificarSenhaForte(str: string): boolean {
    var patt = new RegExp('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[()[\\]{}=\\-~,.;<>:@$!%*?&])[A-Za-z\\d()[\\]{}=\\-~,.;<>:@$!%*?&]{8,}$');
    return patt.test(str);
  }

  desbloquearAcesso(codUsuario: string): Observable<any> {
    return this.http.post<any>(`${c.api}/Usuario/DesbloquearAcesso/` + codUsuario, {}, {
      headers: { Accept: 'application/json', 'No-Auth': 'False' }
    }).pipe(
      map(
        (result) => { return result; },
        (error) => { return error; }
      )
    );
  }

  criar(usuario: Usuario): Observable<Usuario> {
    return this.http.post<Usuario>(`${c.api}/Usuario/Criar`, usuario).pipe(
      map((obj) => obj)
    );
  }
}
