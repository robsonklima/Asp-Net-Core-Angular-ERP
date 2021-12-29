import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatCheckbox } from '@angular/material/checkbox';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DespesaCartaoCombustivelService } from 'app/core/services/despesa-cartao-combustivel.service';
import { DespesaConfiguracaoService } from 'app/core/services/despesa-configuracao.service';
import { DespesaCartaoCombustivel } from 'app/core/types/despesa-cartao-combustivel.types';
import { DespesaConfiguracao } from 'app/core/types/despesa.types';
import { statusConst } from 'app/core/types/status-types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-despesa-configuracao-form',
  templateUrl: './despesa-configuracao-form.component.html'
})
export class DespesaConfiguracaoFormComponent implements OnInit, OnDestroy
{
  codDespesaConfiguracao: number;
  despesaConfiguracao: DespesaConfiguracao;
  form: FormGroup;
  isAddMode: boolean;
  userSession: UsuarioSessao;
  protected _onDestroy = new Subject<void>();
  @ViewChild('indAtivo') private _formIndAtivo: MatCheckbox;

  constructor (
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _router: Router,
    private _userService: UserService,
    private _despesaConfiguracaoSvc: DespesaConfiguracaoService,
    private _snack: CustomSnackbarService,
  )
  {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit()
  {
    this.codDespesaConfiguracao = +this._route.snapshot.paramMap.get('codDespesaConfiguracao');
    this.isAddMode = !this.codDespesaConfiguracao;

    this.inicializarForm();
    this.loadData();
  }

  private inicializarForm(): void
  {
    this.form = this._formBuilder.group({
      codDespesaConfiguracao: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      percentualKmForaCidade: [undefined, Validators.required],
      percentualKmCidade: [undefined, Validators.required],
      valorRefeicaoLimiteTecnico: [undefined, Validators.required],
      valorRefeicaoLimiteOutros: [undefined, Validators.required],
      horaExtraInicioAlmoco: [undefined, Validators.required],
      horaExtraInicioJanta: [undefined, Validators.required],
      percentualNotaKM: [undefined, Validators.required],
      valorKM: [undefined, Validators.required],
      valorAluguelCarro: [undefined],
      dataVigencia: [undefined, Validators.required],
      indAtivo: [true, Validators.required],
    });
  }

  async loadData()
  {
    await this.obterCartaoCombustivel();
  }

  async obterCartaoCombustivel()
  {
    if (this.isAddMode) return;

    this.despesaConfiguracao =
      (await this._despesaConfiguracaoSvc.obterPorCodigo(this.codDespesaConfiguracao).toPromise());

    this.despesaConfiguracao.horaExtraInicioAlmoco =
      moment(this.despesaConfiguracao.horaExtraInicioAlmoco)
        .format("HH:mm");

    this.despesaConfiguracao.horaExtraInicioJanta =
      moment(this.despesaConfiguracao.horaExtraInicioJanta)
        .format("HH:mm");

    this.form.patchValue(this.despesaConfiguracao);
  }

  salvar(): void
  {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private atualizar()
  {
    let configuracao: DespesaConfiguracao = {
      ...this.despesaConfiguracao,
      ...this.form.getRawValue(),
      ...{
        indAtivo: this._formIndAtivo.checked ? statusConst.ATIVO : statusConst.INATIVO,
        dataVigencia: moment(this.form.controls.dataVigencia.value).format('yyyy-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario?.codUsuario,
        dataHoraManut: moment().format('yyyy-MM-DD HH:mm:ss')
      }
    };

    this._despesaConfiguracaoSvc.atualizar(configuracao).toPromise()
      .then(() =>
      {
        this._snack.exibirToast("Configuração atualizada com sucesso!", "success");
        this._router.navigate(['/despesa/configuracoes']);
      }).catch(() =>
      {
        this._snack.exibirToast("Erro ao atualizar configuração.", "error");
        this._router.navigate(['/despesa/configuracoes']);
      });
  }

  private async criar()
  {
    let configuracao: DespesaConfiguracao = {
      ...this.form.getRawValue(),
      ...{
        indAtivo: this._formIndAtivo.checked ? statusConst.ATIVO : statusConst.INATIVO,
        dataVigencia: moment(this.form.controls.dataVigencia.value).format('yyyy-MM-DD HH:mm:ss'),
        dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario?.codUsuario
      }
    };

    this._despesaConfiguracaoSvc.criar(configuracao).toPromise()
      .then(() =>
      {
        this._snack.exibirToast("Configuração criado com sucesso!", "success");
        this._router.navigate(['/despesa/configuracoes']);
      }).catch(() =>
      {
        this._snack.exibirToast("Erro ao criar configuração.", "error");
        this._router.navigate(['/despesa/configuracoes']);
      });
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}