import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DespesaConfiguracaoCombustivelService } from 'app/core/services/despesa-configuracao-combustivel.service';
import { FilialService } from 'app/core/services/filial.service';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { DespesaConfiguracaoCombustivel } from 'app/core/types/despesa-configuracao-combustivel.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { statusConst } from 'app/core/types/status-types';
import { UnidadeFederativa, UnidadeFederativaParameters } from 'app/core/types/unidade-federativa.types';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { Location } from '@angular/common';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';

@Component({
	selector: 'app-valores-combustivel-form',
	templateUrl: './valores-combustivel-form.component.html'
})
export class ValoresCombustivelFormComponent implements OnInit, OnDestroy {

	protected _onDestroy = new Subject<void>();
	public valorCombustivel: DespesaConfiguracaoCombustivel;
	public loading: boolean = true;
	public codDespesaConfiguracaoCombustivel: number;
	public isAddMode: boolean;
	public form: FormGroup;
	userSession: UsuarioSessao;

	public filiais: Filial[] = [];
	public unidadesFederativas: UnidadeFederativa[] = [];
	filialFiltro: FormControl = new FormControl();
	unidadeFederativaFiltro: FormControl = new FormControl();

	constructor(
		private _formBuilder: FormBuilder,
		private _snack: CustomSnackbarService,
		private _route: ActivatedRoute,
		private _despesaConfiguracaoCombustivelService: DespesaConfiguracaoCombustivelService,
		private _filialService: FilialService,
		private _unidadeFederativaService: UnidadeFederativaService,
		public _location: Location,
		private _userService: UserService
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {

		this.codDespesaConfiguracaoCombustivel = +this._route.snapshot.paramMap.get('codDespesaConfiguracaoCombustivel');
		this.isAddMode = !this.codDespesaConfiguracaoCombustivel;
		this.inicializarForm();
		this.obterFiliais();
		this.obterUFs();

		if (!this.isAddMode) {
			this._despesaConfiguracaoCombustivelService.obterPorCodigo(this.codDespesaConfiguracaoCombustivel)
				.pipe(first())
				.subscribe(data => {
					this.form.patchValue(data);
					this.valorCombustivel = data;
				});
		}

		this.loading = false;
	}

	private inicializarForm() {
		this.form = this._formBuilder.group({
			codUF: [undefined, Validators.required],
			codFilial: [undefined, Validators.required],
			precoLitro: [undefined, Validators.required],
		});
	}

	private async obterFiliais() {
		const params: FilialParameters = {
			sortActive: 'nomeFilial',
			sortDirection: 'asc',
			indAtivo: statusConst.ATIVO,
			pageSize: 100
		}
		const data = await this._filialService.obterPorParametros(params).toPromise();
		this.filiais = data.items;
	}

	private async obterUFs() {
		const params: UnidadeFederativaParameters = {
			sortActive: 'siglaUF',
			sortDirection: 'asc',
			codPais: 1,
			pageSize: 100
		}
		const data = await this._unidadeFederativaService.obterPorParametros(params).toPromise();
		this.unidadesFederativas = data.items;
	}

	public salvar(): void {
		this.isAddMode ? this.criar() : this.atualizar();
	}

	atualizar(): void {
		const form: any = this.form.getRawValue();


		let obj = {
			...this.valorCombustivel,
			...form,
			...{
				dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioManut: this.userSession.usuario?.codUsuario
			}
		};

		this._despesaConfiguracaoCombustivelService.atualizar(obj).subscribe(() => {
			this._snack.exibirToast(`Preço atualizado com sucesso!`, "success");
			this._location.back();
		});
	}

	criar(): void {
		const form = this.form.getRawValue();

		let obj = {
			...this.valorCombustivel,
			...form
		};

		this._despesaConfiguracaoCombustivelService.criar(obj).subscribe(() => {
			this._snack.exibirToast(`Valor combustível criado com sucesso!`, "success");
			this._location.back();
		});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}

}
