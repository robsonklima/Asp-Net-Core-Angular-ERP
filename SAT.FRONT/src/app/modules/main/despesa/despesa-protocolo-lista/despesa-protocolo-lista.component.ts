import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { UserService } from 'app/core/user/user.service';
import { LOCALE_ID } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { Filterable } from 'app/core/filters/filterable';
import { MatSidenav } from '@angular/material/sidenav';
import { IFilterable } from 'app/core/types/filtro.types';
import { DespesaProtocoloService } from 'app/core/services/despesa-protocolo.service';
import { DespesaProtocoloData } from 'app/core/types/despesa-protocolo.types';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { fromEvent } from 'rxjs';
registerLocaleData(localePt);

@Component({
  selector: 'app-despesa-protocolo-lista',
  templateUrl: './despesa-protocolo-lista.component.html',
  styles: [`
        .list-grid-despesa-atendimento {
            grid-template-columns: 100px 200px auto 100px 100px 50px;
            @screen sm { grid-template-columns: 100px 200px auto 100px 100px 50px; }
            @screen md { grid-template-columns: 100px 200px auto 100px 100px 50px; }
            @screen lg { grid-template-columns: 100px 200px auto 100px 100px 50px; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})

export class DespesaProtocoloListaComponent extends Filterable implements AfterViewInit, IFilterable
{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;


  isLoading: boolean = false;
  protocolos: DespesaProtocoloData;

  constructor (
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _despesaProtocoloSvc: DespesaProtocoloService)
  {
    super(_userService, "despesa-protocolo");
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
        this.onSortChanged()
        this.obterDados();
      });
    }

    this.registerEmitters();
    this._cdr.detectChanges();
  }

  private async obterProtocolos(filter: string)
  {
    this.protocolos = await this._despesaProtocoloSvc.obterPorParametros(
      {
        pageNumber: this.paginator?.pageIndex + 1,
        pageSize: this.paginator?.pageSize,
        sortActive: 'codDespesaProtocolo',
        sortDirection: 'desc',
        filter: filter
      }).toPromise();

    console.log(this.protocolos);
  }

  public async obterDados(filter: string = null)
  {
    this.isLoading = true;

    await this.obterProtocolos(filter);

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

  obterDataInicial()
  {

  }

  obterDataFinal()
  {

  }

  obterTecnicos()
  {

  }

  public paginar()
  {
    this.onPaginationChanged();
    this.obterDados();
  }
}
