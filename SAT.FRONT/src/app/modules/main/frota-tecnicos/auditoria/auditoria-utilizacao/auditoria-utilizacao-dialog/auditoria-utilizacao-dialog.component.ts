import { Inject, Component, LOCALE_ID, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import 'leaflet';
import 'leaflet-routing-machine';
import _ from 'lodash';
import { Subject } from 'rxjs';
import { Auditoria } from 'app/core/types/auditoria.types';
import { first } from 'rxjs/operators';
import { AuditoriaService } from 'app/core/services/auditoria.service';
import { AuditoriaVeiculoTanque } from 'app/core/types/auditoria-veiculo-tanque.types';
import { AuditoriaVeiculoTanqueService } from 'app/core/services/auditoria-veiculo-tanque.service';
import { DespesaPeriodoTecnico } from 'app/core/types/despesa-periodo.types';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import moment from 'moment';
import { DespesaTipoEnum } from 'app/core/types/despesa.types';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Location } from '@angular/common';
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
  despesasPeriodoTecnico: DespesaPeriodoTecnico[] = [];
  protected _onDestroy = new Subject<void>();

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _formBuilder: FormBuilder,
    private _userSvc: UserService,
    private _auditoriaService: AuditoriaService,
    private _auditoriaVeiculoTanqueService: AuditoriaVeiculoTanqueService,
    private _despesaPeriodoTecnicoService: DespesaPeriodoTecnicoService,
    private _snack: CustomSnackbarService,
    private _location: Location,
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

  registrarEmitters(){
    (this.form.get('step3') as FormGroup).controls["codAuditoriaVeiculoTanque"].valueChanges.subscribe(codAuditoriaVeiculoTanque => {
      this.atualizarTanque(codAuditoriaVeiculoTanque);
    });
  }

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

  private async atualizarTanque(codAuditoriaVeiculoTanque) {
    const veiculoTanque = await this._auditoriaVeiculoTanqueService
      .obterPorCodigo(codAuditoriaVeiculoTanque)
      .toPromise();

    if (!veiculoTanque) 
      return;

    this.auditoria.auditoriaVeiculo.auditoriaVeiculoTanque.qtdLitros = veiculoTanque.qtdLitros;;
  }

  private zerarValores(){
    this.auditoria.kmPercorrido = 0;
    this.auditoria.kmCompensado = 0;
    this.auditoria.despesasSAT = 0;
    this.auditoria.despesasCompensadasValor = 0;
  }

  public async obterDespesas(){
    this.despesasPeriodoTecnico = (await this._despesaPeriodoTecnicoService.obterPorParametros({
      codTecnico: this.auditoria.usuario.codTecnico,
      inicioPeriodo: moment(this.auditoria.dataHoraRetiradaVeiculo).format('YYYY-MM-DD HH:mm:ss').toString(),
    }).toPromise()).items;
  }

  private async calcularValores(){
    this.auditoria.totalDiasEmUso = moment(this.auditoria.dataHoraCad).diff(this.auditoria.dataHoraRetiradaVeiculo,'days');
    this.auditoria.totalMesesEmUso = moment(this.auditoria.dataHoraCad).diff(this.auditoria.dataHoraRetiradaVeiculo,'months');

    for(let x = 0; x < this.despesasPeriodoTecnico.length; x++)
    {
      var despesaPeriodoTecnico = this.despesasPeriodoTecnico[x];
      for(let y = 0; y < despesaPeriodoTecnico.despesas.length; y++)
      {
        var despesa = despesaPeriodoTecnico.despesas[y];
        if(moment(despesaPeriodoTecnico.despesaPeriodo.dataInicio).isBefore(moment(this.auditoria.dataHoraCad)))
        {

          for(let z = 0; z < despesa.despesaItens.length; z++)
          {
            var items = despesa.despesaItens[z];
            if(despesaPeriodoTecnico.indCompensacao == 1 && despesaPeriodoTecnico.codDespesaPeriodoTecnicoStatus == 2
              && items.indAtivo == 1 && items.codDespesaTipo == DespesaTipoEnum.KM)
            {
              this.auditoria.kmCompensado = items.kmPercorrido + this.auditoria.kmCompensado;
              this.auditoria.despesasCompensadasValor = items.despesaValor + this.auditoria.despesasCompensadasValor;
            }
            else if(despesaPeriodoTecnico.codDespesaPeriodoTecnicoStatus == 2
              && items.indAtivo == 1 && items.codDespesaTipo == DespesaTipoEnum.KM)
            {
              this.auditoria.kmPercorrido = items.kmPercorrido + this.auditoria.kmPercorrido;
              this.auditoria.despesasSAT = items.despesaValor + this.auditoria.despesasSAT;
            }
          }
        }
      }
    }

    if(this.auditoria.kmFerias <= 3000){
      this.auditoria.kmParticular = this.auditoria.odometroPeriodoAuditado - this.auditoria.odometroInicialRetirada 
        - this.auditoria.kmPercorrido - this.auditoria.kmCompensado - this.auditoria.kmFerias;
    }
    else{
      this.auditoria.kmParticular = this.auditoria.odometroPeriodoAuditado - this.auditoria.odometroInicialRetirada 
        - this.auditoria.kmPercorrido - this.auditoria.kmCompensado - 3000;
    }
    if(this.auditoria.totalMesesEmUso < 1){
      this.auditoria.kmParticularMes = this.auditoria.kmParticular;
    }
    else{
      this.auditoria.kmParticularMes = (this.auditoria.kmParticular / this.auditoria.totalDiasEmUso) * 30;
    }

    this.auditoria.usoParticular = this.auditoria.creditosCartao - this.auditoria.despesasSAT - this.auditoria.despesasCompensadasValor 
      -this.auditoria.valorTanque - this.auditoria.saldoCartao;
  }

  salvar(){
    this.zerarValores();
    this.calcularValores();
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.auditoria,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
      }
    };

    this._auditoriaService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Auditoria atualizada com sucesso!", "success");
      this.dialogRef.close(true);
    }, e => {
      this.form.enable();
    });

    this.dialogRef.close(true);
  }

}