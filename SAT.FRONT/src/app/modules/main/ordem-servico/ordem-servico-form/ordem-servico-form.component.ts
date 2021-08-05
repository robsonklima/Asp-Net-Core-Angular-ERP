import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ClienteService } from 'app/core/services/cliente.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TipoIntervencaoService } from 'app/core/services/tipo-intervencao.service';
import { Cliente, ClienteData } from 'app/core/types/cliente.types';
import { LocalAtendimento, LocalAtendimentoData } from 'app/core/types/local-atendimento.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { TipoIntervencao, TipoIntervencaoData } from 'app/core/types/tipo-intervencao.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { ReplaySubject, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, first, takeUntil } from 'rxjs/operators';


@Component({
  selector: 'app-ordem-servico-form',
  templateUrl: './ordem-servico-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class OrdemServicoFormComponent implements OnInit, OnDestroy {
  codOS: number;
  ordemServico: OrdemServico;
  form: FormGroup;
  isAddMode: boolean;
  usuario: any;
  
  public clienteFilterCtrl: FormControl = new FormControl();
  public clientes: ReplaySubject<Cliente[]> = new ReplaySubject<Cliente[]>(1);
  public tipoIntervencaoFilterCtrl: FormControl = new FormControl();
  public tiposIntervencao: ReplaySubject<TipoIntervencao[]> = new ReplaySubject<TipoIntervencao[]>(1);
  public localAtendimentoFilterCtrl: FormControl = new FormControl();
  public locaisAtendimento: ReplaySubject<LocalAtendimento[]> = new ReplaySubject<LocalAtendimento[]>(1);

  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _ordemServicoService: OrdemServicoService,
    private _userService: UserService,
    private _tipoIntervencaoService: TipoIntervencaoService,
    private _localAtendimentoService: LocalAtendimentoService,
    private _snack: CustomSnackbarService,
    private _localtion: Location,
    private _clienteService: ClienteService
  ) {
    this.usuario = this._userService.get();
  }

  ngOnInit(): void {
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    this.isAddMode = !this.codOS;

    this.obterClientes();
    this.obterTiposIntervencao();
    
    this.clienteFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterClientes(this.clienteFilterCtrl.value);
      });

    this.tipoIntervencaoFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterTiposIntervencao(this.tipoIntervencaoFilterCtrl.value);
      });

    this.localAtendimentoFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterLocaisAtendimento(this.localAtendimentoFilterCtrl.value);
      });

    this.form = this._formBuilder.group({
      codOS: [
        {
          value: '',
          disabled: true,
        }, [Validators.required]
      ],
      numOSCliente: [''],
      numOSQuarteirizada: [''],
      nomeSolicitante: [''],
      nomeContato: [''],
      telefoneSolicitante: [''],
      codCliente: ['', Validators.required],
      codTipoIntervencao: ['', Validators.required],
      codPosto: ['', Validators.required],
      defeitoRelatado: ['', Validators.required],
      indLiberacaoFechaduraCofre: [''],
      indObservacao: [''],
      indIntegracao: [''],
      indMarcaEspecial: [''],
      observacaoCliente: [''],
      descMotivoMarcaEspecial: [''],
      observacao: [''],
      codEquipContrato: [''],
      codEquip: [''],
      codFilial: [
        {
          value: '',
          disabled: true
        }, [Validators.required]
      ],
      codRegiao: ['', Validators.required],
      codAutorizada: ['', Validators.required],
    });

    if (!this.isAddMode) {
      this._ordemServicoService.obterPorCodigo(this.codOS)
      .pipe(first())
      .subscribe(res => {
        this.ordemServico = res;
        this.form.patchValue(this.ordemServico);
        this.obterClientes(this.ordemServico.cliente.nomeFantasia);
        this.obterTiposIntervencao(this.ordemServico.tipoIntervencao.nomTipoIntervencao);
        this.obterLocaisAtendimento(this.ordemServico.localAtendimento.nomeLocal);
      });
    } 
  }

  obterClientes(filter: string = ''): void {
    this._clienteService.obterPorParametros({
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      indAtivo: 1,
      filter: filter,
      pageSize: 50
    }).subscribe((data: ClienteData) => {
      if (data.clientes.length) this.clientes.next(data.clientes.slice());
    });
  }

  obterTiposIntervencao(filter: string = ''): void {
    this._tipoIntervencaoService.obterPorParametros({
      sortActive: 'nomTipoIntervencao',
      sortDirection: 'asc',
      indAtivo: 1,
      filter: filter,
      pageSize: 50
    }).subscribe((data: TipoIntervencaoData) => {
      if (data.tiposIntervencao.length) this.tiposIntervencao.next(data.tiposIntervencao.slice());
    });
  }

  obterLocaisAtendimento(filter: string = ''): void {
    if (!this.form.controls['codCliente'].value) {
      return;
    }

    this.locaisAtendimento.next([]);

    this._localAtendimentoService.obterPorParametros({
      sortActive: 'nomeLocal',
      sortDirection: 'asc',
      indAtivo: 1,
      filter: filter,
      pageSize: 50,
      codCliente: this.form.controls['codCliente'].value.value
    }).subscribe((data: LocalAtendimentoData) => {
      if (data.locaisAtendimento.length) this.locaisAtendimento.next(data.locaisAtendimento.slice());
    });
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form = this.form.getRawValue();
    form.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');
    form.codUsuarioManut = this.usuario.codUsuario;

    Object.keys(form).forEach(key => {
      typeof form[key] == "boolean"
        ? this.ordemServico[key] = +form[key]
        : this.ordemServico[key] = form[key];
    });

    this._ordemServicoService.atualizar(this.ordemServico).subscribe(() => {
      this._snack.exibirToast("Registro atualizado com sucesso!", "success");
      this._localtion.back();
    });
  }

  criar(): void {
    const form = this.form.getRawValue();
    form.dataHoraSolicitacao = moment().format('YYYY-MM-DD HH:mm:ss');
    form.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');
    form.dataHoraAberturaOS = moment().format('YYYY-MM-DD HH:mm:ss');
    form.codUsuarioCad = this.usuario.codUsuario;
    form.indStatusEnvioReincidencia = -1;
    form.indRevisaoReincidencia = 1;
    form.codStatusServico = 1;

    Object.keys(form).forEach((key) => {
      typeof form[key] == "boolean" ? form[key] = +form[key] : form[key] = form[key];
    });

    this._ordemServicoService.criar(form).subscribe((ordemServico: OrdemServico) => {
      this._snack.exibirToast("Registro adicionado com sucesso!", "success");
      this._localtion.back();
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
