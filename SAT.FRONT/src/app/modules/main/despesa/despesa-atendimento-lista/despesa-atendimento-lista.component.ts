import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { DespesaPeriodoService } from 'app/core/services/despesa-periodo.service';
import { DespesaPeriodo, DespesaPeriodoTecnico } from 'app/core/types/despesa-periodo.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
  selector: 'app-despesa-atendimento-lista',
  templateUrl: './despesa-atendimento-lista.component.html'
})

export class DespesaAtendimentoListaComponent implements OnInit
{
  userSession: UserSession;
  isLoading: boolean = false;
  despesasPeriodoTecnico: DespesaPeriodoTecnico[] = [];
  despesasPeriodo: DespesaPeriodo[] = [];

  constructor (
    private _formBuilder: FormBuilder,
    private _userService: UserService,
    private _despesaPeriodoSvc: DespesaPeriodoService,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService)
  { this.userSession = JSON.parse(this._userService.userSession); }

  ngOnInit(): void
  {
    this.obterDespesasPeriodo();
    this.obterDespesasPeriodoTecnico();
  }

  private async obterDespesasPeriodoTecnico()
  {
    if (!this.userSession.usuario.codTecnico) return;

    this.despesasPeriodoTecnico = (await this._despesaPeriodoTecnicoSvc.obterPorParametros({
      codTecnico: this.userSession.usuario.codTecnico,
      pageSize: 500,
    }).toPromise()).items;
  }

  private async obterDespesasPeriodo()
  {
    this.despesasPeriodo = (await this._despesaPeriodoSvc.obterPorParametros({
      indAtivo: 1,
      pageSize: 500,
    }).toPromise()).items;
  }
}
