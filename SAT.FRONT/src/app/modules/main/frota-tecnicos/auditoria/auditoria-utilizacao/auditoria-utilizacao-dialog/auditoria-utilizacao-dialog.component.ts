import { Inject, Component, LOCALE_ID, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import 'leaflet';
import 'leaflet-routing-machine';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import _ from 'lodash';
import { Subject } from 'rxjs';
import { Auditoria } from 'app/core/types/auditoria.types';
import { first } from 'rxjs/operators';
import { AuditoriaService } from 'app/core/services/auditoria.service';
import { AuditoriaVeiculoTanque } from 'app/core/types/auditoria-veiculo-tanque.types';
import { AuditoriaVeiculoTanqueService } from 'app/core/services/auditoria-veiculo-tanque.service';
declare var L: any;

@Component({
  selector: 'app-auditoria-utilizacao-dialog',
  templateUrl: './auditoria-utilizacao-dialog.component.html',
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})
export class AuditoriaUtilizacaoDialogComponent implements OnInit {
  form: FormGroup;
  userSession: UserSession;
  codAuditoria: number;
  auditoria: Auditoria;
  mapsPlaceholder: any = [];
  tanques: AuditoriaVeiculoTanque[] = [];
  protected _onDestroy = new Subject<void>();

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _formBuilder: FormBuilder,
    private _userSvc: UserService,
    private _auditoriaService: AuditoriaService,
    private _auditoriaVeiculoTanqueService: AuditoriaVeiculoTanqueService,
    private _snack: CustomSnackbarService,
    private dialogRef: MatDialogRef<AuditoriaUtilizacaoDialogComponent>) {
    if (data)
    {
        this.codAuditoria = data.codAuditoria;
    }

    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  async ngOnInit() {
    this.obterAuditoria();
    this.criarForm();
    this.registrarEmitters();
    this.obterTanques();
  }

  criarForm(){
    this.form = this._formBuilder.group({
        step1: this._formBuilder.group({
          dataHoraRetiradaVeiculo: [undefined, Validators.required],
          dataHoraCad: [undefined, Validators.required],
        }),
        step2: this._formBuilder.group({
            odometroInicialRetirada: [undefined, Validators.required],
            odometroPeriodoAuditado: [undefined, Validators.required],
            kmFerias: [undefined, Validators.required],
        }),
        step3: this._formBuilder.group({
            creditosCartao: [undefined, Validators.required],
            saldoCartao: [undefined, Validators.required],
            codAuditoriaVeiculoTanque: [undefined, Validators.required],
            qtdLitros: [undefined, Validators.required],
        }),
      });
  }

  registrarEmitters(){}

  obterAuditoria(){    
    this._auditoriaService.obterPorCodigo(this.codAuditoria)
    .pipe(first())
    .subscribe(data => {
      this.form.patchValue(data);
      this.auditoria = data;
    });}

  private async obterTanques(){
    const data = await this._auditoriaVeiculoTanqueService.obterPorParametros({}).toPromise();
    this.tanques = data.items;
  }

  onProximo(): void {}

  salvar(){}


}