import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { UserSession } from 'app/core/user/user.types';
import { fuseAnimations } from '@fuse/animations';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { RegiaoAutorizada, RegiaoAutorizadaData } from 'app/core/types/regiao-autorizada.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';

@Component({
  selector: 'app-regiao-autorizada-lista',
  templateUrl: './regiao-autorizada-lista.component.html',
  styles: [`
    .regiao-autorizada-list-grid {
      grid-template-columns: 68px auto 146px 146px 146px 32px 56px;
      
      @screen sm {
          grid-template-columns: 68px auto 146px 146px 146px 32px 56px;
      }

      @screen md {
          grid-template-columns: 68px auto 146px 146px 146px 72px 56px;
      }

      @screen lg {
          grid-template-columns: 68px auto 146px 146px 146px 72px 56px;
      }
    }  
  `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class RegiaoAutorizadaListaComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: RegiaoAutorizadaData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  selectedItem: RegiaoAutorizada | null = null;
  userSession: UserSession;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _regiaoAutorizadaService: RegiaoAutorizadaService,
    private _dialog: MatDialog,
    private _snack: CustomSnackbarService
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
    this._regiaoAutorizadaService.obterPorParametros({
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort.active,
      sortDirection: this.sort.direction,
      pageSize: this.paginator?.pageSize,
      filter: this.searchInputControl.nativeElement.val
    }).subscribe((data: RegiaoAutorizadaData) => {
      this.dataSourceData = data;
      this.isLoading = false;
      this._cdr.detectChanges();
    });
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

		dialogRef.afterClosed().subscribe(async (confirmacao: boolean) =>
		{
			if (confirmacao)
			{
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

  paginar() {
    this.obterDados();
  }
}