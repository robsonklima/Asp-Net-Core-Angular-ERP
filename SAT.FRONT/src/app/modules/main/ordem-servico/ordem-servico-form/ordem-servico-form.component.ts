import { Location } from '@angular/common';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { debounceTime, distinctUntilChanged, first, takeUntil } from 'rxjs/operators';
import { forkJoin, Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseAlertType } from '@fuse/components/alert';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { ClienteService } from 'app/core/services/cliente.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { FilialService } from 'app/core/services/filial.service';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { TipoIntervencaoService } from 'app/core/services/tipo-intervencao.service';
import { Autorizada, AutorizadaData } from 'app/core/types/autorizada.types';
import { Cliente, ClienteData } from 'app/core/types/cliente.types';
import { Filial, FilialData } from 'app/core/types/filial.types';
import { LocalAtendimento, LocalAtendimentoData } from 'app/core/types/local-atendimento.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RegiaoAutorizada, RegiaoAutorizadaData } from 'app/core/types/regiao-autorizada.types';
import { Regiao } from 'app/core/types/regiao.types';
import { TipoIntervencao, TipoIntervencaoData } from 'app/core/types/tipo-intervencao.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { EquipamentoContrato, EquipamentoContratoData } from 'app/core/types/equipamento-contrato.types';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';

@Component({
  selector: 'app-ordem-servico-form',
  templateUrl: './ordem-servico-form.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrdemServicoFormComponent implements OnInit {
  codOS: number;
  ordemServico: OrdemServico;
  form: FormGroup;
  isAddMode: boolean;
  usuario: any;
  clienteFilterCtrl: FormControl = new FormControl();
  localAtendimentoFilterCtrl: FormControl = new FormControl();
  equipamentoContratoFilterCtrl: FormControl = new FormControl();
  regiaoFilterCtrl: FormControl = new FormControl();
  autorizadaFilterCtrl: FormControl = new FormControl();
  filialFilterCtrl: FormControl = new FormControl();
  clientes: Cliente[] = [];
  tiposIntervencao: TipoIntervencao[] = [];
  locaisAtendimento: LocalAtendimento[] = [];
  equipamentosContrato: EquipamentoContrato[] = [];
  regioesAutorizadas: RegiaoAutorizada[] = [];
  autorizadas: Autorizada[] = [];
  regioes: Regiao[] = [];
  filiais: Filial[] = [];
  alert: { type: FuseAlertType; message: string } = {
    type: 'success',
    message: ''
  };
  showAlert: boolean = false;

  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _location: Location,
    private _router: Router,
    private _ordemServicoService: OrdemServicoService,
    private _userService: UserService,
    private _tipoIntervencaoService: TipoIntervencaoService,
    private _localAtendimentoService: LocalAtendimentoService,
    private _equipamentoContratoService: EquipamentoContratoService,
    private _snack: CustomSnackbarService,
    private _clienteService: ClienteService,
    private _filialService: FilialService,
    private _autorizadaService: AutorizadaService,
    private _regiaoAutorizadaService: RegiaoAutorizadaService
  ) {
    this.usuario = JSON.parse(this._userService.userSession).usuario;
  }

  ngOnInit(): void {
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    this.isAddMode = !this.codOS;
    this.inicializarForm();
    this.registrarEmitters();

    if (!this.isAddMode) {
      this.obterOrdemServico()
        .then((os: OrdemServico) => {
          this.ordemServico = os;
          this.form.patchValue(this.ordemServico);
          forkJoin([
            this.obterClientes(os?.cliente?.nomeFantasia),
            this.obterTiposIntervencao(),
            this.obterFiliais(),
            this.obterAutorizadas(os?.autorizada?.nomeFantasia),
            this.obterRegioesAutorizadas(os?.autorizada?.nomeFantasia),
            this.obterLocaisAtendimento(os?.localAtendimento?.nomeLocal),
            this.obterEquipamentosContrato(os?.equipamentoContrato?.numSerie)
          ]);
        });
    } else {
      forkJoin([
        this.obterClientes(),
        this.obterTiposIntervencao(),
        this.obterFiliais(),
        this.obterAutorizadas(),
        this.obterRegioesAutorizadas()
      ]);
    }
  }

  obterOrdemServico(): Promise<any> {
    return new Promise((resolve, reject) => {
      this._ordemServicoService.obterPorCodigo(this.codOS)
        .pipe(first())
        .subscribe((ordemServico: OrdemServico) => {
          resolve(ordemServico)
        }, () => {
          reject();
        });
    });
  }

  obterClientes(filter: string = ''): Promise<ClienteData> {
    return new Promise((resolve, reject) => {
      this._clienteService.obterPorParametros({
        sortActive: 'nomeFantasia',
        sortDirection: 'asc',
        indAtivo: 1,
        filter: filter,
        pageSize: 50
      }).subscribe((data: ClienteData) => {
        this.clientes = data.clientes;
        resolve(data);
      }, () => {
        reject();
      });
    })
  }

  obterTiposIntervencao(filter: string = ''): Promise<TipoIntervencaoData> {
    return new Promise((resolve, reject) => {
      this._tipoIntervencaoService.obterPorParametros({
        sortActive: 'nomTipoIntervencao',
        sortDirection: 'asc',
        indAtivo: 1,
        filter: filter,
        pageSize: 50
      }).subscribe((data: TipoIntervencaoData) => {
        this.tiposIntervencao = data.tiposIntervencao;
        resolve(data);
      }, () => {
        reject();
      });
    });
  }

  obterLocaisAtendimento(filter: string = ''): Promise<LocalAtendimentoData> {
    return new Promise((resolve, reject) => {
      this.locaisAtendimento = [];
      let codCliente = this.form.controls['codCliente'].value;

      this._localAtendimentoService.obterPorParametros({
        sortActive: 'nomeLocal',
        sortDirection: 'asc',
        filter: filter,
        pageSize: 50,
        codCliente: codCliente
      }).subscribe((data: LocalAtendimentoData) => {
        this.locaisAtendimento = data.locaisAtendimento;
        resolve(data);
      }, () => {
        reject();
      });
    });
  }

  obterEquipamentosContrato(filter: string = ''): Promise<EquipamentoContratoData> {
    return new Promise((resolve, reject) => {
      this.equipamentosContrato = [];
      let codPosto = this.form.controls['codPosto'].value;

      this._equipamentoContratoService.obterPorParametros({
        pageSize: 50,
        indAtivo: 1,
        filter: filter,
        codPosto: codPosto
      }).subscribe((data: EquipamentoContratoData) => {
        this.equipamentosContrato = data.equipamentosContrato;
        resolve(data);
      }, () => {
        reject()
      });
    });
  }

  obterFiliais(filter: string = ''): Promise<FilialData> {
    return new Promise((resolve, reject) => {
      this._filialService.obterPorParametros({
        sortActive: 'nomeFilial',
        sortDirection: 'asc',
        pageSize: 50,
        filter: filter
      }).subscribe((data: FilialData) => {
        this.filiais = data.filiais;
        this.obterAutorizadas();
        resolve(data);
      }, () => {
        reject()
      });
    });
  }

  obterAutorizadas(filter: string = ''): Promise<AutorizadaData> {
    return new Promise((resolve, reject) => {
      this.regioesAutorizadas = [];
      const codFilial = this.form.controls['codFilial'].value;

      this._autorizadaService.obterPorParametros({
        pageSize: 50,
        codFilial: codFilial,
        indAtivo: 1,
        filter: filter,
      }).subscribe((data: AutorizadaData) => {
        this.autorizadas = data.autorizadas;
        resolve(data);
      }, () => {
        reject();
      });
    });
  }

  obterRegioesAutorizadas(filter: string = ''): Promise<RegiaoAutorizadaData> {
    return new Promise((resolve, reject) => {
      const codAutorizada = this.form.controls['codAutorizada'].value;

      this._regiaoAutorizadaService.obterPorParametros({
        codAutorizada: codAutorizada,
        indAtivo: 1,
        filter: filter,
        pageSize: 50,
      }).subscribe((data: RegiaoAutorizadaData) => {
        this.regioesAutorizadas = data.regioesAutorizadas;
        this.regioes = data.regioesAutorizadas.map((ra) => { return ra.regiao; });
        resolve(data);
      }, () => {
        reject();
      });
    });
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codOS: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      numOSCliente: [undefined],
      numOSQuarteirizada: [undefined],
      nomeSolicitante: [undefined],
      nomeContato: [undefined],
      telefoneSolicitante: [undefined],
      codCliente: [undefined, Validators.required],
      codTipoIntervencao: [undefined, Validators.required],
      codPosto: [undefined, Validators.required],
      defeitoRelatado: [undefined, Validators.required],
      codEquipContrato: [undefined],
      codEquip: [undefined],
      codFilial: [
        {
          value: this.ordemServico?.filial?.codFilial || this.usuario?.filial?.codFilial,
          disabled: (
            this.usuario?.filial?.codFilial ? true : false
          )
        }, [Validators.required]
      ],
      codRegiao: [undefined, Validators.required],
      codAutorizada: [undefined, Validators.required],
      indLiberacaoFechaduraCofre: [undefined],
      indIntegracao: [undefined],
      observacaoCliente: [undefined],
      descMotivoMarcaEspecial: [undefined],
    });
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private atualizar(): void {
    this.form.disable();
    this.showAlert = false;

    const form: any = this.form.getRawValue();
    let obj = {
      ...this.ordemServico,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.usuario.codUsuario
      }
    };

    Object.keys(obj).forEach((key) => {
      typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
    });

    this._ordemServicoService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Chamado atualizado com sucesso!", "success");
      this._location.back();
    }, e => {
      this.form.enable();

      this.alert = {
        type: 'error',
        message: e?.error
      };

      this.showAlert = true;
    });
  }

  private criar(): void {
    const form: any = this.form.getRawValue();
    let obj = {
      ...this.ordemServico,
      ...form,
      ...{
        dataHoraSolicitacao: moment().format('YYYY-MM-DD HH:mm:ss'),
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        dataHoraAberturaOS: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.usuario.codUsuario,
        indStatusEnvioReincidencia: -1,
        indRevisaoReincidencia: 1,
        codStatusServico: 1
      }
    };

    Object.keys(obj).forEach((key) => {
      typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
    });

    this._ordemServicoService.criar(obj).subscribe((os) => {
      this._snack.exibirToast("Registro adicionado com sucesso!", "success");
      this._router.navigate(['ordem-servico/detalhe/' + os.codOS]);
    }, e => {

    });
  }

  private registrarEmitters(): void {
    this.clienteFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterClientes(this.clienteFilterCtrl.value);
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

    this.equipamentoContratoFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterEquipamentosContrato(this.equipamentoContratoFilterCtrl.value);
      });

    this.filialFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterFiliais(this.filialFilterCtrl.value);
      });

    this.regiaoFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterRegioesAutorizadas(this.regiaoFilterCtrl.value);
      });

    this.autorizadaFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterAutorizadas(this.autorizadaFilterCtrl.value);
      });
  }
}
