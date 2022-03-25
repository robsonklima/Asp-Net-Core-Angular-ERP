import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { PecaParameters, PecaStatus } from 'app/core/types/peca.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { ClientePecaService } from 'app/core/services/cliente-peca.service';
import { ClientePecaData, ClientePecaParameters } from 'app/core/types/cliente-peca.types';

@Component({
  selector: 'app-cliente-peca-lista',
  templateUrl: './cliente-peca-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-ge {
          grid-template-columns: 20% 100px 100px 20% 20% auto;
          
          @screen sm {
              grid-template-columns: 20% 100px 100px 20% 20% auto;
          }
      
          @screen md {
              grid-template-columns: 20% 100px 100px 20% 20% auto;
          }
      
          @screen lg {
              grid-template-columns: 20% 100px 100px 20% 20% auto;
          }
      }
    `
  ],
})
export class ClientePecaListaComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: ClientePecaData;
  byteArray;
  isLoading: boolean = false;
  pecaStatus: string[] = [];

  constructor(
    private _cdr: ChangeDetectorRef,
    private _clientePecaService: ClientePecaService) { }

  ngOnInit(): void { }

  ngAfterViewInit(): void {
    this.obterDados();
    this.obterStatus();

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

  public async obterDados() {
    this.isLoading = true;

    const params: ClientePecaParameters =
    {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort?.active || 'codClientePeca',
      sortDirection: this.sort?.direction || 'desc',
      pageSize: this.paginator?.pageSize,
      filter: this.searchInputControl.nativeElement.val
    }

    this.dataSourceData = await this._clientePecaService.obterPorParametros(params).toPromise();
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  private async obterStatus(): Promise<void> {
    Object.keys(PecaStatus)
      .filter((e) => isNaN(Number(e)))
      .forEach((tr) =>
        this.pecaStatus.push(tr));
  }

  public async exportar() {
    this.isLoading = true;

    const params: PecaParameters =
    {
      sortDirection: 'desc',
      pageSize: 1000
    }
    this.isLoading = false;
  }

  public paginar() { this.obterDados(); }
}
