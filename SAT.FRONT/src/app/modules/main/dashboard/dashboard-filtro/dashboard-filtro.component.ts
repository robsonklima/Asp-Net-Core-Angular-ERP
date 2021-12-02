import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { FilterBase } from 'app/core/filters/filter-base';
import { IFilterBase } from 'app/core/types/filtro.types';

@Component({
  selector: 'app-dashboard-filtro',
  templateUrl: './dashboard-filtro.component.html'
})
export class DashboardFiltroComponent extends FilterBase implements OnInit, IFilterBase {

  @Input() sidenav: MatSidenav;

  clienteFilterCtrl: FormControl = new FormControl();

  protected _onDestroy = new Subject<void>();

  constructor(
    protected _userService: UserService,
    protected _formBuilder: FormBuilder
  ) {
    super(_userService, _formBuilder, 'dashboard-filtro');
  }

  ngOnInit(): void {
    this.createForm();
    this.loadData();
  }

  loadData(): void {
  }

  createForm(): void {
    this.form = this._formBuilder.group({
      dataInicio: [undefined],
      dataFim: [undefined]
    });
    this.form.patchValue(this.filter?.parametros);
  }

  registrarEmitters(): void {
    this.clienteFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        // this.obterClientes(this.clienteFilterCtrl.value);
      });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}