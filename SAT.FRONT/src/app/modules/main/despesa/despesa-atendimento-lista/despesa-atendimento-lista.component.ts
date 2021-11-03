import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { UserService } from 'app/core/user/user.service';
import { LOCALE_ID } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { DespesaPeriodoTecnicoAtendimentoData } from 'app/core/types/despesa-adiantamento.types';
import { FilterableComponent } from 'app/shared/filter/filterable-component';
import { MatSidenav } from '@angular/material/sidenav';
registerLocaleData(localePt);

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
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})

export class DespesaAtendimentoListaComponent extends FilterableComponent implements AfterViewInit
{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  @ViewChild('sidenav') sidenav: MatSidenav;

  isLoading: boolean = false;
  atendimentos: DespesaPeriodoTecnicoAtendimentoData;

  constructor (
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService)
  {
    super(_userService, "despesa-atendimento");
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
        this.obterDados();
        this.paginator.pageIndex = 0;
      });
    }

    this._cdr.detectChanges();
  }

  private async obterDespesasPeriodoTecnico()
  {
    this.atendimentos = (await this._despesaPeriodoTecnicoSvc.obterAtendimentos({
      codTecnico: this.userSession.usuario?.codTecnico,
      indAtivoPeriodo: 1,
      pageNumber: this.paginator?.pageIndex + 1,
      pageSize: this.paginator?.pageSize,
      sortActive: 'dataInicio',
      sortDirection: 'desc'
    }).toPromise());
  }

  public async obterDados()
  {
    this.isLoading = true;

    await this.obterDespesasPeriodoTecnico();

    this.isLoading = false;
  }

  public paginar()
  {
    this.obterDados();
  }
}