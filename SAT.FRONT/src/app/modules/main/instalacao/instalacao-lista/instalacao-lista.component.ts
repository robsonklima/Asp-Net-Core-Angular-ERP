import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { FilialService } from 'app/core/services/filial.service';
import { ImportacaoService } from 'app/core/services/importacao.service';
import { InstalacaoLoteService } from 'app/core/services/instalacao-lote.service';
import { InstalacaoService } from 'app/core/services/instalacao.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TransportadoraService } from 'app/core/services/transportadora.service';
import { Contrato } from 'app/core/types/contrato.types';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { Filial } from 'app/core/types/filial.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { ImportacaoLinha } from 'app/core/types/importacao.types';
import { InstalacaoLote } from 'app/core/types/instalacao-lote.types';
import { Instalacao, InstalacaoParameters, InstalacaoData } from 'app/core/types/instalacao.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { statusConst } from 'app/core/types/status-types';
import { Transportadora } from 'app/core/types/transportadora.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { forEach } from 'lodash';
import moment from 'moment';
import { fromEvent, Subject } from 'rxjs';
import { debounceTime, delay, distinctUntilChanged, map, takeUntil, tap } from 'rxjs/operators';
import { InstalacaoRessalvaDialogComponent } from '../instalacao-ressalva-dialog/instalacao-ressalva-dialog.component';
import { InstalacaoListaMaisOpcoesComponent } from './instalacao-lista-mais-opcoes/instalacao-lista-mais-opcoes.component';
@Component({
  selector: 'app-instalacao-lista',
  templateUrl: './instalacao-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-instalacao {
          grid-template-columns: 72px auto 64px 240px 240px 72px 72px 72px;
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class InstalacaoListaComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  codContrato: number;
  contrato: Contrato;
  codInstalLote: number;
  confirma: boolean;
  instalacaoLote: InstalacaoLote;
  instalacaoSelecionada: Instalacao;
  instalacao: Instalacao;
  importacaoLinhas: ImportacaoLinha[] = [];
  transportadoras: Transportadora[] = [];
  filiais: Filial[] = [];
  ordemServico: OrdemServico;
  dataSourceData: InstalacaoData;
  isLoading: boolean = false;
  userSession: UserSession;
  form: FormGroup;
  transportadorasFiltro: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _route: ActivatedRoute,
    private _cdr: ChangeDetectorRef,
    private _formBuilder: FormBuilder,
    private _instalacaoSvc: InstalacaoService,
    private _transportadoraSvc: TransportadoraService,
    private _filialSvc: FilialService,
    private _contratoSvc: ContratoService,
    private _instalacaoLoteSvc: InstalacaoLoteService,
    private _exportacaoService: ExportacaoService,
    private _equipamentoContratoService: EquipamentoContratoService,
    private _ordemServicoService: OrdemServicoService,
    private _snack: CustomSnackbarService,
    private _userSvc: UserService,
    private _dialog: MatDialog,
    private _router: Router,
    protected _userService: UserService,
  ) {
    super(_userService, 'instalacao')
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  ngAfterViewInit(): void {
    this.codContrato = +this._route.snapshot.paramMap.get('codContrato');
    this.codInstalLote = +this._route.snapshot.paramMap.get('codInstalLote');

    this.obterInstalacoes();
    this.obterTransportadoras();
    this.obterFiliais();
    this.obterContrato();
    this.obterLote();
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

    this.form = this._formBuilder.group({
      codInstalacao: [''],
      codTransportadora: [''],
      nomeFilial: [{ value: '', disabled: true }],
      nomeLote: [{ value: '', disabled: true }],
      dataRecLote: [{ value: '', disabled: true }],
      nroContrato: [{ value: '', disabled: true }],
      pedidoCompra: [''],
      superE: [''],
      csl: [''],
      csoServ: [''],
      supridora: [''],
      mst606TipoNovo: [''],
      nomeEquip: [{ value: '', disabled: true }],
      numSerie: [{ value: '', disabled: true }],
      numSerieCliente: [{ value: '', disabled: true }],
      prefixosb: [''],
      nomeLocal: [''],
      cnpj: [{ value: '', disabled: true }],
      endereco: [{ value: '', disabled: true }],
      nomeCidade: [{ value: '', disabled: true }],
      siglaUF: [{ value: '', disabled: true }],
      cep: [{ value: '', disabled: true }],
      dataLimiteEnt: [{ value: '', disabled: true }],
      dataSugEntrega: [''],
      dataConfEntrega: [''],
      nfRemessa: [{ value: '', disabled: true }],
      dataNFRemessa: [{ value: '', disabled: true }],
      dataExpedicao: [''],
      nomeTransportadora: [''],
      agenciaEnt: [''],
      nomeLocalEnt: [''],
      dtbCliente: [''],
      faturaTranspReEntrega: [''],
      dtReEntrega: [''],
      responsavelRecebReEntrega: [''],
      dataHoraChegTranspBT: [''],
      ressalvaEnt: [''],
      nomeRespBancoBT: [''],
      numMatriculaBT: [''],
      indBTOrigEnt: [{ value: '', disabled: true }],
      indBTOK: [{ value: '', disabled: true }],
      nfRemessaConferida: [''],
      dataLimiteIns: [''],
      dataSugInstalacao: [''],
      dataConfInstalacao: [''],
      os: [{ value: '', disabled: true }],
      dataHoraOS: [''],
      instalStatus: [{ value: '', disabled: true }],
      numRAT: [''],
      agenciaIns: [''],
      nomeLocalIns: [{ value: '', disabled: true }],
      dataBI: [''],
      qtdParaboldBI: [''],
      ressalvaIns: [''],
      indEquipRebaixadoBI: [''],
      ressalvaInsR: [{ value: '', disabled: true }]
    })
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

  private async obterInstalacoes(filtro: string = '') {
    this.isLoading = true;

    const parametros: InstalacaoParameters = {
      codContrato: this.codContrato || undefined,
      codInstalLote: this.codInstalLote || undefined,
      pageSize: this.paginator?.pageSize,
      //filter: this.searchInputControl.nativeElement.val,
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
        codInstalLote: this.codInstalLote || undefined
      }
    }
    await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
    this.isLoading = false;
  }

  private async obterTransportadoras(filter: string = '') {
    const data = await this._transportadoraSvc.obterPorParametros({
      indAtivo: statusConst.ATIVO,
      sortActive: 'NomeTransportadora',
      sortDirection: 'asc',
      filter: filter
    }).toPromise();

    this.transportadoras = data.items;
  }

  private async obterFiliais(filter: string = '') {
    const data = await this._filialSvc.obterPorParametros({
      indAtivo: statusConst.ATIVO,
      sortActive: 'NomeFilial',
      sortDirection: 'asc',
      filter: filter
    }).toPromise();

    this.filiais = data.items;
  }


  private async obterContrato() {
    this.contrato = await this._contratoSvc.obterPorCodigo(this.codContrato).toPromise();
  }

  private async obterLote() {
    this.instalacaoLote = await this._instalacaoLoteSvc.obterPorCodigo(this.codInstalLote).toPromise();
  }

  paginar() {
    this.obterInstalacoes();
  }

  alternarDetalhe(codInstalacao: number): void {

    if (this.instalacaoSelecionada && this.instalacaoSelecionada.codInstalacao === codInstalacao) {
      this.fecharDetalhe();
      return;
    }

    //this.isLoading = true;

    this._instalacaoSvc.obterPorCodigo(codInstalacao)
      .subscribe((instalacao) => {

        this.instalacaoSelecionada = instalacao;
        this.form.patchValue(instalacao);
        this.form.controls['nomeFilial'].setValue(instalacao.filial?.nomeFilial);
        this.form.controls['nomeLote'].setValue(instalacao.instalacaoLote?.nomeLote);
        this.form.controls['dataRecLote'].setValue(moment(instalacao.instalacaoLote?.dataRecLote).format('DD/MM/yyyy'));
        this.form.controls['nroContrato'].setValue(instalacao.contrato?.nroContrato);
        this.form.controls['nomeEquip'].setValue(instalacao.equipamento?.nomeEquip);
        this.form.controls['numSerie'].setValue(instalacao.equipamentoContrato?.numSerie);
        this.form.controls['numSerieCliente'].setValue(instalacao.equipamentoContrato?.numSerieCliente);

        if (instalacao.localAtendimentoSol) {
          this.form.controls['prefixosb'].setValue(`${instalacao.localAtendimentoSol?.numAgencia} / ${instalacao.localAtendimentoSol?.dcPosto}`);
        }

        this.form.controls['nomeLocal'].setValue(instalacao.localAtendimentoSol?.nomeLocal);
        this.form.controls['cnpj'].setValue(instalacao.localAtendimentoSol?.cnpj);
        this.form.controls['endereco'].setValue(instalacao.localAtendimentoSol?.endereco);
        this.form.controls['nomeCidade'].setValue(instalacao.localAtendimentoSol?.cidade?.nomeCidade);
        this.form.controls['siglaUF'].setValue(instalacao.localAtendimentoSol?.cidade?.unidadeFederativa?.siglaUF);
        this.form.controls['cep'].setValue(instalacao.localAtendimentoSol?.cep);

        if (instalacao.equipamentoContrato.contrato.contratoEquipamento.codContratoEquipDataEnt > 0) {
          this.form.controls['dataLimiteEnt'].setValue(moment(instalacao.contrato?.dataAssinatura).format('DD/MM/yyyy'));
        }
        else {
          this.form.controls['dataLimiteEnt'].setValue(moment(instalacao.instalacaoLote.dataRecLote).format('DD/MM/yyyy'));
        }

        if (instalacao.dataSugEntrega) {
          this.form.controls['dataSugEntrega'].setValue(moment(instalacao.dataSugEntrega).format('DD/MM/yyyy'));
        }

        if (instalacao.dataConfEntrega) {
          this.form.controls['dataConfEntrega'].setValue(moment(instalacao.dataConfEntrega).format('DD/MM/yyyy'));
        }

        this.form.controls['nfRemessa'].setValue(instalacao.nfRemessa);
        this.form.controls['dataNFRemessa'].setValue(moment(instalacao.dataNFRemessa).format('DD/MM/yyyy'));

        if (instalacao.dataExpedicao) {
          this.form.controls['dataExpedicao'].setValue(moment(instalacao.dataExpedicao).format('DD/MM/yyyy'));
        }

        this.form.controls['nomeTransportadora'].setValue(instalacao.transportadoras?.nomeTransportadora);

        if (instalacao.localAtendimentoEnt) {
          this.form.controls['agenciaEnt'].setValue(`${instalacao.localAtendimentoEnt?.numAgencia} / ${instalacao.localAtendimentoEnt?.dcPosto}`);
        }

        this.form.controls['nomeLocalEnt'].setValue(instalacao.localAtendimentoEnt?.nomeLocal);
        this.form.controls['dtbCliente'].setValue(instalacao.dtbCliente);
        this.form.controls['faturaTranspReEntrega'].setValue(instalacao.faturaTranspReEntrega);

        if (instalacao.dtReEntrega) {
          this.form.controls['dtReEntrega'].setValue(moment(instalacao.dtReEntrega).format('DD/MM/yyyy'));
        }

        this.form.controls['responsavelRecebReEntrega'].setValue(instalacao.responsavelRecebReEntrega);

        if (instalacao.dataHoraChegTranspBt) {
          this.form.controls['dataHoraChegTranspBT'].setValue(moment(instalacao.dataHoraChegTranspBt).format('DD/MM/yyyy'));
        }

        const instalRessalva = instalacao.instalacoesRessalva.sort((a, b) => a.codInstalRessalva - b.codInstalRessalva).shift();

        if (instalRessalva) {
          if (instalRessalva.codInstalMotivoRes === 0) {
            this.form.controls['ressalvaEnt'].setValue('SIM');
          }
          else {
            this.form.controls['ressalvaEnt'].setValue('NÃO');
          }

          if (instalRessalva.codInstalMotivoRes === 1) {
            this.form.controls['ressalvaIns'].setValue('SIM');
          }
          else {
            this.form.controls['ressalvaIns'].setValue('NÃO');
          }

          if (instalRessalva.codInstalMotivoRes === 2) {
            this.form.controls['indEquipRebaixadoBI'].setValue('SIM');
          }
          else {
            this.form.controls['indEquipRebaixadoBI'].setValue('NÃO');
          }

          if (instalRessalva.codInstalMotivoRes === 2) {
            this.form.controls['ressalvaInsR'].setValue('SIM');
          }
          else {
            this.form.controls['ressalvaInsR'].setValue('NÃO');
          }
        }

        this.form.controls['nomeRespBancoBT'].setValue(instalacao.nomeRespBancoBT);
        this.form.controls['numMatriculaBT'].setValue(instalacao.numMatriculaBT);

        if (instalacao.indBTOrigEnt) {
          this.form.controls['indBTOrigEnt'].setValue('SIM');
        }
        else {
          this.form.controls['indBTOrigEnt'].setValue('NÃO');
        }

        if (instalacao.indBTOK) {
          this.form.controls['indBTOK'].setValue('SIM');
        }
        else {
          this.form.controls['indBTOK'].setValue('NÃO');
        }

        this.form.controls['nfRemessaConferida'].setValue(instalacao.nfRemessaConferida);

        switch (instalacao.equipamentoContrato.contrato.contratoEquipamento.codContratoEquipDataIns) {
          case 0:
            this.form.controls['dataLimiteIns'].setValue(moment(instalacao.contrato?.dataAssinatura).add(instalacao.equipamentoContrato.contrato.contratoEquipamento.qtdLimDiaIns, 'days').format('DD/MM/yyyy'));
          case 1:
            this.form.controls['dataLimiteIns'].setValue(moment(instalacao.instalacaoLote?.dataRecLote).add(instalacao.equipamentoContrato.contrato.contratoEquipamento.qtdLimDiaIns, 'days').format('DD/MM/yyyy'));
          case 2:
            this.form.controls['dataLimiteIns'].setValue(moment(instalacao.dataHoraChegTranspBt).add(instalacao.equipamentoContrato.contrato.contratoEquipamento.qtdLimDiaIns, 'days').format('DD/MM/yyyy'));
          default:
            this.form.controls['dataLimiteIns'].setValue(moment(instalacao.contrato?.dataAssinatura).add(instalacao.equipamentoContrato.contrato.contratoEquipamento.qtdLimDiaIns, 'days').format('DD/MM/yyyy'));
        }

        if (instalacao.dataSugInstalacao) {
          this.form.controls['dataSugInstalacao'].setValue(moment(instalacao.dataSugInstalacao).format('DD/MM/yyyy'));
        }

        if (instalacao.dataConfInstalacao) {
          this.form.controls['dataConfInstalacao'].setValue(moment(instalacao.dataConfInstalacao).format('DD/MM/yyyy'));
        }

        this.form.controls['os'].setValue(instalacao.codOS); //Abrir tela de chamados 

        if (instalacao.ordemServico) {
          this.form.controls['dataHoraOS'].setValue(moment(instalacao.ordemServico?.dataHoraSolicAtendimento).format('DD/MM/yyyy'));
        }

        this.form.controls['instalStatus'].setValue(instalacao.instalacaoStatus?.nomeInstalStatus);

        this.form.controls['numRAT'].setValue(
          instalacao.ordemServico?.relatoriosAtendimento[instalacao.ordemServico?.relatoriosAtendimento.length - 1].numRAT
        );

        if (instalacao.localAtendimentoIns) {
          this.form.controls['agenciaIns'].setValue(`${instalacao.localAtendimentoIns?.numAgencia} / ${instalacao.localAtendimentoIns?.dcPosto}`);
        }

        this.form.controls['nomeLocalIns'].setValue(instalacao.localAtendimentoIns?.nomeLocal);

        if (instalacao.dataBI) {
          this.form.controls['dataBI'].setValue(moment(instalacao.dataBI).format('DD/MM/yyyy'));
        }

        this.form.controls['qtdParaboldBI'].setValue(instalacao?.qtdParaboldBI);
        this._cdr.markForCheck();

      }, () => {
        this.isLoading = false;
      });
  }

  fecharDetalhe(): void {
    this.instalacaoSelecionada = null;
  }

  atualizar() {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.instalacaoSelecionada,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario?.codUsuario
      }
    };

    Object.keys(obj).forEach((key) => {
      typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
    });

    this._instalacaoSvc.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Instalação atualizada com sucesso!", "success");
    });

    this.obterInstalacoes();
  }

  verificarExistemItensSelecionados() {
    return this.dataSourceData?.items?.filter(i => i.selecionado)?.length;
  }

  abrirMaisOpcoes() {
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

  async abrirChamados(itens) {
    try {
      for (let index = 0; index < itens.length; index++) {
        const equip = await this._equipamentoContratoService.obterPorCodigo(+itens[index].codEquipContrato).toPromise();

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
            codFilial: equip.codFilial,
            codAutorizada: equip.codAutorizada,
            codRegiao: equip.codRegiao,
            codPosto: equip.codPosto,
            codEquip: equip.codEquip,
            codTipoEquip: equip.codTipoEquip,
            codGrupoEquip: equip.codGrupoEquip,
            codEquipContrato: equip.codEquipContrato,
            codUsuarioCad: this.userSession.usuario?.codUsuario,
          }
        };

        Object.keys(obj).forEach((key) => {
          typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
        });

        this._ordemServicoService.criar(obj).subscribe((os) => {
          this.atualizarInstalacao(+itens[index].codInstalacao, os.codOS);
        });
      }
      this._snack.exibirToast("Chamados abertos com sucesso!");
      this._router.navigate(['instalacao/lista/' + this.codContrato]);
    } catch (error) {
      this._snack.exibirToast("Erro ao abrir chamados!");
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

    this._instalacaoSvc.atualizar(obj).subscribe(() => {
    });
  }

  toggleSelecionarTodos(e: any) {
    this.dataSourceData.items = this.dataSourceData.items.map(i => { return { ...i, selecionado: e.checked } });
  }

  abrirRessalvas(codInstalacao: number): void {
    const dialogRef = this._dialog.open(InstalacaoRessalvaDialogComponent, {
      data: {
        codInstalacao: codInstalacao,
      },
      width: '960px',
      height: '600px'
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
