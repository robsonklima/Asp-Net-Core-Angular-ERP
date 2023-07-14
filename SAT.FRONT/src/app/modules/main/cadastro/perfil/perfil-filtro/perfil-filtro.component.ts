import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { PerfilSetor, PerfilSetorParameters } from 'app/core/types/perfil-setor.types';
import { SetorService } from 'app/core/services/setor.service';
import { Setor, SetorParameters } from 'app/core/types/setor.types';
import { PerfilSetorService } from 'app/core/services/perfil-setor.service';


@Component({
	selector: 'app-perfil-filtro',
	templateUrl: './perfil-filtro.component.html'
})
export class PerfilFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	perfis: PerfilSetor[] = [];
	setores: Setor[] = [];
    perfisFilterCtrl: FormControl = new FormControl();
	setoresFilterCtrl: FormControl = new FormControl();
	
	
	protected _onDestroy = new Subject<void>();

	constructor(
		private perfilSetorSrv: PerfilSetorService,
		private setorSrv: SetorService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'perfil');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterSetores();
		this.registrarEmitters();
		this.aoSelecionarSetor();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codSetor:[undefined],
			codSetores: [undefined],
			codPerfil:[undefined],
			indAtivo:[undefined]
		});
		this.form.patchValue(this.filter?.parametros);
	}

	async obterPerfisSetores(filtro: string = '') {
		let params: PerfilSetorParameters = {
			filter: filtro,
			sortActive: 'codPerfil',
			codSetor: this.form.controls['codSetor'].value,
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this.perfilSetorSrv
			.obterPorParametros(params)
			.toPromise();
		this.perfis = data.items;
	}

	async obterSetores(filtro: string = '') {
		let params: SetorParameters = {
			filter: filtro,
			sortActive: 'nomeSetor',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this.setorSrv
			.obterPorParametros(params)
			.toPromise();
		this.setores = data.items;
	}

	aoSelecionarSetor() {
		if (
			this.form.controls['codSetor'].value &&
			this.form.controls['codSetor'].value != ''
		) {
			this.obterPerfisSetores();
			this.form.controls['codPerfil'].enable();
		}
		else {
			this.form.controls['codPerfil'].disable();
		}

		this.form.controls['codSetor']
			.valueChanges
			.subscribe(() => {
				if (this.form.controls['codSetor'].value && this.form.controls['codSetor'].value != '') {
					this.obterPerfisSetores();
					this.form.controls['codPerfil'].enable();
				}
				else {
					this.form.controls['codPerfil'].setValue(null);
					this.form.controls['codPerfil'].disable();
				}
			});
	}


	private registrarEmitters() {

			this.setoresFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
			
			this.obterSetores(this.setoresFilterCtrl.value);
			});

			this.perfisFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
			
			this.obterPerfisSetores(this.perfisFilterCtrl.value);
			});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}