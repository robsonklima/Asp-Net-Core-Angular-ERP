import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { TicketLogTransacaoService } from 'app/core/services/ticket-log-transacao.service';
import { DefeitoParameters } from 'app/core/types/defeito.types';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { TicketLogTransacao, TicketLogTransacaoData } from 'app/core/types/ticket-log-transacao.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent, interval, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, startWith, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-transacoes-cartao-lista',
  templateUrl: './transacoes-cartao-lista.component.html',
  styles: [`
    .list-grid-u {
      grid-template-columns: 142px auto 70px 108px 35px;
      
      @screen sm {
          grid-template-columns: 142px auto 70px 108px 35px;
      }
  
      @screen md {
          grid-template-columns: 142px auto 70px 108px 35px;
      }
  
      @screen lg {
          grid-template-columns: 142px auto 70px 108px 35px;
      }
    }`
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class TransacoesCartaoListaComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: TicketLogTransacaoData;
  isLoading: boolean = false;
  transacaoSelecionada: TicketLogTransacao;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  protected _onDestroy = new Subject<void>();
  userSession: UserSession;

  constructor(
    private _ticketLogTransacaoService: TicketLogTransacaoService,
    private _cdr: ChangeDetectorRef,
    private _snack: CustomSnackbarService,
    private _exportacaoService: ExportacaoService,
    protected _userService: UserService
  ) {
    super(_userService, 'ticket-log-transacao')
    this.userSession = JSON.parse(this._userService.userSession);
  }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  loadFilter(): void {
    super.loadFilter();
  }

  onSidenavClosed(): void {
    if (this.paginator) this.paginator.pageIndex = 0;
    this.loadFilter();
    this.obterDados();
  }

  async ngAfterViewInit() {
    this.registerEmitters();

    interval(3 * 60 * 1000)
      .pipe(
        startWith(0),
        takeUntil(this._onDestroy)
      )
      .subscribe(() => {
        this.obterDados();
      });

    if (this.sort && this.paginator)
    {
      fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
        map((event: any) => {
          return event.target.value;
        })
        , debounceTime(700)
        , distinctUntilChanged()
      ).subscribe((text: string) => {
        this.paginator.pageIndex = 0;
        this.searchInputControl.nativeElement.val = text;
        this.obterDados(text);
      });

      this.sort.disableClear = true;
      this._cdr.markForCheck();
      this.sort.sortChange.subscribe(() => {
        this.paginator.pageIndex = 0;
        this.obterDados();
      });
    }

    this._cdr.detectChanges();
  }

  ngOnInit(): void {

  }

  async obterDados(filtro: string = '') {
    this.isLoading = true;
    const parametros: DefeitoParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort.active || 'dataTransacao',
			sortDirection: this.sort.direction || 'desc',
      pageSize: this.paginator?.pageSize,
      filter: filtro
    }

    await this._ticketLogTransacaoService.obterPorParametros({
      ...parametros,
      ...this.filter?.parametros
    }).subscribe((data) => {
      this.dataSourceData = data;
      this.isLoading = false;
      this._cdr.detectChanges();
    }, () => {
      this._snack.exibirToast('Erro ao consultar as transações', 'error');
    });
  }

  paginar() {
    this.obterDados();
  }

  public async exportar() {
    this.isLoading = true;

    let exportacaoParam: Exportacao = {
      formatoArquivo: ExportacaoFormatoEnum.EXCEL,
      tipoArquivo: ExportacaoTipoEnum.TICKET_LOG_TRANSACAO,
      entityParameters: {
        ...this.filter?.parametros
      }
    }

    await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
    this.isLoading = false;
  }

  abrirDetalhe(t: TicketLogTransacao): void {
    if (this.transacaoSelecionada && this.transacaoSelecionada.codTicketLogTransacao === t.codTicketLogTransacao)
    {
      this.fecharDetalhe();
      return;
    }

    this.transacaoSelecionada = t;
  }

  fecharDetalhe(): void {
    this.transacaoSelecionada = null;
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}