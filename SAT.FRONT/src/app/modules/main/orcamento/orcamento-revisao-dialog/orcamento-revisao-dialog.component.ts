import { Orcamento, OrcamentoDeslocamento, OrcamentoMotivoEnum } from 'app/core/types/orcamento.types';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OrdemServicoOrcamentosComponent } 
  from '../../ordem-servico/ordem-servico-detalhe/ordem-servico-orcamentos/ordem-servico-orcamentos.component';
import { Router } from '@angular/router';
import { appConfig } from 'app/core/config/app.config';
import { ContratoServicoService } from 'app/core/services/contrato-servico.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { ContratoServicoData, ContratoServicoParameters } from 'app/core/types/contrato-servico.types';
import { OrcamentoMaoDeObra } from 'app/core/types/orcamento-mao-de-obra.types';
import { OrcamentoMaterial } from 'app/core/types/orcamento.material.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { Peca } from 'app/core/types/peca.types';
import { TipoServicoEnum } from 'app/core/types/tipo-servico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import Enumerable from 'linq';
import moment from 'moment';
import { LocalEnvioNFFaturamentoVinculadoService } from 'app/core/services/local-envio-nf-faturamento-vinculado.service';
import { LocalEnvioNFFaturamentoVinculado, LocalEnvioNFFaturamentoVinculadoParameters } from 'app/core/types/local-envio-nf-faturamento-vinculado.types';
import _ from 'lodash';
import { ClientePecaService } from 'app/core/services/cliente-peca.service';
import { ClientePecaData } from 'app/core/types/cliente-peca.types';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { RelatorioAtendimentoData } from 'app/core/types/relatorio-atendimento.types';

@Component({
  selector: 'app-orcamento-revisao-dialog',
  templateUrl: './orcamento-revisao-dialog.component.html'
})
export class OrcamentoRevisaoDialogComponent implements OnInit {
  os: OrdemServico;
  orcamento: Orcamento;
  locais: LocalEnvioNFFaturamentoVinculado;
  userSession: UserSession;
  isValorPecasDesatualizado: boolean = false;
  loading: boolean = true;

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    public _dialogRef: MatDialogRef<OrdemServicoOrcamentosComponent>,
    private _router: Router,
    private _userService: UserService,
    private _orcamentoService: OrcamentoService,
    private _localEnvioNFFaturamentoVinculadoService: LocalEnvioNFFaturamentoVinculadoService,
    private _contratoServicoService: ContratoServicoService,
    private _clientePecaService: ClientePecaService,
    private _relatorioAtendimentoService: RelatorioAtendimentoService,
    private _snack: CustomSnackbarService
  ) {
    this.os = data?.os;
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.os.relatoriosAtendimento = (await this.obterRelatoriosAtendimento()).items;
    this.montaOrcamento();
    await this.montaMaoDeObra();
    await this.montaDeslocamento();
    await this.montaMateriais();
    await this.montaLocais();
    this._orcamentoService.calculaTotalizacao(this.orcamento)
    this.loading = false;
  }

  cancelar() {
    this._dialogRef.close();
  }

  private montaOrcamento() {
    this.orcamento = {
      codigoOrdemServico: this.os?.codOS,
      codigoContrato: this.os?.equipamentoContrato?.codContrato,
      codigoMotivo: 1,
      codigoStatus: 6,
      codigoPosto: this.os?.codPosto,
      codigoCliente: this.os?.codCliente,
      codigoFilial: this.os?.codFilial,
      codigoEquipamentoContrato: this.os?.codEquipContrato,
      codigoEquipamento: this.os?.codEquip,
      codigoSla: this.os?.equipamentoContrato?.codSLA,
      nomeContrato: this.os?.equipamentoContrato?.contrato?.nomeContrato,
      isMaterialEspecifico: this.os?.equipamentoContrato?.contrato?.indPermitePecaEspecifica,
      detalhe: this.os?.relatoriosAtendimento?.find(i => i.relatoSolucao !== null)?.relatoSolucao ?? '',
      valorIss: this.os?.filial?.orcamentoISS?.valor,
      usuarioCadastro: this.userSession?.usuario?.codUsuario,
      dataCadastro: moment().format('yyyy-MM-DD HH:mm:ss'),
      data: moment().format('yyyy-MM-DD HH:mm:ss')
    }
  }

  async montaMateriais() {
    var materiais: OrcamentoMaterial[] = [];

    var detalhesPeca = Enumerable.from(this.os.relatoriosAtendimento)
      .selectMany(i => i.relatorioAtendimentoDetalhes)
      .selectMany(i => i.relatorioAtendimentoDetalhePecas)
      .toArray();  

      for (const dp of detalhesPeca) {
      var valorUnitarioPeca = (await this.obterClientePeca(dp?.peca.codPeca))?.items?.shift()?.valorUnitario;

      if(!valorUnitarioPeca){
        valorUnitarioPeca = this.obterValorMaterial(dp?.peca);
      }

      var m: OrcamentoMaterial = {
        codOrc: this.orcamento?.codOrc,
        codigoMagnus: dp?.peca?.codMagnus,
        codigoPeca: dp?.peca?.codPeca.toString(),
        descricao: dp?.peca?.nomePeca,
        quantidade: dp?.qtdePecas,
        valorUnitario: valorUnitarioPeca || 0.00,
        valorUnitarioFinanceiro: this.obterValorUnitarioFinanceiroMaterial(dp?.peca),
        valorIpi: dp?.peca?.valIPI,
        usuarioCadastro: this.userSession?.usuario?.codUsuario,
        dataCadastro: moment().format('yyyy-MM-DD HH:mm:ss'),
      }
            
      if(!dp?.peca?.isValorAtualizado)
        this.isValorPecasDesatualizado = true;

      m.valorTotal = (m.quantidade * m.valorUnitario) - (m.valorDesconto ?? 0);
      var materialJaEstaNaLista = _.find(materiais, { codigoPeca: m.codigoPeca });

      if (materialJaEstaNaLista) {
        const index = _.findIndex(materiais, { codigoPeca: m.codigoPeca });
        materiais[index].quantidade = materiais[index].quantidade + 1;
        materiais[index].valorTotal = materiais[index].valorUnitario * materiais[index].quantidade;
      } else {
        materiais.push(m);
      }
    };

    this.orcamento.materiais = materiais;
  }

  private async obterClientePeca(codPeca): Promise<ClientePecaData> {
    return await this._clientePecaService.obterPorParametros({
      codCliente: this.orcamento?.codigoCliente,
      codContrato: this.os?.equipamentoContrato?.codContrato,
      codPeca: codPeca
    }).toPromise();
  }

  async montaMaoDeObra() {
    var codServico: number = this.orcamento?.codigoMotivo == OrcamentoMotivoEnum.INSTALACAO_DESINSTACALAO ?
        TipoServicoEnum.ATIVACAO : TipoServicoEnum.HORA_TECNICA;

    const contratoServicoData: ContratoServicoData = await this._contratoServicoService.obterPorParametros({
      codEquip: this.orcamento?.codigoEquipamento,
      codSLA: this.orcamento?.codigoSla,
      codServico: codServico,
      codContrato: this.orcamento.codigoContrato
    }).toPromise();

    const contratoServico = contratoServicoData.items.shift();

    var maoObra: OrcamentoMaoDeObra = {
      codOrc: this.orcamento?.codOrc,
      valorHoraTecnica: contratoServico?.valor,
      previsaoHoras: 2,
      usuarioCadastro: this.userSession?.usuario.codUsuario,
      dataCadastro: moment().format('yyyy-MM-DD HH:mm:ss')
    }

    maoObra.valorTotal = maoObra?.previsaoHoras * maoObra?.valorHoraTecnica;
    this.orcamento.maoDeObra = maoObra;
  }

  async montaDeslocamento() {
    const contratoServicoKmRodadoParams: ContratoServicoParameters = {
      codEquip: this.orcamento?.codigoEquipamento,
      codSLA: this.orcamento?.codigoSla,
      codServico: TipoServicoEnum.KM_RODADO,
      codContrato: this.orcamento.codigoContrato
    };

    const contratoServicoKmRodadoData: ContratoServicoData = await this._contratoServicoService
      .obterPorParametros(contratoServicoKmRodadoParams)
      .toPromise();

    const contratoServicoKmRodado = contratoServicoKmRodadoData.items.shift();

    const contratoServicoHoraViagemParams: ContratoServicoParameters = {
      codEquip: this.orcamento?.codigoEquipamento,
      codSLA: this.orcamento?.codigoSla,
      codContrato: this.orcamento.codigoContrato,
      codServico: TipoServicoEnum.HORA_DE_VIAGEM
    };

    const contratoServicoHoraViagemData: ContratoServicoData = await this._contratoServicoService
      .obterPorParametros(contratoServicoHoraViagemParams)
      .toPromise();

    const contratoServicoHoraViagem = contratoServicoHoraViagemData.items.shift();

    var deslocamento: OrcamentoDeslocamento = {
      codOrc: this.orcamento?.codOrc,
      valorUnitarioKmRodado: contratoServicoKmRodado?.valor || 0.00,
      valorHoraDeslocamento: contratoServicoHoraViagem?.valor || 0.00,
      latitudeOrigem: (this.os?.regiaoAutorizada?.autorizada?.latitude).replace(",","."),
      longitudeOrigem: (this.os?.regiaoAutorizada?.autorizada?.longitude).replace(",","."),
      latitudeDestino: (this.os?.localAtendimento?.latitude).replace(",","."),
      longitudeDestino: (this.os?.localAtendimento?.longitude).replace(",","."),
      quantidadeKm: this.os?.localAtendimento?.distanciaKmPatRes * 2,
      usuarioCadastro: this.userSession?.usuario.codUsuario,
      dataCadastro: moment().format('yyyy-MM-DD HH:mm:ss'),
      data: moment().format('yyyy-MM-DD HH:mm:ss')
    }

    deslocamento.valorTotalKmRodado = (deslocamento.quantidadeKm * deslocamento.valorUnitarioKmRodado);
    deslocamento.quantidadeHoraCadaSessentaKm = (deslocamento.quantidadeKm / 60.0);
    deslocamento.valorTotalKmDeslocamento = (deslocamento.valorHoraDeslocamento * deslocamento.quantidadeHoraCadaSessentaKm);
    this.orcamento.orcamentoDeslocamento = deslocamento;
  }

  async montaLocais(){
    const params: LocalEnvioNFFaturamentoVinculadoParameters = {
      codContrato: this.os?.equipamentoContrato?.codContrato,
      codPosto: this.os?.equipamentoContrato?.codPosto,
    };

    const data = await this._localEnvioNFFaturamentoVinculadoService
        .obterPorParametros(params)
        .toPromise();

    this.locais = data.items.shift();
    this.orcamento.localEnvioNFFaturamentoVinculado = this.locais;
    this.orcamento.codLocalEnvioNFFaturamento = this.locais.codLocalEnvioNFFaturamento;     
  }

  private obterValorMaterial(peca: Peca) {
    let valorPeca: any = 0;

    if (peca?.indValorFixo === 1)
      valorPeca = peca?.clientePecaGenerica?.valorUnitario || peca?.valPeca;

    if (this.orcamento?.isMaterialEspecifico === 1) {
      const clientePeca = peca?.clientePeca?.find(i => i.codCliente == this.orcamento?.codigoCliente && i?.codContrato == this.orcamento?.codigoContrato); 

      if (!clientePeca) {
        this._snack.exibirToast('Este orçamento exige material específico e não foi configurado o valor da peça', 'error');
      
        valorPeca = 0;
      } else {
        valorPeca = clientePeca?.valorUnitario;
      }
    } else {
      valorPeca = ((peca?.valPeca + (peca?.valPeca * (peca?.valIPI / 100.0))) * 1.025) / appConfig.parametroReajusteValorOrcamento;
    }

    return valorPeca;
  }

  private obterValorUnitarioFinanceiroMaterial(peca: Peca) {
    if (this.orcamento.isMaterialEspecifico === 1)
    {
      if (peca?.valIPI <= 0 || peca?.valIPI === null)
        return peca?.valPeca;

      return peca?.valPeca / (1 + (peca?.valIPI / 100.0));
    }

    return (peca?.valPeca * 1.025) / appConfig.parametroReajusteValorOrcamento;
  }

  async atualizarValoresOrcamento(): Promise<Orcamento> {
    this.orcamento = this._orcamentoService.calculaTotalizacao(this.orcamento);

    this.orcamento =(await this._orcamentoService.atualizar(this.orcamento).toPromise());

    return this.orcamento;
  }

  async persistirOrcamento(): Promise<any> {
    this.orcamento = (await this._orcamentoService.criar(this.orcamento).toPromise());
    this.orcamento.numero = this.os?.filial?.nomeFilial + this.orcamento?.codOrc;
    return this._orcamentoService.atualizar(this.orcamento).toPromise();
  }

  private async obterRelatoriosAtendimento(): Promise<RelatorioAtendimentoData> {
    return await this._relatorioAtendimentoService.obterPorParametros({ codOS: this.os.codOS }).toPromise();
  }

  navegarParaPeca(codPeca: string) {
    this._router.navigateByUrl('/peca/form/' + codPeca);
    
    this._dialogRef.close();
  }

  salvar() {
    this.persistirOrcamento()
      .then((orc) => {
        this.atualizarValoresOrcamento().then(() => {
          this._dialogRef.close();
          this._router.navigateByUrl('/orcamento/detalhe/' + orc.codOrc + '/' + orc.codigoOrdemServico);    
        })
        .catch((e) => {
          this._snack.exibirToast(`Erro ao atualizar os valores do orçamento ${e.message}`, 'error');  
        });
      })
      .catch((e) => {
        this._snack.exibirToast(`Erro ao criar orçamento ${e.message}`, 'error');  
      });
  }
}
