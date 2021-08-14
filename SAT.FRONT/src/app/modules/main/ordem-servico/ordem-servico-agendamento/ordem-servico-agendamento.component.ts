import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import moment, { Moment } from 'moment';
import { OrdemServicoDetalheComponent } from '../ordem-servico-detalhe/ordem-servico-detalhe.component';
import { MotivoAgendamentoService } from 'app/core/services/motivo-agendamento.service';
import { MotivoAgendamento, MotivoAgendamentoData, MotivoAgendamentoParameters } from 'app/core/types/motivo-agendamento.types';
import { Agendamento } from 'app/core/types/agendamento.types';
import { UserService } from 'app/core/user/user.service';
import { UsuarioSessionData } from 'app/core/types/usuario.types';

@Component({
  selector: 'app-ordem-servico-agendamento',
  templateUrl: './ordem-servico-agendamento.component.html'
})
export class OrdemServicoAgendamentoComponent implements OnInit {
  form: FormGroup;
  motivosAgendamento: MotivoAgendamento[] = [];
  motivoFilterCtrl: FormControl = new FormControl();
  userSession: UsuarioSessionData;
  hoje: Moment = moment();

  constructor(
    private _formBuilder: FormBuilder,
    private _motivoAgendamentoService: MotivoAgendamentoService,
    private _userService: UserService,
    public dialogRef: MatDialogRef<OrdemServicoDetalheComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.inicializarForm();
    this.obterMotivosAgendamento();
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      data: [
        {
          value: moment(),
          disabled: false,
        }, [Validators.required]
      ],
      hora: [moment().format('HH:mm'), [Validators.required]],
      codMotivo: [
        {
          value: undefined,
          disabled: false,
        }, [Validators.required]
      ],
    });
  }

  private async obterMotivosAgendamento() {
    const parametros: MotivoAgendamentoParameters = { 
      sortActive: 'descricaoMotivo',
      sortDirection: 'asc',
      indAtivo: 1 
    };

    const data: MotivoAgendamentoData = await this._motivoAgendamentoService
      .obterPorParametros(parametros)
      .toPromise();

    this.motivosAgendamento = data.motivosAgendamento;
  }

  salvar(): void {
    const form = this.form.getRawValue();
    let dataHora = moment(`${form.data.format('YYYY-MM-DD')} ${form.hora}`).format('YYYY-MM-DD HH:mm');
    let agendamento: Agendamento = {
      codOS: this.data.codOS,
      codMotivo: form.codMotivo,
      dataHoraUsuAgendamento: moment().format('YYYY-MM-DD HH:mm'),
      dataAgendamento: dataHora,
      codUsuarioAgendamento: this.userSession.usuario.codUsuario
    }
    this.dialogRef.close({ agendamento: agendamento });
  }
}
