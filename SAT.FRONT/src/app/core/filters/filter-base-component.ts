import { Injectable } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { IFilterBase } from './ifilter-base';

@Injectable({
    providedIn: 'root'
})

export class FilterBaseComponent implements IFilterBase
{
    private _filterName: string;
    private _filter: any;
    private _userSession: UserSession;
    private _form: FormGroup;
    sidenav: MatSidenav;

    public get form(): FormGroup
    {
        return this._form;
    }
    public set form(value: FormGroup)
    {
        this._form = value;
    }
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

    constructor (protected _userService: UserService, protected _formBuilder: FormBuilder, filterName: string)
    {
        this.filterName = filterName;
        this.userSession = JSON.parse(this._userService.userSession);
        this.filter = this._userService.obterFiltro(filterName);
    }


    apply(): void
    {
        const form: any = this.form.getRawValue();

        const filtro: any = {
            nome: this.filterName,
            parametros: form
        }

        this._userService.registrarFiltro(filtro);

        const newFilter: any = { nome: this.filterName, parametros: this.form.getRawValue() }
        const oldFilter = this._userService.obterFiltro(this.filterName);

        if (oldFilter != null)
            newFilter.parametros =
            {
                ...newFilter.parametros,
                ...oldFilter.parametros
            };

        this._userService.registrarFiltro(newFilter);
        this.sidenav.close();
    }

    clean(): void
    {
        this.form.reset();
        this.apply();
        this.sidenav.close();
    }

    selectAll(select: AbstractControl, values, propertyName)
    {
        if (select.value[0] == 0 && propertyName != '')
            select.patchValue([...values.map(item => item[`${propertyName}`]), 0]);
        else if (select.value[0] == 0 && propertyName == '')
            select.patchValue([...values.map(item => item), 0]);
        else
            select.patchValue([]);
    }

    createForm(): void
    {
        throw new Error('Method not implemented.');
    }

    loadData(): void
    {
        throw new Error('Method not implemented.');
    }
}