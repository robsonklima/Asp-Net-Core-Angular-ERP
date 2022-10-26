import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { Acao } from 'app/core/types/acao.types';
import { AcaoService } from 'app/core/services/acao.service';

@Component({
	selector: 'app-acao-filtro',
	templateUrl: './acao-filtro.component.html'
})
export class AcaoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	@Input() sidenav: MatSidenav;
	acoes: Acao[];
	protected _onDestroy = new Subject<void>();

	constructor(
		private _acaoService: AcaoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'acao');
	}

	ngOnInit(): void {
		this.obterDados();
		this.createForm();
		this.loadData();

	}
	async obterDados() {
		this.acoes = (await this._acaoService.obterPorParametros(null).toPromise()).items
	}

	async loadData() {
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			indAtivo: [undefined],
		});

		this.form.patchValue(this.filter?.parametros);
	}

	private registrarEmitters() {

	}

	limpar() {
		super.limpar();
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}