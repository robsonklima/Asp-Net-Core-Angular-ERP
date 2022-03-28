import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DespesaAdiantamentoService } from 'app/core/services/despesa-adiantamento.service';
import { FilialService } from 'app/core/services/filial.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { ViewMediaDespesasAdiantamento } from 'app/core/types/despesa-adiantamento.types';
import { Filial } from 'app/core/types/filial.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-despesa-adiantamento-solicitacao',
  templateUrl: './despesa-adiantamento-solicitacao.component.html',
  styles: [`tr:nth-child(odd) { background-color: rgb(239,245,254); }`]
})
export class DespesaAdiantamentoSolicitacaoComponent implements OnInit {
  public form: FormGroup;
  public tecnico: Tecnico;
  public tecnicos: Tecnico[] = [];
  public filiais: Filial[] = [];
  public tecnicosFiltro: FormControl = new FormControl();
  public filiaisFiltro: FormControl = new FormControl();
  public userSession: UserSession;
  public loading: boolean;
  public mediaAdiantamentos: ViewMediaDespesasAdiantamento;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _userService: UserService,
    private _tecnicoService: TecnicoService,
    private _filialService: FilialService,
    private _despesaAdiantamentoService: DespesaAdiantamentoService,
    private _snack: CustomSnackbarService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.inicializarForm();
    this.registrarEmitters();
    this.obterFiliais();
  }

  private inicializarForm()
  {
    this.form = this._formBuilder.group({
      codTecnico: ['', Validators.required],
      codFilial: ['', Validators.required],
      saldoLogix: ['', Validators.required],
      valorAdiantamentoSolicitado: ['', Validators.required],
      emails: ['', Validators.required],
      justificativa: [''],
    });
  }

  private registrarEmitters() {
    this.tecnicosFiltro.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe(async (filtro) =>
      {
        this.obterTecnicos(filtro);
      });

    this.filiaisFiltro.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe(async (filtro) =>
      {
        this.obterFiliais(filtro);
      });

      this.form.controls['codFilial'].valueChanges.subscribe(() =>
      {
        this.obterTecnicos();
      });

    this.form.controls['saldoLogix'].valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe(async (saldoLogix) =>
      {
        this.calcularSaldo(+saldoLogix);
      });

    this.form.controls['codTecnico'].valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        distinctUntilChanged()
      )
      .subscribe(async (codTecnico) =>
      {
        this.loading = true;
        this.tecnico = await this._tecnicoService.obterPorCodigo(codTecnico).toPromise();
        this.mediaAdiantamentos = await (await this._despesaAdiantamentoService.obterMedia(this.form.controls['codTecnico'].value).toPromise()).shift();
        this.loading = false;
      });
  }

  private calcularSaldo(saldoLogix: number)
  {
    this.mediaAdiantamentos.saldoAbertoLogixMensal = saldoLogix;
    this.mediaAdiantamentos.saldoAbertoLogixQuinzenal = saldoLogix / 2;
    this.mediaAdiantamentos.saldoAbertoLogixSemanal = saldoLogix / 4;

    this.mediaAdiantamentos.saldoAdiantamentoSatmensal = this.mediaAdiantamentos.saldoAbertoLogixMensal - this.mediaAdiantamentos.rdsEmAbertoMensal;
    this.mediaAdiantamentos.saldoAdiantamentoSatquinzenal = this.mediaAdiantamentos.saldoAbertoLogixQuinzenal - this.mediaAdiantamentos.rdsEmAbertoQuinzenal;
    this.mediaAdiantamentos.saldoAdiantamentoSatsemanal = this.mediaAdiantamentos.saldoAbertoLogixSemanal - this.mediaAdiantamentos.rdsEmAbertoSemanal;

    this.mediaAdiantamentos.maximoParaSolicitarMensal = this.mediaAdiantamentos.mediaMensal - (this.mediaAdiantamentos.saldoAdiantamentoSatmensal < 0 ? 0 : this.mediaAdiantamentos.saldoAdiantamentoSatmensal);
    this.mediaAdiantamentos.maximoParaSolicitarSemanal = this.mediaAdiantamentos.mediaSemanal - (this.mediaAdiantamentos.saldoAdiantamentoSatquinzenal < 0 ? 0 : this.mediaAdiantamentos.saldoAdiantamentoSatquinzenal);
    this.mediaAdiantamentos.maximoParaSolicitarQuinzenal = this.mediaAdiantamentos.mediaQuinzenal - (this.mediaAdiantamentos.saldoAdiantamentoSatsemanal < 0 ? 0 : this.mediaAdiantamentos.saldoAdiantamentoSatsemanal);

    this.mediaAdiantamentos.maximoParaSolicitarMensal = this.mediaAdiantamentos.maximoParaSolicitarMensal < 0 ? 0 : this.mediaAdiantamentos.maximoParaSolicitarMensal;
    this.mediaAdiantamentos.maximoParaSolicitarQuinzenal = this.mediaAdiantamentos.maximoParaSolicitarQuinzenal < 0 ? 0 : this.mediaAdiantamentos.maximoParaSolicitarQuinzenal;
    this.mediaAdiantamentos.maximoParaSolicitarSemanal = this.mediaAdiantamentos.maximoParaSolicitarSemanal < 0 ? 0 : this.mediaAdiantamentos.maximoParaSolicitarSemanal;

    this.form.controls['valorAdiantamentoSolicitado'].setValue(this.mediaAdiantamentos.maximoParaSolicitarMensal);
    this.form.controls['emails'].setValue(this.mediaAdiantamentos.emailDefault);
  }

  private async obterFiliais(filtro: string='') {
    const data = await this._filialService.obterPorParametros({
      indAtivo: 1,
      sortActive: 'NomeFilial',
      sortDirection: 'asc',
      filter: filtro
    }).toPromise();
    
    this.filiais = data.items;
  }

  private async obterTecnicos(filtro: string='') {
    this.loading = true;

    const data = await this._tecnicoService.obterPorParametros({
      indAtivo: 1,
      sortActive: 'Nome',
      sortDirection: 'asc',
      filter: filtro,
      codFiliais: this.form.controls['codFilial'].value
    }).toPromise();

    this.tecnicos = data.items;
    this.loading = false;
  }

  public verificarJustificativaObrigatoria(): boolean {
    return +this.form.controls['valorAdiantamentoSolicitado'].value > this.mediaAdiantamentos?.maximoParaSolicitarMensal;
  }

  public salvar() {
    if (this.verificarJustificativaObrigatoria() && !this.form.controls['justificativa'].value)
      return this._snack.exibirToast('Favor preencher a justificativa', 'error');

    const conta = this.tecnico.tecnicoContas?.filter(c => c.indAtivo).shift();

    if (!conta)
      return this._snack.exibirToast('Conta do técnico não encontrada', 'error');

    this.form.disable();
    const form = this.form.getRawValue();
    const saldoAbertoLogixMensal = form.saldoLogix;
    const saldoAbertoLogixQuinzenal = form.saldoLogix / 2;
    const saldoAbertoLogixSemanal = form.saldoLogix / 4;

    const solicitacao = {
      ...form,
      ...this.tecnico,
      ...this.mediaAdiantamentos,
      ...{
        saldoAbertoLogixMensal: saldoAbertoLogixMensal,
        saldoAbertoLogixQuinzenal: saldoAbertoLogixQuinzenal,
        saldoAbertoLogixSemanal: saldoAbertoLogixSemanal,
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario?.codUsuario,
        banco: conta.numBanco,
        agencia: conta.numAgencia,
        contaCorrente: conta.numConta,
      },
    }

    this._despesaAdiantamentoService.criarSolicitacao(solicitacao).subscribe(() => {
      this._snack.exibirToast('Solicitação de adiantamento cadastrada com sucesso', 'success');

    }, e => {
      this._snack.exibirToast(e.message || e.error.message, 'error')
    });
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}