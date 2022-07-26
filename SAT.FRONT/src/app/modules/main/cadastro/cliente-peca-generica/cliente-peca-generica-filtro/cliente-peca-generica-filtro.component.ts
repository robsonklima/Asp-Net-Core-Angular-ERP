import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { PecaStatus, PecaStatusParameters } from 'app/core/types/peca.types';
import { PecaStatusService } from 'app/core/services/peca-status.service';




@Component({
	selector: 'app-cliente-peca-generica-filtro',
	templateUrl: './cliente-peca-generica-filtro.component.html'
})
export class ClientePecaGenericaFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	
	@Input() sidenav: MatSidenav;
    status: PecaStatus[] = [];
    statusFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
        private _pecaStatusService: PecaStatusService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'cliente-peca-generica');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
		
	}
	  
	async loadData() {
        this.obterStatus();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			CodPecaStatus: [undefined],

			

		});
		this.form.patchValue(this.filter?.parametros);
	}

    async obterStatus(filtro: string = '') {
		let params: PecaStatusParameters = {
			filter: filtro,
			sortActive: 'nomeStatus',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._pecaStatusService
			.obterPorParametros(params)
			.toPromise();
		this.status = data.items;
	}




	private registrarEmitters() {
        this.statusFilterCtrl.valueChanges
		.pipe(
			takeUntil(this._onDestroy),
			debounceTime(700),
			distinctUntilChanged()
		)
		.subscribe(() => {
	    
        this.obterStatus(this.statusFilterCtrl.value);
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