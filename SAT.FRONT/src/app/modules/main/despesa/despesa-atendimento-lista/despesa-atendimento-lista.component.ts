import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { DespesaPeriodoService } from 'app/core/services/despesa-periodo.service';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
  selector: 'app-despesa-atendimento-lista',
  templateUrl: './despesa-atendimento-lista.component.html'
})

export class DespesaAtendimentoListaComponent implements OnInit
{
  userSession: UserSession;

  constructor (
    private _formBuilder: FormBuilder,
    private _userService: UserService,
    private _despesaPeriodoSvc: DespesaPeriodoService)
  { this.userSession = JSON.parse(this._userService.userSession); }

  ngOnInit(): void
  {
  }

}
