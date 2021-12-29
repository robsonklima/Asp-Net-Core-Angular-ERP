import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { UserService } from 'app/core/user/user.service';
import { FilterBase } from 'app/core/filters/filter-base';
import { IFilterBase } from 'app/core/types/filtro.types';
import { DespesaPeriodoTecnicoStatus, DespesaPeriodoTecnicoStatusEnum } from 'app/core/types/despesa-periodo.types';
import { statusConst } from 'app/core/types/status-types';

@Component({
  selector: 'app-despesa-atendimento-filtro',
  templateUrl: './despesa-atendimento-filtro.component.html'
})
export class DespesaAtendimentoFiltroComponent extends FilterBase implements OnInit, IFilterBase
{
  @Input() sidenav: MatSidenav;
  despesaPeriodoStatus: DespesaPeriodoTecnicoStatus[] = [];

  constructor (
    protected _userService: UserService,
    protected _formBuilder: FormBuilder)
  {
    super(_userService, _formBuilder, 'despesa-atendimento');
  }

  createForm(): void
  {
    this.form = this._formBuilder.group({
      indAtivo: [statusConst.ATIVO],
      codDespesaPeriodoStatus: [undefined],
      inicioPeriodo: [undefined],
      fimPeriodo: [undefined]
    });

    this.form.patchValue(this.filter?.parametros);
  }

  loadData(): void 
  {
    this.obterStatus();
  }

  private obterStatus()
  {
    Object.keys(DespesaPeriodoTecnicoStatusEnum).forEach(key =>
    {
      this.despesaPeriodoStatus.push(
        {
          codDespesaPeriodoTecnicoStatus: DespesaPeriodoTecnicoStatusEnum[key],
          nomeDespesaPeriodoTecnicoStatus: key
        }
      )
    });
  }

  ngOnInit(): void
  {
    this.createForm();
    this.loadData();
  }
}
