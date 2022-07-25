import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { TipoEquipamentoService } from 'app/core/services/tipo-equipamento.service';
import { TipoEquipamento, TipoEquipamentoParameters } from 'app/core/types/tipo-equipamento.types';
import { GrupoEquipamento, GrupoEquipamentoParameters } from 'app/core/types/grupo-equipamento.types';
import { GrupoEquipamentoService } from 'app/core/services/grupo-equipamento.service';




@Component({
	selector: 'app-equipamento-filtro',
	templateUrl: './equipamento-filtro.component.html'
})
export class EquipamentoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	
	@Input() sidenav: MatSidenav;
    tipos: TipoEquipamento[] = [];
    tipoFilterCtrl: FormControl = new FormControl();
    grupos: GrupoEquipamento[] = [];
    grupoFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
        private _tipoEquipamentoService: TipoEquipamentoService,
        private _grupoEquipamentoService: GrupoEquipamentoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'equipamento');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
		
	}
	  
	async loadData() {
        this.obterTipos();
        this.obterGrupos();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			CodTipoEquips: [undefined],
            CodGrupoEquips: [undefined],
			

		});
		this.form.patchValue(this.filter?.parametros);
	}

    async obterTipos(filtro: string = '') {
		let params: TipoEquipamentoParameters = {
			filter: filtro,
			sortActive: 'nomeTipoEquip',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._tipoEquipamentoService
			.obterPorParametros(params)
			.toPromise();
		this.tipos = data.items;
	}

    async obterGrupos(filtro: string = '') {
		let params: GrupoEquipamentoParameters = {
			filter: filtro,
			sortActive: 'nomeGrupoEquip',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._grupoEquipamentoService
			.obterPorParametros(params)
			.toPromise();
		this.grupos = data.items;
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
        this.grupoFilterCtrl.valueChanges
		.pipe(
			takeUntil(this._onDestroy),
			debounceTime(700),
			distinctUntilChanged()
		)
		.subscribe(() => {
			this.obterGrupos(this.grupoFilterCtrl.value);
		});
			

	}

	limpar() {
		super.limpar();

	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}