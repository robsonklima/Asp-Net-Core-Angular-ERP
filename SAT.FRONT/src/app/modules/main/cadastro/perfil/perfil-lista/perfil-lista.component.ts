import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { MatSidenav } from '@angular/material/sidenav';
import { UserService } from 'app/core/user/user.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { PerfilSetorData, PerfilSetorParameters } from 'app/core/types/perfil-setor.types';
import { PerfilSetorService } from 'app/core/services/perfil-setor.service';

@Component({
	selector: 'app-perfil-lista',
	templateUrl: './perfil-lista.component.html',
	styles: [
		/* language=SCSS */
		`
      .list-grid-ge {
          grid-template-columns: 10% 20% auto 15%;
      }
    `
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class PerfilListaComponent extends Filterable implements OnInit, AfterViewInit, IFilterable {
	@ViewChild('sidenav') public sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild('searchInputControl') searchInputControl: ElementRef;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: PerfilSetorData;
	byteArray;
	isLoading: boolean = false;

	constructor(
		protected _userService: UserService,
		private _cdr: ChangeDetectorRef,
		private perfilSetorSrv: PerfilSetorService,
		private _exportacaoService: ExportacaoService

	) {
		super(_userService, 'perfil')
		this.userSession = JSON.parse(this._userService.userSession);
	}

	ngOnInit(): void { }

	registerEmitters(): void {
		this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();
			this.obterDados();
		})
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

	public async obterDados(filtro: string = '') {
		this.isLoading = true;

		const params: PerfilSetorParameters =
		{
			...{
				pageNumber: this.paginator?.pageIndex + 1,
				sortActive: this.sort?.active || 'codPerfil',
				sortDirection: this.sort?.direction || 'desc',
				pageSize: this.paginator?.pageSize,
				filter: filtro
			},
			...this.filter?.parametros
		}

		this.dataSourceData = await this.perfilSetorSrv.obterPorParametros(params).toPromise();
		this.isLoading = false;
		this._cdr.detectChanges();
	}

	async exportar() {
		// this.isLoading = true;

		// let exportacaoParam: Exportacao = {
		// 	formatoArquivo: ExportacaoFormatoEnum.EXCEL,
		// 	tipoArquivo: ExportacaoTipoEnum.PECA,
		// 	entityParameters: this.filter?.parametros
		// }

		// await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);

		// this.isLoading = false;
	}

	public paginar() { this.obterDados(); }
}
