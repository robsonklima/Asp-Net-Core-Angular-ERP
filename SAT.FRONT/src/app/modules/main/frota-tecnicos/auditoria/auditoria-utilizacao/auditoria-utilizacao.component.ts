import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Usuario, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { Location } from '@angular/common';
import { Auditoria } from 'app/core/types/auditoria.types';
import { AuditoriaService } from 'app/core/services/auditoria.service';
import { AuditoriaVeiculo } from 'app/core/types/auditoria-veiculo.types';
import { UsuarioService } from 'app/core/services/usuario.service';
import { AuditoriaVeiculoService } from 'app/core/services/auditoria-veiculo.service';
import { AuditoriaVeiculoTanque, AuditoriaVeiculoTanqueParameters } from 'app/core/types/auditoria-veiculo-tanque.types';
import { AuditoriaVeiculoTanqueService } from 'app/core/services/auditoria-veiculo-tanque.service';
import { DespesaCartaoCombustivelService } from 'app/core/services/despesa-cartao-combustivel.service';
import { DespesaConfiguracaoCombustivel, DespesaConfiguracaoCombustivelParameters } from 'app/core/types/despesa-configuracao-combustivel.types';
import { DespesaConfiguracaoCombustivelService } from 'app/core/services/despesa-configuracao-combustivel.service';

@Component({
  selector: 'app-auditoria-utilizacao',
  templateUrl: './auditoria-utilizacao.component.html',
})
export class AuditoriaUtilizacaoComponent implements OnInit {
  @ViewChild('searchSelectControl') searchSelectControl: ElementRef;

  codAuditoria: number;
  codAuditoriaVeiculo: number;
  auditoria: Auditoria;
  auditoriaVeiculo: AuditoriaVeiculo;
  form: FormGroup;
  isAddMode: boolean;
  isLoading: boolean;
  userSession: UsuarioSessao;
  usuarios: Usuario[] = [];
  auditoriaVeiculos: AuditoriaVeiculo[] = [];
  searching: boolean;
  veiculos: AuditoriaVeiculo[] = [];
  qtdLitros: number;
  tanques: AuditoriaVeiculoTanque[] = [];
  tanque: AuditoriaVeiculoTanque;
  configuracaoCombustivel: DespesaConfiguracaoCombustivel;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _router: Router,
    private _userService: UserService,
    private _auditoriaService: AuditoriaService,
    private _usuarioService: UsuarioService,
    private _snack: CustomSnackbarService,
    private _auditoriaVeiculoService: AuditoriaVeiculoService,
    private _despesaConfiguracaoCombustivelService: DespesaConfiguracaoCombustivelService,
    private _auditoriaVeiculoTanqueService: AuditoriaVeiculoTanqueService,
    private _location: Location,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codAuditoria = +this._route.snapshot.paramMap.get('codAuditoria');
    this.codAuditoriaVeiculo = +this._route.snapshot.paramMap.get('codAuditoriaVeiculo');
    this.inicializarForm();
    this.registrarEmitters();

    this._auditoriaService.obterPorCodigo(this.codAuditoria)
      .pipe(first())
      .subscribe(data => {
        console.log(data);

        this.form.patchValue(data);
        this.auditoria = data;
        this.obterTanques();
        this.obterValoresCombustivel();
      });

  }



  private async obterTanques() {
    const params: AuditoriaVeiculoTanqueParameters = {
      codAuditoriaVeiculoTanque: this.auditoria?.auditoriaVeiculo?.auditoriaVeiculoTanque?.codAuditoriaVeiculoTanque,
    }
    console.log(this.auditoria?.auditoriaVeiculo?.auditoriaVeiculoTanque?.codAuditoriaVeiculoTanque);
    const data = await this._auditoriaVeiculoTanqueService.obterPorParametros(params).toPromise();
    this.tanques = data.items;
    this.tanque = data.items.shift();
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group
      ({
        codAuditoria: [undefined],
        codAuditoriaVeiculoTanque: [undefined],
        dataHoraRetiradaVeiculo: [undefined],
        dataHoraCad: [undefined],
        totalDiasEmUso: [undefined],
        odometroInicialRetirada: [undefined],
        odometroPeriodoAuditado: [undefined],
        despesasSAT: [undefined],
        kmFerias: [undefined],
        creditosCartao: [undefined],
        saldoCartao: [undefined],
        observacoes: [undefined],
        qtdLitros: [undefined],
        valorTanque: [undefined],

      });
  }

  private registrarEmitters() {
    this.form.controls["codAuditoriaVeiculoTanque"].valueChanges.subscribe(a => {
      this.obterTanques();
      this.atualizarValores();
    });
  }

  private async obterValoresCombustivel() {
    const params: DespesaConfiguracaoCombustivelParameters = {
      codFilial : this.auditoria?.usuario?.tecnico?.filial?.codFilial,
      codUf: this.auditoria?.usuario?.tecnico?.cidade?.codUF,
    }
    const data = await this._despesaConfiguracaoCombustivelService.obterPorParametros(params).toPromise();
    this.configuracaoCombustivel = data.items.shift();

  }

  private atualizarValores() {
    this.auditoria.auditoriaVeiculo.auditoriaVeiculoTanque.qtdLitros = this.tanque.qtdLitros;
    this.auditoria.valorTanque = this.auditoria.auditoriaVeiculo.auditoriaVeiculoTanque.qtdLitros * this.configuracaoCombustivel.precoLitro;
  }

  salvar(): void {
    this.form.disable();
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private atualizar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.auditoria,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
      }
    };

    this._usuarioService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Usuario atualizada com sucesso!", "success");
      this._location.back();
    }, e => {
      this.form.enable();
    });
  }

  private criar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.auditoria,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
      }
    };

    this._auditoriaService.criar(obj).subscribe(() => {
      this._snack.exibirToast("Auditoria inserida com sucesso!", "success");
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