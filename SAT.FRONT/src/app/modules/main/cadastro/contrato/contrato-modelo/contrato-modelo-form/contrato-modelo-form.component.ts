import { ContratoEquipamentoDataService } from './../../../../../../core/services/contrato-equipamento-data.service';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ContratoReajusteService } from 'app/core/services/contrato-reajuste.service';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { TipoContratoService } from 'app/core/services/tipo-contrato.service';
import { TipoIndiceReajusteService } from 'app/core/services/tipo-indice-reajuste.service';
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
		private _tipoContratoService: TipoContratoService,
		private _tipoIndiceReajusteService: TipoIndiceReajusteService,
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		this.inicializarForm();
		this.codContrato = +this._route.snapshot.paramMap.get('codContrato');
    	this.modelos = (await this._equipamentoService.obterPorParametros({}).toPromise()).items;
		this.contratoEquipData = (await this._contratoEquipDataService.obterPorParametros({}).toPromise()).items;
		
		this.isAddMode = true; //!this.codContrato;
		this._cdr.detectChanges();
		// Main Obj
		// await this.obterClientes();
		// await this.obterDados();
	}

	private async obterDados() {
		this.tipoContrato = (await this._tipoContratoService.obterPorParametros({}).toPromise()).items;
		this.tipoReajuste = (await this._tipoIndiceReajusteService.obterPorParametros({}).toPromise()).items;
		this.contratoReajuste = (await this._contratoReajusteService
			.obterPorParametros({ codContrato: this.codContrato, indAtivo: 1 })
			.toPromise()).items.pop();
		if (!this.isAddMode) {
			let data = await this._contratoService.obterPorCodigo(this.codContrato).toPromise();
			this.contrato = data;

			this.form.patchValue(this.contrato);
			this.form.patchValue(this.contratoReajuste);
		}
	}

	salvar(): void {
		this.isAddMode ? this.criar() : this.atualizar();
	}

	private atualizar(): void {
		this.form.disable();

		const form: any = this.form.getRawValue();
		let obj = {
			...this.contrato,
			...form,
			...{
				dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioManut: this.userSession.usuario?.codUsuario
			}
		};

		Object.keys(obj).forEach((key) => {
			typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
		});
		
		this._contratoService.atualizar(obj).subscribe((ct) => {
			console.log(form);
			console.log(ct);
			
			let ctReajs: ContratoReajuste = {
				codContrato: ct.codContrato,
				codTipoIndiceReajuste: form.codTipoIndiceReajuste,
				percReajuste: form.percReajuste,
				indAtivo: 1,
				codUsuarioCad: obj.codUsuarioManut,
				dataHoraCad: obj.dataHoraManut
			}

			this._contratoReajusteService.atualizar(ctReajs).subscribe();
			this._snack.exibirToast("Registro atualizado com sucesso!", "success");
			this.form.enable();
		});
	}

	private criar(): void {
		const form: any = this.form.getRawValue();
		let obj = {
			...this.contrato,
			...form,
			...{
				indAtivo: 1,
				dataHoraSolicitacao: moment().format('YYYY-MM-DD HH:mm:ss'),
				dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioCad: this.userSession.usuario?.codUsuario,
			}
		};

		Object.keys(obj).forEach((key) => {
			typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
		});

		this._contratoService.criar(obj).subscribe((ct) => {
			
			let ctReajs: ContratoReajuste = {
				codContrato: ct.codContrato,
				codTipoIndiceReajuste: form.codTipoIndiceReajuste,
				percReajuste: form.percReajuste,
				indAtivo: 1,
				codUsuarioCad: obj.codUsuarioCad,
				dataHoraCad: obj.dataHoraCad
			}

			this._contratoReajusteService.criar(ctReajs).subscribe();
			this._snack.exibirToast("Registro adicionado com sucesso!", "success");
			this._router.navigate(['contrato/' + ct.codContrato]);
		});
	}

	private inicializarForm(): void {
		this.form =  new FormGroup({
			codModelo: new FormControl({value: undefined }, Validators.required),
			codMagnus: new FormControl(undefined, Validators.required),
			vlrUnitario: new FormControl(undefined, Validators.required),
			vlrInstalacao: new FormControl(undefined, Validators.required),
			qtdEquip: new FormControl(undefined, Validators.required),
			qtdLimDiaEnt: new FormControl(undefined, Validators.required),
			qtdLimDiaIns: new FormControl(undefined, Validators.required),
			qtdDiaGarantia: new FormControl(undefined, Validators.required),
			codTipoGarantia: new FormControl(undefined, Validators.required),
			codContratoEquipDataEnt: new FormControl(undefined, Validators.required),
			codContratoEquipDataGar: new FormControl(undefined),
			codContratoEquipDataIns: new FormControl(undefined, Validators.required),
			percValorEnt: new FormControl(undefined, Validators.required),
			percValorIns: new FormControl(undefined, Validators.required),
			dataRecDM: new FormControl(undefined),
			dataInicioMTBF: new FormControl(undefined),
			dataFimMTBF: new FormControl(undefined),
			dataGar: new FormControl(undefined),
		  });
		
		
		
		
		// this._formBuilder.group({
		// 	codContrato: [
		// 		{
		// 			value: undefined,
		// 			disabled: true,
		// 		}, [Validators.required]
		// 	],
		// 	codLogix: [undefined, Validators.required],
		// 	codModelo: [undefined, Validators.required],
		// 	codGarantia: [undefined, Validators.required],
		// 	codTipoContrato: [undefined, Validators.required],
		// 	codTipoIndiceReajuste: [undefined, Validators.required],
		// 	percReajuste: [undefined],
		// 	nroContrato: [undefined, Validators.required],
		// 	nomeContrato: [undefined, Validators.required],
		// 	dataContrato: [undefined, Validators.required],
		// 	dataAssinatura: [undefined, Validators.required],
		// 	dataInicioVigencia: [undefined, Validators.required],
		// 	dataFimVigencia: [undefined, Validators.required],
		// 	dataInicioPeriodoReajuste: [undefined],
		// 	dataFimPeriodoReajuste: [undefined],
		// 	nomeResponsavelPerto: [undefined, Validators.required],
		// 	nomeResponsavelCliente: [undefined, Validators.required],
		// 	objetoContrato: [undefined],
		// 	semCobertura: [undefined],
		// 	valTotalContrato: [undefined, Validators.required],
		// 	indPermitePecaEspecifica: [undefined, Validators.required],
		// 	numDiasSubstEquip: [undefined]

		// });
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
