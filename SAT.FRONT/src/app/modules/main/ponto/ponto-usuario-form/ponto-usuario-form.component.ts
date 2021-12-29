import { Component, Inject, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PontoUsuarioService } from 'app/core/services/ponto-usuario.service';
import { PontoUsuarioData } from 'app/core/types/ponto-usuario-data.types';
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
  @ViewChild('ngForm') ngForm: NgForm;
  pontoUsuarioData: PontoUsuarioData;
  form: FormGroup;
  userSession: UserSession;
  alert: any;

  constructor(
    public dialogRef: MatDialogRef<PontoHorariosListaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _userSvc: UserService,
    private _pontoUsuarioSvc: PontoUsuarioService,
    private _formBuilder: FormBuilder
  ) {
    this.userSession = JSON.parse(this._userSvc.userSession);
    this.pontoUsuarioData = data.pontoUsuarioData;
  }

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      horario: [null, Validators.required]
    });
  }

  salvar() {
    const form = this.form.getRawValue();

    if (!moment(form.horario,"HH:mm", true).isValid()) {
      this.alert = {
        type: 'error',
        message: 'Digite um horário válido!'
      };

      setTimeout(() => { this.alert = null; }, 3000);
      this.clearForm();
      return;
    }

    const dataHoraRegistro = `${moment(this.pontoUsuarioData.dataRegistro).format('yyyy-MM-DD')} ${form.horario}`;
    const pontoUsuario = {
      codPontoPeriodo: this.pontoUsuarioData.codPontoPeriodo,
      codUsuario: this.userSession.usuario.codUsuario,
      codUsuarioCad: this.userSession.usuario.codUsuario,
      dataHoraEnvio: moment().format('yyyy-MM-DD HH:mm:ss'),
      dataHoraRegistro: dataHoraRegistro,
      indAtivo: statusConst.ATIVO
    };

    this._pontoUsuarioSvc.criar(pontoUsuario).subscribe(() => {
      this.pontoUsuarioData.pontosUsuario.push(pontoUsuario);

      this.alert = {
        type: 'success',
        message: 'Registro inserido com sucesso!'
      };
  
      setTimeout(() => { this.alert = null; }, 3000);
      this.clearForm();
    }, () => {
      this.alert = {
        type: 'error',
        message: 'Erro ao inserir horário!'
      };
  
      setTimeout(() => { this.alert = null; }, 3000);
      this.clearForm();
    });
  }

  clearForm(): void {
    this.ngForm?.resetForm();
  }
}
