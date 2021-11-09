import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilialService } from 'app/core/services/filial.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Filial } from 'app/core/types/filial.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';

@Component({
  selector: 'app-agenda-tecnico-filtro',
  templateUrl: './agenda-tecnico-filtro.component.html'
})
export class AgendaTecnicoFiltroComponent implements OnInit
{
  form: FormGroup;
  filtro: any;
  tecnicos: Tecnico[] = [];
  filiais: Filial[] = [];
  sessionData: UsuarioSessao;
  @Input() sidenavFiltro: MatSidenav;

  constructor (
    private _formBuilder: FormBuilder,
    private _userSvc: UserService,
    private _tecnicoSvc: TecnicoService,
    private _filialSvc: FilialService

  )
  {
    this.sessionData = JSON.parse(this._userSvc.userSession);
    this.filtro = this._userSvc.obterFiltro('agenda-tecnico');
  }

  ngOnInit(): void
  {
    this.form = this._formBuilder.group({
      codTecnicos: [undefined],
      codFiliais: [undefined]
    });

    this.form.patchValue(this.filtro?.parametros);

    this.obterTecnicosAoEscolherFilial();
    this.obterFiliais();
    this.configurarFiltro();
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
    var codFilial = this.sessionData.usuario?.filial?.codFilial;

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

  aplicar(): void
  {
    const form: any = this.form.getRawValue();

    const filtro: any = {
      nome: 'agenda-tecnico',
      parametros: form
    }

    this._userSvc.registrarFiltro(filtro);

    const newFilter: any = { nome: 'agenda-tecnico', parametros: this.form.getRawValue() }
    const oldFilter = this._userSvc.obterFiltro('agenda-tecnico');

    if (oldFilter != null)
      newFilter.parametros =
      {
        ...newFilter.parametros,
        ...oldFilter.parametros
      };

    this._userSvc.registrarFiltro(newFilter);
    this.sidenavFiltro.close();
  }

  limpar(): void
  {
    this.form.reset();
    this.aplicar();
    this.sidenavFiltro.close();
  }

  selectAll(select: AbstractControl, values, propertyName)
  {
    if (select.value[0] == 0 && propertyName != '')
      select.patchValue([...values.map(item => item[`${propertyName}`]), 0]);
    else if (select.value[0] == 0 && propertyName == '')
      select.patchValue([...values.map(item => item), 0]);
    else
      select.patchValue([]);
  }
}