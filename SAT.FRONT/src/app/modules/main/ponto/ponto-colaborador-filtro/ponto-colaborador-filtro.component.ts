import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { PontoPeriodoUsuarioStatusService } from 'app/core/services/ponto-periodo-usuario-status.service';
import { IFilterBase } from 'app/core/types/filtro.types';
import { PontoPeriodoUsuarioStatus, PontoPeriodoUsuarioStatusParameters } from 'app/core/types/ponto-periodo-usuario-status.types';
import { PontoPeriodo } from 'app/core/types/ponto-periodo.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-ponto-colaborador-filtro',
  templateUrl: './ponto-colaborador-filtro.component.html'
})
export class PontoColaboradorFiltroComponent  extends FilterBase implements OnInit, IFilterBase {
	@Input() sidenav: MatSidenav;
	pontosPeriodoUsuarioStatus: PontoPeriodoUsuarioStatus[];
	pontosPeriodo: PontoPeriodo[];
	pontoPeriodoUsuarioStatusFiltro: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

  constructor(
		private _pontoPeriodoUsuarioStatusService: PontoPeriodoUsuarioStatusService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'ponto-colaborador');
	}
	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterPontoPeriodoUsuarioStatus();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codPontoPeriodoUsuarioStatus: [undefined],
		});

		this.form.patchValue(this.filter?.parametros);
	}

	async obterPontoPeriodoUsuarioStatus(filtro: string = '') {
		let params: PontoPeriodoUsuarioStatusParameters = {
			filter: filtro,
			sortActive: 'descricao',
			sortDirection: 'asc',
			pageSize: 1000
		};

		const data = await this._pontoPeriodoUsuarioStatusService
			.obterPorParametros(params)
			.toPromise();
							
		this.pontosPeriodoUsuarioStatus = data.items;
	}

	limpar() {
		super.limpar();
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}