import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { Cliente } from 'app/core/types/cliente.types';
import { ClienteService } from 'app/core/services/cliente.service';



@Component({
	selector: 'app-cliente-filtro',
	templateUrl: './cliente-filtro.component.html'
})
export class ClienteFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	
	@Input() sidenav: MatSidenav;
	clientes: Cliente[] = [];
	protected _onDestroy = new Subject<void>();

	constructor(
		private _clienteService: ClienteService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'cliente');
	}

	ngOnInit(): void {
		this.obterDados();
		this.createForm();
		this.loadData();
		
	}
	async obterDados() {
	  this.clientes = (await this._clienteService.obterPorParametros(null).toPromise()).items
	  
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