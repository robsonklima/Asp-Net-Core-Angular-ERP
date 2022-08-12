import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { UserSession } from 'app/core/user/user.types';
import { fuseAnimations } from '@fuse/animations';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { RegiaoAutorizada, RegiaoAutorizadaData, RegiaoAutorizadaParameters } from 'app/core/types/regiao-autorizada.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable, IFilterBase } from 'app/core/types/filtro.types';
import { MatSidenav } from '@angular/material/sidenav';
import { UserService } from 'app/core/user/user.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { FileMime } from 'app/core/types/file.types';

@Component({
  selector: 'app-regiao-autorizada-lista',
  templateUrl: './regiao-autorizada-lista.component.html',
  styles: [`
    .regiao-autorizada-list-grid {
      grid-template-columns: 68px auto 146px 146px 146px 32px 56px;
      
      /* @screen sm {
          grid-template-columns: 68px auto 146px 146px 146px 32px 56px;
      }

      @screen md {
          grid-template-columns: 68px auto 146px 146px 146px 72px 56px;
      }

      @screen lg {
          grid-template-columns: 68px auto 146px 146px 146px 72px 56px;
      } */
    }  
  `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class RegiaoAutorizadaListaComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild('sidenav') public sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: RegiaoAutorizadaData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  selectedItem: RegiaoAutorizada | null = null;
  userSession: UserSession;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _regiaoAutorizadaService: RegiaoAutorizadaService,
    private _dialog: MatDialog,
    private _snack: CustomSnackbarService,
    protected _userService: UserService,
    private _exportacaoService: ExportacaoService
  ) {
    super(_userService, 'regiao-autorizada')
    this.userSession = JSON.parse(this._userService.userSession);
  }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  async ngAfterViewInit() {
    this.registerEmitters();
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
        this.obterDados(text);
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

  async obterDados(filtro: string = '') {
    this.isLoading = true;

    const params: RegiaoAutorizadaParameters = {
      ...{
        pageNumber: this.paginator?.pageIndex + 1,
        sortActive: this.sort?.active,
        sortDirection: this.sort?.direction || 'asc',
        pageSize: this.paginator?.pageSize,
        filter: filtro
      },
      ...this.filter?.parametros
    }
    const data = await this._regiaoAutorizadaService
      .obterPorParametros(params)
      .toPromise();

    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();

  }

  remover(ra: RegiaoAutorizada) {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: `Deseja remover a região autorizada?`,
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
      if (confirmacao) {
        this.isLoading = true;
        this._regiaoAutorizadaService
          .deletar(ra.codRegiao, ra.codAutorizada, ra.codFilial)
          .subscribe(() => {
            this._snack.exibirToast(`Registro removido com sucesso`, 'success');
            this.isLoading = false;
            this.obterDados();
          }, (e) => {
            this._snack.exibirToast(e.message || e.error.message, 'error');
            this.isLoading = false;
          });
      }
    });
  }

  async exportar() {
    this.isLoading = true;

    await this._exportacaoService.exportar('RegiaoAutorizada', FileMime.Excel, this.filter?.parametros);

    this.isLoading = false;
  }

  paginar() {
    this.obterDados();
  }
}