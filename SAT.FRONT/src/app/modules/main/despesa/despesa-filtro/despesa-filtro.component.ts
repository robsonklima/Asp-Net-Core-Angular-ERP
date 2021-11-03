import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilialService } from 'app/core/services/filial.service';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { UserService } from 'app/core/user/user.service';
import { FilterBase } from 'app/core/filters/filter-base';
import { IFilterBase } from 'app/core/filters/ifilter-base';

@Component({
  selector: 'app-despesa-filtro',
  templateUrl: './despesa-filtro.component.html'
})
export class DespesaFiltroComponent extends FilterBase implements OnInit, IFilterBase
{
  @Input() sidenav: MatSidenav;
  filiais: Filial[] = [];

  constructor (
    private _filialService: FilialService,
    protected _userService: UserService,
    protected _formBuilder: FormBuilder)
  {
    super(_userService, _formBuilder, 'despesa-atendimento');
  }

  createForm(): void
  {
    this.form = this._formBuilder.group({
      codFiliais: [undefined]
    });

    this.form.patchValue(this.filter?.parametros);
  }

  loadData(): void
  {
    this.obterFiliais();
  }

  async obterFiliais()
  {
    let params: FilialParameters = {
      indAtivo: 1,
      sortActive: 'nomeFilial',
      sortDirection: 'asc',
      pageSize: 50
    };

    const data = await this._filialService
      .obterPorParametros(params)
      .toPromise();

    this.filiais = data.items;
  }

  ngOnInit(): void
  {
    this.loadData();
    this.createForm();
  }
}
