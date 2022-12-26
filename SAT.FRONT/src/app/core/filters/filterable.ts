import { Injectable } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { Parameters, IFilterableCore } from '../types/filtro.types';

@Injectable({
    providedIn: 'root'
})

export class Filterable implements IFilterableCore {
    public filterName: string;
    public filter: any;
    public userSession: UserSession;

    public sidenav: MatSidenav;
    paginator: MatPaginator;
    sort: MatSort;

    constructor(
        protected _userService: UserService,
        filterName: string
    ) {
        this.filterName = filterName;
        this.userSession = JSON.parse(this._userService.userSession);
        this.loadFilter();
    }

    loadFilter(): void {
        this.filter = this._userService.obterFiltro(this.filterName);
        if (!this.filter) {
            var params: Parameters = {};
            this.filter =
            {
                nome: this.filterName,
                parametros: params
            };

            return;
        }

        Object.keys(this.filter.parametros).forEach((key) => {
            if (this.filter.parametros[key] instanceof Array)
                this.filter.parametros[key] = this.filter.parametros[key].join();
        });
    }

    onSortChanged(): void {
        this._userService.atualizarPropriedade(this.filterName, "sortActive", this.sort.active);
        this._userService.atualizarPropriedade(this.filterName, "sortDirection", this.sort.direction);
        this.paginator.pageIndex = 0;
        this.loadFilter();
    }

    onPaginationChanged(): void {
        this._userService.atualizarPropriedade(this.filterName, "qtdPaginacaoLista", this.paginator?.pageSize);
        this.loadFilter();
    }

    onSidenavClosed(): void {
        if (this.paginator) this.paginator.pageIndex = 0;
        this.loadFilter();
    }
}