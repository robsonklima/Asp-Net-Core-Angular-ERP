import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { PecaService } from 'app/core/services/peca.service';
import { PecaData, PecaParameters } from 'app/core/types/peca.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-peca-lista',
  templateUrl: './peca-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-ge {
          grid-template-columns: 15% auto;
          
          @screen sm {
              grid-template-columns: 15% auto;
          }
      
          @screen md {
              grid-template-columns: 15% auto;
          }
      
          @screen lg {
              grid-template-columns: 15% auto;
          }
      }
    `
  ],
})
export class PecaListaComponent implements OnInit 
{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: PecaData;
  byteArray;
  isLoading: boolean = false;
  
  constructor(private _cdr: ChangeDetectorRef, private _pecaService: PecaService) { }

  ngOnInit(): void { }

  ngAfterViewInit(): void 
  {
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

  public async obterDados() 
  {
    this.isLoading = true;

    const params: PecaParameters = 
    {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort?.active || 'codPeca',
      sortDirection: this.sort?.direction || 'desc',
      pageSize: this.paginator?.pageSize,
      filter: this.searchInputControl.nativeElement.val
    }

    this.dataSourceData = await this._pecaService.obterPorParametros(params).toPromise();
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  public async exportarExcel()
  {
    const data = await this._pecaService.exportarExcel();
    const blob = new Blob([data], {type:'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;'}, );
    var url = window.URL.createObjectURL(blob);
    window.open(url);
  }

  public paginar() { this.obterDados(); }
}