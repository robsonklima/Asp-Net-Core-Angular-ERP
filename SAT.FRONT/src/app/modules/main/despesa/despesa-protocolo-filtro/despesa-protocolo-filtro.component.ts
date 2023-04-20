import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { FilialService } from 'app/core/services/filial.service';
import { Filial } from 'app/core/types/filial.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-despesa-protocolo-filtro',
  templateUrl: './despesa-protocolo-filtro.component.html',
})

export class DespesaProtocoloFiltroComponent extends FilterBase implements OnInit, IFilterBase {
  filiais: Filial[] = [];
  @Input() sidenav: MatSidenav;

  constructor(
    protected _userService: UserService,
    protected _formBuilder: FormBuilder,
    private _filialService: FilialService
  ) {
    super(_userService, _formBuilder, 'despesa-protocolo');
  }

  ngOnInit(): void {
    this.createForm();
    this.loadData();
  }

  createForm(): void {
    this.form = this._formBuilder.group({
      codFilial: [null]
    });

    this.form.patchValue(this.filter?.parametros);
  }

  async loadData() {
    this.obterFiliais();
  }

  private async obterFiliais() {
    this.filiais = (await this._filialService
      .obterPorParametros({ indAtivo: statusConst.ATIVO, sortDirection: 'asc', sortActive: 'nomeFilial' })
      .toPromise()).items;
  }
}
