import { ExportacaoService } from './../../../../../core/services/exportacao.service';
import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Tecnico, TecnicoData, TecnicoParameters } from 'app/core/types/tecnico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { FileMime } from 'app/core/types/file.types';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';

@Component({
	selector: 'app-tecnico-lista',
	templateUrl: './tecnico-lista.component.html',
	styles: [
		/* language=SCSS */
		`
      .list-grid-ge {
          grid-template-columns: 72px auto 5% 10% 10% 48px 42px;
          
          /* @screen sm {
              grid-template-columns: 72px auto 5% 10% 10% 48px 42px;
          }
      
          @screen md {
              grid-template-columns: 72px auto 5% 10% 10% 48px 42px;
          }
      
          @screen lg {
              grid-template-columns: 72px auto 5% 10% 10% 48px 42px;
          } */
      }
    `
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class TecnicoListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: TecnicoData;
	isLoading: boolean = false;
	@ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
	selectedItem: Tecnico | null = null;
	userSession: UserSession;

	constructor(
		private _cdr: ChangeDetectorRef,
		private _tecnicoService: TecnicoService,
		private _exportacaoService: ExportacaoService,
		protected _userService: UserService
	) {
		super(_userService, 'tecnico')
	}
	registerEmitters(): void {
		throw new Error('Method not implemented.');
	}

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

	async obterDados() {
		this.isLoading = true;

		const params: TecnicoParameters = {
			pageNumber: this.paginator?.pageIndex + 1,
			sortActive: this.sort?.active || 'nome',
			sortDirection: this.sort?.direction || 'asc',
			pageSize: this.paginator?.pageSize,
			filter: this.searchInputControl.nativeElement.val
		};

		const data = await this._tecnicoService
			.obterPorParametros(params)
			.toPromise();

		this.dataSourceData = data;
		this.isLoading = false;
		this._cdr.detectChanges();
	}

	async exportar() {
		this.isLoading = true;

		await this._exportacaoService.exportar('Tecnico', FileMime.Excel, this.filter?.parametros);

		this.isLoading = false;
	}

	paginar() {
		this.obterDados();
	}
}
