import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { DespesaAdiantamentoTipo, DespesaAdiantamentoTipoEnum } from 'app/core/types/despesa-adiantamento.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { statusConst } from 'app/core/types/status-types';
import { Tecnico, TecnicoParameters } from 'app/core/types/tecnico.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-despesa-adiantamento-filtro',
  templateUrl: './despesa-adiantamento-filtro.component.html'
})

export class DespesaAdiantamentoFiltroComponent extends FilterBase implements OnInit, IFilterBase
{
  @Input() sidenav: MatSidenav;
  despesaAdiantamentoTipo: DespesaAdiantamentoTipo[] = [];
  tecnicos: Tecnico[] = [];

  constructor (
    protected _userService: UserService,
    private _tecnicoService: TecnicoService,
    protected _formBuilder: FormBuilder)
  {
    super(_userService, _formBuilder, 'despesa-adiantamento');
  }

  createForm(): void
  {
    this.form = this._formBuilder.group({
      indAtivo: [statusConst.ATIVO],
      codDespesaAdiantamentoTipo: [undefined],
      codTecnicos: [undefined]
    });

    this.form.patchValue(this.filter?.parametros);
  }

  loadData(): void 
  {
    this.obterTecnicos();
    this.obterTipos();
  }

  private obterTipos()
  {
    Object.keys(DespesaAdiantamentoTipoEnum).forEach(key =>
    {
      this.despesaAdiantamentoTipo.push(
        {
          codDespesaAdiantamentoTipo: DespesaAdiantamentoTipoEnum[key],
          nomeAdiantamentoTipo: key
        })
    });
  }

  async obterTecnicos()
  {
    const data = await this._tecnicoService
      .obterPorParametros({
        indAtivo: statusConst.ATIVO,
        sortActive: 'nome',
        sortDirection: 'asc',
        codPerfil: 35,
        pageSize: 1000
      })
      .toPromise();

    this.tecnicos = data.items;
  }

  ngOnInit(): void
  {
    this.createForm();
    this.loadData();
  }
}
