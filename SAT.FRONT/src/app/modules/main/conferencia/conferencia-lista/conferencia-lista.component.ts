import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { Conferencia, ConferenciaData, ConferenciaParameters } from 'app/core/types/conferencia.types';
import { ConferenciaService } from 'app/core/services/conferencia.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
	selector: 'app-conferencia-lista',
	templateUrl: './conferencia-lista.component.html',
	styles: [
		`.list-grid-confs {
			grid-template-columns: 72px 256px auto 240px;
		}`
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class ConferenciaListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild('searchInputControl') searchInputControl: ElementRef;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: ConferenciaData;
	userSession: UserSession;
	isLoading: boolean = false;

	constructor(
		private _cdr: ChangeDetectorRef,
		private _snack: CustomSnackbarService,
		private _dialog: MatDialog,
		private _conferenciaService: ConferenciaService,
		protected _userService: UserService
	) {
		super(_userService, 'tecnico')
		this.userSession = JSON.parse(this._userService.userSession);
	}

	registerEmitters(): void {

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

		const params: ConferenciaParameters = {
			...{
				filter: filtro
			},
			...this.filter?.parametros
		};

		const data = await this._conferenciaService
			.obterPorParametros(params)
			.toPromise();

		console.log(data);
		

		this.dataSourceData = data;
		this.isLoading = false;
		this._cdr.detectChanges();
	}

	remover(conferencia: Conferencia) {
		const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: `Deseja remover a conferência autorizada?`,
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
				this._conferenciaService
					.deletar(conferencia.codConferencia)
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

	mostrarNomeParticipantes(conferencia: Conferencia): string {
		return conferencia.participantes.map(p => p.usuarioParticipante.nomeUsuario).join(', ').toUpperCase();
	}

	paginar() {
		this.obterDados();
	}
}
