import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { TicketService } from 'app/core/services/ticket.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { Ticket } from 'app/core/types/ticket.types';
import { UserService } from 'app/core/user/user.service';
import { Utils } from 'app/core/utils/utils';
import moment from 'moment';
import { fromEvent, interval, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, startWith, takeUntil } from 'rxjs/operators';

@Component({
	selector: 'app-ticket-lista',
	templateUrl: './ticket-lista.component.html',
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class TicketListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild('searchInputControl', { read: ElementRef }) searchInputControl: ElementRef;
	tickets: Ticket[] = [];
	isLoading: boolean = true;
	protected _onDestroy = new Subject<void>();

	constructor(
		protected _userService: UserService,
		private _cdr: ChangeDetectorRef,
		private _ticketService: TicketService,
		private _exportacaoService: ExportacaoService,
		private _snack: CustomSnackbarService,
		private _utils: Utils
	) {
		super(_userService, 'ticket');
	}

	ngAfterViewInit() {
		interval(3 * 60 * 1000)
			.pipe(
				startWith(0),
				takeUntil(this._onDestroy)
			)
			.subscribe(() => {
				this.obterDados();
			});

		this.registerEmitters();

		fromEvent(this.searchInputControl?.nativeElement , 'keyup').pipe(
			map((event: any) => {
				return event.target.value;
			})
			, debounceTime(700)
			, distinctUntilChanged()
		).subscribe((text: string) => {
			this.searchInputControl.nativeElement.val = text;
			this.obterDados(text);
		});

		this._cdr.detectChanges();
	}

	registerEmitters(): void {
		this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();
			this.obterDados();
		})
	}

	loadFilter(): void {
		super.loadFilter();
	}

	async obterDados(filter: string = '') {
		this.isLoading = true;

		const data = await this._ticketService.obterPorParametros({
			...{
				filter: filter,
				sortActive: 'ordem',
				sortDirection: 'asc'
			},
			...this.filter?.parametros
		}).toPromise();

		this.tickets = data.items;
		this.isLoading = false;
	}

	async toggleStatusTicket(ticket: Ticket) {
		const t = this._ticketService.atualizar({
			...ticket,
			...{
				indAtivo: +!ticket.indAtivo
			}
		}).subscribe(() => {
			this.obterDados();		
		});
	}

	async exportar() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.TICKET,
			entityParameters: {}
		}

		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);

		this.isLoading = false;
	}

	async dropped(event: CdkDragDrop<string[]>) {
		moveItemInArray(this.tickets, event.previousIndex, event.currentIndex);
		
		for (const [i, ticket] of this.tickets.entries()) {
			ticket.ordem = i+1;

			await this._ticketService.atualizar(ticket).subscribe();
		}
		
		this._snack.exibirToast('Tickets reordenados com sucesso', 'success');
	}

	obterTempoAbertura(dataHora: string): string {
	    return this._utils.obterDuracao(dataHora);
  	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
