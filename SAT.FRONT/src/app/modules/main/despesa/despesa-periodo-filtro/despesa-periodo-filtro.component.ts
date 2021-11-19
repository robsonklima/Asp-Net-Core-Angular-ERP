import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { IFilterBase } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-despesa-periodo-filtro',
  templateUrl: './despesa-periodo-filtro.component.html'
})
export class DespesaPeriodoFiltroComponent extends FilterBase implements OnInit, IFilterBase
{
  @Input() sidenav: MatSidenav;

  constructor (
    protected _userService: UserService,
    protected _formBuilder: FormBuilder)
  {
    super(_userService, _formBuilder, 'despesa-periodo');
  }

  createForm(): void
  {
    this.form = this._formBuilder.group({
      indAtivo: [undefined],
      inicioPeriodo: [undefined],
      fimPeriodo: [undefined]
    });

    this.form.patchValue(this.filter?.parametros);
  }

  loadData(): void { }

  ngOnInit(): void
  {
    this.createForm();
    this.loadData();
  }
}