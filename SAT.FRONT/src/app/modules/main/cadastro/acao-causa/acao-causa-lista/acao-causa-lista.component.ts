import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { AcaoComponenteService } from 'app/core/services/acao-componente.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { AcaoComponente, AcaoComponenteData, AcaoComponenteParameters } from 'app/core/types/acao-componente.types';
import { AcaoParameters } from 'app/core/types/acao.types';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
	selector: 'app-acao-causa-lista',
	templateUrl: './acao-causa-lista.component.html',
	styles: [
		/* language=SCSS */
		`
    .list-grid-u {
      grid-template-columns: 142px 50px 20% 20% 20%;
      
      @screen sm {
          grid-template-columns: 142px 50px 20% 20% 20%;
      }
  
      @screen md {
          grid-template-columns: 142px  50px 20% 20% 20%;
      }
  
      @screen lg {
          grid-template-columns: 142px  50px 20% 20% 20%;
      }
  }
    `
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class AcaoCausaListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: AcaoComponenteData;
	isLoading: boolean = false;
	@ViewChild('searchInputControl', { read: ElementRef }) searchInputControl: ElementRef;
	userSession: UserSession;


	constructor(
		private _acaoComponenteService: AcaoComponenteService,
		private __exportacaoService: ExportacaoService,
		private _cdr: ChangeDetectorRef,
		protected _userService: UserService
	) {
		super(_userService, 'acao-causa')
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

		const parametros: AcaoComponenteParameters = {
			pageNumber: this.paginator?.pageIndex + 1,
			sortActive: 'codAcaoComponente' || 'codECausa' || 'nomeAcao' || 'nomeCausa',
			sortDirection: 'asc',
			pageSize: this.paginator?.pageSize,
			filter: filtro
		}


		const data = await this._acaoComponenteService.obterPorParametros({
			...parametros,
			...this.filter?.parametros
		}).toPromise();



		this.dataSourceData = data;
		this.isLoading = false;
		this._cdr.detectChanges();
	}

	buscaNomeAcao(dados: AcaoComponente) {
		return dados.acao?.codEAcao + ' - ' + dados.acao?.nomeAcao;
	}

	buscaSelecionado(dados: AcaoComponente) {
		return dados.selecionado == 1 ? 'Sim' : "Não";
	}

	public async exportar() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.ACAO_COMPONENTE,
			entityParameters: this.filter?.parametros
		}
		await this.__exportacaoService.exportar(FileMime.Excel, exportacaoParam);
		this.isLoading = false;
	}

	paginar() {
		this.obterDados();
	}
}