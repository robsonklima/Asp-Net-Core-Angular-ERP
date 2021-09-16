import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { Regiao, RegiaoData } from 'app/core/types/regiao.types';
import { RegiaoService } from 'app/core/services/regiao.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { UserSession } from 'app/core/user/user.types';
import { fuseAnimations } from '@fuse/animations';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-regiao-lista',
  templateUrl: './regiao-lista.component.html',
  styles: [`
    .list-grid {
      grid-template-columns: 72px auto 32px;
      
      @screen sm {
          grid-template-columns: 72px auto 32px;
      }

      @screen md {
          grid-template-columns: 72px auto 72px;
      }

      @screen lg {
          grid-template-columns: 72px auto 72px;
      }
    }  
  `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class RegiaoListaComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: RegiaoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  selectedItem: Regiao | null = null;
  userSession: UserSession;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _regiaoService: RegiaoService
  ) { }

  ngAfterViewInit(): void {
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

  obterDados(): void {
    this.isLoading = true;
    this._regiaoService.obterPorParametros({
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort.active,
      sortDirection: this.sort.direction,
      pageSize: this.paginator?.pageSize,
      filter: this.searchInputControl.nativeElement.val
    }).subscribe((data: RegiaoData) => {
      this.dataSourceData = data;
      this.isLoading = false;
      this._cdr.detectChanges();
    });
  }

  paginar() {
    this.obterDados();
  }
}