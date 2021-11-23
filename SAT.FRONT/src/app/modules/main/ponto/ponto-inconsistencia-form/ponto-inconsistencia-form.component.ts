import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PontoUsuarioDataMotivoDivergenciaService } from 'app/core/services/ponto-usuario-data-motivo-divergencia.service';
import { PontoUsuarioDataTipoAdvertenciaService } from 'app/core/services/ponto-usuario-data-tipo-advertencia.service';
import { PontoUsuarioDataService } from 'app/core/services/ponto-usuario-data.service';
import { PontoUsuarioDataMotivoDivergencia } from 'app/core/types/ponto-usuario-data-motivo-divergencia.types';
import { PontoUsuarioDataTipoAdvertencia } from 'app/core/types/ponto-usuario-data-tipo-advertencia.types';
import { PontoUsuarioData } from 'app/core/types/ponto-usuario-data.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
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
    private _pontoUsuarioDataMorivoDivergenciaSvc: PontoUsuarioDataMotivoDivergenciaService,
    private _pontoUsuarioDataTipoAdvertenciaSvc: PontoUsuarioDataTipoAdvertenciaService,
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

    const motivos = await this._pontoUsuarioDataMorivoDivergenciaSvc.obterPorParametros({}).toPromise();
    this.motivosDivergencia = motivos.items;

    const tipos = await this._pontoUsuarioDataTipoAdvertenciaSvc.obterPorParametros({}).toPromise();
    this.tiposAdvertencia = tipos.items;
  }

  salvar(): void {
    //this.pontoUsuarioData.codPontoUsuarioDataStatus = this.form.

    this._pontoUsuarioDataSvc.atualizar(this.pontoUsuarioData).subscribe(() => {
      this.dialogRef.close();  
    })
  }

  fechar() {
    this.dialogRef.close();
  }
}
