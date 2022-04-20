import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { FilialService } from 'app/core/services/filial.service';
import { Autorizada, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import Enumerable from 'linq';

@Component({
  selector: 'app-despesa-tecnico-filtro',
  templateUrl: './despesa-tecnico-filtro.component.html'
})
export class DespesaTecnicoFiltroComponent extends FilterBase implements OnInit, IFilterBase
{
  @Input() sidenav: MatSidenav;
  filiais: Filial[] = [];
  autorizadas: Autorizada[] = [];

  constructor (
    private _filialService: FilialService,
    protected _userService: UserService,
    protected _formBuilder: FormBuilder,
    private _autorizadaService: AutorizadaService
  )
  {
    super(_userService, _formBuilder, 'despesa-tecnico');
  }

  createForm(): void
  {
    this.form = this._formBuilder.group({
      codFiliais: [undefined],
      indAtivo: [statusConst.ATIVO],
      indTecnicoLiberado: [undefined],
      codAutorizadas: [undefined]
    });

    this.form.patchValue(this.filter?.parametros);
  }

  loadData(): void
  {
    this.obterFiliais();
  }

  async obterAutorizadas(filialFilter: any=null) {
    let params: AutorizadaParameters = {
      indAtivo: statusConst.ATIVO,
      codFiliais: filialFilter,
      pageSize: 1000
    };

    const data = await this._autorizadaService
      .obterPorParametros(params)
      .toPromise();

    this.autorizadas = Enumerable.from(data.items).orderBy(i => i.nomeFantasia).toArray();
  }

  async obterFiliais()
  {
    let params: FilialParameters = {
      indAtivo: statusConst.ATIVO,
      codFilial: this.userSession.usuario.codFilial,
      sortActive: 'nomeFilial',
      sortDirection: 'asc',
      pageSize: 50
    };

    const data = await this._filialService
      .obterPorParametros(params)
      .toPromise();

    this.filiais = data.items;
  }

  private registrarEmitters() {
    this.form.controls['codFiliais']
      .valueChanges
      .subscribe(() => {
        if ((this.form.controls['codFiliais'].value && this.form.controls['codFiliais'].value != '')) {
          var filialFilter: any = this.form.controls['codFiliais'].value;
          this.obterAutorizadas(filialFilter);
        }
      });

    if (this.userSession.usuario.codFilial) {
      this.form.controls['codFiliais'].setValue([this.userSession.usuario.codFilial]);
      this.form.controls['codFiliais'].disable();
    }
    else {
      this.form.controls['codFiliais'].enable();
    }
  }

  ngOnInit(): void
  {
    this.loadData();
    this.createForm();
    this.registrarEmitters();
  }
}

