import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { IFilterBase } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { COMMA, ENTER } from '@angular/cdk/keycodes';

@Component({
	selector: 'app-instalacao-filtro',
	templateUrl: './instalacao-filtro.component.html'
})
export class InstalacaoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	@Input() sidenav: MatSidenav;
	readonly separatorKeysCodes = [ENTER, COMMA] as const;
	protected _onDestroy = new Subject<void>();

	constructor(
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'instalacao');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	loadData(): void {
		this.instalacoes = this.filter?.parametros['codInstalacoes'] ? this.filter?.parametros['codInstalacoes']?.split(',') : [];
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codInstalacoes: [undefined]
		});
		this.form.patchValue(this.filter?.parametros);
	}

	add(event: MatChipInputEvent): void {
		const value = (event.value || '').trim();
		if (value) {
			this.instalacoes.push(value);
		}
		event.chipInput!.clear();

		this.form.controls['codInstalacoes'].setValue(this.instalacoes?.join(','));
	}

	paste(event: ClipboardEvent): void {
		event.preventDefault();
		event.clipboardData
			.getData('Text')
			.split(/;|,|\n/)
			.forEach(value => {
				if (value.trim()) {
					this.instalacoes.push(value.trim());
				}
			})
		this.form.controls['codInstalacoes'].setValue(this.instalacoes?.join(','));
	}

	remove(instalacao: any): void {
		const index = this.instalacoes.indexOf(instalacao);

		if (index >= 0) {
			this.instalacoes.splice(index, 1);
		}

		this.form.controls['codInstalacoes'].setValue(this.instalacoes?.join(','));
	}

	async registrarEmitters() {
		this.form.controls['codInstalacoes'].valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				if (this.chamadosPerto == null) {
					this.chamadosPerto = this.filter?.parametros['codInstalacoes'] ? this.filter?.parametros['codInstalacoes']?.split(',') : [];
				}
			});
	}

	limpar() {
		super.limpar();
		this.instalacoes = [];
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
