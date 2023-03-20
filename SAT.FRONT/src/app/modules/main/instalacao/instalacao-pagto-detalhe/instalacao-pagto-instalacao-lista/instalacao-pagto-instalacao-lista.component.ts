import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, Input, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { InstalacaoPagtoInstalService } from 'app/core/services/instalacao-Pagto-instal.service';
import { InstalacaoPagtoInstal, InstalacaoPagtoInstalData, InstalacaoPagtoInstalParameters } from 'app/core/types/instalacao-pagto-instal.types';
import { InstalacaoPagto } from 'app/core/types/instalacao-pagto.types';
import { Instalacao, InstalacaoData, InstalacaoParameters } from 'app/core/types/instalacao.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-instalacao-pagto-instalacao-lista',
  templateUrl: './instalacao-pagto-instalacao-lista.component.html',
  styles: [
    `.list-grid-instalacao-pagto-instal {
            grid-template-columns: 120px 120px 120px 72px 100px 100px 180px 100px 72px 36px;
        }`
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class InstalacaoPagtoInstalacaoListaComponent implements AfterViewInit {
  @Input() instalPagto: InstalacaoPagto;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: InstalacaoPagtoInstalData;
  instalPagtoInstal: InstalacaoPagtoInstal;
  isLoading: boolean = false;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  userSession: UserSession;

  constructor(
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _instalPagtoInstalSvc: InstalacaoPagtoInstalService,
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

    const parametros: InstalacaoPagtoInstalParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort.active || 'CodInstalPagto',
      sortDirection: this.sort.direction || 'desc',
      pageSize: this.paginator?.pageSize,
      codInstalPagto: this.instalPagto.codInstalPagto,
      filter: filtro
    }

    const data: InstalacaoPagtoInstalData = await this._instalPagtoInstalSvc.obterPorParametros({
      ...parametros
    }).toPromise();

    this.dataSourceData = data;

    console.log(this.dataSourceData);
    
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
        this.obterDados();
      });
    }

    this._cdr.detectChanges();
  }

  async onChange($event: MatSlideToggleChange, instalacao: Instalacao) {
    if ($event.checked) {
      this._instalPagtoInstalSvc.criar({
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        codInstalPagto: this.instalPagto.codInstalPagto,
        codInstalacao: instalacao.codInstalacao,
        codInstalTipoParcela: 2
      }).subscribe();
    } else {
      this._instalPagtoInstalSvc.deletar(instalacao.codInstalacao, this.instalPagto.codInstalPagto, 2).subscribe();
    }
  }

  isInstalacaoPagto(codInstalacao: number) {
    //if (this.instalPagto.instalacoesPagtoInstal.find(i => i.codInstalacao == codInstalacao)) {
      return false;
    //}

    return false
  }

  paginar() {
    this.obterDados();
  }
}
