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
import { DespesaProtocolo, DespesaProtocoloData } from 'app/core/types/despesa-protocolo.types';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { fromEvent } from 'rxjs';
import Enumerable from 'linq';
registerLocaleData(localePt);

@Component({
  selector: 'app-despesa-protocolo-lista',
  templateUrl: './despesa-protocolo-lista.component.html',
  styles: [`
        .list-grid-despesa-atendimento {
            grid-template-columns: 100px 100px 250px 100px 100px auto;
            @screen sm { grid-template-columns: 100px 100px 250px 100px 100px auto; }
            @screen md { grid-template-columns: 100px 100px 250px 100px 100px auto; }
            @screen lg { grid-template-columns: 100px 100px 250px 100px 100px auto; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})

export class DespesaProtocoloListaComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  isLoading: boolean = false;
  protocolos: DespesaProtocoloData;

  constructor(
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _despesaProtocoloSvc: DespesaProtocoloService
  ) {
    super(_userService, "despesa-protocolo");
  }

  ngAfterViewInit() {
    this.obterDados();

    if (this.sort && this.paginator)
    {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() => {
        this.onSortChanged()
        this.obterDados();
      });
    }

    this.registerEmitters();
    this._cdr.detectChanges();
  }

  private async obterProtocolos(filter: string) {
    const params = {
      ...{
        pageNumber: this.paginator?.pageIndex + 1,
        pageSize: this.paginator?.pageSize,
        sortActive: 'codDespesaProtocolo',
        sortDirection: 'desc',
        filter: filter,
      },
      ...this.filter?.parametros
    }

    this.protocolos = await this._despesaProtocoloSvc.obterPorParametros(params).toPromise();
  }

  public async obterDados(filter: string = null) {
    this.isLoading = true;

    await this.obterProtocolos(filter);

    this.isLoading = false;
  }

  registerEmitters(): void {
    fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
      map((event: any) => {
        return event.target.value;
      })
      , debounceTime(1000)
      , distinctUntilChanged()
    ).subscribe((text: string) => {
      this.paginator.pageIndex = 0;
      this.obterDados(text);
    });

    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  obterDataInicial(dp: DespesaProtocolo) {
    var dataInicial = Enumerable.from(dp.despesaProtocoloPeriodoTecnico)
      .select(e => e.despesaPeriodoTecnico.despesaPeriodo)
      .orderBy(e => e.dataInicio)
      .firstOrDefault()?.dataInicio;

    return dataInicial != null ? dataInicial : "";
  }

  obterDataFinal(dp: DespesaProtocolo) {
    var dataFinal = Enumerable.from(dp.despesaProtocoloPeriodoTecnico)
      .select(e => e.despesaPeriodoTecnico.despesaPeriodo)
      .orderByDescending(e => e.dataFim)
      .firstOrDefault()?.dataFim;

    return dataFinal != null ? dataFinal : "";
  }

  obterTecnicos(dp: DespesaProtocolo) {
    var tecnicos = Enumerable.from(dp.despesaProtocoloPeriodoTecnico)
      .select(e => e.despesaPeriodoTecnico.tecnico.nome)
      .orderBy(e => e)
      .toArray();

    return tecnicos != null ? tecnicos.join(", ") : "---";
  }

  public paginar() {
    this.onPaginationChanged();
    this.obterDados();
  }
}
