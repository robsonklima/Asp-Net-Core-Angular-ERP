import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { DespesaPeriodoService } from 'app/core/services/despesa-periodo.service';
import { DespesaPeriodo, DespesaPeriodoData, DespesaPeriodoTecnico, DespesaPeriodoTecnicoData } from 'app/core/types/despesa-periodo.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

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


  constructor (
    private _cdr: ChangeDetectorRef,
    private _userService: UserService,
    private _despesaPeriodoSvc: DespesaPeriodoService,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService)
  { this.userSession = JSON.parse(this._userService.userSession); }

  ngAfterViewInit(): void
  {
    this.obterDespesasPeriodo();
    this.obterDespesasPeriodoTecnico();

    if (this.sort && this.paginator)
    {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() =>
      {
        this.paginator.pageIndex = 0;
        this.obterDespesasPeriodo();
      });
    }

    this._cdr.detectChanges();
  }

  private async obterDespesasPeriodoTecnico()
  {
    if (!this.userSession.usuario.codTecnico) return;

    this.despesasPeriodoTecnico = (await this._despesaPeriodoTecnicoSvc.obterPorParametros({
      codTecnico: this.userSession.usuario.codTecnico,
      indAtivoPeriodo: 1,
      pageSize: 500,
    }).toPromise()).items;
  }

  private async obterDespesasPeriodo()
  {
    this.isLoading = true;

    this.despesasPeriodo = (await this._despesaPeriodoSvc.obterPorParametros({
      indAtivo: 1,
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort?.active || 'codDespesaPeriodo',
      sortDirection: this.sort?.direction || 'desc',
      pageSize: this.paginator?.pageSize
    }).toPromise());

    this.isLoading = false;

  }


  paginar()
  {
    this.obterDespesasPeriodo();
  }
}
