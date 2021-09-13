import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { CidadeService } from 'app/core/services/cidade.service';
import { Cidade, CidadeData } from 'app/core/types/cidade.types';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-cidade-lista',
  templateUrl: './cidade-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-cidade {
          grid-template-columns: 72px auto 5% 10% 10% 48px 42px;
          
          @screen sm {
              grid-template-columns: 72px auto 5% 10% 10% 48px 42px;
          }
      
          @screen md {
              grid-template-columns: 72px auto 5% 10% 10% 48px 42px;
          }
      
          @screen lg {
              grid-template-columns: 72px auto 5% 10% 10% 10% 10%;
          }
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class CidadeListaComponent implements AfterViewInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: CidadeData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  selectedItem: Cidade | null = null;
  userSession: UserSession;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _cidadeService: CidadeService) { }

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
    this._cidadeService.obterPorParametros({
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort.active,
      sortDirection: this.sort.direction,
      pageSize: this.paginator?.pageSize,
      filter: this.searchInputControl.nativeElement.val
    }).subscribe((data: CidadeData) => {
      this.dataSourceData = data;
      this.isLoading = false;
      this._cdr.detectChanges();
    });
  }

  paginar() {
    this.obterDados();
  }

}
