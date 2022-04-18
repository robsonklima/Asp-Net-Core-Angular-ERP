import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { ClienteService } from 'app/core/services/cliente.service';
import { FilialService } from 'app/core/services/filial.service';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { TipoIntervencaoService } from 'app/core/services/tipo-intervencao.service';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { LocalAtendimento, LocalAtendimentoParameters } from 'app/core/types/local-atendimento.types';
import { RegiaoAutorizadaParameters } from 'app/core/types/regiao-autorizada.types';
import { Regiao } from 'app/core/types/regiao.types';
import { TipoIntervencao } from 'app/core/types/tipo-intervencao.types';
import { UserService } from 'app/core/user/user.service';
import Enumerable from 'linq';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-agenda-tecnico-chamados-filtro',
  templateUrl: './agenda-tecnico-chamados-filtro.component.html'
})

export class AgendaTecnicoChamadosFiltroComponent extends FilterBase implements OnInit, IFilterBase
{
  @Input() sidenav: MatSidenav;

  tiposIntervencao: TipoIntervencao[] = [];
  filiais: Filial[] = [];
  pas: number[] = [];
  clientes: Cliente[] = [];
  locaisAtendimento: LocalAtendimento[] = [];
  regioes: Regiao[] = [];

  protected _onDestroy = new Subject<void>();

  constructor (
    private _tipoIntervencaoService: TipoIntervencaoService,
    protected _userService: UserService,
    protected _formBuilder: FormBuilder,
    private _filialSvc: FilialService,
    private _clienteSvc: ClienteService,
    private _localAtendimentoSvc: LocalAtendimentoService,
    private _regiaoAutorizadaSvc: RegiaoAutorizadaService
  )
  {
    super(_userService, _formBuilder, 'agenda-tecnico-chamados');
  }

  ngOnInit(): void
  {
    this.createForm();
    this.loadData();
  }

  loadData(): void
  {
    this.obterTiposIntervencao();
    this.obterFiliais();
    this.obterClientes();

    this.configurarFiliais();
    this.configurarClientes();
    this.configurarRegioes();
  }

  createForm(): void
  {
    this.form = this._formBuilder.group({
      codFilial: [undefined],
      pas: [undefined],
      codClientes: [undefined],
      codPostos: [undefined],
      codRegioes: [undefined],
      codTiposIntervencao: [undefined]
    });

    this.form.patchValue(this.filter?.parametros);

    if (this.userSession.usuario.codFilial) {
      this.form.controls['codFilial'].setValue(this.userSession.usuario.codFilial);
      this.form.controls['codFilial'].disable();
    }
  }

  async obterTiposIntervencao()
  {
    let params = {
      indAtivo: 1,
      sortActive: 'nomTipoIntervencao',
      sortDirection: 'asc'
    }

    const data = await this._tipoIntervencaoService
      .obterPorParametros(params)
      .toPromise();

    this.tiposIntervencao = data.items;
  }

  async obterLocaisAtendimentos()
  {
    var filialFilter = this.form.controls['codFilial'].value;
    var clienteFilter = this.form.controls['codClientes'].value;
    var regiaoFilter = this.form.controls['codRegioes'].value;

    let params: LocalAtendimentoParameters = {
      indAtivo: 1,
      codFilial: filialFilter,
      codClientes: clienteFilter,
      codRegioes: regiaoFilter,
      sortActive: 'nomeLocal',
      sortDirection: 'asc',
      pageSize: 1000
    };

    const data = await this._localAtendimentoSvc
      .obterPorParametros(params)
      .toPromise();

    this.locaisAtendimento = data.items;
  }

  async obterFiliais()
  {
    let params: FilialParameters = {
      ...{
        indAtivo: 1,
        sortActive: 'nomeFilial',
        sortDirection: 'asc'
      },
      ...{ codFilial: this.userSession.usuario?.codFilial }
    };

    const data = await this._filialSvc
      .obterPorParametros(params)
      .toPromise();

    this.filiais = data.items;
  }

  async obterRegioesAutorizadas(filialFilter: any)
  {
    let params: RegiaoAutorizadaParameters = {
      indAtivo: 1,
      codFiliais: filialFilter,
      pageSize: 1000
    };

    const data = await this._regiaoAutorizadaSvc
      .obterPorParametros(params)
      .toPromise();

    this.pas = Enumerable.from(data.items).select(ra => ra.pa).distinct(r => r).toArray();
    this.regioes = Enumerable.from(data.items).select(ra => ra.regiao).distinct(r => r.codRegiao).orderBy(r => r.nomeRegiao).toArray();
  }

  configurarFiliais()
  {
    this.form.controls['codFiliais']
      .valueChanges
      .subscribe(() => 
      {
        if (this.form.controls['codFiliais'].value != '' && this.form.controls['codFiliais'].value != null)
        {
          this.obterRegioesAutorizadas(this.form.controls['codFiliais'].value);
          this.obterLocaisAtendimentos();
        }
        else
        {
          this.form.controls['codPostos'].disable();
          this.form.controls['pas'].disable();
        }
      });

    if (this.userSession?.usuario?.codFilial)
    {
      this.form.controls['codFiliais'].setValue(this.userSession.usuario.codFilial);
      this.form.controls['codFiliais'].disable();
    }

    this.obterRegioesAutorizadas(this.form.controls['codFiliais'].value);
    this.obterLocaisAtendimentos();
  }

  configurarClientes()
  {
    this.form.controls['codClientes']
      .valueChanges
      .subscribe(() => 
      {
        this.obterLocaisAtendimentos();
      });
  }

  configurarRegioes()
  {
    this.form.controls['codRegioes']
      .valueChanges
      .subscribe(() => 
      {
        this.obterLocaisAtendimentos();
      });
  }

  async obterClientes(filter: string = '')
  {
    let params: ClienteParameters = {
      filter: filter,
      indAtivo: 1,
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      pageSize: 1000
    };

    const data = await this._clienteSvc
      .obterPorParametros(params)
      .toPromise();

    this.clientes = data.items;
  }

  limpar()
  {
    super.limpar();

    if (this.userSession?.usuario?.codFilial)
    {
      this.form.controls['codFiliais'].setValue(this.userSession.usuario.codFilial);
      this.form.controls['codFiliais'].disable();
    }
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}