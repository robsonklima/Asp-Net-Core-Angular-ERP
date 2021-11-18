import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { ContratoService } from 'app/core/services/contrato.service';
import { ContratoData, ContratoParameters } from 'app/core/types/contrato.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-instalacao-contrato-lista',
  templateUrl: 'instalacao-contrato-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-instalacao-contrato {
          grid-template-columns: 72px 136px auto 136px 200px 154px;
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class InstalacaoContratoListaComponent implements AfterViewInit {
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: ContratoData;
  isLoading: boolean = false;
  userSession: UserSession;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _contratoSvc: ContratoService,
    private _userSvc: UserService
  ) {
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  ngAfterViewInit(): void {
    this.obterContratos();

    if (this.sort && this.paginator) {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() => {
        this.paginator.pageIndex = 0;
        this.obterContratos();
      });
    }

    fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
      map((event: any) => {
        return event.target.value;
      })
      , debounceTime(700)
      , distinctUntilChanged()
    ).subscribe((text: string) => {
      this.paginator.pageIndex = 0;
      this.searchInputControl.nativeElement.val = text;
      this.obterContratos();
    });

    this._cdr.detectChanges();
  }

  async obterContratos() {
    this.isLoading = true;
    
    const params: ContratoParameters = {
      pageSize: this.paginator?.pageSize,
      filter: this.searchInputControl.nativeElement.val,
      pageNumber: this.paginator.pageIndex + 1,
      sortActive: this.sort.active || 'CodContrato',
      sortDirection: this.sort.direction || 'desc',
    };

    const data = await this._contratoSvc
      .obterPorParametros(params)
      .toPromise();

    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  paginar() {
    this.obterContratos();
  }
}