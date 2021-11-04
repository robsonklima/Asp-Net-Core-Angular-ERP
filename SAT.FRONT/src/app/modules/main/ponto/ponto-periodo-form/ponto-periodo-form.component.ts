import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { PontoPeriodoIntervaloAcessoDataIntervaloAcessoDataService } from 'app/core/services/ponto-periodo-intervalo-acesso-data.service';
import { PontoPeriodoModoAprovacaoService } from 'app/core/services/ponto-periodo-modo-aprovacao.service';
import { PontoPeriodoStatusService } from 'app/core/services/ponto-periodo-status.service';
import { PontoPeriodoService } from 'app/core/services/ponto-periodo.service';
import { PontoPeriodoIntervaloAcessoData } from 'app/core/types/ponto-periodo-intervalo-acesso-data.types';
import { PontoPeriodoModoAprovacao } from 'app/core/types/ponto-periodo-modo-aprovacao.types';
import { PontoPeriodoStatus, PontoPeriodoStatusParameters } from 'app/core/types/ponto-periodo-status.types';
import { PontoPeriodo } from 'app/core/types/ponto-periodo.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { first } from 'rxjs/internal/operators/first';

@Component({
  selector: 'app-ponto-periodo-form',
  templateUrl: './ponto-periodo-form.component.html'
})
export class PontoPeriodoFormComponent implements OnInit, OnDestroy {
  pontosPeriodoStatus: PontoPeriodoStatus[] =[];
  pontosIntervalo: PontoPeriodoIntervaloAcessoData[] = [];
  pontosModoAprovacao: PontoPeriodoModoAprovacao [] = [];
  codPontoPeriodo: number;
  pontoPeriodo: PontoPeriodo;
  isAddMode: boolean;
  form: FormGroup;
  userSession: UsuarioSessao;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _location: Location,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _pontoPeriodoSvc: PontoPeriodoService,
    private _pontoPeriodoStatusSvc: PontoPeriodoStatusService,
    private _pontoPeriodoModoAprovacaoSvc: PontoPeriodoModoAprovacaoService,
    private _pontoPeriodoIntervaloSvc: PontoPeriodoIntervaloAcessoDataIntervaloAcessoDataService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codPontoPeriodo = +this._route.snapshot.paramMap.get('codPontoPeriodo');
    this.isAddMode = !this.codPontoPeriodo;

    this.form = this._formBuilder.group({
      codPontoPeriodo: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      dataInicio: [undefined, Validators.required],
      dataFim: [undefined, Validators.required],
      codPontoPeriodoStatus: [undefined, Validators.required],
      codPontoPeriodoModoAprovacao: [undefined, Validators.required],
      codPontoPeriodoIntervaloAcessoData: [undefined, Validators.required]
    });

    this.obterPontosPeriodoStatus();
    this.obterPontosModoAprovacao();
    this.obterPontosIntervalo();
    
    if (!this.isAddMode) {
      this._pontoPeriodoSvc.obterPorCodigo(this.codPontoPeriodo)
        .pipe(first())
        .subscribe(data => {
          this.form.patchValue(data);
          this.pontoPeriodo = data;
        });
    }
  }

  private async obterPontosPeriodoStatus() {
    const params: PontoPeriodoStatusParameters = {
      sortActive: 'nomePeriodoStatus',
      sortDirection: 'asc'
    }

    const data = await this._pontoPeriodoStatusSvc.obterPorParametros(params).toPromise();
    this.pontosPeriodoStatus = data.items;
  }

  private async obterPontosModoAprovacao() {
    const data = await this._pontoPeriodoModoAprovacaoSvc.obterPorParametros({}).toPromise();
    this.pontosModoAprovacao = data.items;
  }

  private async obterPontosIntervalo() {
    const data = await this._pontoPeriodoIntervaloSvc.obterPorParametros({}).toPromise();
    this.pontosIntervalo = data.items;
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.pontoPeriodo,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario
      }
    };
    
    this._pontoPeriodoSvc.atualizar(obj).subscribe(() => {
      this._snack.exibirToast(`Período atualizado com sucesso!`, "success");
      this._location.back();
    });
  }

  criar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.pontoPeriodo,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario
      }
    };

    this._pontoPeriodoSvc.criar(obj).subscribe(() => {
      this._snack.exibirToast(`Período adicionado com sucesso!`, "success");
      this._location.back();
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
