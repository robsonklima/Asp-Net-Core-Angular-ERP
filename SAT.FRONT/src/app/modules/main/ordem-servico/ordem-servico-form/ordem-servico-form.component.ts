import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { debounceTime, delay, distinctUntilChanged, filter, map, takeUntil, tap } from 'rxjs/operators';
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
import { OrdemServico, StatusServicoEnum } from 'app/core/types/ordem-servico.types';
import { RegiaoAutorizada } from 'app/core/types/regiao-autorizada.types';
import { Regiao } from 'app/core/types/regiao.types';
import { TipoIntervencao, TipoIntervencaoEnum } from 'app/core/types/tipo-intervencao.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { EquipamentoContrato } from 'app/core/types/equipamento-contrato.types';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import Enumerable from 'linq';
import { RoleEnum } from 'app/core/user/user.types';
import { statusConst } from 'app/core/types/status-types';

@Component({
  selector: 'app-ordem-servico-form',
  templateUrl: './ordem-servico-form.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrdemServicoFormComponent implements OnInit, OnDestroy
{
  codOS: number;
  ordemServico: OrdemServico;
  form: FormGroup;
  isAddMode: boolean;
  perfis: any;
  userSession: UsuarioSessao;
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
  clienteFilterCtrl: FormControl = new FormControl();
  loading: boolean = true;
  protected _onDestroy = new Subject<void>();

  constructor (
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
  )
  {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit()
  {
    this.loading = true;
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    this.isAddMode = !this.codOS;
    this.inicializarForm();

    this.perfis = RoleEnum;

    // Init
    this.obterTiposIntervencao();
    this.obterClientes();
    this.obterFiliais();
    this.obterRegioes();
    this.obterEquipamentos();

    // Changes Observables
    this.obterLocaisAoTrocarCliente();
    this.obterRegioesAoTrocarAutorizada();
    this.obterAutorizadasAoTrocarFilial();
    this.obterEquipamentosAoTrocarLocal();
    this.obterPATRegiaoAoSelecionarEquipamento();

    this.validaObrigatoriedadeDosCampos();

    // Main Obj
    await this.obterOrdemServico().then(async () =>
    {
      this.loading = false;
    });

    // Filter Observables
    this.obterLocaisAoFiltrar();

    this.form.controls['codTipoIntervencao'].valueChanges.subscribe(() =>
    {
      this.validaIntervencao();
    })
  }

  private inicializarForm(): void
  {
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
      codEquipContrato: [undefined, Validators.required],
      codFilial: [undefined, Validators.required],
      codRegiao: [undefined, Validators.required],
      codAutorizada: [undefined, Validators.required],
      indLiberacaoFechaduraCofre: [undefined],
      indIntegracao: [undefined],
      observacaoCliente: [undefined],
      descMotivoMarcaEspecial: [undefined],
      agenciaPosto: [undefined],
      indOSIntervencaoEquipamento: [undefined],
      indBloqueioReincidencia: [undefined]
    });
  }

  private async obterOrdemServico()
  {
    if (!this.isAddMode)
    {
      this.ordemServico = await this._ordemServicoService.obterPorCodigo(this.codOS).toPromise();
      this.form.patchValue(this.ordemServico);
      this.form.controls['codFilial'].setValue(this.ordemServico?.codFilial);
      this.form.controls['agenciaPosto'].setValue(
        `${this.ordemServico.localAtendimento.numAgencia}/${this.ordemServico.localAtendimento.dcPosto}`);
    }
    else
    {
      const codFilial = this.ordemServico?.filial?.codFilial || this.userSession.usuario?.filial?.codFilial;

      if (codFilial)
        this.form.controls['codFilial'].setValue(codFilial);

      if (this.userSession.usuario?.filial?.codFilial)
        this.form.controls['codFilial'].disable();
    }
    await this.obterAutorizadas();
  }

  private async obterTiposIntervencao()
  {
    this.tiposIntervencao = (await this._tipoIntervencaoService.obterPorParametros({
      indAtivo: statusConst.ATIVO,
      pageSize: 100,
      sortActive: 'nomTipoIntervencao',
      sortDirection: 'asc'
    }).toPromise()).items;
  }

  private async obterClientes(filter: string = '')
  {
    this.clientes = (await this._clienteService.obterPorParametros({
      indAtivo: statusConst.ATIVO,
      pageSize: 500,
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      filter: filter
    }).toPromise()).items;

    this.clienteFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe(() =>
      {
        this.obterClientes(this.clienteFilterCtrl.value);
      });
  }

  private async obterFiliais()
  {
    this.filiais = (await this._filialService.obterPorParametros({
      indAtivo: statusConst.ATIVO,
      pageSize: 500,
      sortActive: 'nomeFilial',
      sortDirection: 'asc'
    }).toPromise()).items;
  }

  private async obterAutorizadas()
  {
    this.autorizadas = (await this._autorizadaService
      .obterPorParametros({
        indAtivo: statusConst.ATIVO,
        pageSize: 500,
        sortActive: 'nomeFantasia',
        sortDirection: 'asc',
        codFilial: this.ordemServico?.filial?.codFilial || this.userSession.usuario?.filial?.codFilial
      }).toPromise()).items;
  }

  private async obterLocaisAoTrocarCliente()
  {
    this.form.controls['codCliente'].valueChanges.subscribe(async codCliente =>
    {
      const data = await this._localAtendimentoService.obterPorParametros({
        indAtivo: statusConst.ATIVO,
        sortActive: 'nomeLocal',
        sortDirection: 'asc',
        codCliente: codCliente,
        pageSize: 1000,
      }).toPromise();

      this.locais = [];
      this.locais = data.items.slice();

      if (!this.isAddMode)
      {
        if (this.locais !== null && !this.locais.filter(i => i.codPosto == this.ordemServico?.codPosto))
          this.locais.push(this.ordemServico?.localAtendimento);
        else
        {
          this.locais = [];
          this.locais.push(this.ordemServico?.localAtendimento);
        }
      }
    });
  }

  private async obterEquipamentos()
  {
    var codPosto = this.form.controls['codPosto'].value ?? null;

    const data = await this._equipamentoContratoService.obterPorParametros({
      sortActive: 'numSerie',
      sortDirection: 'asc',
      codPosto: codPosto,
      pageSize: 100
    }).toPromise();

    this.equipamentosContrato = [];
    this.equipamentosContrato = Enumerable.from(data.items)
      .orderByDescending(i => i.indAtivo)
      .thenBy(i => i.numSerie)
      .toArray();

    if (!this.isAddMode)
    {
      if (this.equipamentosContrato !== null && !this.equipamentosContrato.filter(i => i.codEquipContrato == this.ordemServico?.codEquipContrato))
        this.equipamentosContrato.push(this.ordemServico?.equipamentoContrato);
      else
      {
        this.equipamentosContrato = [];
        this.equipamentosContrato.push(this.ordemServico?.equipamentoContrato);
      }
    }
  }

  private obterEquipamentosAoTrocarLocal()
  {
    this.form.controls['codPosto'].valueChanges.subscribe(() =>
    {
      this.obterEquipamentos();
    });
  }

  private async obterLocaisAoFiltrar()
  {
    this.locaisFiltro.valueChanges.pipe(
      filter(query => !!query),
      tap(() => this.searching = true),
      debounceTime(700),
      map(async query =>
      {
        const codCliente = this.form.controls['codCliente'].value;

        if (!codCliente)
        {
          return [];
        }

        const data = await this._localAtendimentoService.obterPorParametros({
          sortActive: 'nomeLocal',
          sortDirection: 'asc',
          indAtivo: statusConst.ATIVO,
          filter: query,
          codCliente: codCliente,
          pageSize: 10
        }).toPromise();

        return data.items.slice();
      }),
      delay(500),
      takeUntil(this._onDestroy)
    )
      .subscribe(async locaisFiltrados =>
      {
        this.searching = false;
        this.locais = await locaisFiltrados;
      },
        () =>
        {
          this.searching = false;
        }
      );
  }

  private async obterRegioes()
  {
    var codAutorizada = this.form.controls['codAutorizada'].value ?? null;

    const data = await this._regiaoAutorizadaService.obterPorParametros({
      indAtivo: statusConst.ATIVO,
      codAutorizada: codAutorizada,
      pageSize: 100
    }).toPromise();

    this.regioes = Enumerable.from(data.items)
      .where(ra => ra.codAutorizada === codAutorizada && ra.indAtivo == statusConst.ATIVO && ra.regiao?.indAtivo == statusConst.ATIVO)
      .select(ra => ra.regiao).orderBy(ra => ra.nomeRegiao).toArray();

  }

  private async obterRegioesAoTrocarAutorizada()
  {
    this.form.controls['codAutorizada'].valueChanges.subscribe(() =>
    {
      this.obterRegioes();
    });
  }

  private obterPATRegiaoAoSelecionarEquipamento()
  {
    this.form.controls['codEquipContrato'].valueChanges.subscribe(async codEquipContrato =>
    {
      if (!this.isAddMode) return;

      var equipContrato = Enumerable.from(this.equipamentosContrato)
        .firstOrDefault(i => i?.codEquipContrato == codEquipContrato);

      if (equipContrato != null)
      {
        if (!this.userSession?.usuario?.filial?.codFilial)
        {
          var filial = (await this._filialService.obterPorCodigo(equipContrato?.codFilial).toPromise());
          this.form.controls['codFilial'].setValue(filial?.codFilial);
        }

        this.form.controls['codRegiao'].setValue(equipContrato?.codRegiao);
        this.form.controls['codAutorizada'].setValue(equipContrato?.codAutorizada);
      }
    });
  }

  private async obterAutorizadasAoTrocarFilial()
  {
    this.form.controls['codFilial'].valueChanges.subscribe(async codFilial =>
    {
      const data = await this._autorizadaService.obterPorParametros({
        indAtivo: statusConst.ATIVO,
        sortActive: 'nomeFantasia',
        sortDirection: 'asc',
        codFilial: codFilial,
        pageSize: 50
      }).toPromise();

      this.autorizadas = data.items.slice();
    });
  }

  salvar(): void
  {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  escondeCamposClientes(): boolean
  {
    var perfilUsuarioLogado: RoleEnum = this.userSession.usuario.perfil?.codPerfil;

    var perfisClientes =
      [
        RoleEnum.CLIENTE_BASICO_BIOMETRIA,
        RoleEnum.CLIENTE_BASICO_C_RESTRICOES
      ];

    if (perfisClientes.includes(perfilUsuarioLogado))
      return true;

    return false;
  }

  private validaIntervencao(): void
  {
    let perfilUsuarioLogado = this.userSession.usuario?.perfil?.codPerfil;
    let novoTipoIntervencao = this.form.controls['codTipoIntervencao'].value;

    var podemAlterarOrcamento = [
      RoleEnum.ADMIN,
      RoleEnum.FINANCEIRO_COORDENADOR,
      RoleEnum.FINANCEIRO_ADMINISTRATIVO,
      RoleEnum.PONTO_FINANCEIRO,
      RoleEnum.FINANCEIRO_COORDENADOR_PONTO,
      RoleEnum.FILIAIS_SUPERVISOR,
      RoleEnum.FILIAL_COORDENADOR,
      RoleEnum.FILIAL_LIDER,
      RoleEnum.FINANCEIRO_COORDENADOR_CREDITO
    ];

    var podemAlterarOrcamentoFilial = [
      RoleEnum.ADMIN,
      RoleEnum.FINANCEIRO_COORDENADOR,
      RoleEnum.FINANCEIRO_ADMINISTRATIVO,
      RoleEnum.PONTO_FINANCEIRO,
      RoleEnum.FINANCEIRO_COORDENADOR_PONTO,
      RoleEnum.FILIAIS_SUPERVISOR,
      RoleEnum.FILIAL_COORDENADOR,
      RoleEnum.FILIAL_LIDER,
      RoleEnum.FINANCEIRO_COORDENADOR_CREDITO
    ];

    var perfisPodemAlterarCorretiva = [
      RoleEnum.ADMIN,
      RoleEnum.PV_COORDENADOR_DE_CONTRATO
    ];

    var perfisPodemApenasCriarAutorizacaoDeslocamento = [
      RoleEnum.FILIAL_LIDER
    ];

    var intervencoesDeOrcamento = [
      TipoIntervencaoEnum.ORC_APROVADO,
      TipoIntervencaoEnum.ORC_REPROVADO,
      TipoIntervencaoEnum.ORC_PEND_APROVACAO_CLIENTE
    ];

    var intervencoesDeOrcamentoFilial = [
      TipoIntervencaoEnum.ORCAMENTO,
      TipoIntervencaoEnum.ORC_APROVADO,
      TipoIntervencaoEnum.ORC_REPROVADO,
      TipoIntervencaoEnum.ORC_PEND_APROVACAO_CLIENTE,
      TipoIntervencaoEnum.ORC_PEND_FILIAL_DETALHAR_MOTIVO
    ];

    // lider só pode criar autorização deslocamento
    if (perfisPodemApenasCriarAutorizacaoDeslocamento.includes(perfilUsuarioLogado) && novoTipoIntervencao != TipoIntervencaoEnum.AUTORIZACAO_DESLOCAMENTO)
    {
      this.form.controls['codTipoIntervencao'].setErrors({ 'naoPermiteCriar': true });
    }

    // só RPV pode alterar para corretiva
    if (novoTipoIntervencao == TipoIntervencaoEnum.CORRETIVA && !perfisPodemAlterarCorretiva.includes(perfilUsuarioLogado))
    {
      this.form.controls['codTipoIntervencao'].setErrors({ 'naoPermiteAlterarCorretiva': true });
    }

    if (intervencoesDeOrcamento.includes(novoTipoIntervencao))
    {
      if (!podemAlterarOrcamento.includes(perfilUsuarioLogado))
        this.form.controls['codTipoIntervencao'].setErrors({ 'naoPermiteAlterarOrcamento': true });
    }

    if (intervencoesDeOrcamentoFilial.includes(novoTipoIntervencao))
    {
      if (!podemAlterarOrcamentoFilial.includes(perfilUsuarioLogado))
        this.form.controls['codTipoIntervencao'].setErrors({ 'naoPermiteAlterarOrcamento': true });
    }

    this.form.controls['codTipoIntervencao'].setErrors(null);
  }

  validaObrigatoriedadeDosCampos()
  {
    this.form.get('codTipoIntervencao').valueChanges.subscribe(val =>
    {
      if (val == TipoIntervencaoEnum.AUTORIZACAO_DESLOCAMENTO)
        this.form.controls['codEquipContrato'].clearValidators();
      else
        this.form.controls['codEquipContrato'].setValidators([Validators.required]);

      this.form.controls['codEquipContrato'].updateValueAndValidity();
    });
  }

  private obterModelo(codEquipContrato: number): number
  {
    return this.equipamentosContrato.filter(e => e.codEquipContrato === codEquipContrato).shift()?.codEquip;
  }

  private atualizar(): void
  {
    this.form.disable();

    const form: any = this.form.getRawValue();
    let obj = {
      ...this.ordemServico,
      ...form,
      ...{
        codEquip: this.obterModelo(form.codEquipContrato),
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario?.codUsuario
      }
    };

    Object.keys(obj).forEach((key) =>
    {
      typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
    });

    this._ordemServicoService.atualizar(obj).subscribe(() =>
    {
      this._snack.exibirToast("Chamado atualizado com sucesso!", "success");
      this._router.navigate(['ordem-servico/detalhe/' + this.codOS]);
    });
  }

  private criar(): void
  {
    const form: any = this.form.getRawValue();
    let obj: OrdemServico = {
      ...this.ordemServico,
      ...form,
      ...{
        dataHoraSolicitacao: moment().format('YYYY-MM-DD HH:mm:ss'),
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        dataHoraAberturaOS: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario?.codUsuario,
        indStatusEnvioReincidencia: -1,
        indRevisaoReincidencia: 1,
        codStatusServico: 1,
        codEquip: this.obterModelo(form.codEquipContrato),
        indRevOK: null
      }
    };

    Object.keys(obj).forEach((key) =>
    {
      typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
    });

    this._ordemServicoService.criar(obj).subscribe((os) =>
    {
      this._snack.exibirToast("Registro adicionado com sucesso!", "success");
      this._router.navigate(['ordem-servico/detalhe/' + os.codOS]);
    });
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
