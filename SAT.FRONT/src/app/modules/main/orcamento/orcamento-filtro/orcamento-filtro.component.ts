import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { IFilterBase } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-orcamento-filtro',
  templateUrl: './orcamento-filtro.component.html'
})
export class OrcamentoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
  @Input() sidenav: MatSidenav;

  constructor(
    protected _userService: UserService,
    protected _formBuilder: FormBuilder
  ) {
    super(_userService, _formBuilder, 'ordem-servico');
  }

  createForm(): void {
    throw new Error('Method not implemented.');
  }

  loadData(): void {
    throw new Error('Method not implemented.');
  }

  ngOnInit(): void {

  }
}
