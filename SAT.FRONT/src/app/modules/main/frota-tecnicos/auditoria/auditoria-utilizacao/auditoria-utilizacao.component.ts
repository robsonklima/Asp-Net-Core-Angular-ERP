import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { Location } from '@angular/common';
import { Auditoria } from 'app/core/types/auditoria.types';
import { AuditoriaService } from 'app/core/services/auditoria.service';
import { AuditoriaVeiculoTanque } from 'app/core/types/auditoria-veiculo-tanque.types';
import { AuditoriaVeiculoTanqueService } from 'app/core/services/auditoria-veiculo-tanque.service';
import { DespesaConfiguracaoCombustivel, DespesaConfiguracaoCombustivelParameters } from 'app/core/types/despesa-configuracao-combustivel.types';
import { DespesaConfiguracaoCombustivelService } from 'app/core/services/despesa-configuracao-combustivel.service';
import { DespesaPeriodoTecnico } from 'app/core/types/despesa-periodo.types';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { AuditoriaUtilizacaoDialogComponent } from './auditoria-utilizacao-dialog/auditoria-utilizacao-dialog.component';
import { MatDialog } from '@angular/material/dialog';


@Component({
  selector: 'app-auditoria-utilizacao',
  templateUrl: './auditoria-utilizacao.component.html',
})
export class AuditoriaUtilizacaoComponent implements OnInit {
  codAuditoria: number;
  codAuditoriaVeiculo: number;
  auditoria: Auditoria;
  form: FormGroup;
  userSession: UsuarioSessao;
  qtdLitros: number;
  tanques: AuditoriaVeiculoTanque[] = [];
  despesasPeriodoTecnico: DespesaPeriodoTecnico[] = [];
  configuracaoCombustivel: DespesaConfiguracaoCombustivel;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _auditoriaService: AuditoriaService,
    private _snack: CustomSnackbarService,
    private _despesaConfiguracaoCombustivelService: DespesaConfiguracaoCombustivelService,
    private _despesaPeriodoTecnicoService: DespesaPeriodoTecnicoService,
    private _auditoriaVeiculoTanqueService: AuditoriaVeiculoTanqueService,
    private _dialog: MatDialog,
    private _location: Location,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codAuditoria = +this._route.snapshot.paramMap.get('codAuditoria');
    this.codAuditoriaVeiculo = +this._route.snapshot.paramMap.get('codAuditoriaVeiculo');
    this.inicializarForm();
    this.registrarEmitters();
    this.obterAuditoria();


  }

  obterAuditoria(){
    this._auditoriaService.obterPorCodigo(this.codAuditoria)
      .pipe(first())
      .subscribe(data => {
        this.form.patchValue(data);
        this.auditoria = data;
        this.obterTanques();
        this.obterValoresCombustivel();
      });
  }

  editarAuditoria(){    
    const dialogRef = this._dialog.open(AuditoriaUtilizacaoDialogComponent, {
    data:
    {
      codAuditoria: this.auditoria.codAuditoria,
    }
  });

  dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
    if (confirmacao)
      this.obterAuditoria();
  });}

  private async obterTanques() {
    const data = await this._auditoriaVeiculoTanqueService.obterPorParametros({}).toPromise();
    this.tanques = data.items;
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codAuditoria: [undefined],
      codAuditoriaVeiculoTanque: [undefined],
      dataHoraRetiradaVeiculo: [undefined],
      dataHoraCad: [undefined],
      totalDiasEmUso: [undefined],
      totalMesesEmUso: [undefined],
      odometroInicialRetirada: [undefined],
      odometroPeriodoAuditado: [undefined],
      despesasSAT: [undefined],
      kmFerias: [undefined],
      creditosCartao: [undefined],
      saldoCartao: [undefined],
      observacoes: [undefined],
      qtdLitros: [undefined],
      valorTanque: [undefined],
      kmPercorrido: [undefined],
      kmCompensado: [undefined],
      despesasCompensadasValor: [undefined],
      kmParticular: [undefined],
      kmParticularMes: [undefined],
      usoParticular: [undefined],

    });
  }

  private registrarEmitters() {
    this.form.controls["codAuditoriaVeiculoTanque"].valueChanges.subscribe(codAuditoriaVeiculoTanque => {
      this.atualizarValoresTanque(codAuditoriaVeiculoTanque);
    });
    
    this.form.controls["dataHoraRetiradaVeiculo"].valueChanges.subscribe(async dataHoraRetiradaVeiculo => {
      this.despesasPeriodoTecnico = (await this._despesaPeriodoTecnicoService.obterPorParametros({
        codTecnico: this.auditoria.usuario.codTecnico,
        inicioPeriodo: dataHoraRetiradaVeiculo.format('YYYY-MM-DD HH:mm:ss'),
      }).toPromise()).items;

      this.atualizarValoresDespesas(dataHoraRetiradaVeiculo);
    });

  }
 
  private async obterValoresCombustivel() {
    const params: DespesaConfiguracaoCombustivelParameters = {
      codFilial: this.auditoria?.usuario?.tecnico?.filial?.codFilial,
      codUf: this.auditoria?.usuario?.tecnico?.cidade?.codUF,
    }
    const data = await this._despesaConfiguracaoCombustivelService.obterPorParametros(params).toPromise();
    this.configuracaoCombustivel = data.items.shift();
  }
  
  private async atualizarValoresDespesas(dataHoraRetiradaVeiculo){
    this.auditoria.kmPercorrido = 0;
    this.auditoria.kmCompensado = 0;
    this.auditoria.despesasSAT = 0;
    this.auditoria.despesasCompensadasValor = 0;
    this.auditoria.totalDiasEmUso = moment(this.auditoria.dataHoraCad).diff(dataHoraRetiradaVeiculo,'days');
    this.auditoria.totalMesesEmUso = moment(this.auditoria.dataHoraCad).diff(dataHoraRetiradaVeiculo,'months');

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
            if(despesaPeriodoTecnico.indCompensacao == 1)
            {
              this.auditoria.kmCompensado = items.kmPercorrido + this.auditoria?.kmCompensado;
              this.auditoria.despesasCompensadasValor = items.despesaValor + this.auditoria?.despesasCompensadasValor;
            }
            else
            {
              this.auditoria.kmPercorrido = items.kmPercorrido + this.auditoria?.kmPercorrido;
              this.auditoria.despesasSAT = items.despesaValor + this.auditoria?.despesasSAT;
            }
          }
        }
      }
    }
  }

  private async atualizarValoresTanque(codAuditoriaVeiculoTanque) {
    const veiculoTanque = await this._auditoriaVeiculoTanqueService
      .obterPorCodigo(codAuditoriaVeiculoTanque)
      .toPromise();

    if (!veiculoTanque) 
      return;

    this.qtdLitros = veiculoTanque.qtdLitros;
    this.auditoria.auditoriaVeiculo.auditoriaVeiculoTanque.qtdLitros = this.qtdLitros;
    this.auditoria.valorTanque = this.qtdLitros * this.configuracaoCombustivel.precoLitro;
    this.auditoria.usoParticular = this.auditoria.creditosCartao - this.auditoria.despesasSAT - this.auditoria.despesasCompensadasValor -this.auditoria.valorTanque - this.auditoria.saldoCartao;
    this.atualizarValoresKMParticular();
  }

  private atualizarValoresKMParticular(){
    if(this.auditoria.kmFerias <= 3000){
      console.log("menos 3000");
      this.auditoria.kmParticular = this.auditoria.odometroPeriodoAuditado - this.auditoria.odometroInicialRetirada - this.auditoria.kmPercorrido - this.auditoria.kmCompensado - this.auditoria.kmFerias;
    }
    else{
      this.auditoria.kmParticular = this.auditoria.odometroPeriodoAuditado - this.auditoria.odometroInicialRetirada - this.auditoria.kmPercorrido - this.auditoria.kmCompensado - 3000;
    }
    if(this.auditoria.totalMesesEmUso < 1){
      this.auditoria.kmParticularMes = this.auditoria.kmParticular;
    }
    else{
      this.auditoria.kmParticularMes = this.auditoria.kmParticular / this.auditoria.totalMesesEmUso;
    }

  }

  salvar(): void {
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
      this._location.back();
    }, e => {
      this.form.enable();
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}