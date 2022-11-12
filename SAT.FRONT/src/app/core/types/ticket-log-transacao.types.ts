import { Meta, QueryStringParameters } from "./generic.types";

export interface TicketLogTransacao {
    codTicketLogTransacao: number;
    quilometrosPorLitro: number;
    codigoTransacao: number;
    cor: string;
    valorTransacao: number;
    dataTransacao: string;
    codigoFamiliaVeiculo: number;
    numeroCartao: string;
    principal: string;
    veiculoFabricante: string;
    uf: string;
    cnpjEstabelecimento: string;
    segmentoVeiculo: string;
    controlaHodometro: string;
    grupoRestricaoVeiculo: string;
    litros: number;
    codigoLiberacaoRestricao: number;
    grupoRestricaoTransacao: string;
    descricaoSeriePos: string;
    quilometrosRodados: number;
    responsavel: string;
    placa: string;
    valorSaldoAnterior: number;
    ano: number;
    codigoOrdemServico: number;
    tipoCombustivel: string;
    nomeCidade: string;
    numeroTerminal: string;
    codigoServico: string;
    exibeMediaQuilometragem: string;
    nomeMotorista: string;
    nomeReduzidoEstabelecimento: string;
    tipoFrota: string;
    veiculoModelo: string;
    controleDesempenho: string;
    codigoVeiculoCliente: number;
    codigoTipoCombustivel: number;
    numeroMatricula: number;
    quilometragemInicial: boolean;
    valorLitro: number;
    codigoUsuarioCartao: number;
    codigoEstabelecimento: number;
    quilometragem: number;
    servico: string;
    codigoServicoOrdemServico: number;
    controlaHorimetro: string;
    dataHoraConsulta: string;
}

export interface TicketLogTransacaoData extends Meta {
    items: TicketLogTransacao[];
};

export interface TicketLogTransacaoParameters extends QueryStringParameters {
    dataInicio?: string;
    dataFim?: string;
    responsavel?: string;
    cidade?: string;
    uf?: string;
    numeroCartao?: string;
}