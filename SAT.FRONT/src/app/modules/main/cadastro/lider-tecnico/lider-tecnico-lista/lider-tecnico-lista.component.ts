import { AfterViewChecked, AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { LiderTecnicoService } from 'app/core/services/lider-tecnico.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { LiderTecnicoData, LiderTecnicoParameters } from 'app/core/types/lider-tecnico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
	selector: 'app-lider-tecnico-lista',
	templateUrl: './lider-tecnico-lista.component.html',
	styles: [
		/* language=SCSS */
		`
    .list-grid-u {
      grid-template-columns: 50% 25% ;
      
      /* @screen sm {
          grid-template-columns: 50% 25% ;
      }
  
      @screen md {
          grid-template-columns: 50% 25% ;
      }
  
      @screen lg {
          grid-template-columns: 50% 25% ;
      } */
  }
    `
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class LiderTecnicoListaComponent extends Filterable implements OnInit, AfterViewInit, IFilterable {
	@ViewChild('sidenav') public sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: LiderTecnicoData;
	isLoading: boolean = false;
	@ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
	userSession: UserSession;

	constructor(
		protected _userService: UserService,
		private _liderTecnicoService: LiderTecnicoService,
		private _cdr: ChangeDetectorRef,
		private _exportacaoService: ExportacaoService
	) {
		super(_userService, 'lider-tecnico')
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

	ngOnInit(): void {

	}

	async obterDados(filtro: string = '') {
		this.isLoading = true;
		const parametros: LiderTecnicoParameters = {
			...{
				pageNumber: this.paginator?.pageIndex + 1,
				sortActive: 'codLiderTecnico',
				sortDirection: 'asc',
				pageSize: this.paginator?.pageSize,
				filter: filtro
			},
			...this.filter?.parametros
		}
		const data = await this._liderTecnicoService.obterPorParametros(parametros).toPromise();
		this.dataSourceData = data;
		this.isLoading = false;
		this._cdr.detectChanges();
	}

	async exportar() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.LIDERTECNICO,
			entityParameters: this.filter?.parametros
		}

		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);

		this.isLoading = false;
	}

	paginar() {
		this.obterDados();
	}
}

export class liderTecnicoModel {
	codLiderTecnico: number;
	nomeLider: string;
	nomeTecnico: string;
}