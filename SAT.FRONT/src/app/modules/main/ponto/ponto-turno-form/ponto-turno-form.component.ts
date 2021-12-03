import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { TurnoService } from 'app/core/services/turno.service';
import { Turno } from 'app/core/types/turno.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { first } from 'rxjs/internal/operators/first';

@Component({
  selector: 'app-ponto-turno-form',
  templateUrl: './ponto-turno-form.component.html'
})
export class PontoTurnoFormComponent implements OnInit, OnDestroy {
  codTurno: number;
  turno: Turno;
  isAddMode: boolean;
  form: FormGroup;
  userSession: UsuarioSessao;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _turnoService: TurnoService,
    private _location: Location
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codTurno = +this._route.snapshot.paramMap.get('codTurno');
    this.isAddMode = !this.codTurno;
    
    this.form = this._formBuilder.group({
      codTurno: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      descTurno: [undefined, Validators.required],
      horaInicio1: [undefined, Validators.required],
      horaFim1: [undefined, Validators.required],
      horaInicio2: [undefined, Validators.required],
      horaFim2: [undefined, Validators.required],
      indAtivo: [undefined],
    });
    
    if (!this.isAddMode) {
      this._turnoService.obterPorCodigo(this.codTurno)
        .pipe(first())
        .subscribe(data => {
          data.horaInicio1 = moment(data.horaInicio1).format('HH:mm');
          data.horaFim1 = moment(data.horaFim1).format('HH:mm');
          data.horaInicio2 = moment(data.horaInicio2).format('HH:mm');
          data.horaFim2 = moment(data.horaFim2).format('HH:mm');
          this.form.patchValue(data);
          this.turno = data;
        });
    }
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.turno,
      ...form,
      ...{
        horaInicio1: moment().format(`yyyy-MM-DD ${form.horaInicio1}`),
        horaFim1: moment().format(`yyyy-MM-DD ${form.horaFim1}`),
        horaInicio2: moment().format(`yyyy-MM-DD ${form.horaInicio2}`),
        horaFim2: moment().format(`yyyy-MM-DD ${form.horaFim2}`),
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
        indAtivo: +form.indAtivo
      }
    };
    
    this._turnoService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast(`Turno atualizado com sucesso!`, "success");
      this._location.back();
    });
  }

  criar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.turno,
      ...form,
      ...{
        horaInicio1: moment().format(`yyyy-MM-DD ${form.horaInicio1}`),
        horaFim1: moment().format(`yyyy-MM-DD ${form.horaFim1}`),
        horaInicio2: moment().format(`yyyy-MM-DD ${form.horaInicio2}`),
        horaFim2: moment().format(`yyyy-MM-DD ${form.horaFim2}`),
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        indAtivo: +form.indAtivo
      }
    };

    this._turnoService.criar(obj).subscribe(() => {
      this._snack.exibirToast(`Turno adicionado com sucesso!`, "success");
      this._location.back();
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
