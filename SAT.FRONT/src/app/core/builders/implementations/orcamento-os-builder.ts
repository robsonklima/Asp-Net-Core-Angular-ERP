import { Injectable } from "@angular/core";
import { appConfig } from "app/core/config/app.config";
import { OrcamentoService } from "app/core/services/orcamento.service";
import { ContratoServico } from "app/core/types/contrato.types";
import { Orcamento, OrcamentoDeslocamento, OrcamentoMaoDeObra, OrcamentoMaterial, OrcamentoMotivoEnum } from "app/core/types/orcamento.types";
import { OrdemServico } from "app/core/types/ordem-servico.types";
import { Peca } from "app/core/types/peca.types";
import { TipoServicoEnum } from "app/core/types/tipo-servico.types";
import { UserSession } from "app/core/user/user.types";
import Enumerable from "linq";
import moment from "moment";
import { IOrcamentoOSBuilder, ISpecifyBaseOrcamentoOSBuilder, ISpecifyDeslocamentoOrcamentoOSBuilder, ISpecifyMaoDeObraOrcamentoOSBuilder, ISpecifyMateriaisOrcamentoOSBuilder } from "../interfaces/iorcamento-os-builder";

@Injectable({
    providedIn: 'root'
})
export class OrcamentoOSBuilder
{
    constructor (private _orcamentoService: OrcamentoService) { }

    public async create(ordemServico: OrdemServico, userSession: UserSession): Promise<ISpecifyBaseOrcamentoOSBuilder>
    {
        return new OrcamentoOSBuilderImplementation(ordemServico, userSession, this._orcamentoService);
    }
}

class OrcamentoOSBuilderImplementation implements
    ISpecifyBaseOrcamentoOSBuilder,
    ISpecifyMateriaisOrcamentoOSBuilder,
    ISpecifyMaoDeObraOrcamentoOSBuilder,
    ISpecifyDeslocamentoOrcamentoOSBuilder,
    IOrcamentoOSBuilder
{
    private orcamento: Orcamento;
    private os: OrdemServico;
    private userSession: UserSession;

    constructor (os: OrdemServico, userSession: UserSession, protected _orcamentoService: OrcamentoService)
    {
        this.os = os;
        this.userSession = userSession;
    }

    async specifyBase(): Promise<ISpecifyMateriaisOrcamentoOSBuilder>
    {
        this.orcamento.codigoOrdemServico = this.os?.codOS;
        this.orcamento.codigoContrato = this.os?.codContrato;
        this.orcamento.codigoMotivo = 1;
        this.orcamento.codigoStatus = 1;
        this.orcamento.codigoPosto = this.os?.codPosto;
        this.orcamento.codigoCliente = this.os?.codCliente;
        this.orcamento.codigoFilial = this.os?.codFilial;
        this.orcamento.codigoEquipamentoContrato = this.os?.codEquipContrato;
        this.orcamento.codigoEquipamento = this.os?.codEquip;
        this.orcamento.codigoSla = this.os?.equipamentoContrato?.codSLA;
        this.orcamento.nomeContrato = this.os?.equipamentoContrato?.contrato?.nomeContrato;
        this.orcamento.isMaterialEspecifico = this.os?.equipamentoContrato?.contrato?.indPermitePecaEspecifica;
        this.orcamento.detalhe = this.os?.relatoriosAtendimento?.find(i => i.relatoSolucao !== null)?.relatoSolucao ?? '';
        this.orcamento.valorIss = this.os?.filial?.orcamentoISS?.valor;
        this.orcamento.usuarioCadastro = this.userSession?.usuario?.codUsuario;
        this.orcamento.dataCadastro = moment().format('yyyy-MM-DD HH:mm:ss');
        this.orcamento.data = moment().format('yyyy-MM-DD HH:mm:ss');

        this.orcamento =
            (await this._orcamentoService.criar(this.orcamento).toPromise());

        this.orcamento.numero =
            this.os?.filial?.nomeFilial + this.orcamento?.codOrc;

        this.orcamento =
            (await this._orcamentoService.atualizar(this.orcamento).toPromise());

        return this;
    }

    async specifyMateriais(): Promise<ISpecifyMaoDeObraOrcamentoOSBuilder>
    {
        var materiais: OrcamentoMaterial[] = [];

        var detalhesPeca = Enumerable.from(this.os.relatoriosAtendimento)
            .selectMany(i => i.relatorioAtendimentoDetalhes)
            .selectMany(i => i.relatorioAtendimentoDetalhePecas)
            .toArray();

        detalhesPeca.forEach(dp =>
        {
            var m: OrcamentoMaterial =
            {
                codOrc: this.orcamento?.codOrc,
                codigoMagnus: dp?.peca?.codMagnus,
                codigoPeca: dp?.peca?.codPeca.toString(),
                descricao: dp?.peca?.nomePeca,
                quantidade: dp?.qtdePecas,
                valorUnitario: this.obterValorMaterial(dp?.peca),
                valorUnitarioFinanceiro: this.obterValorUnitarioFinanceiroMaterial(dp?.peca),
                valorIpi: dp?.peca?.valIPI,
                usuarioCadastro: this.userSession?.usuario?.codUsuario,
                dataCadastro: moment().format('yyyy-MM-DD HH:mm:ss')
            }

            m.valorTotal =
                (m.quantidade * m.valorUnitario) - m.valorDesconto;

            // cria material
            materiais.push(m);
        });

        this.orcamento.materiais = materiais;

        return this;
    }

    private obterValorMaterial(peca: Peca)
    {
        if (peca?.indValorFixo === 1)
            return peca?.clientePecaGenerica?.valorUnitario || peca?.valPeca;

        return this.orcamento?.isMaterialEspecifico === 1 ?
            peca?.clientePeca?.find(i => i.codCliente == this.orcamento?.codigoCliente && i?.codContrato == this.orcamento?.codigoContrato)?.valorUnitario :
            ((peca?.valPeca + (peca?.valPeca * (peca?.valIPI / 100.0))) * 1.025) / appConfig.parametroReajusteValorOrcamento;
    }

    private obterValorUnitarioFinanceiroMaterial(peca: Peca)
    {
        if (this.orcamento.isMaterialEspecifico === 1)
        {
            if (peca?.valIPI <= 0 || peca?.valIPI === null)
                return peca?.valPeca;

            return peca?.valPeca / (1 + (peca?.valIPI / 100.0));
        }

        return (peca?.valPeca * 1.025) / appConfig.parametroReajusteValorOrcamento;
    }

    async specifyMaoDeObra(): Promise<ISpecifyDeslocamentoOrcamentoOSBuilder>
    {
        var codServico: number =
            this.orcamento?.codigoMotivo == OrcamentoMotivoEnum.INSTALACAO_DESINSTACALAO ?
                TipoServicoEnum.ATIVACAO : TipoServicoEnum.HORA_TECNICA;

        var contratoServico: ContratoServico = this.os?.equipamentoContrato?.contrato?.contratoServico?.find(i =>
            i.codEquip == this.orcamento?.codigoEquipamento &&
            i.codSLA == this.orcamento?.codigoSla &&
            i.codServico == codServico);

        var m: OrcamentoMaoDeObra =
        {
            codOrc: this.orcamento?.codOrc,
            valorHoraTecnica: contratoServico?.valor,
            previsaoHoras: 1,
            usuarioCadastro: this.userSession?.usuario.codUsuario,
            dataCadastro: moment().format('yyyy-MM-DD HH:mm:ss')
        }

        m.valorTotal =
            m?.previsaoHoras * m?.valorHoraTecnica;

        // cria mao de obra
        this.orcamento.maoDeObra = m;

        return this;
    }

    async specifyDeslocamento(): Promise<IOrcamentoOSBuilder>
    {
        var valorUnitarioKmRodado =
            this.os?.equipamentoContrato?.contrato?.contratoServico?.find(i =>
                i.codEquip == this.orcamento?.codigoEquipamento &&
                i.codSLA == this.orcamento?.codigoSla &&
                i.codServico == TipoServicoEnum.KM_RODADO)?.valor ?? 0;

        var valorHoraDeslocamento =
            this.os?.equipamentoContrato?.contrato?.contratoServico?.find(i =>
                i.codEquip == this.orcamento?.codigoEquipamento &&
                i.codSLA == this.orcamento?.codigoSla &&
                i.codServico == TipoServicoEnum.HORA_DE_VIAGEM)?.valor ?? 0;

        var d: OrcamentoDeslocamento =
        {
            codOrc: this.orcamento?.codOrc,
            valorUnitarioKmRodado: valorUnitarioKmRodado,
            valorHoraDeslocamento: valorHoraDeslocamento,
            latitudeOrigem: this.os?.localAtendimento?.autorizada?.latitude,
            longitudeOrigem: this.os?.localAtendimento?.autorizada?.longitude,
            latitudeDestino: this.os?.localAtendimento?.latitude,
            longitudeDestino: this.os?.localAtendimento?.longitude,
            quantidadeKm: 0,
            usuarioCadastro: this.userSession?.usuario.codUsuario,
            dataCadastro: moment().format('yyyy-MM-DD HH:mm:ss'),
            data: moment().format('yyyy-MM-DD HH:mm:ss')
        }

        d.valorTotalKmRodado =
            d.quantidadeKm * d.valorHoraDeslocamento;

        d.quantidadeHoraCadaSessentaKm =
            d.quantidadeKm / 65.0;

        d.valorTotalKmDeslocamento =
            d.valorHoraDeslocamento * d.quantidadeHoraCadaSessentaKm;

        // cria deslocamento
        this.orcamento.orcamentoDeslocamento = d;

        return this;
    }

    build(): Orcamento
    {
        this.orcamento.valorTotal =
            (Enumerable.from(this.orcamento?.materiais).sum(i => i?.valorTotal) +
                this.orcamento?.maoDeObra?.valorTotal +
                this.orcamento?.orcamentoDeslocamento?.valorTotalKmDeslocamento) ?? 0;

        this.orcamento.valorTotalDesconto =
            Enumerable.from(this.orcamento.descontos).sum(i => i.valorTotal) ?? 0;

        return this.orcamento;
    }
}