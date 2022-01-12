import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { DeslocamentoService } from 'app/core/services/deslocamento.service';
import { NominatimService } from 'app/core/services/nominatim.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { Orcamento, OrcamentoData } from 'app/core/types/orcamento.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-relatorio-atendimento-deslocamento',
  templateUrl: './relatorio-atendimento-deslocamento.component.html',
  styles: [`
        .list-grid-deslocamentos {
            grid-template-columns: 98px 72px;
            
            @screen md {
              grid-template-columns: 98px 72px;
            }

            @screen lg {
              grid-template-columns: 98px 72px;
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

  private async obterDados(filtro: string = '')
  {
    this.isLoading = true;

    const d = await this._deslocamentoService.obterPorParametros({}).toPromise();
    console.log(d);


    const data = {
      items: [{
        tecnico: 'João da Silva',
        numRAT: '8876744',
        origem: {
          lat: -29.950113739140246,
          lng: -51.09963131523978
        },
        destino: {
          lat: -29.993157876813363,
          lng: -51.274954971424926
        },
        distancia: 0,
        tempo: 0
      }] 
    }

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
