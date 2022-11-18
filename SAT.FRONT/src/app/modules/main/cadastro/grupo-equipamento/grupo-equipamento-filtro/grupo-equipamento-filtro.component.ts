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




@Component({
	selector: 'app-grupo-equipamento-filtro',
	templateUrl: './grupo-equipamento-filtro.component.html'
})
export class GrupoEquipamentoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	
	@Input() sidenav: MatSidenav;
    tipos: TipoEquipamento[] = [];
    tipoFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
        private _tipoEquipamentoService: TipoEquipamentoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'grupo-equipamento');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
		
	}
	  
	async loadData() {
        this.obterTipos();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			CodTipoEquips: [undefined],
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
			

	}

	limpar() {
		super.limpar();

	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}