import { Equipamento } from './equipamento.types';
import { Email } from "./email.types";

export interface Exportacao
{
    email?: Email;
    formatoArquivo: ExportacaoFormatoEnum;
    tipoArquivo: ExportacaoTipoEnum;
    entityParameters: any;
}

export enum ExportacaoFormatoEnum {
    EXCEL,
    PDF,
    TXT
}

export enum ExportacaoTipoEnum {
    ORDEM_SERVICO,
    EQUIPAMENTO_CONTRATO,
    ACAO,
    AUTORIZADA,
    TECNICO,
    ACAO_COMPONENTE,
    CIDADE,
    CLIENTE,
    CLIENTEPECA,
    CLIENTEPECAGENERICA,
    CLIENTEBANCADA,
    CONTRATO,
    DEFEITO,
    EQUIPAMENTO,
    GRUPOEQUIPAMENTO,
    TIPOEQUIPAMENTO,
    DEFEITOCOMPONENTE,
    EQUIPAMENTOMODULO,
    FERIADO,
    FERRAMENTATECNICO,
    FORMAPAGAMENTO,
    LIDERTECNICO,
    LOCALATENDIMENTO,
    PECA,
    USUARIO,
    ORCAMENTO,
    REGIAO,
    REGIAOAUTORIZADA,
    FILIAL,
    TICKET,
    AUDITORIA,
    VALOR_COMBUSTIVEL,
    DESPESA_PERIODO_TECNICO,
    OR,
    OR_ITEM,
    PEDIDOS_CREDITO,
    ORDEM_REPARO,
    OR_CHECKLIST,
    DESPESA_CARTAO_COMBUSTIVEL,
    TICKET_LOG_TRANSACAO,
    INSTALACAO,
    LAUDO,
    PONTO_USUARIO,
    INSTALACAO_PLEITO,
    ORC_BANCADA,
    NF_BANCADA
}