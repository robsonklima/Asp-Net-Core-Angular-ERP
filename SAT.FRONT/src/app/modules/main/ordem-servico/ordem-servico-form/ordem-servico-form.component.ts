import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { debounceTime, delay, filter, map, takeUntil, tap } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { ClienteService } from 'app/core/services/cliente.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { FilialService } from 'app/core/services/filial.service';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { TipoIntervencaoService } from 'app/core/services/tipo-intervencao.service';
import { Autorizada } from 'app/core/types/autorizada.types';
import { Cliente } from 'app/core/types/cliente.types';
import { Filial } from 'app/core/types/filial.types';
import { LocalAtendimento } from 'app/core/types/local-atendimento.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RegiaoAutorizada } from 'app/core/types/regiao-autorizada.types';
import { Regiao } from 'app/core/types/regiao.types';
import { TipoIntervencao } from 'app/core/types/tipo-intervencao.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { EquipamentoContrato } from 'app/core/types/equipamento-contrato.types';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';

@Component({
  selector: 'app-ordem-servico-form',
  templateUrl: './ordem-servico-form.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrdemServicoFormComponent implements OnInit, OnDestroy {
  codOS: number;
  ordemServico: OrdemServico;
  form: FormGroup;
  isAddMode: boolean;
  usuario: any;
  clientes: Cliente[] = [];
  autorizadas: Autorizada[] = [];
  regioes: Regiao[] = [];
  filiais: Filial[] = [];
  tiposIntervencao: TipoIntervencao[] = [];
  equipamentosContrato: EquipamentoContrato[] = [];
  regioesAutorizadas: RegiaoAutorizada[] = [];
  locaisFiltro: FormControl = new FormControl();
  locais: LocalAtendimento[] = [];
  searching: boolean;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
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

    // Init
    this.obterTiposIntervencao();
    this.obterClientes();
    this.obterFiliais();

    // Changes Observables
    this.obterLocaisAoTrocarCliente();
    this.obterRegioesAoTrocarAutorizada();
    this.obterAutorizadasAoTrocarFilial();
    this.obterEquipamentosAoTrocarLocal();
    
    // Filter Observables
    this.obterLocaisAoFiltrar();

    // Main Obj
    this.obterOrdemServico();
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
      codFilial: [undefined, Validators.required],
      codRegiao: [undefined, Validators.required],
      codAutorizada: [undefined, Validators.required],
      indLiberacaoFechaduraCofre: [undefined],
      indIntegracao: [undefined],
      observacaoCliente: [undefined],
      descMotivoMarcaEspecial: [undefined],
    });
  }

  private async obterOrdemServico() {
    if (!this.isAddMode) {
      this.ordemServico = await this._ordemServicoService.obterPorCodigo(this.codOS).toPromise();
      this.form.patchValue(this.ordemServico);
      this.form.controls['codFilial'].setValue(this.ordemServico?.filial?.codFilial);
    } 

    const codFilial = this.ordemServico?.filial?.codFilial || this.usuario?.filial?.codFilial;

    if (this.usuario?.filial?.codFilial) {
      this.form.controls['codFilial'].setValue(codFilial);
      this.form.controls['codFilial'].disable();

      await this.obterAutorizadas();
    }
  }

  private async obterTiposIntervencao() {
    this.tiposIntervencao = (await this._tipoIntervencaoService.obterPorParametros({
      indAtivo: 1,
      pageSize: 100,
      sortActive: 'nomTipoIntervencao',
      sortDirection: 'asc'
    }).toPromise()).tiposIntervencao;
  }

  private async obterClientes() {
    this.clientes = (await this._clienteService.obterPorParametros({
      indAtivo: 1,
      pageSize: 500,
      sortActive: 'nomeFantasia',
      sortDirection: 'asc'
    }).toPromise()).clientes;
  }

  private async obterFiliais() {
    this.filiais = (await this._filialService.obterPorParametros({
      indAtivo: 1,
      pageSize: 500,
      sortActive: 'nomeFilial',
      sortDirection: 'asc'
    }).toPromise()).filiais;
  }

  private async obterAutorizadas() {
    this.autorizadas = (await this._autorizadaService
      .obterPorParametros({
        indAtivo: 1,
        pageSize: 500,
        sortActive: 'nomeFantasia',
        sortDirection: 'asc',
        codFilial: this.ordemServico?.filial?.codFilial || this.usuario?.filial?.codFilial
      }).toPromise()).autorizadas;
  }

  private async obterLocaisAoTrocarCliente() {
    this.form.controls['codCliente'].valueChanges.subscribe(async codCliente => {
      const data = await this._localAtendimentoService.obterPorParametros({
        indAtivo: 1,
        sortActive: 'nomeLocal',
        sortDirection: 'asc',
        codCliente: codCliente,
        pageSize: 50,
      }).toPromise();

      this.locais = data.locaisAtendimento.slice();

      // POG: Adiciona o local da OS na lista
      // if (!this.isAddMode && this.form.controls['codPosto'].value === this.ordemServico.codPosto) {
      //   this.locais.push(await this._localAtendimentoService
      //     .obterPorCodigo(this.ordemServico.codPosto)
      //     .toPromise());
      // }
    });
  }

  private async obterEquipamentosAoTrocarLocal() {
    this.form.controls['codPosto'].valueChanges.subscribe(async codPosto => {
      const data = await this._equipamentoContratoService.obterPorParametros({
        sortActive: 'numSerie',
        sortDirection: 'asc',
        codPosto: codPosto,
        pageSize: 100
      }).toPromise();

      this.equipamentosContrato = data.equipamentosContrato.slice();
    });
  }

  private async obterLocaisAoFiltrar() {
    this.locaisFiltro.valueChanges.pipe(
      filter(query => !!query),
      tap(() => this.searching = true),
      takeUntil(this._onDestroy),
      debounceTime(700),
      map(async query => {
        const codCliente = this.form.controls['codCliente'].value;

        if (!codCliente) {
          return [];
        }

        const data = await this._localAtendimentoService.obterPorParametros({
          sortActive: 'nomeLocal',
          sortDirection: 'asc',
          indAtivo: 1,
          filter: query,
          codCliente: codCliente,
          pageSize: 10
        }).toPromise();

        return data.locaisAtendimento.slice();
      }),
      delay(500),
      takeUntil(this._onDestroy)
    )
    .subscribe(async locaisFiltrados => {
      this.searching = false;
      this.locais = await locaisFiltrados;
    },
      () => {
        this.searching = false;
      }
    );
  }

  private async obterRegioesAoTrocarAutorizada() {
    this.form.controls['codAutorizada'].valueChanges.subscribe(async codAutorizada => {
      const data = await this._regiaoAutorizadaService.obterPorParametros({
        indAtivo: 1,
        codAutorizada: codAutorizada,
        pageSize: 100
      }).toPromise();

      this.regioes = data.regioesAutorizadas
        .filter(ra => ra.codAutorizada === codAutorizada)
        .map(ra => ra.regiao);
    });
  }

  private async obterAutorizadasAoTrocarFilial() {
    this.form.controls['codFilial'].valueChanges.subscribe(async codFilial => {
      const data = await this._autorizadaService.obterPorParametros({
        indAtivo: 1,
        sortActive: 'nomeFantasia',
        sortDirection: 'asc',
        codFilial: codFilial,
        pageSize: 50
      }).toPromise();

      this.autorizadas = data.autorizadas.slice();
    });
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private atualizar(): void {
    this.form.disable();

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
      this._router.navigate(['ordem-servico/detalhe/'+this.codOS]);
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
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
