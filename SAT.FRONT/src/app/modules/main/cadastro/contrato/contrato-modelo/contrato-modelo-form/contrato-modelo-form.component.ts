import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { ContratoEquipamentoService } from 'app/core/services/contrato-equipamento.service';
import { ContratoEquipamento } from './../../../../../../core/types/contrato-equipamento.types';
import { ContratoEquipamentoDataService } from './../../../../../../core/services/contrato-equipamento-data.service';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Contrato } from 'app/core/types/contrato.types';
import { Equipamento } from 'app/core/types/equipamento.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { ContratoEquipamentoData } from 'app/core/types/contrato-equipamento-data.types';
import { debounceTime, delay, filter, map, takeUntil, tap } from 'rxjs/operators';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

@Component({
	selector: 'app-contrato-modelo-form',
	templateUrl: './contrato-modelo-form.component.html',
})
export class ContratoModeloFormComponent implements OnInit {
	codContrato: number;
	codEquip: number;
	contrato: Contrato;
	form: FormGroup;
	isLoading: boolean;
	isAddMode: boolean;
	userSession: UsuarioSessao;
	modelos: Equipamento[] = [];
	contratoEquip: ContratoEquipamento;
	contratoEquipData: ContratoEquipamentoData[] = [];
	searching: boolean;
	protected _onDestroy = new Subject<void>();
	modelosFiltro: FormControl = new FormControl();

	constructor(
		private _cdr: ChangeDetectorRef,
		private _route: ActivatedRoute,
		private _userService: UserService,
		private _contratoService: ContratoService,
		private _snack: CustomSnackbarService,
		private _router: Router,
		private _equipamentoService: EquipamentoService,
		private _contratoEquipDataService: ContratoEquipamentoDataService,
		private _contratoEquipamentoService: ContratoEquipamentoService,

	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		this.isLoading = true;
		this.codContrato = +this._route.snapshot.paramMap.get('codContrato');
		this.codEquip = +this._route.snapshot.paramMap.get('codEquip');
		this.inicializarForm();
		this.isAddMode = !this.codEquip;
		await this.obterDados();
		this._cdr.detectChanges();
	}

	private async obterDados() {
		if (!this.isAddMode) {
			this.contratoEquip =
				await this._contratoEquipamentoService
					.obterPorCodigo(this.codContrato, this.codEquip).toPromise();

			this.form.patchValue(this.contratoEquip);
		}
		this.modelos = (await this._equipamentoService.obterPorParametros({}).toPromise()).items;
		this.contratoEquipData = (await this._contratoEquipDataService.obterPorParametros({}).toPromise()).items;
		this.contrato = await this._contratoService.obterPorCodigo(this.codContrato).toPromise();
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
		this.isLoading = false;
	}


	salvar(): void {
		this.isAddMode ? this.criar() : this.atualizar();
	}

	private atualizar(): void {
		this.form.disable();
		const form: any = this.form.getRawValue();
		let equip = this.modelos.find(m => m.codEquip == form.codEquip)
		let obj = {
			...this.contratoEquip,
			...form,
			...{
				codContrato: this.codContrato,
				codGrupoEquip: equip.codGrupoEquip,
				codTipoEquip: equip.codTipoEquip,
				dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioManut: this.userSession.usuario?.codUsuario
			}
		};

		Object.keys(obj).forEach((key) => {
			typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
		});

		this._contratoEquipamentoService.atualizar(obj).subscribe(() => {
			this._snack.exibirToast("Registro atualizado com sucesso!", "success");
		});

	}

	private criar(): void {		
		this.form.disable();
		const form: any = this.form.getRawValue();
		let equip = this.modelos.find(m => m.codEquip == form.codEquip)
		let obj = {
			...form,
			...{
				codContrato: this.codContrato,
				codGrupoEquip: equip.codGrupoEquip,
				codTipoEquip: equip.codTipoEquip,
				dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioCad: this.userSession.usuario?.codUsuario
			}
		};

		Object.keys(obj).forEach((key) => {
			typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
		});

		this._contratoEquipamentoService.criar(obj).subscribe(() => {

			this._snack.exibirToast("Registro adicionado com sucesso!", "success");
			this._router.navigate(['contrato/' + this.codContrato]);

		});
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
			dataRecDM: new FormControl({value: undefined, disabled: true}),
			dataInicioMTBF: new FormControl({value: undefined, disabled: true}),
			dataFimMTBF: new FormControl({value: undefined, disabled: true}),
			dataGar: new FormControl(undefined),
		});

		// if (!this.isAddMode) {
		// 	this.form.controls['codEquip'].disable();
		// }
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
