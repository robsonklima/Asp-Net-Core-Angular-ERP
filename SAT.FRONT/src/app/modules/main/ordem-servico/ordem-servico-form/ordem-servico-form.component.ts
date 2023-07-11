import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { ClienteService } from 'app/core/services/cliente.service';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { FilialService } from 'app/core/services/filial.service';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { TipoIntervencaoService } from 'app/core/services/tipo-intervencao.service';
import { Autorizada } from 'app/core/types/autorizada.types';
import { Cliente, ClienteEnum } from 'app/core/types/cliente.types';
import { Contrato, ContratoParameters } from 'app/core/types/contrato.types';
import { EquipamentoContrato } from 'app/core/types/equipamento-contrato.types';
import { Equipamento } from 'app/core/types/equipamento.types';
import { Filial } from 'app/core/types/filial.types';
import { LocalAtendimento } from 'app/core/types/local-atendimento.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RegiaoAutorizada } from 'app/core/types/regiao-autorizada.types';
import { Regiao } from 'app/core/types/regiao.types';
import { SetorEnum } from 'app/core/types/setor.types';
import { statusConst } from 'app/core/types/status-types';
import { TipoIntervencao, TipoIntervencaoEnum } from 'app/core/types/tipo-intervencao.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { RoleEnum } from 'app/core/user/user.types';
import Enumerable from 'linq';
import moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, delay, distinctUntilChanged, filter, map, takeUntil, tap } from 'rxjs/operators';

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
	perfis: any;
	userSession: UsuarioSessao;
	clientes: Cliente[] = [];
	autorizadas: Autorizada[] = [];
	regioes: Regiao[] = [];
	filiais: Filial[] = [];
	equipamentos: Equipamento[] = [];
	contratos: Contrato[] = [];
	tiposIntervencao: TipoIntervencao[] = [];
	equipamentosContrato: EquipamentoContrato[] = [];
	atmsIds: EquipamentoContrato[] = [];
	regioesAutorizadas: RegiaoAutorizada[] = [];
	locaisFiltro: FormControl = new FormControl();
	locais: LocalAtendimento[] = [];
	searching: boolean;
	clienteFilterCtrl: FormControl = new FormControl();
	equipamentoFilterCtrl: FormControl = new FormControl();
	contratoFilterCtrl: FormControl = new FormControl();
	atmIdFilterCtrl: FormControl = new FormControl();
	equipamentosContratoFilterCtrl: FormControl = new FormControl();
	validaCliente: boolean = this._userService.isCustomer;
	loading: boolean = true;
	indAtivo: boolean = true;
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
		private _regiaoAutorizadaService: RegiaoAutorizadaService,
		private _contratoService: ContratoService,
		private _equipamentoService: EquipamentoService
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		this.loading = true;
		this.codOS = +this._route.snapshot.paramMap.get('codOS');
		this.isAddMode = !this.codOS;
		this.inicializarForm();

		this.perfis = RoleEnum;

		this.obterTiposIntervencao();
		this.obterClientes();
		this.obterFiliais();
		this.obterRegioes();
		this.registrarEmitters();
		this.validaObrigatoriedadeDosCampos();

		await this.obterOrdemServico().then(async () => {
			this.loading = false;
		});

		this.form.controls['enderecoLocal'].disable();
		this.form.controls['atmId'].disable();
		this.form.controls['codEquip'].disable();
		this.form.controls['codContrato'].disable();
	}

	private inicializarForm(): void {
		this.form = this._formBuilder.group({
			codOS: [{
				value: undefined,
				disabled: true,
			}, [Validators.required]],
			atmId: [undefined],
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
			indBloqueioReincidencia: [undefined],
			codEquip: [undefined, Validators.required],
			codContrato: [undefined],
			indAtivo: [undefined],
			enderecoLocal: [undefined],
			codSeveridade: [undefined]
		});
	}

	private async obterOrdemServico() {
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

	private async obterTiposIntervencao() {
		let tiposIntervencao = null;

		if (this.validaCliente)
			tiposIntervencao = `${TipoIntervencaoEnum.CORRETIVA}, ${TipoIntervencaoEnum.PREVENTIVA}`;

		if (this.userSession.usuario.codCliente == ClienteEnum.RIOCARD 
			|| this.userSession.usuario.codCliente == ClienteEnum.VLT)
			tiposIntervencao = `${TipoIntervencaoEnum.HELP_DESK}`;

		this.tiposIntervencao = (await this._tipoIntervencaoService.obterPorParametros({
			indAtivo: statusConst.ATIVO,
			pageSize: 100,
			sortActive: 'nomTipoIntervencao',
			sortDirection: 'asc',
			codTiposIntervencao: tiposIntervencao,
		}).toPromise()).items;
	}

	public obterTiposIntervencaoPorPerfil(): TipoIntervencao[] {
		return this.tiposIntervencao;
	}

	private async obterClientes(filter: string = '') {
		this.clientes = (await this._clienteService.obterPorParametros({
			indAtivo: statusConst.ATIVO,
			pageSize: 500,
			sortActive: 'nomeFantasia',
			sortDirection: 'asc',
			codCliente: this.userSession?.usuario?.codCliente,
			filter: filter
		}).toPromise()).items;
	}

	private async obterFiliais() {
		this.filiais = (await this._filialService.obterPorParametros({
			indAtivo: statusConst.ATIVO,
			pageSize: 500,
			sortActive: 'nomeFilial',
			sortDirection: 'asc'
		}).toPromise()).items;
	}

	private async obterContratos(filter: string = '') {
		var codCliente = this.form.controls['codEquipContrato'].value ?? null;

		const params: ContratoParameters = {
			filter: filter,
			indAtivo: statusConst.ATIVO,
			codClientes: codCliente.toString()
		}

		const data = await this._contratoService.obterPorParametros(params).toPromise();
		this.contratos = data.items;
	}

	private async obterAutorizadas() {
		this.autorizadas = (await this._autorizadaService
			.obterPorParametros({
				indAtivo: statusConst.ATIVO,
				pageSize: 500,
				sortActive: 'nomeFantasia',
				sortDirection: 'asc',
				codFilial: this.ordemServico?.filial?.codFilial || this.userSession.usuario?.filial?.codFilial
			}).toPromise()).items;
	}

	private async obterLocais(filter: string = '') {
		var codCliente = this.form.controls['codCliente'].value ?? null;

		const data = await this._localAtendimentoService.obterPorParametros({
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeLocal',
			sortDirection: 'asc',
			codCliente: codCliente,
			pageSize: 1000,
			filter: filter
		}).toPromise();

		this.locais = [];
		this.locais = data.items.slice();

		if (!this.isAddMode)
		{
			if (this.locais !== null && !this.locais.filter(i => i.codPosto == this.ordemServico?.codPosto))
				this.locais.push(this.ordemServico?.localAtendimento);
			else
			{
				this.locais.push(this.ordemServico?.localAtendimento);
			}
		}
	}

	private registrarEmitters() {
		this.form.controls['codCliente'].valueChanges.subscribe(async codCliente => {
			if (!this.form.controls['atmId'].value)
			{
				this.obterLocais();
				this.obterEquipamentosContrato();
			}
			this.form.controls['atmId'].enable();
		});

		this.form.controls['codPosto'].valueChanges.subscribe(() => {
			if (!this.form.controls['atmId'].value)
			{
				this.obterEquipamentosContrato();
			}
		});

		this.form.controls['codAutorizada'].valueChanges.subscribe(() => {
			this.obterRegioes();
		});

		this.form.controls['codEquipContrato'].valueChanges.subscribe(async codEquipContrato => {
			const eContrato = await this._equipamentoContratoService.obterPorCodigo(codEquipContrato).toPromise();
			const contrato = await this._contratoService.obterPorCodigo(eContrato.codContrato).toPromise();
			const equipamento = await this._equipamentoService.obterPorCodigo(eContrato.codEquip).toPromise();

			this.contratos = [contrato];
			this.equipamentos = [equipamento];

			this.form.controls['codContrato'].setValue(eContrato.codContrato);
			this.form.controls['codEquip'].setValue(eContrato.codEquip);

			const endereco: string = `${eContrato.localAtendimento?.endereco || 'N/I'} - ${eContrato.localAtendimento?.cidade?.nomeCidade || 'N/I'} - ${eContrato.localAtendimento?.cidade?.unidadeFederativa?.siglaUF || 'N/I'}`;
			this.form.controls['enderecoLocal'].setValue(endereco.toString());

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
				this.form.controls['codFilial'].setValue(equipContrato?.codFilial);
			}
		});

		this.clienteFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(500),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterClientes(this.clienteFilterCtrl.value);
			});

		this.form.controls['codFilial'].valueChanges.subscribe(async codFilial => {
			const data = await this._autorizadaService.obterPorParametros({
				indAtivo: statusConst.ATIVO,
				sortActive: 'nomeFantasia',
				sortDirection: 'asc',
				codFilial: codFilial,
				pageSize: 50
			}).toPromise();

			this.autorizadas = data.items.slice();
		});

		this.form.controls['codTipoIntervencao'].valueChanges.subscribe(() => {
			this.validaIntervencao();
		});

		this.form.controls['codCliente'].valueChanges.subscribe(() => {
			if (!this.form.controls['atmId'].value)
				this.obterEquipamentosContrato();
		});

		this.form.controls['codPosto'].valueChanges.subscribe(() => {
			if (!this.form.controls['atmId'].value)
				this.obterEquipamentosContrato();
		});

		this.form.controls['atmId'].valueChanges.subscribe(async (codEquipContrato) => {
			if (!codEquipContrato) return;

			const eContrato = await this._equipamentoContratoService.obterPorCodigo(codEquipContrato).toPromise();
			const lAtendimento = await this._localAtendimentoService.obterPorCodigo(eContrato.codPosto).toPromise();
			const contrato = await this._contratoService.obterPorCodigo(eContrato.codContrato).toPromise();
			const equipamento = await this._equipamentoService.obterPorCodigo(eContrato.codEquip).toPromise();

			this.locais = [lAtendimento];
			this.equipamentosContrato = [eContrato];
			this.contratos = [contrato];
			this.equipamentos = [equipamento];

			this.form.controls['codPosto'].setValue(eContrato.codPosto);
			this.form.controls['codEquipContrato'].setValue(eContrato.codEquipContrato);
			this.form.controls['codContrato'].setValue(eContrato.codContrato);
			this.form.controls['codEquip'].setValue(eContrato.codEquip);

			const endereco: string = `${eContrato.localAtendimento?.endereco || 'N/I'} - ${eContrato.localAtendimento?.cidade?.nomeCidade || 'N/I'} - ${eContrato.localAtendimento?.cidade?.unidadeFederativa?.siglaUF || 'N/I'}`;

			this.form.controls['enderecoLocal'].setValue(endereco.toString());
		});

		this.atmIdFilterCtrl.valueChanges.pipe(
			filter(q => q != ''),
			debounceTime(700),
			map(async query => { return query }),
			delay(500),
			takeUntil(this._onDestroy)
		)
			.subscribe(async promisse => {
				promisse.then(query => {
					this.obterAtmsIds(query);
				});
			});

		this.equipamentosContratoFilterCtrl.valueChanges.pipe(
			filter(q => q != ''),
			debounceTime(700),
			map(async query => {
				return query;
			}),
			delay(500),
			takeUntil(this._onDestroy)
		)
			.subscribe(async promisse => {
				promisse.then(query => {
					this.obterEquipamentosContrato(query);
				});
			});

		this.locaisFiltro.valueChanges.pipe(
			filter(q => q != ''),
			tap(() => this.searching = true),
			debounceTime(700),
			map(async query => {
				return query;
			}),
			delay(500),
			takeUntil(this._onDestroy)
		)
			.subscribe(async promisse => {
				promisse.then(query => {
					this.obterLocais(query);
				});
			});

		this.contratoFilterCtrl.valueChanges.pipe(
			filter(q => q != ''),
			debounceTime(700),
			map(async query => {
				return query;
			}),
			delay(500),
			takeUntil(this._onDestroy)
		).subscribe(async promisse => {
			promisse.then(query => {
				this.obterContratos(query);
			});
		});
	}

	public async obterEquipamentosContrato(filter: string = '') {
		var codCliente = this.form.controls['codCliente'].value ?? null;
		var codPosto = this.form.controls['codPosto'].value ?? null;

		if (!codPosto || !codCliente) return;

		const data = await this._equipamentoContratoService.obterPorParametros({
			sortActive: 'numSerie',
			sortDirection: 'asc',
			codPosto: codPosto,
			codClientes: codCliente?.toString(),
			pageSize: 50,
			filter: filter,
			indAtivo: +this.indAtivo
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

	public async obterAtmsIds(filter: string = '') {
		var codCliente = this.form.controls['codCliente'].value ?? null;

		const data = await this._equipamentoContratoService.obterPorParametros({
			sortActive: 'numSerie',
			sortDirection: 'asc',
			codClientes: codCliente?.toString(),
			indAtivo: +this.indAtivo,
			pageSize: 50,
			filter: filter,
		}).toPromise();

		this.atmsIds = [];
		this.atmsIds = Enumerable.from(data.items)
			.orderByDescending(i => i.indAtivo)
			.thenBy(i => i.numSerie)
			.toArray();
	}

	private async obterRegioes() {
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

	escondeCamposClientes(): boolean {
		var perfilUsuarioLogado: RoleEnum = this.userSession.usuario.perfil?.codPerfil;

		var perfisClientes =
			[
				RoleEnum.CLIENTE_AVANÇADO,
				RoleEnum.CLIENTE_BASICO,
				RoleEnum.CLIENTE_INTERMEDIARIO
			];

		if (perfisClientes.includes(perfilUsuarioLogado))
			return true;

		return false;
	}

	private validaIntervencao(): void {
		let perfilUsuarioLogado = this.userSession.usuario?.perfil?.codPerfil;
		let setorUsuarioLogado = this.userSession.usuario?.setor?.codSetor;
		let novoTipoIntervencao = this.form.controls['codTipoIntervencao'].value;

		var podemAlterarOrcamento = [
			RoleEnum.ADM_DO_SISTEMA,
			RoleEnum.SUPERVISOR,
			RoleEnum.COORDENADOR,
			RoleEnum.LIDER,
			RoleEnum.ANALISTA,
			RoleEnum.ASSISTENTE,
			RoleEnum.PV_COORDENADOR_DE_CONTRATO,
		];

		var podemAlterarOrcamentoFilial = [
			RoleEnum.ADM_DO_SISTEMA,
			RoleEnum.SUPERVISOR,
			RoleEnum.COORDENADOR,
			RoleEnum.LIDER,
			RoleEnum.PV_COORDENADOR_DE_CONTRATO,
			RoleEnum.ANALISTA
		];

		var perfisPodemAlterarCorretiva = [
			RoleEnum.ADM_DO_SISTEMA,
			RoleEnum.PV_COORDENADOR_DE_CONTRATO,
			RoleEnum.CLIENTE_AVANÇADO,
			RoleEnum.ANALISTA,
			RoleEnum.COORDENADOR,
		];

		var setoresPodemAlterarCorretiva = [
			SetorEnum.HELPDESK_NOC,
			SetorEnum.COORDENACAO_DE_CONTRATOS,
			SetorEnum.CLIENTE
		];

		var perfisPodemApenasPreventivaGerencial = [
			RoleEnum.TECNICO_OPERACOES,
		];

		var perfisPodemApenasCriarAutorizacaoDeslocamento = [
			RoleEnum.LIDER,
			RoleEnum.COORDENADOR
		];

		var setoresPodemApenasCriarAutorizacaoDeslocamento = [
			SetorEnum.CENTRO_TECNICO_OPERACIONAL,
			SetorEnum.OPERACAO_DE_CAMPO
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

		// filial só pode criar autorização deslocamento	
		if (perfisPodemApenasCriarAutorizacaoDeslocamento.includes(perfilUsuarioLogado)
			&& setoresPodemApenasCriarAutorizacaoDeslocamento.includes(setorUsuarioLogado)
			&& novoTipoIntervencao != TipoIntervencaoEnum.AUTORIZACAO_DESLOCAMENTO
			&& this.isAddMode)
		{
			this.form.controls['codTipoIntervencao'].setErrors({ 'naoPermiteCriar': true });
		}

		// tecnico operaçoes só permite Manutenção Preventica Gerencial
		if (perfisPodemApenasPreventivaGerencial.includes(perfilUsuarioLogado)
			&& novoTipoIntervencao != TipoIntervencaoEnum.PREVENTIVA_GERENCIAL
			&& this.isAddMode)
		{
			this.form.controls['codTipoIntervencao'].setErrors({ 'naoPermiteCriar': true });
		}

		// só RPV pode alterar para corretiva
		if (novoTipoIntervencao == TipoIntervencaoEnum.CORRETIVA 
			&& !perfisPodemAlterarCorretiva.includes(perfilUsuarioLogado) 
			&& !setoresPodemAlterarCorretiva.includes(setorUsuarioLogado))
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
	}

	validaObrigatoriedadeDosCampos() {
		this.form.get('codTipoIntervencao').valueChanges.subscribe(val => {
			if (val == TipoIntervencaoEnum.AUTORIZACAO_DESLOCAMENTO)
				this.form.controls['codEquipContrato'].clearValidators();
			else
				this.form.controls['codEquipContrato'].setValidators([Validators.required]);

			this.form.controls['codEquipContrato'].updateValueAndValidity();
		});
	}

	private obterModelo(codEquipContrato: number): number {
		return this.equipamentosContrato.filter(e => e.codEquipContrato === codEquipContrato).shift()?.codEquip;
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
				codEquip: this.obterModelo(form.codEquipContrato),
				dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioManut: this.userSession.usuario?.codUsuario,
				codUsuarioManutencao: this.userSession.usuario?.codUsuario
			}
		};

		Object.keys(obj).forEach((key) => {
			typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
		});

		this._ordemServicoService.atualizar(obj).subscribe(() => {
			this._snack.exibirToast("Chamado atualizado com sucesso!", "success");
			this._router.navigate(['ordem-servico/detalhe/' + this.codOS]);
		});
	}

	private criar(): void {
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

		Object.keys(obj).forEach((key) => {
			typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
		});

		this._ordemServicoService.criar(obj).subscribe((os) => {
			this._snack.exibirToast("Registro adicionado com sucesso!", "success");
			this._router.navigate(['ordem-servico/detalhe/' + os.codOS]);
		});
	}

	refresh(): void {
		window.location.reload();
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
