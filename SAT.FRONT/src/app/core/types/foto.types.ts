import { Meta, QueryStringParameters } from "./generic.types";

export interface Foto
{
    codRATFotoSmartphone?: number;
    codOS: number;
    numRAT: string;
    nomeFoto: string;
    modalidade: string;
    dataHoraCad: string;
    base64: string;
}

export interface FotoData extends Meta
{
    items: Foto[];
};

export interface FotoParameters extends QueryStringParameters
{
    codOS?: number;
    numRAT?: string;
    codUsuario?: string;
};

export enum FotoModalidadeEnum
{
    ASSINATURACLIENTELAUDO = 'Ass cliente laudo',
    ASSINATURATECNICOLAUDO = 'Ass técnico laudo',
    CHECKLIST_PREVENTIVA = 'Checklist preventiva',
    COMPINICIALIZACAO = 'Comp Inicialização',
    EQUIPAMENTOOPERACIONAL = 'Equipamento Operacional',
    ETIQUETANUMSERIE = 'Etiqueta nro série',
    PROTOCOLOVISITATECNICA = 'Protocolo visita',
    RAT_ASSINATURA_CLIENTE = 'Ass cliente RAT',
    RAT_ASSINATURA_TECNICO = 'Ass técnico RAT',
    VERIFICACAOVERSAO = 'Verificação da versão',
    LAUDO_SIT_1 = 'Laudo situação 1',
    LAUDO_SIT_2 = 'Laudo situação 2',
    LAUDO_SIT_3 = 'Laudo situação 3',
    PARTEINTERNA = 'Parte interna',
    RAT = 'Relatório de Atendimento'
}