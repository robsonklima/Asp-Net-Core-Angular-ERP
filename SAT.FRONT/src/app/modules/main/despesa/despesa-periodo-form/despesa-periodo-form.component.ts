import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatCheckbox } from '@angular/material/checkbox';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DespesaConfiguracaoService } from 'app/core/services/despesa-configuracao.service';
import { DespesaPeriodoService } from 'app/core/services/despesa-periodo.service';
import { DespesaPeriodo } from 'app/core/types/despesa-periodo.types';
import { DespesaConfiguracao } from 'app/core/types/despesa.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-despesa-periodo-form',
  templateUrl: './despesa-periodo-form.component.html'
})
export class DespesaPeriodoFormComponent implements OnInit, OnDestroy
{
  codDespesaPeriodo: number;
  despesaPeriodo: DespesaPeriodo;
  despesaConfiguracao: DespesaConfiguracao[] = [];
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
    private _despesaPeriodoService: DespesaPeriodoService,
    private _despesaConfiguracaoService: DespesaConfiguracaoService,
    private _snack: CustomSnackbarService,
  )
  {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit()
  {
    this.codDespesaPeriodo = +this._route.snapshot.paramMap.get('codDespesaPeriodo');
    this.isAddMode = !this.codDespesaPeriodo;

    this.inicializarForm();
    this.loadData();
  }

  private inicializarForm(): void
  {
    this.form = this._formBuilder.group({
      codDespesaPeriodo: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      codDespesaConfiguracao: [
        {
          value: undefined,
        }, [Validators.required]
      ],
      dataInicio: [
        {
          value: undefined,
        }, [Validators.required]
      ],
      dataFim: [
        {
          value: undefined,
        }, [Validators.required]
      ],
      indAtivo: [
        {
          value: undefined,
        }, [Validators.required]
      ]
    });
  }

  async loadData() 
  {
    await this.obterDespesasConfiguracao();
    await this.obterDespesaPeriodo();
  }

  async obterDespesaPeriodo()
  {
    if (this.isAddMode) return;

    this.despesaPeriodo =
      (await this._despesaPeriodoService.obterPorCodigo(this.codDespesaPeriodo).toPromise());

    this.form.patchValue(this.despesaPeriodo);
  }

  async obterDespesasConfiguracao()
  {
    this.despesaConfiguracao = (await this._despesaConfiguracaoService
      .obterPorParametros({
        indAtivo: 1
      }).toPromise()).items;
  }

  salvar(): void
  {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private atualizar()
  {
    let despesa: DespesaPeriodo = {
      ...this.despesaPeriodo,
      ...this.form.getRawValue(),
      ...{
        dataFim: moment(this.form.controls.dataFim.value).format('yyyy-MM-DD HH:mm:ss'),
        dataInicio: moment(this.form.controls.dataInicio.value).format('yyyy-MM-DD HH:mm:ss'),
        indAtivo: this._formIndAtivo.checked ? 1 : 0,
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario?.codUsuario
      }
    };

    this._despesaPeriodoService.atualizar(despesa).toPromise()
      .then(() =>
      {
        this._snack.exibirToast("Período atualizado com sucesso!", "success");
        this._router.navigate(['/despesa/periodos']);
      }).catch(() =>
      {
        this._snack.exibirToast("Erro ao atualizar período.", "error");
        this._router.navigate(['/despesa/periodos']);
      });
  }

  private async criar()
  {
    let despesa: DespesaPeriodo = {
      ...this.form.getRawValue(),
      ...{
        dataFim: moment(this.form.controls.dataFim.value).format('yyyy-MM-DD HH:mm:ss'),
        dataInicio: moment(this.form.controls.dataInicio.value).format('yyyy-MM-DD HH:mm:ss'),
        indAtivo: this._formIndAtivo.checked ? 1 : 0,
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario?.codUsuario
      }
    };

    this._despesaPeriodoService.criar(despesa).toPromise()
      .then(() =>
      {
        this._snack.exibirToast("Período criado com sucesso!", "success");
        this._router.navigate(['/despesa/periodos']);
      }).catch(() =>
      {
        this._snack.exibirToast("Erro ao criar período.", "error");
        this._router.navigate(['/despesa/periodos']);
      });
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}