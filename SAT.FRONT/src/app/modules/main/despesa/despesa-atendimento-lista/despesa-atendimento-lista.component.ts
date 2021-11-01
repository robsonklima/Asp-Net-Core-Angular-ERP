import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { DespesaAdiantamentoPeriodoService } from 'app/core/services/despesa-adiantamento-periodo.service';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { DespesaPeriodoService } from 'app/core/services/despesa-periodo.service';
import { DespesaAdiantamentoPeriodo, DespesaAdiantamentoPeriodoData, DespesaPeriodo, DespesaPeriodoData, DespesaPeriodoTecnico, DespesaPeriodoTecnicoData } from 'app/core/types/despesa-atendimento.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import Enumerable from 'linq';

@Component({
  selector: 'app-despesa-atendimento-lista',
  templateUrl: './despesa-atendimento-lista.component.html',
  styles: [`
        .list-grid-despesa-atendimento {
            grid-template-columns: 50px 130px 130px 130px 130px 150px 130px auto 50px;
            @screen sm { grid-template-columns: 50px 130px 130px 130px 130px 150px 130px auto 50px; }
            @screen md { grid-template-columns: 50px 130px 130px 130px 130px 150px 130px auto 50px; }
            @screen lg { grid-template-columns: 50px 130px 130px 130px 130px 150px 130px auto 50px; }
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
  despesasPeriodoTecnico: DespesaPeriodoTecnicoData;
  despesasPeriodo: DespesaPeriodoData;
  despesasAdiantamentoPeriodo: DespesaAdiantamentoPeriodoData;

  constructor (
    private _cdr: ChangeDetectorRef,
    private _userService: UserService,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService)
  { this.userSession = JSON.parse(this._userService.userSession); }

  ngAfterViewInit()
  {

    if (this.sort && this.paginator)
    {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() =>
      {
        this.paginator.pageIndex = 0;
      });
    }

    this.obterDespesasPeriodoTecnico();
    this._cdr.detectChanges();
  }

  private async obterDespesasPeriodoTecnico()
  {
    this.despesasPeriodoTecnico = (await this._despesaPeriodoTecnicoSvc.obterPorParametros({
      codTecnico: this.userSession.usuario?.codTecnico,
      indAtivoPeriodo: 1,
      pageSize: this.paginator?.pageSize
    }).toPromise());
  }

  public paginar()
  {

  }
}