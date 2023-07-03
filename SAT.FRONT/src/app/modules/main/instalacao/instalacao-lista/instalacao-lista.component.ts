import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { InstalacaoService } from 'app/core/services/instalacao.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Contrato } from 'app/core/types/contrato.types';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { InstalacaoLote } from 'app/core/types/instalacao-lote.types';
import { Instalacao, InstalacaoData, InstalacaoParameters } from 'app/core/types/instalacao.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import moment from 'moment';
import { fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { InstalacaoAnexoDialogComponent } from '../instalacao-anexo-dialog/instalacao-anexo-dialog.component';
import { InstalacaoRessalvaDialogComponent } from '../instalacao-ressalva-dialog/instalacao-ressalva-dialog.component';
import { InstalacaoListaMaisOpcoesComponent } from './instalacao-lista-mais-opcoes/instalacao-lista-mais-opcoes.component';
import _ from 'lodash';
import { FormBuilder, FormGroup } from '@angular/forms';
import { InstalacaoPleitoInstalService } from 'app/core/services/instalacao-pleito-instal.service';
@Component({
  selector: 'app-instalacao-lista',
  templateUrl: './instalacao-lista.component.html',
  styles: [`
    .list-grid-instalacao {
        grid-template-columns: 36px 36px 160px 120px 64px auto 36px 120px 100px 120px 120px 72px 72px 72px 72px;
    }
  `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class InstalacaoListaComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  bordero: number;
  codContrato: number;
  contrato: Contrato;
  codInstalLote: number;
  codCliente: number;
  confirma: boolean;
  instalacaoLote: InstalacaoLote;
  instalacaoSelecionada: Instalacao;
  instalacao: Instalacao;
  ordemServico: OrdemServico;
  dataSourceData: InstalacaoData;
  isLoading: boolean = false;
  userSession: UserSession;
  form: FormGroup;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _route: ActivatedRoute,
    private _cdr: ChangeDetectorRef,
    private _instalacaoSvc: InstalacaoService,
    private _instalPleitoInstalSvc: InstalacaoPleitoInstalService,
    private _exportacaoService: ExportacaoService,
    private _equipamentoContratoService: EquipamentoContratoService,
    private _ordemServicoService: OrdemServicoService,
    private _snack: CustomSnackbarService,
    private _userSvc: UserService,
    private _dialog: MatDialog,
    private _formBuilder: FormBuilder,
    protected _userService: UserService,
  ) {
    super(_userService, 'instalacao')
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  ngAfterViewInit(): void {
    this.codContrato = +this._route.snapshot.paramMap.get('codContrato');
    this.codInstalLote = +this._route.snapshot.paramMap.get('codInstalLote');
    this.codCliente = +this._route.snapshot.paramMap.get('codCliente');
    this.inicializarForm();
    this.obterInstalacoes();
    this.registerEmitters();

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
        this.obterInstalacoes(text);
      });

      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() => {
        this.paginator.pageIndex = 0;
        this.obterInstalacoes();
      });
    }

    this._cdr.detectChanges();
  }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.obterInstalacoes();
    })
  }

  loadFilter(): void {
    super.loadFilter();
  }

  onSidenavClosed(): void {
    if (this.paginator) this.paginator.pageIndex = 0;
    this.loadFilter();
    this.obterInstalacoes();
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codInstalacao: [undefined],
      filial: [undefined],
      autorizada: [undefined],
      
    });
  }

  private async obterInstalacoes(filtro: string = '') {
    this.isLoading = true;

    const parametros: InstalacaoParameters = {
      codContrato: this.codContrato || undefined,
      codInstalLote: this.codInstalLote || undefined,
      codCliente: this.codCliente || undefined,
      pageSize: this.paginator?.pageSize,
      filter: filtro,
      pageNumber: this.paginator.pageIndex + 1,
      sortActive: this.sort.active || 'CodInstalacao',
      sortDirection: this.sort.direction || 'desc'
    };

    const data: InstalacaoData = await this._instalacaoSvc.obterPorParametros({
      ...parametros,
      ...this.filter?.parametros
    }).toPromise();
    this.dataSourceData = data;

    this.isLoading = false;
    this._cdr.detectChanges();
  }

  public async exportar() {
    this.isLoading = true;

    let exportacaoParam: Exportacao = {
      formatoArquivo: ExportacaoFormatoEnum.EXCEL,
      tipoArquivo: ExportacaoTipoEnum.INSTALACAO,
      entityParameters: {
        codContrato: this.codContrato || undefined,
        codInstalLote: this.codInstalLote || undefined,
        codCliente: this.codCliente || undefined
      }
    }
    await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
    this.isLoading = false;
  }

  paginar() {
    this.obterInstalacoes();
  }

  async alternarDetalhe(codInstalacao: number) {
    if (this.instalacaoSelecionada && this.instalacaoSelecionada.codInstalacao === codInstalacao) {
      this.fecharDetalhe();
      return;
    }

    this.instalacaoSelecionada = (await this._instalacaoSvc.obterPorView({ codInstalacao: codInstalacao }).toPromise()).items.shift();
    this.bordero = (await this._instalPleitoInstalSvc
      .obterPorParametros(
        { 
          codInstalacao: codInstalacao, 
          sortDirection: 'desc', 
          sortActive:'dataHoraCad' 
        }).toPromise()).items.shift().codInstalPleito;

      }

  fecharDetalhe(): void {
    this.instalacaoSelecionada = null;
  }

  verificarExistemItensSelecionados() {
    return this.dataSourceData?.items?.filter(i => i.selecionado)?.length;
  }

  editar() {
    const itens = this.dataSourceData.items.filter(i => i.selecionado);

    const dialogRef = this._dialog.open(InstalacaoListaMaisOpcoesComponent, {
      data: {
        itens: itens
      },
      width: '960px',
      height: '600px'
    });

    dialogRef.afterClosed().subscribe(confirmacao => {
      if (confirmacao) this.obterInstalacoes();
    });
  }

  async confirmarAberturaChamados() {
    const itens = this.dataSourceData.items.filter(i => i.selecionado);

    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Você deseja abrir chamados para as linhas selecionadas?',
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
      if (confirmacao) {
        this.abrirChamados(itens);
      }
    });
  }

  async abrirChamados(instalacoes) {

    for (const instalacao of instalacoes) {
      const equip = await this._equipamentoContratoService.obterPorCodigo(+instalacao.codEquipContrato).toPromise();

      let obj: OrdemServico = {
        ...this.ordemServico,
        ...{
          codStatusServico: 1,
          codTipoIntervencao: 4,
          indStatusEnvioReincidencia: -1,
          indRevisaoReincidencia: 1,
          indRevOK: null,
          dataHoraSolicitacao: moment().format('YYYY-MM-DD HH:mm:ss'),
          dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
          dataHoraAberturaOS: moment().format('YYYY-MM-DD HH:mm:ss'),
          codCliente: equip.codCliente,
          codFilial: instalacao?.localAtendimentoSol?.codFilial,
          codAutorizada: instalacao?.localAtendimentoSol?.codAutorizada,
          codRegiao: instalacao?.localAtendimentoSol?.codRegiao,
          codPosto: instalacao.codPosto,
          codEquip: equip.codEquip,
          codTipoEquip: equip.codTipoEquip,
          codGrupoEquip: equip.codGrupoEquip,
          codEquipContrato: equip.codEquipContrato,
          codUsuarioCad: this.userSession.usuario?.codUsuario,
        }
      };

      if ((!obj.codAutorizada) || (!obj.codFilial) || (!obj.codRegiao)) {
        this._snack.exibirToast("Cadastro de Local está incompleto!!!", 'error');

      } else {
        Object.keys(obj).forEach((key) => {
          typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
        });

        this._ordemServicoService.criar(obj).subscribe((os) => {
          this.atualizarInstalacao(+instalacao.codInstalacao, os.codOS);
        });

        this._snack.exibirToast("Chamados abertos com sucesso!", 'success');
      }
    }
  }

  async atualizarInstalacao(codInstalacao, codOS) {
    this.instalacao = await this._instalacaoSvc.obterPorCodigo(codInstalacao).toPromise();

    let obj = {
      ...this.instalacao,
      ...{
        codOS: codOS,
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario?.codUsuario
      }
    };

    Object.keys(obj).forEach((key) => {
      typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
    });

    this._instalacaoSvc.atualizar(obj).subscribe();
  }

  toggleSelecionarTodos(e: any) {
    this.dataSourceData.items = this.dataSourceData.items
      .map(i => { return { ...i, selecionado: e.checked } });
  }

  abrirRessalvas(codInstalacao: number): void {
    this._dialog.open(InstalacaoRessalvaDialogComponent, {
      data: {
        codInstalacao: codInstalacao,
      },
      width: '960px',
      height: '600px'
    });
  }
  excluir(codInstalacao: number){
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
          titulo: 'Confirmação',
          message: 'Deseja excluir esta linha?',
          buttonText: {
              ok: 'Sim',
              cancel: 'Não'
          }
        }
      });
  
    dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
      if (confirmacao) {
          this._instalacaoSvc.deletar(codInstalacao).subscribe(() =>{
              this._snack.exibirToast("Linha excluída com sucesso!", "success");
          });

          this.ngAfterViewInit();
        }
      });
  }

  abrirPaginaAnexo(instalacao: Instalacao) {
    const dialogRef = this._dialog.open(InstalacaoAnexoDialogComponent, {
      data: {
        instalacao: instalacao,
      },
    });

    dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
      if (confirmacao) { }
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
