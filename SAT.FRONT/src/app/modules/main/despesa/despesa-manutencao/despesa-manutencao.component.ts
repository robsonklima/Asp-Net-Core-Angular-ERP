import { ChangeDetectorRef, Component, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DespesaConfiguracaoCombustivelService } from 'app/core/services/despesa-configuracao-combustivel.service';
import { DespesaConfiguracaoService } from 'app/core/services/despesa-configuracao.service';
import { DespesaItemAlertaService } from 'app/core/services/despesa-item-alerta.service';
import { DespesaItemService } from 'app/core/services/despesa-item.service';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { DespesaService } from 'app/core/services/despesa.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { DespesaCartaoCombustivelParameters } from 'app/core/types/despesa-cartao-combustivel.types';
import { DespesaConfiguracaoCombustivel, DespesaConfiguracaoCombustivelParameters } from 'app/core/types/despesa-configuracao-combustivel.types';
import { DespesaPeriodoTecnico } from 'app/core/types/despesa-periodo.types';
import { Despesa, DespesaConfiguracaoData, DespesaItem, DespesaItemAlertaData } from 'app/core/types/despesa.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import Enumerable from 'linq';
import moment from 'moment';
import { DespesaItemDialogComponent } from './despesa-item-dialog/despesa-item-dialog.component';

@Component({
  selector: 'app-despesa-manutencao',
  templateUrl: './despesa-manutencao.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})
export class DespesaManutencaoComponent implements OnInit
{
  isLoading: boolean;
  isDespesaLoading: boolean;
  sort: any;
  paginator: any;
  codRAT: number;
  codDespesaPeriodo: number;
  despesa: Despesa;
  despesaItens: DespesaItem[] = [];
  rat: RelatorioAtendimento;
  ordemServico: OrdemServico;
  displayedColumns: string[] = ['codDespesaItem', 'despesaTipo', 'numNF', 'quilometragem', 'valorTotal', 'acao'];
  userSession: UserSession;
  despesaConfiguracaoCombustivel: DespesaConfiguracaoCombustivel
  despesaConfiguracao: DespesaConfiguracaoData;
  despesaItemAlerta: DespesaItemAlertaData;
  despesaPeriodoTecnico: DespesaPeriodoTecnico;

  constructor (
    public _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _route: ActivatedRoute,
    private _despesaSvc: DespesaService,
    private _snack: CustomSnackbarService,
    private _despesaItemSvc: DespesaItemService,
    private _relatorioAtendimentoSvc: RelatorioAtendimentoService,
    private _ordemServicoSvc: OrdemServicoService,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService,
    private _despesaConfiguracaoService: DespesaConfiguracaoService,
    private _despesaItemAlertaService: DespesaItemAlertaService,
    private _despesaConfCombustivelSvc: DespesaConfiguracaoCombustivelService,
    private _dialog: MatDialog)
  {
    this.userSession = JSON.parse(this._userService.userSession);
    this.codRAT = +this._route.snapshot.paramMap.get('codRAT');
    this.codDespesaPeriodo = +this._route.snapshot.paramMap.get('codDespesaPeriodo');
  }

  async ngOnInit()
  {
    this.isDespesaLoading = false;
    this.isLoading = false;

    await this.obterDados();

    if (this.sort && this.paginator)
    {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() =>
      {
        this.obterDados();
      });
    }

    this._cdr.detectChanges();
  }

  public async obterDados()
  {
    this.isLoading = true;

    await this.obterRAT();
    await this.obterOS();
    await this.obterDespesaPeriodoTecnico();
    await this.obterDespesa();
    await this.obterConfiguracaoCombustivel();
    await this.obterDespesaConfiguracao();
    await this.obterDespesaItemAlertas();

    this.isLoading = false;
  }

  private async obterDespesaPeriodoTecnico()
  {
    this.despesaPeriodoTecnico = (await this._despesaPeriodoTecnicoSvc.obterPorParametros({
      codTecnico: this.rat.codTecnico.toString(),
      codDespesaPeriodo: this.codDespesaPeriodo
    }).toPromise()).items[0];
  }

  private async obterRAT()
  {
    this.rat = (await this._relatorioAtendimentoSvc.obterPorCodigo(this.codRAT).toPromise());
  }

  private async obterOS()
  {
    this.ordemServico = (await this._ordemServicoSvc.obterPorCodigo(this.rat.codOS).toPromise());
  }

  private async obterDespesa()
  {
    this.isDespesaLoading = true;
    this.despesa = (await this._despesaSvc.obterPorParametros({ codRATs: this.codRAT.toString() }).toPromise()).items[0];
    this.despesaItens = Enumerable.from(this.despesa?.despesaItens).orderByDescending(i => i.codDespesaItem).toArray();
    this.despesaItens = Enumerable.from(this.despesaItens).where(i => i.indAtivo === 1).toArray();
    this.isDespesaLoading = false;
  }

  private async criaDespesa()
  {
    var despesa: Despesa =
    {
      codRAT: this.rat.codRAT,
      indAtivo: statusConst.ATIVO,
      centroCusto: "1008",
      codFilial: this.rat.tecnico.codFilial,
      codTecnico: this.rat.codTecnico,
      codDespesaPeriodo: this.codDespesaPeriodo,
      codUsuarioCad: this.userSession.usuario.codUsuario,
      dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss')
    };

    this.despesa = (await this._despesaSvc.criar(despesa).toPromise());
  }

  async lancarDespesaItem()
  {
    if (!this.despesa) {
      this.criaDespesa().finally(() => this.abrirDialogoDespesaItem());
    } else {
      this.abrirDialogoDespesaItem();
    }
  }

  abrirDialogoDespesaItem(): void
  {
    const dialogRef = this._dialog.open(DespesaItemDialogComponent, {
      data:
      {
        codDespesa: this.despesa.codDespesa,
        ordemServico: this.ordemServico,
        rat: this.rat,
        despesa: this.despesa,
        despesaConfiguracaoCombustivel: this.despesaConfiguracaoCombustivel,
        despesaConfiguracao: this.despesaConfiguracao?.items.shift(),
        despesaItemAlerta: this.despesaItemAlerta
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
    {
      if (confirmacao)
        this.obterDespesa();
    });
  }

  public excluirDespesaItem(di: DespesaItem)
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Deseja remover este lançamento de despesa?',
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
        this._despesaItemSvc.deletar(di.codDespesaItem).subscribe(() =>
        {
          this._snack.exibirToast('Despesa removida com sucesso!', 'success');
          this.obterDespesa();
        }, e =>
        {
          this._snack.exibirToast('Erro ao remover despesa', 'error');
        })
      }
    });
  }

  public async obterConfiguracaoCombustivel()
  {
    const params: DespesaConfiguracaoCombustivelParameters = {
      codFilial: this.ordemServico.codFilial,
      codUf: this.ordemServico.localAtendimento.cidade?.codUF
    };

    const data = await this._despesaConfCombustivelSvc.obterPorParametros(params).toPromise();
    const conf = Enumerable.from(data.items).firstOrDefault();

    if (conf) 
      this.despesaConfiguracaoCombustivel = conf;
    else
      this._snack.exibirToast('Não foi possível obter os valores de combustível para esta região. Favor entrar em contato com a sua filial.', 'error');
  }

  async obterDespesaConfiguracao()
  {
    this.despesaConfiguracao =
      (await this._despesaConfiguracaoService.obterPorParametros({
        indAtivo: statusConst.ATIVO
      }).toPromise());
  }

  async obterDespesaItemAlertas()
  {
    this.despesaItemAlerta =
      (await this._despesaItemAlertaService.obterPorParametros({}).toPromise());
  }
}
