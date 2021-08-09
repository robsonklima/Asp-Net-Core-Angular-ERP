import { Location } from '@angular/common';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { debounceTime, distinctUntilChanged, first, takeUntil } from 'rxjs/operators';
import { forkJoin, Subject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
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

@Component({
  selector: 'app-ordem-servico-form',
  templateUrl: './ordem-servico-form.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrdemServicoFormComponent implements OnInit {
  codOS: number;
  ordemServico: OrdemServico;
  stepperForm: FormGroup;
  isAddMode: boolean;
  usuario: any;
  clienteFilterCtrl: FormControl = new FormControl();
  localAtendimentoFilterCtrl: FormControl = new FormControl();
  regiaoFilterCtrl: FormControl = new FormControl();
  autorizadaFilterCtrl: FormControl = new FormControl();
  filialFilterCtrl: FormControl = new FormControl();
  clientes: Cliente[] = [];
  tiposIntervencao: TipoIntervencao[] = [];
  locaisAtendimento: LocalAtendimento[] = [];
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
    private _ordemServicoService: OrdemServicoService,
    private _userService: UserService,
    private _tipoIntervencaoService: TipoIntervencaoService,
    private _localAtendimentoService: LocalAtendimentoService,
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
          this.stepperForm.get('step1').patchValue(this.ordemServico);
          this.stepperForm.get('step2').patchValue(this.ordemServico);
          this.stepperForm.get('step3').patchValue(this.ordemServico);
          forkJoin([
            this.obterClientes(os?.cliente?.nomeFantasia),
            this.obterTiposIntervencao(os?.tipoIntervencao?.nomTipoIntervencao),
            this.obterFiliais(os?.filial?.nomeFilial),
            this.obterAutorizadas(os?.autorizada?.nomeFantasia),
            this.obterRegioesAutorizadas(os?.autorizada?.nomeFantasia),
            this.obterLocaisAtendimento(os?.localAtendimento?.nomeLocal)
          ]);
        });
    } else {
      forkJoin([
        this.obterClientes(),
        this.obterTiposIntervencao(),
        this.obterFiliais(),
        this.obterAutorizadas(),
        this.obterRegioesAutorizadas(),
        this.obterLocaisAtendimento()
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
      let codCliente = this.stepperForm.get('step1').get('codCliente').value;

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
      const codFilial = this.stepperForm.get('step1').get('codFilial').value;

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
      const codAutorizada = this.stepperForm.get('step1').get('codAutorizada').value;

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
    this.stepperForm = this._formBuilder.group({
      step1: this._formBuilder.group({
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
        codEquipContrato: [''],
        codEquip: [''],
        codFilial: [
          {
            value: this.ordemServico?.filial?.codFilial || this.usuario?.filial?.codFilial,
            disabled: (
              this.usuario?.filial?.codFilial ? true : false
            )
          }, [Validators.required]
        ],
        codRegiao: ['', Validators.required],
        codAutorizada: ['', Validators.required]
      }),
      step2: this._formBuilder.group({
        indLiberacaoFechaduraCofre: [''],
        indIntegracao: [''],
      }),
      step3: this._formBuilder.group({
        observacaoCliente: [''],
        descMotivoMarcaEspecial: [''],
      })
    });
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private atualizar(): void {
    this.stepperForm.disable();
    this.showAlert = false;

    const stepperForm: any = this.stepperForm.getRawValue();
    let obj = {
      ...this.ordemServico,
      ...stepperForm.step1,
      ...stepperForm.step2,
      ...stepperForm.step3,
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
      this.stepperForm.enable();

      this.alert = {
        type: 'error',
        message: e?.error
      };

      this.showAlert = true;
    });
  }

  private criar(): void {
    const stepperForm: any = this.stepperForm.getRawValue();
    let obj = {
      ...this.ordemServico,
      ...stepperForm.step1,
      ...stepperForm.step2,
      ...stepperForm.step3,
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

    this._ordemServicoService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Registro adicionado com sucesso!", "success");
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
