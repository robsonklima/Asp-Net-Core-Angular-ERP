import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { Filial } from 'app/core/types/filial.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-despesa-protocolo-filtro',
  templateUrl: './despesa-protocolo-filtro.component.html',
})

export class DespesaProtocoloFiltroComponent extends FilterBase implements OnInit, IFilterBase {
  filiais: Filial[] = [];
  filialFilterCtrl: FormControl = new FormControl();
  @Input() sidenav: MatSidenav;

  constructor(
    protected _userService: UserService,
    protected _formBuilder: FormBuilder) {
    super(_userService, _formBuilder, 'despesa-protocolo');
  }

  createForm(): void {
    this.form = this._formBuilder.group({
      codFilial: [null],
      codTecnicos: [null]
    });

    this.form.patchValue(this.filter?.parametros);
  }

  loadData(): void {

  }

  ngOnInit(): void {
    this.createForm();
    this.loadData();
  }
}
