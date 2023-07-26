  import { Component, Input, OnInit } from '@angular/core';
  import { FormBuilder, FormControl } from '@angular/forms';
  import { MatSidenav } from '@angular/material/sidenav';
  import { Subject } from 'rxjs';
  import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
  import { ClienteService } from 'app/core/services/cliente.service';
  import { TipoContratoService } from 'app/core/services/tipo-contrato.service';
  import { TipoContrato, TipoContratoParameters } from 'app/core/types/tipo-contrato.types';
  import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
  import { FilterBase } from 'app/core/filters/filter-base';
  import { IFilterBase } from 'app/core/types/filtro.types';
  import { UserService } from 'app/core/user/user.service';
  import { Contrato, ContratoParameters } from 'app/core/types/contrato.types';
  import { ContratoService } from 'app/core/services/contrato.service';
  
  @Component({
    selector: 'app-instalacao-pagto-filtro',
    templateUrl: './instalacao-pagto-filtro.component.html',
  })
  export class InstalacaoPagtoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
  
    @Input() sidenav: MatSidenav;
    tipos: TipoContrato[] = [];
    tipoFilterCtrl: FormControl = new FormControl();
    clientes: Cliente[] = [];
    contratos: Contrato[] = [];
    clienteFilterCtrl: FormControl = new FormControl();
    contratoFilterCtrl: FormControl = new FormControl();
    protected _onDestroy = new Subject<void>();
  
    constructor(
      private _tipoContratoService: TipoContratoService,
      private _clienteService: ClienteService,
      private _contratoService: ContratoService,
      protected _userService: UserService,
      protected _formBuilder: FormBuilder
    ) {
      super(_userService, _formBuilder, 'instalacao-pagto');
    }
  
    ngOnInit(): void {
      this.createForm();
      this.loadData();
  
    }
  
    async loadData() {
      this.obterTipos();
      this.obterClientes();
      this.obterContratos();
      this.registrarEmitters();
    }
  
    createForm(): void {
      this.form = this._formBuilder.group({
        codTipoContratos: [undefined],
        codCliente: [undefined],
        codContratos: [undefined],
        dataPagto: [undefined],
        vlrPagto: [undefined],
        indAtivo: [undefined],
      });
      this.form.patchValue(this.filter?.parametros);
    }
  
    async obterTipos(filtro: string = '') {
      let params: TipoContratoParameters = {
        filter: filtro,
        sortActive: 'nomeTipoContrato',
        sortDirection: 'asc',
        pageSize: 1000
      };
      const data = await this._tipoContratoService
        .obterPorParametros(params)
        .toPromise();
      this.tipos = data.items;
    }
  
    async obterClientes(filtro: string = '') {
      let params: ClienteParameters = {
        filter: filtro,
        sortActive: 'nomeFantasia',
        sortDirection: 'asc',
        pageSize: 1000
      };
      const data = await this._clienteService
        .obterPorParametros(params)
        .toPromise();
      this.clientes = data.items;
    }
  
    async obterContratos(filtro: string = '') {
      var codCliente = this.form.controls['codCliente'].value ?? null;
  
      let params: ContratoParameters = {
        filter: filtro,
        codCliente: codCliente,
        sortActive: 'nroContrato',
        sortDirection: 'asc',
        pageSize: 1000
      };
      const data = await this._contratoService
        .obterPorParametros(params)
        .toPromise();
      this.contratos = data.items;
    }
  
    private registrarEmitters() {
      this.form.controls['codCliente'].valueChanges.subscribe(() => {
        this.obterContratos();
      });
  
      this.clienteFilterCtrl.valueChanges
        .pipe(
          takeUntil(this._onDestroy),
          debounceTime(700),
          distinctUntilChanged()
        )
        .subscribe(() => {
          this.obterClientes(this.clienteFilterCtrl.value);
        });
  
      this.contratoFilterCtrl.valueChanges
        .pipe(
          takeUntil(this._onDestroy),
          debounceTime(700),
          distinctUntilChanged()
        )
        .subscribe(() => {
          this.obterContratos(this.contratoFilterCtrl.value);
        });
  
      this.tipoFilterCtrl.valueChanges
        .pipe(
          takeUntil(this._onDestroy),
          debounceTime(700),
          distinctUntilChanged()
        )
        .subscribe(() => {
          this.obterTipos(this.tipoFilterCtrl.value);
        });
    }
  
    limpar() {
      super.limpar();
  
    }
  
    ngOnDestroy() {
      this._onDestroy.next();
      this._onDestroy.complete();
    }
  }