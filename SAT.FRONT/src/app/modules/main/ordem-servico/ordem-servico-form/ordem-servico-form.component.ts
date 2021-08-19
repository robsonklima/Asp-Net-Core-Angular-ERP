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
  localFilterCtrl: FormControl = new FormControl();
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

  async ngOnInit() {
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    this.isAddMode = !this.codOS;
    this.inicializarForm();
    this.registrarEmitters();

    if (!this.isAddMode) {
      this.ordemServico = await this._ordemServicoService.obterPorCodigo(this.codOS).toPromise();
      this.form.patchValue(this.ordemServico);
      this.obterClientes(this.ordemServico?.cliente?.nomeFantasia);
      this.obterTiposIntervencao();
      this.obterFiliais();
      this.obterAutorizadas(this.ordemServico?.autorizada?.nomeFantasia);
      this.obterRegioesAutorizadas(this.ordemServico?.autorizada?.nomeFantasia);
      this.obterLocaisAtendimento(this.ordemServico?.localAtendimento?.nomeLocal);
      this.obterEquipamentosContrato(this.ordemServico?.equipamentoContrato?.numSerie);
    } else {
      this.obterClientes();
      this.obterTiposIntervencao();
      this.obterFiliais();
      this.obterAutorizadas();
      this.obterRegioesAutorizadas();
    }
  }

  async obterClientes(filter: string = '') {
    const data = await this._clienteService.obterPorParametros({
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      indAtivo: 1,
      filter: filter,
      pageSize: 300
    }).toPromise();

    this.clientes = data.clientes;
  }

  async obterTiposIntervencao(filter: string = '') {
    const data = await this._tipoIntervencaoService.obterPorParametros({
      sortActive: 'nomTipoIntervencao',
      sortDirection: 'asc',
      indAtivo: 1,
      filter: filter,
      pageSize: 50
    }).toPromise();

    this.tiposIntervencao = data.tiposIntervencao;
  }

  async obterLocaisAtendimento(filter: string = '') {
    let codCliente = this.form.controls['codCliente'].value;

    const data = await this._localAtendimentoService.obterPorParametros({
      sortActive: 'nomeLocal',
      sortDirection: 'asc',
      filter: filter,
      codCliente: codCliente,
      indAtivo: 1
    }).toPromise();

    this.locaisAtendimento = data.locaisAtendimento;
  }

  async obterEquipamentosContrato(filter: string = '') {
    let codPosto = this.form.controls['codPosto'].value;

    const data = await this._equipamentoContratoService.obterPorParametros({
      indAtivo: 1,
      filter: filter,
      codPosto: codPosto,
      pageSize: 200
    }).toPromise();

    this.equipamentosContrato = data.equipamentosContrato;
  }

  async obterFiliais(filter: string = '') {
    const data = await this._filialService.obterPorParametros({
      sortActive: 'nomeFilial',
      sortDirection: 'asc',
      filter: filter,
      pageSize: 50
    }).toPromise();

    this.filiais = data.filiais;
  }

  async obterAutorizadas(filter: string = '') {
    const codFilial = this.form.controls['codFilial'].value;

    const data = await this._autorizadaService.obterPorParametros({
      codFilial: codFilial,
      indAtivo: 1,
      filter: filter,
      pageSize: 50
    }).toPromise();

    this.autorizadas = data.autorizadas;
  }

  async obterRegioesAutorizadas(filter: string = '') {
    const codAutorizada = this.form.controls['codAutorizada'].value;

    const data = await this._regiaoAutorizadaService.obterPorParametros({
      codAutorizada: codAutorizada,
      indAtivo: 1,
      filter: filter,
      pageSize: 50
    }).toPromise();

    this.regioesAutorizadas = data.regioesAutorizadas;
    this.regioes = data.regioesAutorizadas.map((ra) => { return ra.regiao; });
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
    this.localFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe((query) => {
        this.obterLocaisAtendimento(query);
      });
  }
}
