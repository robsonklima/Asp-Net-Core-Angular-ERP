import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Notifications } from '@mobiscroll/angular';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { MbscAgendaTecnicoCalendarEvent } from 'app/core/types/agenda-tecnico.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';
import { AgendaTecnicoValidator } from '../agenda-tecnico.validator';

@Component({
  selector: 'app-agenda-tecnico-realocacao-dialog',
  templateUrl: './agenda-tecnico-realocacao-dialog.component.html'
})

export class AgendaTecnicoRealocacaoDialogComponent implements OnInit
{
  agendamentos: MbscAgendaTecnicoCalendarEvent[];
  tecnico: Tecnico;
  codTecnico: number;
  form: FormGroup;
  initialTime: string;
  userSession: UserSession;
  isLoading: boolean = false;

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<AgendaTecnicoRealocacaoDialogComponent>,
    private _formBuilder: FormBuilder,
    private _agendaTecnicoSvc: AgendaTecnicoService,
    private _notify: Notifications,
    private _userService: UserService,
    private _validator: AgendaTecnicoValidator,
    private _tecnicoSvc: TecnicoService)
  {
    if (data)
    {
      this.agendamentos = data.agendamentos;
      this.initialTime = data.initialTime;
      this.codTecnico = data.codTecnico;
    }
    this.userSession = JSON.parse(this._userService.userSession);
    this.criarForm();
  }

  async ngOnInit()
  {
    this.isLoading = true;
    await this.obterTecnico();
    this.isLoading = false;
  }

  criarForm()
  {
    this.form = this._formBuilder.group({
      codAgendaTecnico: [undefined, [Validators.required]]
    });
  }

  async obterTecnico()
  {
    this.tecnico = (await this._tecnicoSvc.obterPorCodigo(this.codTecnico).toPromise());
  }

  async confirmar()
  {
    await this.atualizarAtendimento();
    this.dialogRef.close(true);
  }

  async atualizarAtendimento()
  {
    var codAgendaTecnico = this.form.controls.codAgendaTecnico.value;

    var agendamento = (await this._agendaTecnicoSvc.obterPorCodigo(codAgendaTecnico).toPromise());
    agendamento.inicio = moment(this.initialTime).format('yyyy-MM-DD HH:mm:ss');
    agendamento.fim = moment(this.initialTime).add(1, 'hour').format('yyyy-MM-DD HH:mm:ss');
    agendamento.cor = this._validator.getRealocationStatusColor(moment(this.initialTime));
    agendamento.usuarioAtualizacao = this.userSession.usuario.codUsuario;
    agendamento.ultimaAtualizacao = moment().format('yyyy-MM-DD HH:mm:ss');

    await this._agendaTecnicoSvc.atualizar(agendamento).toPromise().then(() =>
    {
      this._notify.toast({ message: 'Atendimento realocado com sucesso.', color: 'success' });
    })
      .catch(() =>
      {
        this._notify.toast({ message: 'Erro ao realocar atendimento.', color: 'danger' });
      });
  }
}