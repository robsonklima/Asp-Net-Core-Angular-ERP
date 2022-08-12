import { EquipamentoService } from 'app/core/services/equipamento.service';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Contrato, ContratoServico } from 'app/core/types/contrato.types';
import { Equipamento } from 'app/core/types/equipamento.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { debounceTime, delay, filter, map, takeUntil, tap } from 'rxjs/operators';
import { AcordoNivelServico } from 'app/core/types/acordo-nivel-servico.types';
import { TipoServico } from 'app/core/types/tipo-servico.types';
import { AcordoNivelServicoService } from 'app/core/services/acordo-nivel-servico.service';
import { TipoServicoService } from 'app/core/services/tipo-servico.service';
import { ContratoServicoService } from 'app/core/services/contrato-servico.service';
import { Subject } from 'rxjs';
import moment from 'moment';

@Component({
	selector: 'app-contrato-servico-form',
	templateUrl: './contrato-servico-form.component.html',
})
export class ContratoServicoFormComponent implements OnInit {
	codContrato: number;
	codEquip: number;
	codServico: number;
	codSLA: number;
	codContratoServico: number;
	contrato: Contrato;
	form: FormGroup;
	isLoading: boolean = true;
	isAddMode: boolean;
	userSession: UsuarioSessao;
	modelos: Equipamento[] = [];
	slas: AcordoNivelServico[] = [];
	servicos: TipoServico[] = [];
	contratoServico: ContratoServico;
	searching: boolean;
	protected _onDestroy = new Subject<void>();
	modelosFiltro: FormControl = new FormControl();
	slasFiltro: FormControl = new FormControl();
	servicosFiltro: FormControl = new FormControl();

	constructor(
		private _cdr: ChangeDetectorRef,
		private _route: ActivatedRoute,
		private _userService: UserService,
		private _contratoService: ContratoService,
		private _contratoServicoService: ContratoServicoService,
		private _acordoNivelServicoService: AcordoNivelServicoService,
		private _tipoServicoService: TipoServicoService,
		private _snack: CustomSnackbarService,
		private _router: Router,
		private _equipamentoService: EquipamentoService,

	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		this.codContrato = +this._route.snapshot.paramMap.get('codContrato');
		this.codEquip = +this._route.snapshot.paramMap.get('codEquip');
		this.codContratoServico = +this._route.snapshot.paramMap.get('codContratoServico');
		this.codSLA = +this._route.snapshot.paramMap.get('codSLA');
		this.codServico = +this._route.snapshot.paramMap.get('codServico');

		this.inicializarForm();
		this.registrarEmitters();
		this.isAddMode = !this.codContratoServico;
		await this.obterDados();
		this._cdr.detectChanges();
	}

	private async obterDados() {
		if (!this.isAddMode) {
			this.contratoServico = await this._contratoServicoService
				.obterPorCodigo(this.codContratoServico).toPromise();

			this.modelos = (await this._equipamentoService
				.obterPorParametros({ pageSize: 1, codEquip: this.contratoServico.codEquip }).toPromise()).items;

			this.servicos = (await this._tipoServicoService
				.obterPorParametros({ pageSize: 1, codServico: this.contratoServico.codServico }).toPromise()).items;

			this.slas = (await this._acordoNivelServicoService
				.obterPorParametros({ pageSize: 1, codSLA: this.contratoServico.codSLA }).toPromise()).items;

			this.form.patchValue(this.contratoServico);
		} else {
			this.modelos = (await this._equipamentoService.obterPorParametros({ pageSize: 50 }).toPromise()).items;
			this.servicos = (await this._tipoServicoService.obterPorParametros({ pageSize: 50 }).toPromise()).items;
			this.slas = (await this._acordoNivelServicoService.obterPorParametros({ pageSize: 50 }).toPromise()).items;
		}

		this.contrato = await this._contratoService.obterPorCodigo(this.codContrato).toPromise();
		this.isLoading = false;
	}

	private inicializarForm(): void {
		this.form = new FormGroup({
			codEquip: new FormControl({ value: undefined }, Validators.required),
			codServico: new FormControl(undefined, Validators.required),
			codSLA: new FormControl(undefined, Validators.required),
			valor: new FormControl(undefined, Validators.required),
		});
	}

	private registrarEmitters() {
		this.modelosFiltro.valueChanges
			.pipe(filter(query => !!query),
				tap(() => this.searching = true),
				takeUntil(this._onDestroy),
				debounceTime(700),
				map(async query => {
					const data = await this._equipamentoService.obterPorParametros({
						sortActive: 'NomeEquip',
						sortDirection: 'asc',
						filter: query,
						pageSize: 100,
					}).toPromise();

					return data.items.slice();
				}),
				delay(500),
				takeUntil(this._onDestroy)
			)
			.subscribe(async data => {
				this.searching = false;
				this.modelos = await data;
			},
				() => {
					this.searching = false;
				}
			);

		this.slasFiltro.valueChanges
			.pipe(filter(query => !!query),
				tap(() => this.searching = true),
				takeUntil(this._onDestroy),
				debounceTime(700),
				map(async query => {
					const data = await this._acordoNivelServicoService.obterPorParametros({
						sortActive: 'NomeSLA',
						sortDirection: 'asc',
						filter: query,
						pageSize: 100,
					}).toPromise();

					return data.items.slice();
				}),
				delay(500),
				takeUntil(this._onDestroy)
			)
			.subscribe(async data => {
				this.searching = false;
				this.slas = await data;
			},
				() => {
					this.searching = false;
				}
			);

		this.servicosFiltro.valueChanges
			.pipe(filter(query => !!query),
				tap(() => this.searching = true),
				takeUntil(this._onDestroy),
				debounceTime(700),
				map(async query => {
					const data = await this._tipoServicoService.obterPorParametros({
						sortActive: 'NomeServico',
						sortDirection: 'asc',
						filter: query,
						pageSize: 100,
					}).toPromise();

					return data.items.slice();
				}),
				delay(500),
				takeUntil(this._onDestroy)
			)
			.subscribe(async data => {
				this.searching = false;
				this.servicos = await data;
			},
				() => {
					this.searching = false;
				}
			);
	}

	salvar(): void {
		this.isAddMode ? this.criar() : this.atualizar();
	}

	private atualizar(): void {
		this.form.disable();
		const form: any = this.form.getRawValue();
		let obj = {
			...this.contratoServico,
			...form,
			...{
				codContrato: this.codContrato,
				dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioManut: this.userSession.usuario?.codUsuario
			}
		};

		Object.keys(obj).forEach((key) => {
			typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
		});

		this._contratoServicoService.atualizar(obj).subscribe(() => {
			this._snack.exibirToast("Registro atualizado com sucesso!", "success");
		});

	}

	private criar(): void {
		this.form.disable();
		const form: any = this.form.getRawValue();
		let obj = {
			...form,
			...{
				codContrato: this.codContrato,
				dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioCad: this.userSession.usuario?.codUsuario
			}
		};

		Object.keys(obj).forEach((key) => {
			typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
		});

		this._contratoServicoService.criar(obj).subscribe(() => {
			this._snack.exibirToast("Registro adicionado com sucesso!", "success");
			this._router.navigate(['contrato/' + this.codContrato]);
		});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
