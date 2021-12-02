import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PontoUsuarioDataDivergenciaService } from 'app/core/services/ponto-usuario-data-divergencia.service';
import { PontoUsuarioDataMotivoDivergenciaService } from 'app/core/services/ponto-usuario-data-motivo-divergencia.service';
import { PontoUsuarioDataTipoAdvertenciaService } from 'app/core/services/ponto-usuario-data-tipo-advertencia.service';
import { PontoUsuarioDataService } from 'app/core/services/ponto-usuario-data.service';
import { PontoUsuarioDataDivergencia } from 'app/core/types/ponto-usuario-data-divergencia.types';
import { PontoUsuarioDataMotivoDivergencia } from 'app/core/types/ponto-usuario-data-motivo-divergencia.types';
import { pontoUsuarioDataStatusConst } from 'app/core/types/ponto-usuario-data-status.types';
import { PontoUsuarioDataTipoAdvertencia, pontoUsuarioDataTipoAdvertenciConst } from 'app/core/types/ponto-usuario-data-tipo-advertencia.types';
import { PontoUsuarioData } from 'app/core/types/ponto-usuario-data.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';
import { PontoHorariosListaComponent } from '../ponto-horarios-lista/ponto-horarios-lista.component';

@Component({
  selector: 'app-ponto-inconsistencia-form',
  templateUrl: './ponto-inconsistencia-form.component.html'
})
export class PontoInconsistenciaFormComponent implements OnInit {
  form: FormGroup;
  userSession: UserSession;
  motivosDivergencia: PontoUsuarioDataMotivoDivergencia[] = [];
  tiposAdvertencia: PontoUsuarioDataTipoAdvertencia[] = [];
  pontoUsuarioData: PontoUsuarioData;

  constructor(
    private _pontoUsuarioDataMotivoDivergenciaSvc: PontoUsuarioDataMotivoDivergenciaService,
    private _pontoUsuarioDataTipoAdvertenciaSvc: PontoUsuarioDataTipoAdvertenciaService,
    private _pontoUsuarioDataDivergenciaSvc: PontoUsuarioDataDivergenciaService,
    public dialogRef: MatDialogRef<PontoHorariosListaComponent>,
    private _pontoUsuarioDataSvc: PontoUsuarioDataService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _formBuilder: FormBuilder,
    private _userSvc: UserService
  ) {
    this.userSession = JSON.parse(this._userSvc.userSession);
    this.pontoUsuarioData = data.pontoUsuarioData;
  }

  async ngOnInit() {
    this.form = this._formBuilder.group({
      codPontoUsuarioDataMotivoDivergencia: [undefined, [Validators.required]],
      codPontoUsuarioDataTipoAdvertencia: [undefined, [Validators.required]],
    });

    const motivos = await this._pontoUsuarioDataMotivoDivergenciaSvc
      .obterPorParametros({})
      .toPromise();
    
    this.motivosDivergencia = motivos.items;

    const tipos = await this._pontoUsuarioDataTipoAdvertenciaSvc
      .obterPorParametros({})
      .toPromise();
    
    this.tiposAdvertencia = tipos.items;
  }

  async salvar() {
    const form: any = this.form.getRawValue();

    const divergencia: PontoUsuarioDataDivergencia = {
      codPontoUsuarioData: this.pontoUsuarioData.codPontoUsuarioData,
      codUsuarioCad: this.userSession.usuario.codUsuario,
      dataHoraCad: moment().format('yyyy-MM-dd HH:mm:ss'),
      divergenciaValidada: 0,
      codPontoUsuarioDataModoDivergencia: pontoUsuarioDataTipoAdvertenciConst.MANUAL,
      codPontoUsuarioDataMotivoDivergencia: form.codPontoUsuarioDataMotivoDivergencia
    };

    //await this._pontoUsuarioDataDivergenciaSvc.criar(divergencia).subscribe();

    this.pontoUsuarioData.codPontoUsuarioDataStatus = pontoUsuarioDataStatusConst.INCONSISTENTE;
    this.pontoUsuarioData.dataHoraManut = moment().format('yyyy-MM-dd HH:mm:ss');
    this.pontoUsuarioData.codUsuarioManut = this.userSession.usuario.codUsuario;

    //await this._pontoUsuarioDataSvc.atualizar(this.pontoUsuarioData).subscribe();

    this.dialogRef.close();  
  }

  fechar() {
    this.dialogRef.close();
  }
}
