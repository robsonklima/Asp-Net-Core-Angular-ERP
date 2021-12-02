import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatCheckbox } from '@angular/material/checkbox';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DespesaAdiantamentoTipoService } from 'app/core/services/despesa-adiantamento-tipo.service';
import { DespesaAdiantamentoService } from 'app/core/services/despesa-adiantamento.service';
import { DespesaConfiguracaoService } from 'app/core/services/despesa-configuracao.service';
import { DespesaPeriodoService } from 'app/core/services/despesa-periodo.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { DespesaAdiantamento, DespesaAdiantamentoTipo } from 'app/core/types/despesa-adiantamento.types';
import { DespesaPeriodo } from 'app/core/types/despesa-periodo.types';
import { DespesaConfiguracao } from 'app/core/types/despesa.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-despesa-adiantamento-form',
  templateUrl: './despesa-adiantamento-form.component.html'
})

export class DespesaAdiantamentoFormComponent implements OnInit, OnDestroy
{
  codDespesaAdiantamento: number;
  despesaAdiantamento: DespesaAdiantamento;
  despesaAdiantamentoTipo: DespesaAdiantamentoTipo[] = [];
  tecnico: Tecnico[] = [];
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
    private _despesaAdiantamentoSvc: DespesaAdiantamentoService,
    private _despesaAdiantamentoTipoSvc: DespesaAdiantamentoTipoService,
    private _tecnicoSvc: TecnicoService,
    private _snack: CustomSnackbarService,
  )
  {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit()
  {
    this.codDespesaAdiantamento = +this._route.snapshot.paramMap.get('codDespesaAdiantamento');
    this.isAddMode = !this.codDespesaAdiantamento;

    this.inicializarForm();
    this.loadData();
  }

  private inicializarForm(): void
  {
    this.form = this._formBuilder.group({
      codDespesaAdiantamento: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      codTecnico: [
        {
          value: undefined,
        }, [Validators.required]
      ],
      codDespesaAdiantamentoTipo: [
        {
          value: undefined,
        }, [Validators.required]
      ],
      dataAdiantamento: [
        {
          value: undefined,
        }, [Validators.required]
      ],
      valorAdiantamento: [
        {
          value: undefined,
        }, [Validators.required]
      ],
      indAtivo: [
        {
          value: undefined,
        }, [Validators.required]
      ],
    });
  }

  async loadData()
  {
    await this.obterDespesaAdiantamentoTipo();
    await this.obterDespesaAdiantamento();
    await this.obterTecnicos();
  }

  async obterDespesaAdiantamento()
  {
    if (this.isAddMode) return;

    this.despesaAdiantamento =
      (await this._despesaAdiantamentoSvc.obterPorCodigo(this.codDespesaAdiantamento).toPromise());

    this.form.patchValue(this.despesaAdiantamento);
  }

  async obterDespesaAdiantamentoTipo()
  {
    this.despesaAdiantamentoTipo = (await this._despesaAdiantamentoTipoSvc
      .obterPorParametros({}).toPromise()).items;
  }

  async obterTecnicos()
  {
    this.tecnico = (await this._tecnicoSvc
      .obterPorParametros({
        indAtivo: 1,
        sortActive: 'nome',
        sortDirection: 'asc'
      }).toPromise()).items;
  }

  salvar(): void
  {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private atualizar()
  {
    let despesa: DespesaAdiantamento = {
      ...this.despesaAdiantamento,
      ...this.form.getRawValue(),
      ...{
        dataAdiantamento: moment(this.form.controls.dataAdiantamento.value).format('yyyy-MM-DD HH:mm:ss'),
        indAtivo: this._formIndAtivo.checked ? 1 : 0,
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario?.codUsuario
      }
    };

    this._despesaAdiantamentoSvc.atualizar(despesa).toPromise()
      .then(() =>
      {
        this._snack.exibirToast("Adiantamento atualizado com sucesso!", "success");
        this._router.navigate(['/despesa/adiantamentos']);
      }).catch(() =>
      {
        this._snack.exibirToast("Erro ao atualizar adiantamento.", "error");
        this._router.navigate(['/despesa/adiantamentos']);
      });
  }

  private async criar()
  {
    let despesa: DespesaAdiantamento = {
      ...this.form.getRawValue(),
      ...{
        dataAdiantamento: moment(this.form.controls.dataAdiantamento.value).format('yyyy-MM-DD HH:mm:ss'),
        indAtivo: this._formIndAtivo.checked ? 1 : 0,
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario?.codUsuario
      }
    };

    this._despesaAdiantamentoSvc.criar(despesa).toPromise()
      .then(() =>
      {
        this._snack.exibirToast("Adiantamento criado com sucesso!", "success");
        this._router.navigate(['/despesa/adiantamentos']);
      }).catch(() =>
      {
        this._snack.exibirToast("Erro ao criar adiantamento.", "error");
        this._router.navigate(['/despesa/adiantamentos']);
      });
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}