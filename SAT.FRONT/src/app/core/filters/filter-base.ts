import { Injectable } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { IFilterBaseCore } from './ifilter-base-core';

@Injectable({
    providedIn: 'root'
})

export class FilterBase implements IFilterBaseCore
{
    public filterName: string;
    public filter: any;
    public userSession: UserSession;
    public form: FormGroup;
    public sidenav: MatSidenav;

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
}