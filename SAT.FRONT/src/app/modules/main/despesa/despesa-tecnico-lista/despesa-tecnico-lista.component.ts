import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, LOCALE_ID, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { DespesaAdiantamentoPeriodoService } from 'app/core/services/despesa-adiantamento-periodo.service';
import { DespesaAdiantamentoPeriodoConsultaTecnicoData } from 'app/core/types/despesa-adiantamento.types';
import { UserService } from 'app/core/user/user.service';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { fromEvent } from 'rxjs';

@Component({
  selector: 'app-despesa-tecnico-lista',
  templateUrl: './despesa-tecnico-lista.component.html',
  styles: [`
        .list-grid-tecnico {
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

export class DespesaTecnicoListaComponent extends Filterable implements AfterViewInit, IFilterable
{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;

  isLoading: boolean = false;
  tecnicos: DespesaAdiantamentoPeriodoConsultaTecnicoData;

  constructor (
    private _cdr: ChangeDetectorRef,
    protected _userService: UserService,
    private _despesaAdiantamentoPeriodoSvc: DespesaAdiantamentoPeriodoService)
  {
    super(_userService, 'despesa-tecnico');
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

  private async obterConsultaTecnicos(filter?: string)
  {
    this.tecnicos = (await this._despesaAdiantamentoPeriodoSvc.obterConsultaTecnicos({
      codFiliais: this.filter?.parametros?.codFiliais,
      indAtivoTecnico: this.filter?.parametros?.indAtivo || 1,
      indTecnicoLiberado: this.filter?.parametros?.indTecnicoLiberado,
      filter: filter,
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort?.active || 'nome',
      sortDirection: this.sort?.direction || 'asc',
      pageSize: this.paginator?.pageSize
    }).toPromise());
  }

  private async obterDados(filter?: string)
  {
    this.isLoading = true;

    await this.obterConsultaTecnicos(filter);

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
    ).subscribe((text: string) =>
    {
      this.paginator.pageIndex = 0;
      this.obterDados(text);
    });

    this.sidenav.closedStart.subscribe(() =>
    {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  paginar()
  {
    this.onPaginationChanged();
    this.obterDados();
  }
}