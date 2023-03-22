import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, Input, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { InstalacaoMotivoMultaService } from 'app/core/services/instalacao-motivo-multa.service';
import { InstalacaoPagtoInstalService } from 'app/core/services/instalacao-Pagto-instal.service';
import { InstalacaoMotivoMulta, InstalacaoMotivoMultaParameters } from 'app/core/types/instalacao-motivo-multa.types';
import { InstalacaoPagtoInstal, InstalacaoPagtoInstalData, InstalacaoPagtoInstalParameters } from 'app/core/types/instalacao-pagto-instal.types';
import { InstalacaoPagto } from 'app/core/types/instalacao-pagto.types';
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
  motivosMulta: InstalacaoMotivoMulta[] = [];
  isLoading: boolean = false;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  userSession: UserSession;

  constructor(
    private _instalPagtoInstalSvc: InstalacaoPagtoInstalService,
    private _instalMotivoMultaSvc: InstalacaoMotivoMultaService,
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _snack: CustomSnackbarService,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngAfterViewInit() {
    this.obterMotivoMulta();
    this.obterDados();
    this.registerEmitters();
  }

  onInstalMotivoMultaChange(codInstalMotivoMulta: any, instal: InstalacaoPagtoInstal) {
    let obj: InstalacaoPagtoInstal = {
      ...instal,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
        codInstalMotivoMulta: codInstalMotivoMulta
      }
    };

    this._instalPagtoInstalSvc.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Motivo tualizado com sucesso!", "success");
    });
  }

  onIndEndossarMultaChange(indEndossarMulta: any, instal: InstalacaoPagtoInstal) {
    let obj: InstalacaoPagtoInstal = {
      ...instal,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
        indEndossarMulta: indEndossarMulta
      }
    };
    
    this._instalPagtoInstalSvc.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Motivo tualizado com sucesso!", "success");
    });
  }

  onVlrMultaChange(vlrMulta: any, instal: InstalacaoPagtoInstal) {
    let obj: InstalacaoPagtoInstal = {
      ...instal,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
        vlrMulta: vlrMulta.target.value
      }
    };
    
    this._instalPagtoInstalSvc.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Motivo tualizado com sucesso!", "success");
    });
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

  async obterMotivoMulta(filtro: string = '') {
		let params: InstalacaoMotivoMultaParameters = {
			filter: filtro,
			sortActive: 'nomeMotivoMulta',
			sortDirection: 'asc',
			pageSize: 10
		};

		const data = await this._instalMotivoMultaSvc
			.obterPorParametros(params)
			.toPromise();

		this.motivosMulta = data.items;
	}

  paginar() {
    this.obterDados();
  }
}
