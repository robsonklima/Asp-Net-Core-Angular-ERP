import { AfterViewInit, ChangeDetectorRef, Component, LOCALE_ID, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { DespesaAdiantamentoPeriodoService } from 'app/core/services/despesa-adiantamento-periodo.service';
import { DespesaAdiantamentoPeriodoConsultaTecnicoData } from 'app/core/types/despesa-adiantamento';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

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
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})

export class DespesaTecnicoListaComponent implements AfterViewInit
{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;

  userSession: UserSession;
  isLoading: boolean = false;
  tecnicos: DespesaAdiantamentoPeriodoConsultaTecnicoData;

  constructor (
    private _cdr: ChangeDetectorRef,
    private _userService: UserService,
    private _despesaAdiantamentoPeriodoSvc: DespesaAdiantamentoPeriodoService)
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

  private async obterConsultaTecnicos()
  {
    this.tecnicos = (await this._despesaAdiantamentoPeriodoSvc.obterConsultaTecnicos({
      indAtivoTecnico: 1,
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort?.active || 'nome',
      sortDirection: this.sort?.direction || 'asc',
      pageSize: this.paginator?.pageSize
    }).toPromise());
  }

  private async obterDados()
  {
    this.isLoading = true;

    await this.obterConsultaTecnicos();

    this.isLoading = false;
  }

  paginar()
  {
    this.obterDados();
  }
}