import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { FilialService } from 'app/core/services/filial.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-despesa-credito-cartao-filtro',
  templateUrl: './despesa-credito-cartao-filtro.component.html'
})
export class DespesaCreditoCartaoFiltroComponent extends FilterBase implements OnInit, IFilterBase
{
  @Input() sidenav: MatSidenav;
  tecnicos: Tecnico[] = [];
  filiais: Filial[] = [];


  constructor (
    protected _userService: UserService,
    private _tecnicoService: TecnicoService,
    private _filialService: FilialService,
    protected _formBuilder: FormBuilder)
  {
    super(_userService, _formBuilder, 'despesa-credito-cartao');
  }

  createForm(): void
  {
    this.form = this._formBuilder.group({
      codTecnicos: [undefined],
      codFiliais: [undefined]
    });

    this.form.patchValue(this.filter?.parametros);
  }

  loadData(): void 
  {
    this.obterTecnicos();
    this.obterFiliais();
  }

  async obterTecnicos()
  {
    const data = await this._tecnicoService
      .obterPorParametros({
        indAtivo: 1,
        sortActive: 'nome',
        sortDirection: 'asc',
        codPerfil: 35,
        pageSize: 1000
      })
      .toPromise();

    this.tecnicos = data.items;
  }

  async obterFiliais()
  {
    let params: FilialParameters = {
      indAtivo: 1,
      sortActive: 'nomeFilial',
      sortDirection: 'asc'
    };

    const data = await this._filialService
      .obterPorParametros(params)
      .toPromise();

    this.filiais = data.items;
  }

  ngOnInit(): void
  {
    this.createForm();
    this.loadData();
  }
}