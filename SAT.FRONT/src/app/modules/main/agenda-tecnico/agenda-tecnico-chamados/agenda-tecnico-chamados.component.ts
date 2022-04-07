import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Filterable } from 'app/core/filters/filterable';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { AgendaTecnicoTipoEnum, MbscAgendaTecnicoCalendarEvent } from 'app/core/types/agenda-tecnico.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { OrdemServicoData, StatusServicoEnum } from 'app/core/types/ordem-servico.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject, fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { AgendaTecnicoValidator } from '../agenda-tecnico.validator';

@Component({
  selector: 'app-agenda-tecnico-chamados',
  templateUrl: './agenda-tecnico-chamados.component.html'
})

export class AgendaTecnicoChamadosComponent extends Filterable implements AfterViewInit, OnInit, IFilterable
{
  @Input() sidenavOut: MatSidenav;
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  externalEvents: MbscAgendaTecnicoCalendarEvent = [];

  dataSourceData: OrdemServicoData;
  isLoading: boolean = false;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _cdr: ChangeDetectorRef,
    private _ordemServicoService: OrdemServicoService,
    protected _userService: UserService,
    private _validator: AgendaTecnicoValidator
  )
  {
    super(_userService, 'agenda-tecnico-chamados')
  }

  ngOnInit(): void
  {
    this.obterOrdensServico();
  }

  ngAfterViewInit(): void
  {
    this.registerEmitters();
  }

  loadFilter(): void
  {
    super.loadFilter();

    if (this.userSession?.usuario?.codFilial && this.filter)
      this.filter.parametros.codFiliais = this.userSession.usuario.codFilial
  }

  async obterOrdensServico(filter: string = null)
  {
    this.isLoading = true;

    var statusNotIn = [
      StatusServicoEnum.FECHADO, 
      StatusServicoEnum.TRANSFERIDO, 
      StatusServicoEnum.PECA_FALTANTE,
      StatusServicoEnum.PECAS_PENDENTES,
      StatusServicoEnum.PECA_SEPARADA,
      StatusServicoEnum.CANCELADO
    ].join(',');

    this.dataSourceData = await this._ordemServicoService
      .obterPorParametros({
        sortActive: 'codOS',
        sortDirection: 'desc',
        pageSize: 100,
        filter: filter,
        notIn_CodStatusServicos: statusNotIn,
        ...this.filter?.parametros,
      })
      .toPromise();

    this.externalEvents = this.dataSourceData.items.map(os =>
    {
      return {
        title: os.localAtendimento?.nomeLocal.toUpperCase(),
        nomeLocal: os.localAtendimento?.nomeLocal,
        cliente: os.cliente?.nomeFantasia,
        serie: os.equipamentoContrato?.numSerie,
        intervencao: os.tipoIntervencao?.nomTipoIntervencao,
        regiao: os.regiaoAutorizada?.regiao?.nomeRegiao,
        autorizada: os.regiaoAutorizada?.autorizada?.nomeFantasia,
        color: !os.agendamentos?.length ? this._validator.getTypeColor(AgendaTecnicoTipoEnum.OS) : this._validator.agendamentoColor(),
        indAgendamento: os.agendamentos?.length ? 1 : 0,
        start: moment(),
        end: moment().add(60, 'minutes'),
        ordemServico: os
      }
    });

    this.isLoading = false;
  }

  registerEmitters(): void
  {
    fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
      map((event: any) =>
      {
        return event.target.value;
      })
      , debounceTime(1000)
      , distinctUntilChanged()
    ).subscribe((text: string) =>
    {
      this.obterOrdensServico(text);
    });

    this.sidenav.closedStart.subscribe(() =>
    {
      this.onSidenavClosed();
      this.obterOrdensServico();
    });

    this.sidenavOut.openedStart.subscribe(() =>
    {
      this.obterOrdensServico();
    });

    this._cdr.detectChanges();
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}