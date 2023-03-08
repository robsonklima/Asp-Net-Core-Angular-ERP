import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, Input, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { InstalacaoPleitoInstalService } from 'app/core/services/instalacao-pleito-instal.service';
import { InstalacaoPleitoInstalData, InstalacaoPleitoInstalParameters } from 'app/core/types/instalacao-pleito-instal.types';
import { InstalacaoPleito, InstalacaoPleitoData } from 'app/core/types/instalacao-pleito.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-instalacao-pleito-instalacao-lista',
  templateUrl: './instalacao-pleito-instalacao-lista.component.html',
  styles: [
    `.list-grid-instalacao-pleito-instal {
          grid-template-columns: 36px 36px 180px 150px 120px 72px auto 100px 36px 72px 72px 36px 42px 110px 150px;
      }`
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class InstalacaoPleitoInstalacaoListaComponent implements AfterViewInit {
  @Input() instalPleito: InstalacaoPleito;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: InstalacaoPleitoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  userSession: UserSession;

  constructor(
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _InstalacaoPleitoInstalSvc: InstalacaoPleitoInstalService,
    private _userSvc: UserService
  ) {
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  async ngAfterViewInit() {
    this.obterDados();
    this.registerEmitters();
  }

  async obterDados(filtro: string = '') {
    this.isLoading = true;
    this._cdr.detectChanges();

    const parametros: InstalacaoPleitoInstalParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort.active || 'CodInstalPleito',
      sortDirection: this.sort.direction || 'desc',
      pageSize: this.paginator?.pageSize,
      codInstalPleito: this.instalPleito?.codInstalPleito,
      filter: filtro
    }

    const data: InstalacaoPleitoInstalData = await this._InstalacaoPleitoInstalSvc.obterPorParametros({
      ...parametros
    }).toPromise();

    this.dataSourceData = data;
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

    if (this.sort && this.paginator) {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() => {
        console.log(12356);

        this.obterDados();
      });
    }

    this._cdr.detectChanges();
  }

  paginar() {
    this.obterDados();
  }
}
