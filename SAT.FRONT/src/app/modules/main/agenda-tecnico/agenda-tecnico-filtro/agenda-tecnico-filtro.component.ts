import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Tecnico } from 'app/core/types/tecnico.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';

@Component({
  selector: 'app-agenda-tecnico-filtro',
  templateUrl: './agenda-tecnico-filtro.component.html'
})
export class AgendaTecnicoFiltroComponent implements OnInit {
  form: FormGroup;
  filtro: any;
  tecnicos: Tecnico[] = [];
  sessionData: UsuarioSessao;
  @Input() sidenavFiltro: MatSidenav;

  constructor(
    private _formBuilder: FormBuilder,
    private _userSvc: UserService,
    private _tecnicoSvc: TecnicoService
  ) {
    this.filtro = this._userSvc.obterFiltro('agenda-tecnico');
  }

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      codTecnicos: [undefined],
    });

    this.form.patchValue(this.filtro?.parametros);

    this.obterTecnicos();
  }

  private async obterTecnicos() {
    const data = await this._tecnicoSvc.obterPorParametros({
      indAtivo: 1,
      codFiliais: "4",
      codPerfil: 35,
      periodoMediaAtendInicio: moment().add(-7, 'days').format('yyyy-MM-DD 00:00'),
      periodoMediaAtendFim: moment().format('yyyy-MM-DD 23:59'),
      sortActive: 'nome',
      sortDirection: 'asc'
    }).toPromise();

    this.tecnicos = data.items;
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
