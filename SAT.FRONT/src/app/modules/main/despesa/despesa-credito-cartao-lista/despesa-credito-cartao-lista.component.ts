import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, LOCALE_ID, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { DespesaProtocoloService } from 'app/core/services/despesa-protocolo.service';
import { DespesaCreditoCartaoStatusEnum, DespesaCreditosCartaoListView, DespesaPeriodoTecnico, DespesaPeriodoTecnicoData, DespesaPeriodoTecnicoFilterEnum } from 'app/core/types/despesa-periodo.types';
import { DespesaTipoEnum } from 'app/core/types/despesa.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { TecnicoCategoriaCreditoEnum } from 'app/core/types/tecnico.types';
import { UserService } from 'app/core/user/user.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import Enumerable from 'linq';
import moment from 'moment';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { DespesaCreditoCreditarDialogComponent } from './despesa-credito-creditar-dialog/despesa-credito-creditar-dialog.component';

@Component({
  selector: 'app-despesa-credito-cartao-lista',
  templateUrl: './despesa-credito-cartao-lista.component.html',
  styles: [`
        .list-grid-despesa-credito-cartao {
            grid-template-columns: 50px 50px 50px auto 30px 30px 115px 75px 60px 85px 60px 75px 40px 60px;
            @screen sm { grid-template-columns: 50px 50px 50px auto 30px 30px 115px 75px 60px 85px 60px 75px 40px 60px; }
            @screen md { grid-template-columns: 50px 50px 50px auto 30px 30px 115px 75px 60px 85px 60px 75px 40px 60px; }
            @screen lg { grid-template-columns: 50px 50px 50px auto 30px 30px 115px 75px 60px 85px 60px 75px 40px 60px; }
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
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  isLoading: boolean = false;
  periodos: DespesaPeriodoTecnicoData;
  listview: DespesaCreditosCartaoListView[] = [];

  constructor (
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService,
    private _despesaProtocoloSvc: DespesaProtocoloService,
    private _dialog: MatDialog)
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

  public async obterDados(filter: string = null)
  {
    this.isLoading = true;

    await this.obterPeriodosTecnico(filter);
    await this.criarListView();

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
    ).subscribe((filter: string) =>
    {
      this.paginator.pageIndex = 0;
      this.obterDados(filter);
    });

    this.sidenav.closedStart.subscribe(() =>
    {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  private async obterPeriodosTecnico(filter: string = null)
  {
    this.periodos = (await this._despesaPeriodoTecnicoSvc.obterPorParametros(
      {
        filterType: DespesaPeriodoTecnicoFilterEnum.FILTER_CREDITOS_CARTAO,
        pageNumber: this.paginator.pageIndex + 1,
        pageSize: this.paginator.pageSize,
        codTecnico: this.filter?.parametros.codTecnicos,
        codFilial: this.filter?.parametros.codFiliais,
        codDespesaProtocolo: this.filter?.parametros.codDespesaProtocolo,
        codCreditoCartaoStatus: this.filter?.parametros.codCreditoCartaoStatus,
        filter: filter
      }
    ).toPromise());
  }

  private criarListView()
  {
    this.listview = [];

    this.periodos.items
      .forEach(p =>
      {
        this.listview.push(
          {
            protocolo: "P" + p.despesaProtocoloPeriodoTecnico?.codDespesaProtocolo,
            rd: p.codDespesaPeriodoTecnico,
            liberacao: p.despesaProtocoloPeriodoTecnico.dataHoraCad,
            tecnico: p.tecnico?.nome,
            categoriaCredito: p.tecnico.tecnicoCategoriaCredito,
            filial: p.tecnico?.filial?.nomeFilial,
            cartao: this.obterCartaoAtual(p),
            saldo: this.obterSaldoAtual(p),
            dataManutSaldo: moment(this.obterHorarioSaldoAtual(p)).format('DD/MM HH:mm'),
            integrado: p.ticketLogPedidoCredito?.dataHoraProcessamento ? moment(p.ticketLogPedidoCredito?.dataHoraProcessamento).format('DD/MM HH:mm') : null,
            inicio: moment(p.despesaPeriodo.dataInicio).format('DD/MM/YY'),
            fim: moment(p.despesaPeriodo.dataFim).format('DD/MM/YY'),
            combustivel: this.obterDespesasCombustivel(p),
            indCreditado: p.indCredito == 1 ? true : false,
            indCompensado: p.indCompensacao == 1 ? true : false,
            indVerificado: p.indVerificacao == 1 ? true : false,
            obs: p.ticketLogPedidoCredito?.observacao,
            indErroAoCreditar: p.ticketLogPedidoCredito?.observacao != null && p.ticketLogPedidoCredito?.observacao != '' && p.indCredito == 1 ? true : false
          });
      });
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
        .where(i => i.indAtivo
          && (i.codDespesaTipo == DespesaTipoEnum.KM || i.codDespesaTipo == DespesaTipoEnum.COMBUSTIVEL))
        .sum(i => i.despesaValor));
  }

  getCategoriaCredito(c: TecnicoCategoriaCreditoEnum)
  {
    return TecnicoCategoriaCreditoEnum[c];
  }

  paginar()
  {
    this.onPaginationChanged();
    this.obterDados();
  }

  creditarRD(a: DespesaCreditosCartaoListView)
  {
    var despesaPeriodoTecnico = Enumerable.from(this.periodos.items)
      .firstOrDefault(i => i.codDespesaPeriodoTecnico == a.rd);

    const dialogRef = this._dialog.open(DespesaCreditoCreditarDialogComponent, {
      data: {
        despesaCreditosCartaoListView: a,
        despesaPeriodoTecnico: despesaPeriodoTecnico
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
    {
      if (confirmacao)
        this.obterDados();
    });
  }

  verificarRD(a: DespesaCreditosCartaoListView)
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Verificação',
        message: `Deseja verificar o RD no valor de ${a.combustivel.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' })} para o técnico ${a.tecnico}?`,
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
    {
      var despesaPeriodoTecnico = Enumerable.from(this.periodos.items)
        .firstOrDefault(i => i.codDespesaPeriodoTecnico == a.rd);

      if (confirmacao)
      {
        despesaPeriodoTecnico.indVerificacao = 1;
        despesaPeriodoTecnico.codUsuarioVerificacao = this.userSession.usuario.codUsuario;
        despesaPeriodoTecnico.dataHoraVerificacao = moment().format('DD/MM/YY HH:mm:ss');
        this._despesaPeriodoTecnicoSvc.atualizar(despesaPeriodoTecnico).subscribe(i =>
        {
          this.fecharProtocolo(despesaPeriodoTecnico.despesaProtocoloPeriodoTecnico.codDespesaProtocolo);
        });
      }
    });
  }

  cancelarVerificacaoRD(a: DespesaCreditosCartaoListView)
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Cancelar Verificação',
        message: `Deseja cancelar o RD no valor de ${a.combustivel.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' })} para o técnico ${a.tecnico}?`,
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
    {
      var despesaPeriodoTecnico = Enumerable.from(this.periodos.items)
        .firstOrDefault(i => i.codDespesaPeriodoTecnico == a.rd);

      if (confirmacao)
      {
        despesaPeriodoTecnico.indVerificacao = 0;
        despesaPeriodoTecnico.codUsuarioVerificacaoCancelado = this.userSession.usuario.codUsuario;
        despesaPeriodoTecnico.dataHoraVerificacaoCancelado = moment().format('DD/MM/YY HH:mm');
        // this._despesaPeriodoTecnicoSvc.atualizar(despesaPeriodoTecnico).toPromise();
      }
    });
  }

  getStatus(a: DespesaCreditosCartaoListView)
  {
    if (a.indErroAoCreditar)
      return DespesaCreditoCartaoStatusEnum[DespesaCreditoCartaoStatusEnum['ERRO AO CREDITAR']];
    else if (a.indCreditado)
      return DespesaCreditoCartaoStatusEnum[DespesaCreditoCartaoStatusEnum.CREDITADO];
    else if (a.indCompensado)
      return DespesaCreditoCartaoStatusEnum[DespesaCreditoCartaoStatusEnum.COMPENSADO];

    return DespesaCreditoCartaoStatusEnum[DespesaCreditoCartaoStatusEnum.PENDENTE];
  }

  async fecharProtocolo(codDespesaProtocolo: number)
  {
    var protocolo = (await this._despesaProtocoloSvc
      .obterPorCodigo(codDespesaProtocolo)
      .toPromise());

    var rdsAbertos = Enumerable.from(protocolo.despesaProtocoloPeriodoTecnico)
      .select(i => i.despesaPeriodoTecnico)
      .where(i => i.indVerificacao != 1)
      .toArray();

    var message: string = 'O protocolo foi fechado com sucesso!';

    if (rdsAbertos?.length > 0)
    {
      message = `O protocolo ainda possui ${rdsAbertos.length} RD(s) em aberto.`;

      var rdsSemCombustivel =
        Enumerable.from(rdsAbertos)
          .where(i => !Enumerable.from(i.tecnico.despesaCartaoCombustivelTecnico).any())
          .toArray();

      if (rdsSemCombustivel?.length > 0)
        message += ` ${rdsSemCombustivel?.length} não possuem cartão combustível vinculado.`;
    }
    else
    {
      protocolo.indFechamento = 1;
      protocolo.dataHoraFechamento = moment().format('DD/MM/YY HH:mm:ss');
      await this._despesaProtocoloSvc.atualizar(protocolo).toPromise();
    }

    this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: `Protocolo ${codDespesaProtocolo}`,
        message: message,
        hideCancel: true,
        buttonText: { ok: 'Ok' }
      }
    });
  }

  async reabrirProtocolo(codDespesaProtocolo: number)
  {
  }
}