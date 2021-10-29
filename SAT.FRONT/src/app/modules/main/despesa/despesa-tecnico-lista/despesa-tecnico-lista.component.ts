import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { DespesaAdiantamentoPeriodoService } from 'app/core/services/despesa-adiantamento-periodo.service';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { DespesaPeriodoService } from 'app/core/services/despesa-periodo.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { DespesaAdiantamentoPeriodoData, DespesaPeriodoData, DespesaPeriodoTecnicoData } from 'app/core/types/despesa-atendimento.types';
import { TecnicoData } from 'app/core/types/tecnico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import Enumerable from 'linq';

@Component({
  selector: 'app-despesa-tecnico-lista',
  templateUrl: './despesa-tecnico-lista.component.html',
  styles: [`
        .list-grid-tecnico-atendimento {
            grid-template-columns: auto 130px 130px 130px 50px;
            @screen sm { grid-template-columns: auto 130px 130px 130px 50px; }
            @screen md { grid-template-columns: auto 130px 130px 130px 50px; }
            @screen lg { grid-template-columns: auto 130px 130px 130px 50px; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class DespesaTecnicoListaComponent implements AfterViewInit
{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;

  userSession: UserSession;
  isLoading: boolean = false;
  despesasPeriodoTecnico: DespesaPeriodoTecnicoData;
  despesasPeriodo: DespesaPeriodoData;
  despesasAdiantamentoPeriodo: DespesaAdiantamentoPeriodoData;
  tecnicos: TecnicoData;

  constructor (
    private _cdr: ChangeDetectorRef,
    private _userService: UserService,
    private _tecnicoSvc: TecnicoService,
    private _despesaAdiantamentoPeriodoSvc: DespesaAdiantamentoPeriodoService,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService)
  { this.userSession = JSON.parse(this._userService.userSession); }

  ngAfterViewInit()
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

  private async obterTecnicos()
  {
    this.tecnicos = (await this._tecnicoSvc.obterPorParametros({
      indAtivo: 1,
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort?.active || 'nome',
      sortDirection: this.sort?.direction || 'asc',
      pageSize: this.paginator?.pageSize
    }).toPromise());
  }

  private async obterDespesasPeriodoTecnico()
  {
    this.despesasPeriodoTecnico = (await this._despesaPeriodoTecnicoSvc.obterPorParametros({
      codTecnicos: this.getCodTecnicos(),
      indAtivoPeriodo: 1,
      pageSize: this.paginator?.pageSize
    }).toPromise());

    console.log(this.despesasPeriodoTecnico);

  }

  private async obterDespesasAdiantamentoPeriodo()
  {
    this.despesasAdiantamentoPeriodo = (await this._despesaAdiantamentoPeriodoSvc.obterPorParametros({
      codTecnicos: this.getCodTecnicos(),
      indAtivoPeriodo: 1,
      pageSize: this.paginator?.pageSize
    }).toPromise());

    console.log(this.despesasAdiantamentoPeriodo);
  }

  private async obterDados()
  {
    this.isLoading = true;

    await this.obterTecnicos();
    await this.obterDespesasPeriodoTecnico();
    await this.obterDespesasAdiantamentoPeriodo();

    this.isLoading = false;
  }


  private getCodTecnicos(): string
  {
    return Enumerable.from(this.tecnicos.items).select(e => e.codTecnico).toArray().join(',');
  }

  paginar()
  {
    this.obterDados();
  }
}