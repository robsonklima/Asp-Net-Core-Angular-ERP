import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { FilialService } from 'app/core/services/filial.service';
import { Filial, FilialParameters } from 'app/core/types/filial.types';


@Component({
	selector: 'app-autorizada-filtro',
	templateUrl: './autorizada-filtro.component.html'
})
export class AutorizadaFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	filiais: Filial[] = [];
	filialFilterCtrl: FormControl = new FormControl();

	protected _onDestroy = new Subject<void>();

	constructor(
		private _filialService: FilialService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'autorizada');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterFiliais();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			indAtivo: [undefined],
			codFiliais: [undefined]
		});
		this.form.patchValue(this.filter?.parametros);
	}

	async obterFiliais(filtro: string = '') {
		let params: FilialParameters = {
			filter: filtro,
			sortActive: 'nomeFilial',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._filialService
			.obterPorParametros(params)
			.toPromise();
		this.filiais = data.items;
	}


	private registrarEmitters() {

		this.filialFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterFiliais(this.filialFilterCtrl.value);
			});

	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}