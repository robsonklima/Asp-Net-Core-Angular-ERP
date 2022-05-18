import { statusConst } from '../../../../../core/types/status-types';
import { UserService } from '../../../../../core/user/user.service';
import { ClienteService } from '../../../../../core/services/cliente.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { Cliente, ClienteParameters } from '../../../../../core/types/cliente.types';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Contrato, ContratoParameters } from 'app/core/types/contrato.types';
import { ContratoService } from 'app/core/services/contrato.service';


@Component({
  selector: 'app-equipamento-contrato-filtro',
  templateUrl: './equipamento-contrato-filtro.component.html'
})
export class EquipamentoContratoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
  
  @Input() sidenav: MatSidenav;

  clientes: Cliente[] = [];  
  clienteFilterCtrl: FormControl = new FormControl();
  contratos: Contrato[] = [];
  contratoFilterCtrl: FormControl = new FormControl();
    
  protected _onDestroy = new Subject<void>();
v
  constructor(
    
    private _clienteService: ClienteService,
    private _contratoService: ContratoService,
    protected _userService: UserService,    
    protected _formBuilder: FormBuilder
  ) {
    super(_userService, _formBuilder, 'equipamento-contrato');
  }

  ngOnInit(): void {
    this.createForm();
    this.loadData();
  }

  loadData(): void {
    debugger
    this.obterClientes();
    this.obterContratos();

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
  }

  createForm(): void {
    this.form = this._formBuilder.group({
      codClientes: [undefined],  
      indAtivo: [undefined]
    });

    this.form.patchValue(this.filter?.parametros);
  }

  async obterClientes(filtro: string = '') {
    let params: ClienteParameters = {
      filter: filtro,
      indAtivo: statusConst.ATIVO,
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
    var clienteFilter = this.form.controls['codClientes'].value;
    let params: ContratoParameters = {
      filter: filtro,
      indAtivo: statusConst.ATIVO,
      codClientes: clienteFilter,
      sortActive: 'nomeContrato',
      sortDirection: 'asc',
      pageSize: 1000
    };

    const data = await this._contratoService
      .obterPorParametros(params)
      .toPromise();

    this.contratos = data.items;
  }

  limpar() {
    super.limpar();

    if (this.userSession?.usuario?.codFilial) {
      this.form.controls['codFiliais'].setValue([this.userSession.usuario.codFilial]);
      this.form.controls['codFiliais'].disable();
    }
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}