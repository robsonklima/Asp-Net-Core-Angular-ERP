import { ContratoEquipamentoDataService } from './../../../../../../core/services/contrato-equipamento-data.service';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ContratoReajusteService } from 'app/core/services/contrato-reajuste.service';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ContratoReajuste } from 'app/core/types/contrato-reajuste.types';
import { Contrato } from 'app/core/types/contrato.types';
import { Equipamento } from 'app/core/types/equipamento.types';
import { TipoContrato } from 'app/core/types/tipo-contrato.types';
import { TipoIndiceReajuste } from 'app/core/types/tipo-indice-reajuste.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { ContratoEquipamentoData } from 'app/core/types/contrato-equipamento-data.types';
import { debounceTime, delay, map, takeUntil, tap } from 'rxjs/operators';

@Component({
	selector: 'app-contrato-modelo-form',
	templateUrl: './contrato-modelo-form.component.html',
})
export class ContratoModeloFormComponent implements OnInit {
	codContrato: number;
	contrato: Contrato;
	contratoReajuste: ContratoReajuste;
	form: FormGroup;
	isAddMode: boolean;
	userSession: UsuarioSessao;
	modelos: Equipamento[] = [];
	contratoEquipData: ContratoEquipamentoData[] = [];
	searching: boolean;
	protected _onDestroy = new Subject<void>();
	tipoContrato: TipoContrato[];
	tipoReajuste: TipoIndiceReajuste[];
	modelosFiltro: FormControl = new FormControl();

	constructor(
		private _cdr: ChangeDetectorRef,
		private _route: ActivatedRoute,
		private _router: Router,
		private _userService: UserService,
		private _contratoService: ContratoService,
		private _contratoReajusteService: ContratoReajusteService,
		private _snack: CustomSnackbarService,
		private _equipamentoService: EquipamentoService,
		private _contratoEquipDataService: ContratoEquipamentoDataService,
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		this.codContrato = +this._route.snapshot.paramMap.get('codContrato');
		this.inicializarForm();
		await this.obterDados();
		this.isAddMode = true; //!this.codContrato;
		this._cdr.detectChanges();



	}

	private async obterDados() {
		this.modelos = (await this._equipamentoService.obterPorParametros({}).toPromise()).items;
		this.contratoEquipData = (await this._contratoEquipDataService.obterPorParametros({}).toPromise()).items;
		this.contrato = await this._contratoService.obterPorCodigo(this.codContrato).toPromise();
		this.modelosFiltro.valueChanges
			.pipe(
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
		// if (!this.isAddMode) {			

		// 	this.form.patchValue(this.contrato);
		// 	this.form.patchValue(this.contratoReajuste);
		// }
	}


	salvar(): void {
		this.isAddMode ? this.criar() : this.atualizar();
	}

	private atualizar(): void {
		this.form.disable();


		// const form: any = this.form.getRawValue();
		// let obj = {
		// 	...this.contrato,
		// 	...form,
		// 	...{
		// 		dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
		// 		codUsuarioManut: this.userSession.usuario?.codUsuario
		// 	}
		// };

		// Object.keys(obj).forEach((key) => {
		// 	typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
		// });

		// this._contratoService.atualizar(obj).subscribe((ct) => {
		// 	console.log(form);
		// 	console.log(ct);

		// 	let ctReajs: ContratoReajuste = {
		// 		codContrato: ct.codContrato,
		// 		codTipoIndiceReajuste: form.codTipoIndiceReajuste,
		// 		percReajuste: form.percReajuste,
		// 		indAtivo: 1,
		// 		codUsuarioCad: obj.codUsuarioManut,
		// 		dataHoraCad: obj.dataHoraManut
		// 	}

		// 	this._contratoReajusteService.atualizar(ctReajs).subscribe();
		// 	this._snack.exibirToast("Registro atualizado com sucesso!", "success");
		// 	this.form.enable();
		// });
	}

	private criar(): void {
		
		const form: any = this.form.getRawValue();

		let equip = this.modelos.find( m => m.codEquip == form.codEquip)
		
		let obj = {
			...form,
			...{
				codGrupoEquip: equip.codGrupoEquip,
				codTipoEquip: equip.codTipoEquip,
				dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioCad: this.userSession.usuario?.codUsuario
			}
		};

		console.log(obj);

		Object.keys(obj).forEach((key) => {
			typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
		});
		
		// 	this._contratoReajusteService.criar(ctReajs).subscribe();
		// 	this._snack.exibirToast("Registro adicionado com sucesso!", "success");
		// 	this._router.navigate(['contrato/' + ct.codContrato]);
	}

	private inicializarForm(): void {
		this.form = new FormGroup({
			codEquip: new FormControl({ value: undefined }, Validators.required),
			codMagnus: new FormControl(undefined, Validators.required),
			vlrUnitario: new FormControl(undefined, Validators.required),
			vlrInstalacao: new FormControl(undefined, Validators.required),
			qtdEquip: new FormControl(undefined, Validators.required),
			qtdLimDiaEnt: new FormControl(undefined, Validators.required),
			qtdLimDiaIns: new FormControl(undefined, Validators.required),
			qtdDiaGarantia: new FormControl(undefined, Validators.required),
			codTipoGarantia: new FormControl(undefined, Validators.required),
			percValorEnt: new FormControl(undefined, Validators.required),
			percValorIns: new FormControl(undefined, Validators.required),
			codContratoEquipDataEnt: new FormControl(undefined, Validators.required),
			codContratoEquipDataIns: new FormControl(undefined, Validators.required),
			codContratoEquipDataGar: new FormControl(undefined),
			dataRecDM: new FormControl(undefined),
			dataInicioMTBF: new FormControl(undefined),
			dataFimMTBF: new FormControl(undefined),
			dataGar: new FormControl(undefined),
		});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
