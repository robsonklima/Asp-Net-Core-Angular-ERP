export interface Chamado {
    codChamado: number;
    chamadoDadosAdicionais: ChamadoDadosAdicionais;
    operadoraTelefonia: OperadoraTelefonia;
    defeitoPOS: DefeitoPOS;
    codOS: number | null;
    numeroOSCliente: string;
    dataHoraAbertura: string;
    classificacao: string;
    foneOrigem: string;
    cnpjCpf: string;
    nome: string;
    nomeFantasia: string;
    enderecoSolicitante: string;
    cep: string;
    cidade: string;
    uF: string;
    nomeContato: string;
    foneContato: string;
    rede: string;
    estabelecimento: string;
    terminal: string;
    modelo: string;
    numeroSerie: string;
    descricao: string;
    dataHoraCadastro: string;
    codUsuarioCadastro: string;
    codUsuarioOS: string;
    dataHoraOS: string;
    codUsuarioAtendimento: string;
    descricaoAtendimento: string;
    horarioInicioAtendimento: string;
    horarioFimAtendimento: string;
    codStatus: number;
    codMotivoCancelamento: number;
    codDefeitoPOS: number;
    codPosto: number;
    codCliente: number;
    codEquip: number;
    codGrupoEquip: number;
    codTipoEquip: number;
    codEquipContrato: number;
    linha: string;
    complementoDefeito: string;
    dataSolicitacao: string;
    exigeVisitaTecnica: boolean;
    codOperadoraTelefonia: number;
    versao: string;
}

export interface ChamadoDadosAdicionais {
    codChamadoDadosAdicionais: number;
    codChamado: number;
    simCard: string;
    dDD: string;
    numero: string;
    codOperadoraTelefonia: number;
    operadoraTelefoniaTela: string;
    autenticacao: string;
    simCardHist: string;
    dDDHist: string;
    numeroHist: string;
    codOperadoraTelefoniaHist: number;
    operadoraTelefoniaTelaHist: string;
    autenticacaoHist: string;
    codTipoComunicacao: number;
    tipoComunicacaoTela: string;
    codUsuarioCadastro: string;
    modoEquipamento: string;
}

export interface DefeitoPOS {
    codDefeitoPOS: number;
    nomeDefeitoPOS: string;
    dataCadastro: string;
    codUsuarioCadastro: string;
    ativo: boolean;
    codDefeito: number;
    exigeTrocaEquipamento: boolean;
}

export interface OperadoraTelefonia {
    codOperadoraTelefonia: number;
    nomeOperadoraTelefonia: string;
    indAtivo: boolean;
}