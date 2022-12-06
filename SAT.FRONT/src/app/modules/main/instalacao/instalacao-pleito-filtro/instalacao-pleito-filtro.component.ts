import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { FilterBase } from 'app/core/filters/filter-base';
import { IFilterBase } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { InstalacaoTipoPleito, InstalacaoTipoPleitoParameters } from 'app/core/types/instalacao-tipo-pleito.types';
import { Contrato, ContratoParameters } from 'app/core/types/contrato.types';
import { ContratoService } from 'app/core/services/contrato.service';
import { InstalacaoTipoPleitoService } from 'app/core/services/instalacao-tipo-pleito-service';
import { statusConst } from 'app/core/types/status-types';

@Component({
  selector: 'app-instalacao-pleito-filtro',
  templateUrl: './instalacao-pleito-filtro.component.html' 
})
export class InstalacaoPleitoFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;
	tipos: InstalacaoTipoPleito[] = [];
	tipoFilterCtrl: FormControl = new FormControl();
	contratos: Contrato[] = [];
	contratoFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
		private _tipoPleitoService: InstalacaoTipoPleitoService,
		private _contratoService: ContratoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'instalacao-pleito');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterTipos();
		this.obterContratos();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			CodContratos: [undefined],
			CodInstalTipoPleitos: [undefined],
		});
		this.form.patchValue(this.filter?.parametros);
	}

	async obterTipos(filtro: string = '') {
		let params: InstalacaoTipoPleitoParameters = {
			filter: filtro,
			sortActive: 'nomeTipoPleito',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._tipoPleitoService 
			.obterPorParametros(params)
			.toPromise();
		this.tipos = data.items;	
	}

	async obterContratos(filtro: string = '') {
		let params: ContratoParameters = {
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeContrato',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._contratoService
			.obterPorParametros(params)
			.toPromise();
		this.contratos = data.items;
	}

	private registrarEmitters() {
		this.tipoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {

				this.obterTipos(this.tipoFilterCtrl.value);
			});
		this.contratoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterContratos(this.contratoFilterCtrl.value);
			});		
	}

	limpar() {
		super.limpar();

	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}11:32
}