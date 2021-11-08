import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { FilialService } from 'app/core/services/filial.service';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-despesa-tecnico-filtro',
  templateUrl: './despesa-tecnico-filtro.component.html'
})
export class DespesaTecnicoFiltroComponent extends FilterBase implements OnInit, IFilterBase
{
  @Input() sidenav: MatSidenav;
  filiais: Filial[] = [];

  constructor (
    private _filialService: FilialService,
    protected _userService: UserService,
    protected _formBuilder: FormBuilder)
  {
    super(_userService, _formBuilder, 'despesa-tecnico');
  }

  createForm(): void
  {
    this.form = this._formBuilder.group({
      codFiliais: [undefined],
      indAtivo: [1],
      indTecnicoLiberado: [undefined]
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

