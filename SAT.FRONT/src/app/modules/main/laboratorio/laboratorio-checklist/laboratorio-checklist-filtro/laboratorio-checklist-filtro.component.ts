import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { ORStatusService } from 'app/core/services/or-status.service';
import { ORStatus, ORStatusParameters } from 'app/core/types/or-status.types';
import { ORTipo } from 'app/core/types/or-tipo.types';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { UserService } from '../../../../../core/user/user.service';

@Component({
  selector: 'app-laboratorio-checklist-filtro',
  templateUrl: './laboratorio-checklist-filtro.component.html'
})
export class LaboratorioChecklistFiltroComponent extends FilterBase implements OnInit, IFilterBase {
  @Input() sidenav: MatSidenav;
  tipos: ORTipo[] = [];
  status: ORStatus[] = [];
  statusFilterCtrl: FormControl = new FormControl();
  tipoORFilterCtrl: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _orStatusService: ORStatusService,
    protected _userService: UserService,
    protected _formBuilder: FormBuilder
  ) {
    super(_userService, _formBuilder, 'laboratorio-checklist');
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
      codORCheckList: [undefined],
    });

    this.form.patchValue(this.filter?.parametros);
  }

  private registrarEmitters() {}

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