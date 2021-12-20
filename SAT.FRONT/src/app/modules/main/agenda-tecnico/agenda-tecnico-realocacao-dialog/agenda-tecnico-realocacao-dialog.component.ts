import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
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
  isRealocando: boolean = false;

  snackConfigInfo: MatSnackBarConfig = { duration: 2000, panelClass: 'info', verticalPosition: 'top', horizontalPosition: 'right' };
  snackConfigDanger: MatSnackBarConfig = { duration: 2000, panelClass: 'danger', verticalPosition: 'top', horizontalPosition: 'right' };
  snackConfigSuccess: MatSnackBarConfig = { duration: 2000, panelClass: 'success', verticalPosition: 'top', horizontalPosition: 'right' };

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<AgendaTecnicoRealocacaoDialogComponent>,
    private _formBuilder: FormBuilder,
    private _snack: MatSnackBar,
    private _agendaTecnicoSvc: AgendaTecnicoService,
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
    this.isLoading = true;
    this.isRealocando = true;
    var codAgendaTecnico = this.form.controls.codAgendaTecnico.value;

    var agendamento = (await this._agendaTecnicoSvc.obterPorCodigo(codAgendaTecnico).toPromise());
    agendamento.inicio = moment(this.initialTime).format('yyyy-MM-DD HH:mm:ss');
    agendamento.fim = moment(this.initialTime).add(1, 'hour').format('yyyy-MM-DD HH:mm:ss');
    agendamento.cor = this._validator.getRealocationStatusColor(agendamento, moment(this.initialTime));
    agendamento.codUsuarioManut = this.userSession.usuario.codUsuario;
    agendamento.dataHoraManut = moment().format('yyyy-MM-DD HH:mm:ss');

    await this._agendaTecnicoSvc.atualizar(agendamento).toPromise().then(async a =>
    {
      await this._snack.open('Atendimento realocado com sucesso.', null, this.snackConfigSuccess).afterDismissed().toPromise();
    })
      .catch(async () =>
      {
        await this._snack.open('Erro ao realocar atendimento.', null, this.snackConfigDanger).afterDismissed().toPromise();
      });
  }
}