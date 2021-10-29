import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { DespesaAdiantamentoPeriodoService } from 'app/core/services/despesa-adiantamento-periodo.service';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { DespesaPeriodoService } from 'app/core/services/despesa-periodo.service';
import { DespesaAdiantamentoPeriodoData, DespesaPeriodo, DespesaPeriodoData, DespesaPeriodoTecnico, DespesaPeriodoTecnicoData } from 'app/core/types/despesa-atendimento.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import Enumerable from 'linq';

@Component({
  selector: 'app-despesa-atendimento-lista',
  templateUrl: './despesa-atendimento-lista.component.html',
  styles: [`
        .list-grid-despesa-atendimento {
            grid-template-columns: 50px 130px 130px 130px 130px auto 130px 130px  5px;
            @screen sm { grid-template-columns: 50px 130px 130px 130px 130px auto 130px 130px  50px; }
            @screen md { grid-template-columns: 50px 130px 130px 130px 130px auto 130px 130px  50px; }
            @screen lg { grid-template-columns: 50px 130px 130px 130px 130px auto 130px 130px  50px; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class DespesaAtendimentoListaComponent implements AfterViewInit
{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;

  userSession: UserSession;
  isLoading: boolean = false;
  despesasPeriodoTecnico: DespesaPeriodoTecnico[] = [];
  despesasPeriodo: DespesaPeriodoData;
  despesasAdiantamentoPeriodo: DespesaAdiantamentoPeriodoData;


  constructor (
    private _cdr: ChangeDetectorRef,
    private _userService: UserService,
    private _despesaPeriodoSvc: DespesaPeriodoService,
    private _despesaAdiantamentoPeriodoSvc: DespesaAdiantamentoPeriodoService,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService)
  { this.userSession = JSON.parse(this._userService.userSession); }

  ngAfterViewInit(): void
  {
    this.obterDados();

    if (this.sort && this.paginator)
    {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() =>
      {
        this.paginator.pageIndex = 0;
        this.obterDados();
      });
    }

    this._cdr.detectChanges();
  }

  private async obterDespesasPeriodo()
  {
    this.despesasPeriodo = (await this._despesaPeriodoSvc.obterPorParametros({
      indAtivo: 1,
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort?.active || 'codDespesaPeriodo',
      sortDirection: this.sort?.direction || 'desc',
      pageSize: this.paginator?.pageSize
    }).toPromise());
  }

  private async obterDespesasPeriodoTecnico()
  {
    if (!this.userSession.usuario.codTecnico) return;

    this.despesasPeriodoTecnico = (await this._despesaPeriodoTecnicoSvc.obterPorParametros({
      codTecnico: this.userSession.usuario.codTecnico,
      codDespesaPeriodos: this.getCodDespesaPeriodos(),
      indAtivoPeriodo: 1,
      pageSize: this.paginator?.pageSize
    }).toPromise()).items;
  }

  private async obterDespesasAdiantamentoPeriodo()
  {
    if (!this.userSession.usuario.codTecnico) return;

    this.despesasAdiantamentoPeriodo = (await this._despesaAdiantamentoPeriodoSvc.obterPorParametros({
      codTecnico: this.userSession.usuario.codTecnico,
      codDespesaPeriodos: this.getCodDespesaPeriodos(),
      indAtivoPeriodo: 1,
      pageSize: this.paginator?.pageSize
    }).toPromise());
  }

  private async obterDados()
  {
    this.isLoading = true;

    await this.obterDespesasPeriodo();
    this.obterDespesasPeriodoTecnico();
    this.obterDespesasAdiantamentoPeriodo();

    this.isLoading = false;
  }

  private getCodDespesaPeriodos(): string
  {
    return Enumerable.from(this.despesasPeriodo.items).select(e => e.codDespesaPeriodo).toArray().join(',');
  }

  paginar()
  {
    this.obterDespesasPeriodo();
  }
}