import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { ContratoService } from 'app/core/services/contrato.service';
import { InstalacaoLoteService } from 'app/core/services/instalacao-lote.service';
import { Contrato } from 'app/core/types/contrato.types';
import { InstalacaoLoteData, InstalacaoLoteParameters } from 'app/core/types/instalacao-lote.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-instalacao-lote-lista',
  templateUrl: 'instalacao-lote-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-instalacao-lote {
          grid-template-columns: 72px auto 240px 120px 154px;
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class InstalacaoLoteListaComponent implements AfterViewInit {
  codContrato: number;
  contrato: Contrato;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: InstalacaoLoteData;
  isLoading: boolean = false;
  userSession: UserSession;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _instalacaoLoteSvc: InstalacaoLoteService,
    private _contratoSvc: ContratoService,
    private _userSvc: UserService,
    private _route: ActivatedRoute
  ) {
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  ngAfterViewInit(): void {
	  this.codContrato = +this._route.snapshot.paramMap.get('codContrato');

    this.obterLotes();
    this.obterContrato();

    if (this.sort && this.paginator) {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() => {
        this.paginator.pageIndex = 0;
        this.obterLotes();
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
      this.obterLotes();
    });

    this._cdr.detectChanges();
  }

  async obterLotes() {
     this.isLoading = true;
    
     const params: InstalacaoLoteParameters = {
       pageSize: this.paginator?.pageSize,
       filter: this.searchInputControl.nativeElement.val,
       pageNumber: this.paginator.pageIndex + 1,
       sortActive: this.sort.active || 'NomeLote',
       sortDirection: this.sort.direction || 'asc',
       codContrato: this.codContrato
     };

     const data = await this._instalacaoLoteSvc
       .obterPorParametros(params)
       .toPromise();

     this.dataSourceData = data;
     this.isLoading = false;
     this._cdr.detectChanges();
  }

  async obterContrato() {
    this.contrato = await this._contratoSvc.obterPorCodigo(this.codContrato).toPromise();
  }

  paginar() {
    this.obterLotes();
  }
}