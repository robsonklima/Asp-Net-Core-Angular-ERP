import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { FerramentaTecnico, FerramentaTecnicoParameters } from 'app/core/types/ferramenta-tecnico.types';
import { FerramentaTecnicoService } from 'app/core/services/ferramenta-tecnico.service';


@Component({
	selector: 'app-ferramenta-tecnico-filtro',
	templateUrl: './ferramenta-tecnico-filtro.component.html'
})
export class FerramentaTecnicoFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	ferramentasTecnicos: FerramentaTecnico[] = [];
	ferramentaTecnicoFilterCtrl: FormControl = new FormControl();
	
	
	protected _onDestroy = new Subject<void>();

	constructor(

		private _ferramentaTecnicoService: FerramentaTecnicoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'ferramenta-tecnico');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterFerramentasTecnicos();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			indAtivo: [undefined],
			ferramentasTecnicos: [undefined]
		});
		this.form.patchValue(this.filter?.parametros);
	}


	async obterFerramentasTecnicos(filtro: string = '') {
		let params: FerramentaTecnicoParameters = {
			filter: filtro,
			sortActive: 'nome',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._ferramentaTecnicoService
			.obterPorParametros(params)
			.toPromise();
		this.ferramentasTecnicos = data.items;
	}

	

	private registrarEmitters() {

		this.ferramentaTecnicoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterFerramentasTecnicos(this.ferramentaTecnicoFilterCtrl.value);
			});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}