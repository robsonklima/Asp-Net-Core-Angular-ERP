import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { AuthUtils } from 'app/core/auth/auth.utils';
import { UserService } from 'app/core/user/user.service';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable()
export class AuthService {
    private _authenticated: boolean = this.accessToken !== undefined;

    constructor(
        private _httpClient: HttpClient,
        private _userService: UserService
    ) {
        this._authenticated = this.accessToken !== '';
    }

    set accessToken(token: string) {
        localStorage.setItem('accessToken', token);
    }

    get accessToken(): string {
        return localStorage.getItem('accessToken') ?? '';
    }

    forgotPassword(email: string): Observable<any> {
        return this._httpClient.post('api/auth/forgot-password', email);
    }

    resetPassword(password: string): Observable<any> {
        return this._httpClient.post('api/auth/reset-password', password);
    }

    signIn(codUsuario: string, senha: string): Observable<any> {
        if (this._authenticated) {
            return throwError('Usuário já está logado.');
        }

        return this._httpClient.post(c.api + '/Usuario/Login', { codUsuario: codUsuario, senha: senha }).pipe(
            switchMap((response: any) => {
                this.accessToken = response.token;
                this._authenticated = true;
                this._userService.user = response.usuario;
                this._userService.userSession = response;
                return of(response);
            })
        );
    }

    signInUsingToken(): Observable<any> {
        return this._httpClient.post('api/auth/refresh-access-token', {
            accessToken: this.accessToken
        }).pipe(
            catchError(() =>
                of(false)
            ),
            switchMap((response: any) => {
                this.accessToken = response.accessToken;
                this._authenticated = true;
                this._userService.user = response.user;
                return of(true);
            })
        );
    }

    signOut(): Observable<any> {
        localStorage.removeItem('accessToken');
        this._authenticated = false;
        return of(true);
    }

    signUp(user: { name: string; email: string; password: string; company: string }): Observable<any> {
        return this._httpClient.post('api/auth/sign-up', user);
    }

    unlockSession(credentials: { email: string; password: string }): Observable<any> {
        return this._httpClient.post('api/auth/unlock-session', credentials);
    }

    check(): Observable<boolean> {
        if (this._authenticated) {
            return of(true);
        }

        if (!this.accessToken) {
            return of(false);
        }

        if (AuthUtils.isTokenExpired(this.accessToken)) {
            return of(false);
        }

        return this.signInUsingToken();
    }

    getUserHash(): string {
        return JSON.parse(localStorage.getItem("hash"));
    }

    setUserHash(hash: string) {
        localStorage.setItem("hash", JSON.stringify(hash));
    }
}
