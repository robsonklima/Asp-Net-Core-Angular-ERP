import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { ORTipoService } from 'app/core/services/or-tipo.service';
import { ORStatusService } from 'app/core/services/or-status.service';
import { ORTipo, ORTipoParameters } from 'app/core/types/or-tipo.types';
import { ORStatus, ORStatusParameters } from 'app/core/types/or-status.types';

@Component({
  selector: 'app-laboratorio-processo-reparo-filtro',
  templateUrl: './laboratorio-processo-reparo-filtro.component.html'
})
export class LaboratorioProcessoReparoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
  @Input() sidenav: MatSidenav;
  tipos: ORTipo[] = [];
  status: ORStatus[] = [];
  statusFilterCtrl: FormControl = new FormControl();
  tipoORFilterCtrl: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _orTipoService: ORTipoService,
    private _orStatusService: ORStatusService,
    protected _userService: UserService,
    protected _formBuilder: FormBuilder
  ) {
    super(_userService, _formBuilder, 'processo-reparo');
  }

  ngOnInit(): void {
    this.createForm();
    this.loadData();
  }

  async loadData() {
    this.obterTipos();
    this.obterStatus();
    this.registrarEmitters();
  }

  createForm(): void {
    this.form = this._formBuilder.group({
      codOR: [undefined],
      codTiposOR: [undefined],
      codStatus: [undefined],
    });

    this.form.patchValue(this.filter?.parametros);
  }

  private registrarEmitters() {
    
  }

  async obterTipos(filtro: string = '') {
		let params: ORTipoParameters = {
			filter: filtro,
			sortActive: 'descricaoTipo',
			sortDirection: 'asc',
			pageSize: 1000
		};

		const data = await this._orTipoService
			.obterPorParametros(params)
			.toPromise();

		this.tipos = data.items;
	}

  async obterStatus(filtro: string = '') {
		let params: ORStatusParameters = {
			filter: filtro,
			sortActive: 'descStatus',
			sortDirection: 'asc',
			pageSize: 1000
		};

		const data = await this._orStatusService
			.obterPorParametros(params)
			.toPromise();

		this.status = data.items;
	}

  limpar() {
    super.limpar();
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}