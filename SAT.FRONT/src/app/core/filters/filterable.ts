import { Injectable } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { IFilterableCore } from './ifilterable-core';
@Injectable({
    providedIn: 'root'
})

export class Filterable implements IFilterableCore
{
    public filterName: string;
    public filter: any;
    public userSession: UserSession;
    public sidenav: MatSidenav;

    carregaFiltro(): void
    {
        this.filter = this._userService.obterFiltro(this.filterName);

        if (!this.filter) return;

        Object.keys(this.filter?.parametros).forEach((key) =>
        {
            if (this.filter?.parametros[key] instanceof Array)
                this.filter.parametros[key] = this.filter.parametros[key].join();
        });
    }

    constructor (protected _userService: UserService, filterName: string)
    {
        this.filterName = filterName;
        this.userSession = JSON.parse(this._userService.userSession);
        this.carregaFiltro();
    }
}