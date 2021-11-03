import { Injectable } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { IFilterable } from './ifilterable';
@Injectable({
    providedIn: 'root'
})

export class FilterableComponent implements IFilterable
{
    private _filterName: string;
    private _filter: any;
    private _userSession: UserSession;
    sidenav: MatSidenav;

    public get userSession(): UserSession
    {
        return this._userSession;
    }
    public set userSession(value: UserSession)
    {
        this._userSession = value;
    }
    public get filter(): any
    {
        return this._filter;
    }
    public set filter(value: any)
    {
        this._filter = value;
    }
    public get filterName(): string
    {
        return this._filterName;
    }
    public set filterName(value: string)
    {
        this._filterName = value;
    }

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