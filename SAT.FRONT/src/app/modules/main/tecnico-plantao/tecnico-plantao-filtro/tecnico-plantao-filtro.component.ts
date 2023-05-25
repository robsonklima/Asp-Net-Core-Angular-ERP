import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { Autorizada } from 'app/core/types/autorizada.types';
import { Filial } from 'app/core/types/filial.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { Regiao } from 'app/core/types/regiao.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-tecnico-plantao-filtro',
  templateUrl: './tecnico-plantao-filtro.component.html'
})
export class TecnicoPlantaoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
  @Input() sidenav: MatSidenav;

  filiais: Filial[] = [];
  filialFilterCtrl: FormControl = new FormControl();
  autorizadas: Autorizada[] = [];
  autorizadaFilterCtrl: FormControl = new FormControl();
  regioes: Regiao[] = [];
  regiaoFilterCtrl: FormControl = new FormControl();

  protected _onDestroy = new Subject<void>();

  constructor(
    protected _userService: UserService,
    protected _formBuilder: FormBuilder
  ) {
    super(_userService, _formBuilder, 'plantao-tecnico');
  }

  ngOnInit(): void {
    this.createForm();
    this.loadData();
  }

  async loadData() {

  }

  createForm(): void {
    this.form = this._formBuilder.group({
      indAtivo: [undefined]
    });
    this.form.patchValue(this.filter?.parametros);
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}