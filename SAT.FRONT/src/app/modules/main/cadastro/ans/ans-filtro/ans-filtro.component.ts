import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';

@Component({
	selector: 'app-ans-filtro',
  	templateUrl: './ans-filtro.component.html'
})
export class ANSFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	@Input() sidenav: MatSidenav;
	protected _onDestroy = new Subject<void>();

	constructor(
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'equipamento-contrato');
	}

	ngOnInit(): void {
		this.createForm();

    this.loadData();
	}

	async loadData() {
		
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			nomeANS: [undefined],
		});

		this.form.patchValue(this.filter?.parametros);
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
