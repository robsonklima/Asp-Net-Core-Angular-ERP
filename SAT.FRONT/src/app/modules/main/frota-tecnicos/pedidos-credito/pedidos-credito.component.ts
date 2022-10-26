import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { TicketLogPedidoCreditoService } from 'app/core/services/ticket-log-pedido-credito.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { TicketLogPedidoCredito, TicketLogPedidoCreditoData, TicketLogPedidoCreditoParameters } from 'app/core/types/ticket-log-pedido-credito.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
	selector: 'app-pedidos-credito',
	templateUrl: './pedidos-credito.component.html',
	styles: [
		`.list-grid-pedidos-credito {
			grid-template-columns: 96px 96px 96px auto 104px 144px 86px;
			
			@screen sm {
				grid-template-columns: 96px 96px 96px auto 104px 144px 86px;
			}
		
			@screen md {
				grid-template-columns: 96px 96px 96px auto 104px 144px 86px;
			}
		
			@screen lg {
				grid-template-columns: 96px 96px 96px auto 104px 144px 86px;
			}
		}`
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class PedidosCreditoComponent implements AfterViewInit {
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: TicketLogPedidoCreditoData;
	isLoading: boolean = false;
	@ViewChild('searchInputControl', { read: ElementRef }) searchInputControl: ElementRef;
	userSession: UserSession;
	protected _onDestroy = new Subject<void>();

	constructor(
		private _pedidoCreditoService: TicketLogPedidoCreditoService,
		private _cdr: ChangeDetectorRef,
		private _exportacaoService: ExportacaoService,
		private _snack: CustomSnackbarService,
		private _userService: UserService,
		private _dialog: MatDialog
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	onSidenavClosed(): void {
		if (this.paginator) this.paginator.pageIndex = 0;
		this.obterDados();
	}

	async ngAfterViewInit() {
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
		const parametros: TicketLogPedidoCreditoParameters = {
			pageNumber: this.paginator?.pageIndex + 1,
			sortActive: 'CodTicketLogPedidoCredito',
			sortDirection: 'desc',
			pageSize: this.paginator?.pageSize,
			filter: filtro
		}

		const data = await this._pedidoCreditoService.obterPorParametros({
			...parametros
		}).toPromise();

		this.dataSourceData = data;
		console.log(data.items);
		
		this.isLoading = false;
		this._cdr.detectChanges();
	}

	public async exportar() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.PEDIDOS_CREDITO,
			entityParameters: {}
		}

		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
		this.isLoading = false;
	}

	paginar() {
		this.obterDados();
	}

	deletar(pedido: TicketLogPedidoCredito) {
		const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
			  titulo: 'Confirmação',
			  message: `Deseja remover o pedido ${ pedido.codDespesaPeriodoTecnico } 
			  	no valor de R$ ${ pedido.valor } para o cartão ${ pedido.numeroCartao }?`,
			  buttonText: {
				ok: 'Sim',
				cancel: 'Não'
			  }
			},
			backdropClass: 'static'
		  });
	  
		  dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
		  {
			if (confirmacao)
			{
			  this._pedidoCreditoService.deletar(pedido.codTicketLogPedidoCredito).toPromise();
			  this._snack.exibirToast('Pedido excluido com sucesso', 'success');
			  this.obterDados();
			}
		  });
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
