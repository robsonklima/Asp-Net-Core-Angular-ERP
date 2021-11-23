import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { UserService } from 'app/core/user/user.service';
import { LOCALE_ID } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { Filterable } from 'app/core/filters/filterable';
import { MatSidenav } from '@angular/material/sidenav';
import { IFilterable } from 'app/core/types/filtro.types';
import { ActivatedRoute } from '@angular/router';
import { DespesaPeriodoTecnico, DespesaPeriodoTecnicoAtendimentoData, DespesaPeriodoTecnicoAtendimentoItem, DespesaPeriodoTecnicoStatusEnum } from 'app/core/types/despesa-periodo.types';
import { MatDialog } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import moment from 'moment';
import { DespesaAtendimentoAdiantamentoDialogComponent } from './despesa-atendimento-adiantamento-dialog/despesa-atendimento-adiantamento-dialog.component';
import { DespesaAtendimentoRelatorioImpressaoComponent } from './despesa-atendimento-relatorio-impressao/despesa-atendimento-relatorio-impressao.component';
registerLocaleData(localePt);

@Component({
  selector: 'app-despesa-atendimento-lista',
  templateUrl: './despesa-atendimento-lista.component.html',
  styles: [`.list-grid-despesa-atendimento {
            grid-template-columns: 80px 80px 70px 90px 90px 115px auto 50px 150px;
            @screen sm { grid-template-columns: 80px 80px 70px 90px 90px 115px auto 50px 150px; }
            @screen md { grid-template-columns: 80px 80px 70px 90px 90px 115px auto 50px 150px; }
            @screen lg { grid-template-columns: 80px 80px 70px 90px 90px 115px auto 50px 150px; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})

export class DespesaAtendimentoListaComponent extends Filterable implements AfterViewInit, IFilterable
{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('sidenav') sidenav: MatSidenav;

  isLoading: boolean = false;
  atendimentos: DespesaPeriodoTecnicoAtendimentoData;
  codTecnico: string;
  periodoLiberado: DespesaPeriodoTecnicoStatusEnum = DespesaPeriodoTecnicoStatusEnum['LIBERADO PARA ANÁLISE'];

  constructor (
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _route: ActivatedRoute,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService,
    private _snack: CustomSnackbarService,
    private _dialog: MatDialog)
  {
    super(_userService, "despesa-atendimento");
    this.codTecnico = this._route.snapshot.paramMap.get('codTecnico') || this.userSession.usuario?.codTecnico;
  }

  ngAfterViewInit()
  {
    this.obterDados();

    if (this.sort && this.paginator)
    {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() =>
      {
        this.onSortChanged()
        this.obterDados();
      });
    }

    this.registerEmitters();
    this._cdr.detectChanges();
  }

  private async obterDespesasPeriodoTecnico()
  {
    this.atendimentos = (await this._despesaPeriodoTecnicoSvc.obterAtendimentos({
      codTecnico: this.codTecnico,
      indAtivoPeriodo: this.filter?.parametros?.indAtivo,
      codDespesaPeriodoStatus: this.filter?.parametros?.codDespesaPeriodoStatus,
      inicioPeriodo: this.filter?.parametros?.inicioPeriodo,
      fimPeriodo: this.filter?.parametros?.fimPeriodo,
      pageNumber: this.paginator?.pageIndex + 1,
      pageSize: this.paginator?.pageSize,
      sortActive: 'dataInicio',
      sortDirection: 'desc'
    }).toPromise());
  }

  public async obterDados()
  {
    this.isLoading = true;

    await this.obterDespesasPeriodoTecnico();

    this.isLoading = false;
  }

  registerEmitters(): void
  {
    this.sidenav.closedStart.subscribe(() =>
    {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  public paginar()
  {
    this.onPaginationChanged();
    this.obterDados();
  }


  criaDespesaPeriodoTecnico(dpi: DespesaPeriodoTecnicoAtendimentoItem): DespesaPeriodoTecnico
  {
    var dp: DespesaPeriodoTecnico =
    {
      codDespesaPeriodo: dpi.codDespesaPeriodo,
      codTecnico: parseInt(dpi.codTecnico),
      codDespesaPeriodoTecnicoStatus: parseInt(dpi.status.codDespesaPeriodoTecnicoStatus),
      dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss'),
      codUsuarioCad: this.userSession.usuario.codUsuario
    }

    return dp;
  }

  liberar(dpi: DespesaPeriodoTecnicoAtendimentoItem)
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Deseja liberar este período?',
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
    {
      if (confirmacao)
      {
        dpi.status = { codDespesaPeriodoTecnicoStatus: this.periodoLiberado };
        var dp = this.criaDespesaPeriodoTecnico(dpi);

        this._despesaPeriodoTecnicoSvc.criar(dp).subscribe(() =>
        {
          this._snack.exibirToast('Período liberado com sucesso!', 'success');
          this.obterDados();
        },
          e =>
          {
            this._snack.exibirToast('Erro ao liberar período.', 'error');
          })
      }
    });
  }

  listarAdiantamentos(dpi: DespesaPeriodoTecnicoAtendimentoItem)
  {

    this._dialog.open(DespesaAtendimentoAdiantamentoDialogComponent, {
      data:
      {
        codTecnico: this.codTecnico,
        codPeriodo: dpi.codDespesaPeriodo
      }
    });
  }

  imprimir(dpi: DespesaPeriodoTecnicoAtendimentoItem)
  {
    const printDialog = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Deseja imprimir o RD?',
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        },
      }
    });

    printDialog.afterClosed().subscribe(async (confirmacao: boolean) =>
    {
      if (confirmacao)
      {
        this._dialog.open(DespesaAtendimentoRelatorioImpressaoComponent, {
          panelClass: 'no-padding-dialog-container',
          data:
          {
            codDespesaPeriodoTecnico: dpi.codDespesaPeriodoTecnico
          }
        });
      }
    });

  }
}