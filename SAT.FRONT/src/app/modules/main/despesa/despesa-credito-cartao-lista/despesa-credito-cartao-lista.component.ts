import { AfterViewInit, ChangeDetectorRef, Component, LOCALE_ID, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { DespesaCreditosCartaoListView, DespesaPeriodoTecnico, DespesaPeriodoTecnicoData, DespesaPeriodoTecnicoFilterEnum } from 'app/core/types/despesa-periodo.types';
import { DespesaTipoEnum } from 'app/core/types/despesa.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import Enumerable from 'linq';
import moment from 'moment';

@Component({
  selector: 'app-despesa-credito-cartao-lista',
  templateUrl: './despesa-credito-cartao-lista.component.html',
  styles: [`
        .list-grid-despesa-credito-cartao {
            grid-template-columns: 60px 60px 70px auto 30px 150px 115px 85px 60px 60px 80px 105px;
            @screen sm { grid-template-columns: 60px 60px 70px auto 30px 150px 115px 85px 60px 60px 80px 105px; }
            @screen md { grid-template-columns: 60px 60px 70px auto 30px 150px 115px 85px 60px 60px 80px 105px; }
            @screen lg { grid-template-columns: 60px 60px 70px auto 30px 150px 115px 85px 60px 60px 80px 105px; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})
export class DespesaCreditoCartaoListaComponent extends Filterable implements AfterViewInit, IFilterable
{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('sidenav') sidenav: MatSidenav;
  isLoading: boolean = false;
  periodos: DespesaPeriodoTecnicoData;
  listview: DespesaCreditosCartaoListView[] = [];

  constructor (
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService)
  {
    super(_userService, 'despesa-credito-cartao');
  }

  ngAfterViewInit()
  {
    this.obterDados();

    if (this.sort && this.paginator)
    {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() =>
      {
        this.onSortChanged();
        this.obterDados();
      });
    }

    this.registerEmitters();
    this._cdr.detectChanges();
  }

  public async obterDados()
  {
    this.isLoading = true;

    await this.obterPeriodosTecnico();
    await this.criarListView();

    this.isLoading = false;
  }

  registerEmitters(): void
  {
    this.sidenav.closedStart.subscribe(() =>
    {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  private async obterPeriodosTecnico()
  {
    this.periodos = (await this._despesaPeriodoTecnicoSvc.obterPorParametros(
      {
        filterType: DespesaPeriodoTecnicoFilterEnum.FILTER_CREDITOS_CARTAO,
        pageNumber: this.paginator.pageIndex + 1,
        pageSize: this.paginator.pageSize,
        codTecnico: this.filter?.parametros.codTecnicos,
        codFilial: this.filter?.parametros.codFiliais,
        sortActive: 'codDespesaPeriodoTecnico',
        sortDirection: 'desc'
      }
    ).toPromise());
  }

  private criarListView()
  {
    this.listview = [];

    this.periodos.items.forEach(p =>
    {
      console.log(p.tecnico.tecnicoCategoriaCredito);

      this.listview.push(
        {
          protocolo: "P" + p.despesaProtocoloPeriodoTecnico?.codDespesaProtocolo,
          rd: p.codDespesaPeriodoTecnico,
          cadastro: moment(p.dataHoraManut).format('DD/MM'),
          tecnico: p.tecnico?.nome,
          filial: p.tecnico?.filial?.nomeFilial,
          cartao: this.obterCartaoAtual(p),
          saldo: this.obterSaldoAtual(p),
          dataManutSaldo: moment(this.obterHorarioSaldoAtual(p)).format('DD/MM HH:mm'),
          integrado: p.ticketLogPedidoCredito?.dataHoraProcessamento ? moment(p.ticketLogPedidoCredito?.dataHoraProcessamento).format('DD/MM HH:mm') : null,
          inicio: moment(p.despesaPeriodo.dataInicio).format('DD/MM/YY'),
          fim: moment(p.despesaPeriodo.dataFim).format('DD/MM/YY'),
          combustivel: this.obterDespesasCombustivel(p),
          indCreditado: p.indCredito == 1 ? true : false
        });
    });

    this.listview = Enumerable.from(this.listview)
      .orderByDescending(i => i.cadastro)
      .toArray();
  }

  private obterCartaoAtual(p: DespesaPeriodoTecnico)
  {
    return Enumerable.from(p.tecnico.despesaCartaoCombustivelTecnico)
      .orderByDescending(i => i.dataHoraInicio)
      .firstOrDefault()?.despesaCartaoCombustivel?.numero;
  }

  private obterSaldoAtual(p: DespesaPeriodoTecnico)
  {
    return Enumerable.from(p.tecnico.despesaCartaoCombustivelTecnico)
      .orderByDescending(i => i.dataHoraInicio)
      .firstOrDefault()?.despesaCartaoCombustivel?.ticketLogUsuarioCartaoPlaca?.saldo;
  }

  private obterHorarioSaldoAtual(p: DespesaPeriodoTecnico)
  {
    return Enumerable.from(p.tecnico.despesaCartaoCombustivelTecnico)
      .orderByDescending(i => i.dataHoraInicio)
      .firstOrDefault()?.despesaCartaoCombustivel?.ticketLogUsuarioCartaoPlaca?.dataHoraManut;
  }

  private obterDespesasCombustivel(p: DespesaPeriodoTecnico)
  {
    return Enumerable.from(p.despesas).sum(i =>
      Enumerable.from(i.despesaItens)
        .where(i => i.codDespesaTipo == DespesaTipoEnum.KM)
        .sum(i => i.despesaValor));
  }

  paginar()
  {
    this.onPaginationChanged();
    this.obterDados();
  }

  creditarRD()
  {
    alert("creditar");
  }

  verificarProtocolo()
  {
    alert("verificar");
  }
}