import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { Filial } from 'app/core/types/filial.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-transacoes-cartao-filtro',
  templateUrl: './transacoes-cartao-filtro.component.html'
})
export class TransacoesCartaoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
  @Input() sidenav: MatSidenav;
  tecnicos: Tecnico[] = [];
  filiais: Filial[] = [];
  creditoCartaoStatus: any = [];

  constructor(
    protected _userService: UserService,
    protected _formBuilder: FormBuilder) {
    super(_userService, _formBuilder, 'ticket-log-transacao');
  }

  createForm(): void {
    this.form = this._formBuilder.group({
      numeroCartao: [undefined],
      uf: [undefined],
      cidade: [undefined],
      responsavel: [undefined],
      dataInicio: [undefined],
      dataFim: [undefined],
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