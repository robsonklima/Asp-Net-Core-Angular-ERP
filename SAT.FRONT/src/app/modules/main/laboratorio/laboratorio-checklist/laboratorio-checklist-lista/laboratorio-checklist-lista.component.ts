import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { ORCheckListService } from 'app/core/services/or-checklist.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { ORCheckListData } from 'app/core/types/or-checklist.types';
import { OR, ORParameters } from 'app/core/types/OR.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import moment from 'moment';
import { fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-laboratorio-checklist-lista',
  templateUrl: './laboratorio-checklist-lista.component.html',
  styles: [
    `.list-grid-or-checklist {
			grid-template-columns: 72px auto 64px 98px 264px 156px 96px;
			
			@screen sm {
				grid-template-columns: 72px auto 64px 98px 264px 156px 96px;
			}
		
			@screen md {
				grid-template-columns: 72px auto 64px 98px 264px 156px 96px;
			}
		
			@screen lg {
				grid-template-columns: 72px auto 64px 98px 264px 156px 96px;
			}
		}`
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class LaboratorioCheckListListaComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: ORCheckListData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { read: ElementRef }) searchInputControl: ElementRef;
  userSession: UserSession;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _orCheckListService: ORCheckListService,
    private _cdr: ChangeDetectorRef,
    private _exportacaoService: ExportacaoService,
    private _snack: CustomSnackbarService,
    private _dialog: MatDialog,
    protected _userService: UserService,
  ) {
    super(_userService, 'laboratorio-checklist');
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngAfterViewInit() {
    this.registerEmitters();
    this.obterDados();

    if (this.sort && this.paginator)
    {
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
    const parametros: ORParameters = {
      ...{
        pageNumber: this.paginator?.pageIndex + 1,
        sortActive: 'codORCheckList',
        sortDirection: 'desc',
        pageSize: this.paginator?.pageSize,
        filter: filtro
      },
      ...this.filter?.parametros
    }

    await this._orCheckListService.obterPorParametros(parametros).subscribe((data) => {
      this.dataSourceData = data;
      this.isLoading = false;
      this._cdr.detectChanges();
      console.log(data);
      
    }, () => {
      this._snack.exibirToast('Erro ao carregar os dados', 'error');
      this.isLoading = false;
    });
  }

  public async exportar() {
    this.isLoading = true;

    let exportacaoParam: Exportacao = {
      formatoArquivo: ExportacaoFormatoEnum.EXCEL,
      tipoArquivo: ExportacaoTipoEnum.ORDEM_REPARO,
      entityParameters: {}
    }

    await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
    this.isLoading = false;
  }

  paginar() {
    this.obterDados();
  }

  deletar(or: OR) {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: `Deseja remover a ordem ${or.codOR}?`,
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      },
      backdropClass: 'static'
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
      if (confirmacao)
      {
        this._orCheckListService.deletar(or.codOR).toPromise();
        this._snack.exibirToast('Pedido excluido com sucesso', 'success');
        this.obterDados();
      }
    });
  }

  onSidenavClosed(): void {
		if (this.paginator) this.paginator.pageIndex = 0;
		this.loadFilter();
		this.obterDados();
	}
  
  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  obterTempo(minutos: number) {
    return moment.utc().startOf('day').add(minutos, 'minutes').format('HH:mm')
  }

  loadFilter(): void {
		super.loadFilter();
	}

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}

