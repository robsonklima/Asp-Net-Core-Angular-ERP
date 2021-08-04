import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { User } from 'app/core/user/user.types';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    private _user: ReplaySubject<User> = new ReplaySubject<User>(1);

    constructor() { }

    set user(value: User) {
        this._user.next(value);
    }

    set userSession(session: string)
    {
        localStorage.setItem('userSession', JSON.stringify(session));
    }

    get userSession(): string
    {
        return localStorage.getItem('userSession') ?? '';
    }

    get user$(): Observable<User> {
        return this._user.asObservable();
    }

    get(): Observable<User> {
        return JSON.parse(this.userSession).usuario;
    }
}
