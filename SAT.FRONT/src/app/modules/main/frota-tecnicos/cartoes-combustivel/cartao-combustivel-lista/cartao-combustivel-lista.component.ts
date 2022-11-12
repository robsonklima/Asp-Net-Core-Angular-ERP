import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { fuseAnimations } from '@fuse/animations';
import { UserService } from 'app/core/user/user.service';
import { LOCALE_ID } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { DespesaCartaoCombustivelData } from 'app/core/types/despesa-cartao-combustivel.types';
import { DespesaCartaoCombustivelService } from 'app/core/services/despesa-cartao-combustivel.service';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { fromEvent } from 'rxjs';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { MatSort } from '@angular/material/sort';
registerLocaleData(localePt);

@Component({
  selector: 'app-cartao-combustivel-lista',
  templateUrl: './cartao-combustivel-lista.component.html',
  styles: [`
        .list-grid-despesa-cartao-combustivel {
            grid-template-columns: 140px auto 80px 80px 110px 130px 80px;
            @screen sm { grid-template-columns: 140px auto 80px 80px 110px 130px 80px;}
            @screen md { grid-template-columns: 140px auto 80px 80px 110px 130px 80px; }
            @screen lg { grid-template-columns: 140px auto 80px 80px 110px 130px 80px; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})

export class CartaoCombustivelListaComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  @ViewChild(MatSort) sort: MatSort;
  isLoading: boolean = false;
  cartoes: DespesaCartaoCombustivelData;

  constructor(
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _exportacaoService: ExportacaoService,
    private _cartaoCombustivelSvc: DespesaCartaoCombustivelService) { }

  ngAfterViewInit() {
    this.obterDados();

    if (this.paginator)
      this._cdr.markForCheck();

    this.registerEmitters();
    this._cdr.detectChanges();
  }

  private async obterCartoes(filter: string) {
    this.cartoes = (await this._cartaoCombustivelSvc.obterPorParametros({
      pageNumber: this.paginator.pageIndex + 1,
      pageSize: this.paginator?.pageSize,
      sortActive: this.sort.active || 'codDespesaCartaoCombustivel',
			sortDirection: this.sort.direction || 'desc',
      filter: filter
    }).toPromise());
  }

  public async obterDados(filter: string = null) {
    this.isLoading = true;
    this.obterCartoes(filter);
    this.isLoading = false;
  }

  registerEmitters(): void {
    fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
      map((event: any) => {
        return event.target.value;
      })
      , debounceTime(1000)
      , distinctUntilChanged()
    ).subscribe((text: string) => {
      if (text && text != "")
      {
        this.paginator.pageIndex = 0;
        this.obterDados(text);
      }
    });
  }

  public async exportar() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.DESPESA_CARTAO_COMBUSTIVEL,
			entityParameters: {}
		}

		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
    this.isLoading = false;
	}

  public paginar() {
    this.obterDados();
  }
}