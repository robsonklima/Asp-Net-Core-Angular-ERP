import { TipoContratoService } from './../../../../../core/services/tipo-contrato.service';
import { TipoContrato } from 'app/core/types/tipo-contrato.types';
import { IFilterBase } from './../../../../../core/types/filtro.types';
import { MatSidenav } from '@angular/material/sidenav';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormBuilder } from '@angular/forms';
import { ClienteService } from 'app/core/services/cliente.service';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { EquipamentoParameters } from 'app/core/types/equipamento.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { FilterBase } from 'app/core/filters/filter-base';

@Component({
  selector: 'app-contrato-filtro',
  templateUrl: './contrato-filtro.component.html',
})

export class ContratoFiltroComponent extends FilterBase implements OnInit, IFilterBase{

  @Input() sidenav: MatSidenav;

  tiposContrato: TipoContrato[] = [];
  clientes: Cliente[] = [];
  clienteFilterCtrl: FormControl = new FormControl();

  protected _onDestroy = new Subject<void>();

  constructor (
    private _tipoContratoService: TipoContratoService,
    private _equipamentoService: EquipamentoService,
    private _clienteService: ClienteService,
    protected _userService: UserService,
    protected _formBuilder: FormBuilder
  )
  {
    super(_userService, _formBuilder, 'contrato');
  }

  ngOnInit(): void
  {
    this.createForm();
    this.loadData();
  }

  loadData(): void
  {
    this.obterClientes();
    this.obterTipoContrato();
    this.registrarEmitters();

  }

  createForm(): void
  {
    this.form = this._formBuilder.group({
      codClientes: [undefined],
      codTipoContrato: [undefined],
    });

    this.form.patchValue(this.filter?.parametros);
  }

  async obterTipoContrato()
  {
    let params = {
      indAtivo: 1,
      sortActive: 'nomeTipoContrato',
      sortDirection: 'asc'
    }

    const data = await this._tipoContratoService
      .obterPorParametros(params)
      .toPromise();

    this.tiposContrato = data.items;
  }

  async obterClientes(filter: string = '')
  {
    let params: ClienteParameters = {
      filter: filter,
      indAtivo: 1,
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      pageSize: 1000
    };

    const data = await this._clienteService
      .obterPorParametros(params)
      .toPromise();

    this.clientes = data.items;
  }

  registrarEmitters(): void
  {
    this.clienteFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() =>
      {
        this.obterClientes(this.clienteFilterCtrl.value);
      });
  }

  clean()
  {
    super.clean();

    if (this.userSession?.usuario?.codFilial)
    {
      this.form.controls['codFiliais'].setValue([this.userSession.usuario.codFilial]);
      this.form.controls['codFiliais'].disable();
    }
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}