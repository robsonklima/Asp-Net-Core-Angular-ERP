import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { DeslocamentoService } from 'app/core/services/deslocamento.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { Orcamento } from 'app/core/types/orcamento.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-relatorio-atendimento-deslocamento',
  templateUrl: './relatorio-atendimento-deslocamento.component.html',
  styles: [`
        .list-grid-deslocamentos {
            grid-template-columns: 200px auto 128px;
            
            @screen md {
              grid-template-columns: 200px auto 128px;
            }

            @screen lg {
              grid-template-columns: 200px auto 128px;
          }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class RelatorioAtendimentoDeslocamentoComponent extends Filterable implements AfterViewInit, IFilterable
{
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;

  @ViewChild(MatSort) sort: MatSort;

  dataSourceData: any;
  selectedItem: Orcamento | null = null;
  isLoading: boolean = false;
  protected _onDestroy = new Subject<void>();

  constructor (
    private _cdr: ChangeDetectorRef,
    private _deslocamentoService: DeslocamentoService,
    protected _userService: UserService
  )
  {
    super(_userService, 'deslocamento');
  }

  ngAfterViewInit(): void
  {
    this.obterDados();
    this.registerEmitters();
    this._cdr.detectChanges();
  }

  registerEmitters(): void
  {
    this.sidenav.closedStart.subscribe(() =>
    {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  private async obterDados()
  {
    this.isLoading = true;

    const data = await this._deslocamentoService.obterPorParametros({
      codTecnico: 2460,
      dataHoraInicioInicio: moment().add(-60, 'days').format('yyyy-MM-DD 00:00:01'),
      dataHoraInicioFim: moment().format('yyyy-MM-DD 23:59:59'),
      pageNumber: this.paginator.pageIndex + 1,
      pageSize: this.paginator?.pageSize,
    }).toPromise();

    this.dataSourceData = data;
    this.isLoading = false;
  }

  paginar()
  {
    this.onPaginationChanged();
    this.obterDados();
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
