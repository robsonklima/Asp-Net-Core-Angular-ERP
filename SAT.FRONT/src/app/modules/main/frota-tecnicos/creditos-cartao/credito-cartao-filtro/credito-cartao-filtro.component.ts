import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { FilialService } from 'app/core/services/filial.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { DespesaCreditoCartaoStatusEnum } from 'app/core/types/despesa-periodo.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { statusConst } from 'app/core/types/status-types';
import { Tecnico } from 'app/core/types/tecnico.types';
import { UserService } from 'app/core/user/user.service';
import { RoleEnum } from 'app/core/user/user.types';

@Component({
  selector: 'app-credito-cartao-filtro',
  templateUrl: './credito-cartao-filtro.component.html'
})
export class CreditoCartaoFiltroComponent extends FilterBase implements OnInit, IFilterBase
{
  @Input() sidenav: MatSidenav;
  tecnicos: Tecnico[] = [];
  filiais: Filial[] = [];
  creditoCartaoStatus: any = [];


  constructor (
    protected _userService: UserService,
    private _tecnicoService: TecnicoService,
    private _filialService: FilialService,
    protected _formBuilder: FormBuilder)
  {
    super(_userService, _formBuilder, 'despesa-credito-cartao');
  }

  createForm(): void
  {
    this.form = this._formBuilder.group({
      codTecnicos: [undefined],
      codFiliais: [undefined],
      codDespesaProtocolo: [undefined],
      codCreditoCartaoStatus: [undefined],
      inicioPeriodo: [undefined],
      fimPeriodo: [undefined]
    });

    this.form.patchValue(this.filter?.parametros);
  }

  loadData(): void 
  {
    this.obterTecnicos();
    this.obterFiliais();
    this.obterStatus();
  }

  configurarFiltro(): void
  {
    this.form.controls['codFiliais'].valueChanges.subscribe(async codFilial =>
    {
      this.obterTecnicos(codFilial);
    });
  }

  async obterTecnicos(codFilial: string = null)
  {
    const data = await this._tecnicoService
      .obterPorParametros({
        indAtivo: statusConst.ATIVO,
        sortActive: 'nome',
        sortDirection: 'asc',
        codPerfil: RoleEnum.FILIAL_TECNICO_DE_CAMPO,
        codFiliais: codFilial,
        pageSize: 1000
      })
      .toPromise();

    this.tecnicos = data.items;
  }

  async obterFiliais()
  {
    let params: FilialParameters = {
      indAtivo: statusConst.ATIVO,
      sortActive: 'nomeFilial',
      sortDirection: 'asc'
    };

    const data = await this._filialService
      .obterPorParametros(params)
      .toPromise();

    this.filiais = data.items;
  }

  private obterStatus()
  {
    Object.keys(DespesaCreditoCartaoStatusEnum).map(key => parseInt(key)).forEach(key =>
    {
      if (!(Number.isNaN(key)))
        this.creditoCartaoStatus.push({
          codigo: key,
          nome: DespesaCreditoCartaoStatusEnum[key]
        })
    });
  }

  ngOnInit(): void
  {
    this.createForm();
    this.loadData();
  }
}