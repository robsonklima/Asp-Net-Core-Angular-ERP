import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { FilialService } from 'app/core/services/filial.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Filial } from 'app/core/types/filial.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-agenda-tecnico-filtro',
  templateUrl: './agenda-tecnico-filtro.component.html'
})
export class AgendaTecnicoFiltroComponent extends FilterBase implements OnInit, IFilterBase
{
  tecnicos: Tecnico[] = [];
  filiais: Filial[] = [];
  @Input() sidenav: MatSidenav;
  protected _onDestroy = new Subject<void>();

  constructor (
    private _tecnicoSvc: TecnicoService,
    private _filialSvc: FilialService,
    protected _userService: UserService,
    protected _formBuilder: FormBuilder
  )
  {
    super(_userService, _formBuilder, 'agenda-tecnico');
  }

  createForm(): void
  {
    this.form = this._formBuilder.group({
      codTecnicos: [undefined],
      codFiliais: [undefined]
    });

    this.form.patchValue(this.filter?.parametros);
  }

  loadData(): void
  {
    this.obterTecnicosAoEscolherFilial();
    this.obterFiliais();
    this.configurarFiltro();
  }

  ngOnInit(): void
  {
    this.createForm();
    this.loadData();
  }

  configurarFiltro()
  {
    if (this.form.controls['codFiliais'].value && this.form.controls['codFiliais'].value != "")
      this.obterTecnicos(this.form.controls['codFiliais'].value);
  }

  private async obterTecnicosAoEscolherFilial()
  {
    this.form.controls['codFiliais'].valueChanges.subscribe(async codFilial =>
    {
      this.obterTecnicos(codFilial);
    });
  }

  private async obterTecnicos(codFilial: string)
  {
    const data = await this._tecnicoSvc.obterPorParametros({
      indAtivo: 1,
      codFiliais: codFilial,
      codPerfil: 35,
      periodoMediaAtendInicio: moment().add(-7, 'days').format('yyyy-MM-DD 00:00'),
      periodoMediaAtendFim: moment().format('yyyy-MM-DD 23:59'),
      sortActive: 'nome',
      sortDirection: 'asc'
    }).toPromise();

    this.tecnicos = data.items;
  }

  private async obterFiliais()
  {
    var codFilial = this.userSession.usuario?.filial?.codFilial;

    if (codFilial)
    {
      this.form.controls['codFiliais'].setValue(codFilial);
      this.form.controls['codFiliais'].disable();
    }
    else if (this.form.controls['codFiliais'].value == "" || !this.form.controls['codFiliais'].value)
    {
      this.form.controls['codFiliais'].setValue(4);
    }
    const data = await this._filialSvc.obterPorParametros({
      indAtivo: 1,
      sortActive: 'nomeFilial',
      sortDirection: 'asc'
    }).toPromise();

    this.filiais = data.items;
  }
  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}