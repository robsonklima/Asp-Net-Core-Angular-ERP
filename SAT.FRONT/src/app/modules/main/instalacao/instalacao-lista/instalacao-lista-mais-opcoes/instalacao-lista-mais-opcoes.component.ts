import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { FilialService } from 'app/core/services/filial.service';
import { InstalacaoService } from 'app/core/services/instalacao.service';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { TransportadoraService } from 'app/core/services/transportadora.service';
import { EquipamentoContrato } from 'app/core/types/equipamento-contrato.types';
import { Filial, FilialData, FilialParameters } from 'app/core/types/filial.types';
import { Instalacao } from 'app/core/types/instalacao.types';
import { LocalAtendimento } from 'app/core/types/local-atendimento.types';
import { statusConst } from 'app/core/types/status-types';
import { Transportadora } from 'app/core/types/transportadora.types';
import Enumerable from 'linq';
import { Subject } from 'rxjs';
import { InstalacaoListaComponent } from '../instalacao-lista.component';

@Component({
  selector: 'app-instalacao-lista-mais-opcoes',
  templateUrl: './instalacao-lista-mais-opcoes.component.html'
})
export class InstalacaoListaMaisOpcoesComponent implements OnInit {
  itens: Instalacao[] = [];
  filiais: Filial[] = [];
  transportadoras: Transportadora[] = [];
  equipamentosContrato: EquipamentoContrato[] = [];
  locaisAtendimento: LocalAtendimento[] = [];
  locaisFiltro: FormControl = new FormControl();
  searching: boolean;
  formInstalacao: FormGroup;
  codCliente: number;
  protected _onDestroy = new Subject<void>();

  constructor(
    @Inject(MAT_DIALOG_DATA) protected data: any,
    protected dialogRef: MatDialogRef<InstalacaoListaComponent>,
    private _filialService: FilialService,
    private _transportadoraService: TransportadoraService,
    private _equipamentoContratoService: EquipamentoContratoService,
    private _localAtendimentoService: LocalAtendimentoService,
    private _instalacaoService: InstalacaoService,
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
  ) {
      this.itens = data?.itens;
      this.codCliente = data?.itens[0]?.codCliente;
  }

  async ngOnInit() {
    this.criarForms();
    this.filiais = (await this.obterFiliais()).items;
    this.obterTransportadoras();
    this.obterEquipamentosContrato();
    this.obterLocaisAtendimento();
  }

  private criarForms() {
    this.formInstalacao = this._formBuilder.group({
      codInstalacao: [''],
      codInstalLote: [undefined],
      codContrato: [undefined],
      codTipoEquip: [undefined],
      codGrupoEquip: [undefined],
      codEquip: [undefined],
      codRegiao: [undefined],
      codAutorizada: [undefined],
      codFilial: [undefined],
      codSla: [undefined],
      codEquipContrato: [undefined],
      codCliente: [undefined],
      codPosto: [undefined],
      dataSugEntrega: [undefined],
      dataConfEntrega: [undefined],
      dataRecDm: [undefined],
      nfRemessa: [undefined],
      dataNFRemessa: [undefined],
      dataExpedicao: [undefined],
      codTransportadora: [undefined],
      codClienteEnt: [undefined],
      codPostoEnt: [undefined],
      dataHoraChegTranspBT: [undefined],
      indEquipPosicOkbt: [undefined],
      nomeRespBancoBT: [undefined],
      numMatriculaBT: [undefined],
      indBTOrigEnt: [undefined],
      indBTOK: [undefined],
      dataSugInstalacao: [undefined],
      dataConfInstalacao: [undefined],
      codOS: [undefined],
      codRAT: [undefined],
      codClienteIns: [undefined],
      codPostoIns: [undefined],
      dataBI: [undefined],
      qtdParaboldBI: [undefined],
      superE: [undefined],
      csl: [undefined],
      csoServ: [undefined],
      supridora: [undefined],
      msT606TipoNovo: [undefined],
      indEquipRebaixadoBI: [undefined],
      nomeRespBancoBI: [undefined],
      numMatriculaBI: [undefined],
      indBiorigEnt: [undefined],
      indBIOK: [undefined],
      indRATOK: [undefined],
      indLaudoOK: [undefined],
      indRE5330OK: [undefined],
      codInstalNFVenda: [undefined],
      nfVenda_DEL: [undefined],
      dataNFVenda_DEL: [undefined],
      dataNFVendaEnvio_DEL: [undefined],
      codInstalNFAut: [undefined],
      vlrPagtoNFAut: [undefined],
      numFaturaTransp: [undefined],
      codInstalStatus: [undefined],
      codUsuarioBlock: [undefined],
      termoDescaracterizacao: [undefined],
      fornecedorTradeIn1: [undefined],
      fornecedorTradeIn2: [undefined],
      bemTradeIn: [undefined],
      fabricante: [undefined],
      modelo: [undefined],
      dataRetirada: [undefined],
      nfTradeIn1: [undefined],
      nfTradeIn2: [undefined],
      dataNFTradeIn1: [undefined],
      dataNFTradeIn2: [undefined],
      nomeTransportadora: [undefined],
      nomeReponsavelBancoAcompanhamento: [undefined],
      numMatriculaRespBancoAcompanhamento: [undefined],
      vlrKMTradeIn1: [undefined],
      vlrKMTradeIn2: [undefined],
      vlrDesFixacao1: [undefined],
      vlrDesFixacao2: [undefined],
      distanciaKmTradeIn1: [undefined],
      distanciaKmTradeIn2: [undefined],
      nfTransportadoraTradeIn: [undefined],
      dataNFTransportadoraTradeIn: [undefined],
      vlrRecolhimentoTradeIn: [undefined],
      codUsuarioCad: [undefined],
      dataHoraCad: [undefined],
      codUsuarioManut: [undefined],
      dataHoraManut: [undefined],
      fornecedorCompraTradeIn: [undefined],
      nfVendaTradeIn: [undefined],
      dataNFVendaTradeIn: [undefined],
      valorUnitarioVenda: [undefined],
      romaneio: [undefined],
      dtPrevRecolhimentoTradeIn: [undefined],
      nfRemessaConferida: [undefined],
      dtbCliente: [undefined],
      faturaTranspReEntrega: [undefined],
      dtReEntrega: [undefined],
      responsavelRecebReEntrega: [undefined],
      indInstalacao: [undefined],
      pedidoCompra: [undefined],
      dtVencBord100: [undefined],
      dtEntBord100: [undefined],
      dtVencBord90: [undefined],
      dtEntBord90: [undefined],
      dtVencBord10: [undefined],
      dtEntBord10: [undefined],
      valorFrete1: [undefined],
      faturaFrete1: [undefined],
      cteFrete1: [undefined],
      dtFaturaFrete1: [undefined],
      valorFrete2: [undefined],
      faturaFrete2: [undefined],
      cteFrete2: [undefined],
      dtFaturaFrete2: [undefined],
      valorExtraFrete: [undefined],
      ddd: [undefined],
      telefone1: [undefined],
      redestinacao: [undefined],
      antigoPrefixoRedestinacao: [undefined],
      antigoSbRedestinacao: [undefined],
      antigoNomeDependenciaRedestinacao: [undefined],
      antigoUfRedestinacao: [undefined],
      antigoTipoDependenciaRedestinacao: [undefined],
      antigoPedidoCompraRedestinacao: [undefined],
      antigoProtocoloCdo: [undefined],
      novoProtocoloCdo: [undefined] //
    });
  }

  async obterFiliais(): Promise<FilialData> {
    let params: FilialParameters = {
      indAtivo: statusConst.ATIVO,
      sortActive: 'nomeFilial',
      sortDirection: 'asc'
    };

    return await this._filialService
      .obterPorParametros(params)
      .toPromise();
  }

  async obterTransportadoras(filter: string = '') {
    const data = await this._transportadoraService.obterPorParametros({
      indAtivo: statusConst.ATIVO,
      sortActive: 'NomeTransportadora',
      sortDirection: 'asc',
      filter: filter
    }).toPromise();

    this.transportadoras = data.items;
  }

  async obterEquipamentosContrato(filter: string = '') {
    const data = await this._equipamentoContratoService.obterPorParametros({
      sortActive: 'numSerie',
      sortDirection: 'asc',
      codClientes: this.codCliente?.toString(),
      pageSize: 50,
      filter: filter,
      indAtivo: statusConst.ATIVO
    }).toPromise();

    this.equipamentosContrato = [];

    this.equipamentosContrato = Enumerable.from(data.items)
      .orderByDescending(i => i.indAtivo)
      .thenBy(i => i.numSerie)
      .toArray();
  }

  async obterLocaisAtendimento(filter: string = '') {
    const data = await this._localAtendimentoService.obterPorParametros({
      indAtivo: statusConst.ATIVO,
      sortActive: 'NomeLocal',
      sortDirection: 'asc',
      codClientes: this.codCliente?.toString(),
      pageSize: 50,
      filter: filter
    }).toPromise();

    this.locaisAtendimento = [];

    this.locaisAtendimento = Enumerable.from(data.items)
      .orderByDescending(i => i.indAtivo)
      .thenBy(i => i.numAgencia)
      .toArray();
  }

  async salvar() {
    const formInst = this.formInstalacao.getRawValue();
    let erro: boolean = false;

    // for (const item of this.itens) {
    //   let inst = {}

    //   Object.keys(item).forEach(key => {
    //     inst[key] = formInst[key] || item[key];
    //   });

    //   this._instalacaoService
    //     .atualizar({ ...item, ...inst })
    //     .subscribe(() => {}, () => { erro = true});
    // }

    // if (erro) 
    //   this._snack.exibirToast('Erro ao atualizar os registros', 'error');
    // else
    //   this._snack.exibirToast('Registros atualizados com sucesso', 'success');
  }

  fechar() {
    this.dialogRef.close();
  }
}

