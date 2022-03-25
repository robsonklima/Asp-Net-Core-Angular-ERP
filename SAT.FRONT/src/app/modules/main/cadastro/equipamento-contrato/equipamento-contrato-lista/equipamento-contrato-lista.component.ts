import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { EquipamentoContratoData, EquipamentoContratoParameters } from 'app/core/types/equipamento-contrato.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-equipamento-contrato-lista',
  templateUrl: './equipamento-contrato-lista.component.html',
  styles: [
    /* language=SCSS */
    `
    .list-grid {
      grid-template-columns: 72px 345px 240px 120px 120px 120px 120px 120px auto;
      
      @screen sm {
          grid-template-columns: 72px 345px 240px 120px 120px 120px 120px 120px auto;
      }

      @screen md {
          grid-template-columns: 72px 345px 240px 120px 120px 120px 120px 120px auto;
      }

      @screen lg {
          grid-template-columns: 72px 345px 240px 120px 120px 120px 120px 120px auto;
      }
    }  
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class EquipamentoContratoListaComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: EquipamentoContratoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _equipamentoContratoService: EquipamentoContratoService,
  ) { }

  async ngAfterViewInit() {
    this.obterDados();

    if (this.sort && this.paginator) {
      fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
        map((event: any) => {
          return event.target.value;
        })
        , debounceTime(700)
        , distinctUntilChanged()
      ).subscribe((text: string) => {
        this.paginator.pageIndex = 0;
        this.searchInputControl.nativeElement.val = text;
        this.obterDados();
      });

      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() => {
        this.paginator.pageIndex = 0;
        this.obterDados();
      });
    }

    this._cdr.detectChanges();
  }

  async obterDados() {
    this.isLoading = true;
    this.dataSourceData = await this._equipamentoContratoService.obterPorParametros({
      pageNumber: this.paginator?.pageIndex + 1,
      sortDirection: this.sort?.direction || 'asc',
      pageSize: this.paginator?.pageSize,
      filter: this.searchInputControl.nativeElement.val
    }).toPromise();

    this.isLoading = false;
    this._cdr.detectChanges();
  }

  paginar() {
    this.obterDados();
  }
}
