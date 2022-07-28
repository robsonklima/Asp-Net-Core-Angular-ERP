import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { FormaPagamento, FormaPagamentoParameters } from 'app/core/types/forma-pagamento.types';
import { FormaPagamentoService } from 'app/core/services/forma-pagamento.service';


@Component({
	selector: 'app-formas-pagamento-filtro',
	templateUrl: './formas-pagamento-filtro.component.html'
})
export class FormasPagamentoFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	formasPagamentos: FormaPagamento[] = [];
	formaPagamentoFilterCtrl: FormControl = new FormControl();
	
	
	protected _onDestroy = new Subject<void>();

	constructor(

		private _formaPagamentoService: FormaPagamentoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'forma-pagamento');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterFormasPagamento();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			indAtivo: [undefined],
			CodFormaPgto: [undefined]
		});
		this.form.patchValue(this.filter?.parametros);
	}


	async obterFormasPagamento(filtro: string = '') {
		let params: FormaPagamentoParameters = {
			filter: filtro,
			sortActive: 'descFormaPagto',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._formaPagamentoService
			.obterPorParametros(params)
			.toPromise();
		this.formasPagamentos = data.items;
	}

	

	private registrarEmitters() {

		this.formaPagamentoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterFormasPagamento(this.formaPagamentoFilterCtrl.value);
			});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}