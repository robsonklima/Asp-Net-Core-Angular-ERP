import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, Input, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Filterable } from 'app/core/filters/filterable';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { MbscAgendaTecnicoCalendarEvent } from 'app/core/types/agenda-tecnico.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { OrdemServicoData } from 'app/core/types/ordem-servico.types';
import { UserService } from 'app/core/user/user.service';
import Enumerable from 'linq';
import moment from 'moment';
import { Subject, interval, fromEvent } from 'rxjs';
import { startWith, takeUntil, debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-agenda-tecnico-chamados',
  templateUrl: './agenda-tecnico-chamados.component.html'
})

export class AgendaTecnicoChamadosComponent extends Filterable implements AfterViewInit, IFilterable
{
  @Input() sidenavOut: MatSidenav;
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  externalEvents: MbscAgendaTecnicoCalendarEvent = [];

  dataSourceData: OrdemServicoData;
  isLoading: boolean = false;
  protected _onDestroy = new Subject<void>();

  constructor (
    private _cdr: ChangeDetectorRef,
    private _ordemServicoService: OrdemServicoService,
    protected _userService: UserService
  )
  {
    super(_userService, 'agenda-tecnico-chamados')
  }

  ngAfterViewInit(): void
  {
    this.registerEmitters();
  }

  async obterOrdensServico(filter: string = null)
  {
    this.isLoading = true;

    this.dataSourceData = await this._ordemServicoService
      .obterPorParametros({
        sortActive: 'codOS',
        sortDirection: 'desc',
        pageSize: 100,
        filter: filter,
        ...this.filter?.parametros,
      })
      .toPromise();

    this.externalEvents = Enumerable.from(this.dataSourceData.items).where(i => !i.agendaTecnico.length).select(os =>
    {
      return {
        title: os.localAtendimento?.nomeLocal,
        nomeLocal: os.localAtendimento?.nomeLocal,
        cliente: os.cliente?.razaoSocial,
        regiao: os.regiaoAutorizada?.regiao?.nomeRegiao,
        autorizada: os.regiaoAutorizada?.autorizada?.nomeFantasia,
        color: '#1064b0',
        start: moment(),
        end: moment().add(60, 'minutes'),
        ordemServico: os
      }
    }).toArray();

    this.isLoading = false;
  }

  registerEmitters(): void
  {
    interval(5 * 60 * 500)
      .pipe(
        startWith(0),
        takeUntil(this._onDestroy)
      )
      .subscribe(() =>
      {
        this.obterOrdensServico();
      });

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

  loadFilter(): void
  {
    super.loadFilter();

    // Filtro obrigatorio de filial quando o usuario esta vinculado a uma filial
    if (this.userSession?.usuario?.codFilial && this.filter)
      this.filter.parametros.codFiliais = this.userSession.usuario.codFilial
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}