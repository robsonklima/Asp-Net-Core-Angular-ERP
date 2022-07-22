import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { Defeito } from 'app/core/types/defeito.types';
import { DefeitoService } from 'app/core/services/defeito.service';




@Component({
	selector: 'app-defeito-filtro',
	templateUrl: './defeito-filtro.component.html'
})
export class DefeitoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	
	@Input() sidenav: MatSidenav;
	defeitos: Defeito[];
	protected _onDestroy = new Subject<void>();

	constructor(
		private _defeitoService: DefeitoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'defeito');
	}

	ngOnInit(): void {
		this.obterDados();
		this.createForm();
		this.loadData();
		
	}
	async obterDados() {
	  this.defeitos = (await this._defeitoService.obterPorParametros(null).toPromise()).items
	  
	}

	async loadData() {

		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			IndAtivo: [undefined],
			

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