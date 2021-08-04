import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { Navigation } from 'app/core/navigation/navigation.types';
import { UserService } from '../user/user.service';
import { FuseNavigationService } from '@fuse/components/navigation';

@Injectable({
    providedIn: 'root'
})
export class NavigationService {
    private _navigation: ReplaySubject<Navigation> = new ReplaySubject<Navigation>(1);

    constructor(
        private _userService: UserService,
        private _fuseNavigationService: FuseNavigationService
    ) { }

    get navigation$(): Observable<Navigation> {
        return this._navigation.asObservable();
    }

    get(): Promise<Navigation> {
        let nav: any = JSON.parse(this._userService.userSession).navegacoes;
        
        let navs: any = {
            compact: nav,
            default: nav,
            futuristic: nav,
            horizontal: nav,
        }

        return new Promise((resolve, reject) => {
            this._navigation.next(navs);
            resolve(navs);
        });
    }
}
