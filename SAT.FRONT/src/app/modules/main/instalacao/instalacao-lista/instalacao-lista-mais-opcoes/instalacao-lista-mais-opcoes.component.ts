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
import { LocalAtendimento, LocalAtendimentoParameters } from 'app/core/types/local-atendimento.types';
import { statusConst } from 'app/core/types/status-types';
import { Transportadora } from 'app/core/types/transportadora.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import Enumerable from 'linq';
import moment from 'moment';
import { Subject } from 'rxjs';
import { InstalacaoListaComponent } from '../instalacao-lista.component';
import { debounceTime, distinctUntilChanged, filter, takeUntil } from 'rxjs/operators';
import { InstalacaoStatus } from 'app/core/types/instalacao-status.types';
import { InstalacaoStatusService } from 'app/core/services/instalacao-status.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { ContratoEquipamentoService } from 'app/core/services/contrato-equipamento.service';
import { ContratoEquipamento } from 'app/core/types/contrato-equipamento.types';

@Component({
  selector: 'app-instalacao-lista-mais-opcoes',
  templateUrl: './instalacao-lista-mais-opcoes.component.html'
})
export class InstalacaoListaMaisOpcoesComponent implements OnInit {
  itens: Instalacao[] = [];
  instalacoesStatus: InstalacaoStatus[] = [];
  locaisAtendimento: LocalAtendimento[] = [];
  locaisFiltro: FormControl = new FormControl();
  searching: boolean;
  formInstalacao: FormGroup;
  codCliente: number;
  userSession: UserSession;
  instalStatusFiltro: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    @Inject(MAT_DIALOG_DATA) protected data: any,
    protected dialogRef: MatDialogRef<InstalacaoListaComponent>,
    private _instalStatusSvc: InstalacaoStatusService,
    private _equipamentoContratoService: EquipamentoContratoService,
    private _contratoEquipamentoService: ContratoEquipamentoService,
    private _relatorioAtendimentoSvc: RelatorioAtendimentoService,
    private _localAtendimentoService: LocalAtendimentoService,
    private _instalacaoService: InstalacaoService,
    private _formBuilder: FormBuilder,
    private _userSvc: UserService,
    private _snack: CustomSnackbarService,
  ) {
    this.itens = data?.itens;
    this.codCliente = data?.itens[0]?.codCliente;
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  async ngOnInit() {
    this.criarForms();
    this.obterStatusInstalacao();
    this.obterLocaisAtendimento();

    this.locaisFiltro.valueChanges
      .pipe(
        filter(t => t != ''),
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterLocaisAtendimento(this.locaisFiltro.value);
      });
  }

  private async obterStatusInstalacao(filter: string = '') {
    const data = await this._instalStatusSvc.obterPorParametros({
      sortActive: 'NomeInstalStatus',
      sortDirection: 'asc',
      filter: filter
    }).toPromise();

    this.instalacoesStatus = data.items;
  }

  private criarForms() {
    this.formInstalacao = this._formBuilder.group({
      codInstalacao: [''],
      codPosto: [''],
      codRAT: [''],
      codOS: [undefined],
      nfRemessa: [undefined],
      dataNFRemessa: [undefined],
      nomeRespBancoBT: [undefined],
      numMatriculaBT: [undefined],
      codPostoIns: [undefined],
      dataBI: [undefined],
      qtdParaboldBI: [undefined],
      indEquipRebaixadoBI: [undefined],
      nomeRespBancoBI: [undefined],
      numMatriculaBI: [undefined],
      indBiorigEnt: [undefined],
      indRATOK: [undefined],
      termoDescaracterizacao: [undefined],
      bemTradeIn: [undefined],
      dataRetirada: [undefined],
      nomeReponsavelBancoAcompanhamento: [undefined],
      numMatriculaRespBancoAcompanhamento: [undefined],
      codUsuarioCad: [undefined],
      dataHoraCad: [undefined],
      codUsuarioManut: [undefined],
      dataHoraManut: [undefined],
      fornecedorCompraTradeIn: [undefined],
      nfVendaTradeIn: [undefined],
      nfRemessaConferida: [undefined],
      dtReEntrega: [undefined],
      pedidoCompra: [undefined],
      numRAT: [undefined],
      codInstalStatus: [undefined],
      dataNFVendaTradeIn: [undefined],
      dataHoraChegTranspBT: [undefined],
    });
  }

  async obterLocaisAtendimento(filtro: string = '') {
    const params: LocalAtendimentoParameters = {
      sortActive: 'nomeLocal',
      sortDirection: 'asc',
      indAtivo: statusConst.ATIVO,
      filter: filtro,
      codClientes: this.codCliente?.toString(),
      pageSize: 10
    }

    this.locaisAtendimento = (await this._localAtendimentoService.obterPorParametros(params).toPromise()).items;
  }

  async salvar() {
    const formInst = this.formInstalacao.getRawValue();
    formInst.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');
    formInst.codUsuarioManut = this.userSession.usuario?.codUsuario;

    if (formInst.codPostoIns) {
      var posto = await this._localAtendimentoService.obterPorCodigo(formInst.codPostoIns).toPromise();
      formInst.codPosto = formInst.codPostoIns;
      formInst.codFilial = posto.codFilial;
      formInst.codRegiao = posto.codRegiao;
      formInst.codAutorizada = posto.codAutorizada;
    }

    let erro: boolean = false;

    for (const item of this.itens) {
      if ((formInst.codInstalStatus == 3) && (item.codEquipContrato != null) && (item.codOS != null)) {
        if (item.equipamentoContrato.indAtivo != statusConst.ATIVO) {

          var qtdDiaGarantia = (await this._contratoEquipamentoService.obterPorCodigo(item.codContrato, item.codEquip).toPromise()).qtdDiaGarantia;

          let objEqp: EquipamentoContrato = {
            ...item.equipamentoContrato,
            ...{
              dataAtivacao: item.ordemServico.dataHoraFechamento,
              dataInicGarantia: item.ordemServico.dataHoraFechamento,
              dataFimGarantia: moment(item.ordemServico.dataHoraFechamento).add(qtdDiaGarantia, 'days').format('YYYY-MM-DD HH:mm:ss'),
              indAtivo: statusConst.ATIVO,
              indReceita: statusConst.ATIVO,
              indInstalacao: statusConst.ATIVO,
              indRepasse: statusConst.INATIVO,
              codPosto: item.ordemServico.codPosto,
              CodFilial: item.ordemServico.codFilial,
              CodAutorizada: item.ordemServico.codAutorizada,
              CodRegiao: item.ordemServico.codRegiao,
              codUsuarioManut: this.userSession.usuario?.codUsuario,
              codUsuarioManutencao: this.userSession.usuario?.codUsuario,
              dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
              dataManutencao: moment().format('YYYY-MM-DD HH:mm:ss')

            }
          };

          this._equipamentoContratoService.atualizar(objEqp).subscribe(() => {
            this._snack.exibirToast("Equipamento ativado com sucesso!", "success");
          });

        }
        var rat = (await this._relatorioAtendimentoSvc.obterPorParametros({ codOS: item.codOS, sortDirection: 'desc', sortActive: 'codRAT' }).toPromise()).items.shift();
        item.codRAT = rat.codRAT;
        item.dataBI = rat.dataHoraSolucao;
      }

      let inst: any = {}
      Object.keys(item).forEach(key => {
        inst[key] = formInst[key] || item[key];
      });

      this._instalacaoService.atualizar(inst).subscribe(err => alert(err));
    }

    if (erro)
      this._snack.exibirToast('Erro ao atualizar os registros', 'error');
    else
      this._snack.exibirToast('Registros atualizados com sucesso', 'success');
  }

  fechar() {
    this.dialogRef.close();
  }
}

