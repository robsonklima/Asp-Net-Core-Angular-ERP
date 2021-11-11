import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { IFilterBase } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-despesa-adiantamento-filtro',
  templateUrl: './despesa-adiantamento-filtro.component.html'
})

export class DespesaAdiantamentoFiltroComponent extends FilterBase implements OnInit, IFilterBase
{
  @Input() sidenav: MatSidenav;

  constructor (
    protected _userService: UserService,
    protected _formBuilder: FormBuilder)
  {
    super(_userService, _formBuilder, 'despesa-adiantamento');
  }

  createForm(): void
  {
    this.form = this._formBuilder.group({
      indAtivo: [1]
    });

    this.form.patchValue(this.filter?.parametros);
  }

  loadData(): void 
  {
  }

  ngOnInit(): void
  {
    this.createForm();
    this.loadData();
  }
}
