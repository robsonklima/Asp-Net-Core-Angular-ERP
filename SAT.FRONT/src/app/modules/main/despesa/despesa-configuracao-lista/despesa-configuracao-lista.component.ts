import { AfterViewInit, ChangeDetectorRef, Component, LOCALE_ID, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { fuseAnimations } from '@fuse/animations';
import { DespesaConfiguracaoService } from 'app/core/services/despesa-configuracao.service';
import { DespesaConfiguracaoData } from 'app/core/types/despesa.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-despesa-configuracao-lista',
  templateUrl: './despesa-configuracao-lista.component.html',
  styles: [`
        .list-grid-despesa-configuracao {
            grid-template-columns: 60px auto auto 70px 70px 70px 70px 70px 70px 70px 70px 30px;
            @screen sm { grid-template-columns: 60px auto auto 70px 70px 70px 70px 70px 70px 70px 70px 30px; }
            @screen md { grid-template-columns: 60px auto auto 70px 70px 70px 70px 70px 70px 70px 70px 30px; }
            @screen lg { grid-template-columns: 60px auto auto 70px 70px 70px 70px 70px 70px 70px 70px 30px; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})

export class DespesaConfiguracaoListaComponent implements AfterViewInit
{
  @ViewChild(MatPaginator) paginator: MatPaginator;

  isLoading: boolean = false;
  configuracoes: DespesaConfiguracaoData;

  constructor (
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _despesaConfiguracaoSvc: DespesaConfiguracaoService)
  {
  }

  ngAfterViewInit()
  {
    this.obterDados();
    this._cdr.markForCheck();
    this._cdr.detectChanges();
  }

  private async obterConfiguracoes()
  {
    this.configuracoes = (await this._despesaConfiguracaoSvc.obterPorParametros({
      pageNumber: this.paginator?.pageIndex + 1,
      pageSize: this.paginator?.pageSize,
      sortActive: 'indAtivo',
      sortDirection: 'desc'
    }).toPromise());
  }

  public async obterDados()
  {
    this.isLoading = true;

    await this.obterConfiguracoes();

    this.isLoading = false;
  }

  public paginar()
  {
    this.obterDados();
  }
}