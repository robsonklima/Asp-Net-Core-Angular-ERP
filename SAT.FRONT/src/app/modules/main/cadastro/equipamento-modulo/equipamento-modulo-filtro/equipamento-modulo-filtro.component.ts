import { TipoEquipamento, TipoEquipamentoParameters } from 'app/core/types/tipo-equipamento.types';
import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { TipoEquipamentoService } from 'app/core/services/tipo-equipamento.service';
import { Equipamento, EquipamentoParameters } from 'app/core/types/equipamento.types';
import { EquipamentoService } from 'app/core/services/equipamento.service';


@Component({
	selector: 'app-equipamento-modulo-filtro',
	templateUrl: './equipamento-modulo-filtro.component.html'
})
export class EquipamentoModuloFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	equipamentos: Equipamento[] = [];
	tipoEquipamentos: TipoEquipamento[] = [];
	equipamentoFilterCtrl: FormControl = new FormControl();
	tipoEquipamentoFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(

		private _equipamentoService: EquipamentoService,
		private _tipoEquipamentoService: TipoEquipamentoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'equipamento-modulo');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterEquipamentos();
		this.obterTipoEquipamentos();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			indAtivo: [undefined],
			codEquips: [undefined],
			codTipoEquips: [undefined]
		});
		this.form.patchValue(this.filter?.parametros);
	}


	async obterEquipamentos(filtro: string = '') {
		let params: EquipamentoParameters = {
			filter: filtro,
			sortActive: 'nomeEquip',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._equipamentoService
			.obterPorParametros(params)
			.toPromise();
		this.equipamentos = data.items;
	}

	async obterTipoEquipamentos(filtro: string = '') {
		let params: TipoEquipamentoParameters = {
			filter: filtro,
			sortActive: 'nomeTipoEquip',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._tipoEquipamentoService
			.obterPorParametros(params)
			.toPromise();
		this.tipoEquipamentos = data.items;
	}

	private registrarEmitters() {

		this.equipamentoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterEquipamentos(this.equipamentoFilterCtrl.value);
			});

		this.tipoEquipamentoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterTipoEquipamentos(this.tipoEquipamentoFilterCtrl.value);
			});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}