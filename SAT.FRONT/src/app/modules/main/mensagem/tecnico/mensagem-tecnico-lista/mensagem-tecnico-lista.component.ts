import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { MensagemTecnicoService } from 'app/core/services/mensagem-tecnico.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { MensagemTecnico, MensagemTecnicoData, MensagemTecnicoParameters } from 'app/core/types/mensagem-tecnico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
	selector: 'app-mensagem-tecnico-lista',
	templateUrl: './mensagem-tecnico-lista.component.html',
	styles: [
		`.list-grid-mensagem-tecnico {
          grid-template-columns: 64px 196px 196px auto 96px 96px 64px 64px;
    	}`
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})

export class MensagemTecnicoListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: MensagemTecnicoData;
	isLoading: boolean = false;
	@ViewChild('searchInputControl') searchInputControl: ElementRef;
	selectedItem: MensagemTecnico | null = null;
	userSession: UserSession;

	constructor(
		protected _userService: UserService,
		private _cdr: ChangeDetectorRef,
		private _dialog: MatDialog,
		private _snack: CustomSnackbarService,
		private _mensagemTecnicoService: MensagemTecnicoService
	) {
		super(_userService, 'mensagem-tecnico')
		this.userSession = JSON.parse(this._userService.userSession);
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

	onSidenavClosed(): void {
		if (this.paginator) this.paginator.pageIndex = 0;
		this.loadFilter();
		this.obterDados();
	}

	ngAfterViewInit(): void {
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
		const parametros: MensagemTecnicoParameters = {
			pageNumber: this.paginator?.pageIndex + 1,
			sortActive: this.sort.active,
			sortDirection: 'asc',
			pageSize: this.paginator?.pageSize,
			filter: filtro
		}

		const data: MensagemTecnicoData = await this._mensagemTecnicoService.obterPorParametros({
			...parametros,
			...this.filter?.parametros
		}).toPromise();

		this.dataSourceData = data;
		this.isLoading = false;
		this._cdr.detectChanges();
	}

	paginar() {
		this.obterDados();
	}

	marcarTodos(ev: any) {
		this.dataSourceData?.items.forEach(x => x.selecionado = ev.target.checked)
	}

	isTodosMarcados() {
		return this.dataSourceData?.items.every(p => p.selecionado);
	}

	isAlgumMarcado() {
		return this.dataSourceData?.items.some(p => p.selecionado);
	}

	async deletar() {
		const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: `Deseja remover a(s) mensagem(s) selecionada(s)?`,
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
			if (confirmacao)
			{
				this.isLoading = true;
				for (const msg of this.dataSourceData?.items) {
					if (msg.selecionado) await this._mensagemTecnicoService.deletar(msg.codMensagemTecnico).toPromise();
				}
		
				this._snack.exibirToast(`Registro(s) deletado(s) com sucesso!`, "success");
				this.obterDados();
				this.isLoading = false;
			}
		});
	}

	public async exportar() {
		
	}
}
