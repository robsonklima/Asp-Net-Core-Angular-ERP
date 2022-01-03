import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { StatusServicoService } from 'app/core/services/status-servico.service';
import { IFilterBase } from 'app/core/types/filtro.types';
import { StatusServico, StatusServicoParameters } from 'app/core/types/status-servico.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-orcamento-filtro',
  templateUrl: './orcamento-filtro.component.html'
})
export class OrcamentoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
  @Input() sidenav: MatSidenav;
  statusServicos: StatusServico[] = [];

  constructor(
    protected _userService: UserService,
    protected _formBuilder: FormBuilder,
    private _statusServicoSvc: StatusServicoService
  ) {
    super(_userService, _formBuilder, 'orcamento');
  }

  ngOnInit(): void {
    this.createForm();
    this.loadData();
  }

  createForm(): void {
    this.form = this._formBuilder.group({
      codStatusServicos: [undefined],
    });

    this.form.patchValue(this.filter?.parametros);
  }

  loadData(): void {
    this.obterStatusServicos();
  }

  async obterStatusServicos() {
    let params: StatusServicoParameters = {
      indAtivo: statusConst.ATIVO,
      sortActive: 'nomeStatusServico',
      sortDirection: 'asc'
    }

    const data = await this._statusServicoSvc
      .obterPorParametros(params)
      .toPromise();

    this.statusServicos = data.items;
  }
}
