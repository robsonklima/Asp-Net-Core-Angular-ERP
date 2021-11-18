import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { IFilterBase } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-despesa-credito-cartao-filtro',
  templateUrl: './despesa-credito-cartao-filtro.component.html'
})
export class DespesaCreditoCartaoFiltroComponent extends FilterBase implements OnInit, IFilterBase
{
  @Input() sidenav: MatSidenav;

  constructor (
    protected _userService: UserService,
    protected _formBuilder: FormBuilder)
  {
    super(_userService, _formBuilder, 'despesa-credito-cartao');
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