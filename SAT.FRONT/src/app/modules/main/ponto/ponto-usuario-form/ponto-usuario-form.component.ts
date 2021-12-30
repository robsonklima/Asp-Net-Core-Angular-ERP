import { Component, Inject, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { PontoUsuarioDataService } from 'app/core/services/ponto-usuario-data.service';
import { PontoUsuarioService } from 'app/core/services/ponto-usuario.service';
import { pontoUsuarioDataStatusConst } from 'app/core/types/ponto-usuario-data-status.types';
import { PontoUsuarioData } from 'app/core/types/ponto-usuario-data.types';
import { PontoUsuario } from 'app/core/types/ponto-usuario.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';
import { PontoHorariosListaComponent } from '../ponto-horarios-lista/ponto-horarios-lista.component';

@Component({
  selector: 'app-ponto-usuario-form',
  templateUrl: './ponto-usuario-form.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class PontoUsuarioFormComponent implements OnInit {
  pontoUsuarioData: PontoUsuarioData;
  form: FormGroup;
  userSession: UserSession;

  constructor(
    public dialogRef: MatDialogRef<PontoHorariosListaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _userSvc: UserService,
    private _snack: CustomSnackbarService,
    private _pontoUsuarioSvc: PontoUsuarioService,
    private _pontoUsuarioDataSvc: PontoUsuarioDataService,
    private _formBuilder: FormBuilder
  ) {
    this.userSession = JSON.parse(this._userSvc.userSession);
    this.pontoUsuarioData = data.pontoUsuarioData;
    this.ordenarRegistros();
  }

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      data: [{value: moment(this.pontoUsuarioData.dataRegistro).format('DD/MM/yyyy'), disabled: true}],
      horario: ['', Validators.required]
    });
  }

  salvar() {
    const form = this.form.getRawValue();

    if (!moment(form.horario,"HH:mm", true).isValid()) {
      this._snack.exibirToast('Digite um horário válido!', 'error');

      return;
    }

    const dataHoraRegistro = `${moment(this.pontoUsuarioData.dataRegistro).format('yyyy-MM-DD')} ${form.horario}:00`;
    const pontoUsuario = {
      codPontoPeriodo: this.pontoUsuarioData.codPontoPeriodo,
      codUsuario: this.userSession.usuario.codUsuario,
      codUsuarioCad: this.userSession.usuario.codUsuario,
      dataHoraEnvio: moment().format('yyyy-MM-DD HH:mm'),
      dataHoraRegistro: dataHoraRegistro,
      indAtivo: statusConst.ATIVO
    };
    
    this._pontoUsuarioSvc.criar(pontoUsuario).subscribe((ponto) => {
      this.pontoUsuarioData.pontosUsuario.push(ponto);
      this.ordenarRegistros();
      this._snack.exibirToast('Registro inserido com sucesso!', 'success');
    }, () => {
      this._snack.exibirToast('Erro ao inserir o registro!', 'error');
    });

    this.form.controls['horario'].setValue('');
    this.form.markAsUntouched();
    this.form.markAsPristine();
  }

  async remover(pontoUsuario: PontoUsuario) {
    this._pontoUsuarioSvc.deletar(pontoUsuario.codPontoUsuario).subscribe(() => {
      const index = this.pontoUsuarioData.pontosUsuario.indexOf(pontoUsuario);
      if (index > -1) {
        this.pontoUsuarioData.pontosUsuario.splice(index, 1);
      }

      this._snack.exibirToast('Registro removido com sucesso!', 'success');
    }, () => {
      this._snack.exibirToast('Erro ao remover o registro!', 'error');
    });
  }

  private ordenarRegistros(): void {
    this.pontoUsuarioData.pontosUsuario = this.pontoUsuarioData.pontosUsuario
        .sort((a, b) => (moment(a.dataHoraRegistro) > moment(b.dataHoraRegistro)) ? 1 : ((moment(b.dataHoraRegistro) > moment(a.dataHoraRegistro)) ? -1 : 0));
  }

  finalizar() {
    this.pontoUsuarioData.codPontoUsuarioDataStatus = pontoUsuarioDataStatusConst.AGUARDANDO_CONFERENCIA;
    this.pontoUsuarioData.codUsuarioManut = this.userSession.usuario.codUsuario;
    this.pontoUsuarioData.dataHoraManut = moment().format('yyyy-MM-DD HH:mm');
    this._pontoUsuarioDataSvc.atualizar(this.pontoUsuarioData).subscribe(() => {
      this._snack.exibirToast('Registro atualizado com sucesso!', 'success');
      this.dialogRef.close();
    }, () => {
      this._snack.exibirToast('Erro ao atualizar o registro!', 'error');
    })
  }
}
