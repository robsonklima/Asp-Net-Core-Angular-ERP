export interface Meta {
    totalCount?: number;
    pageSize?: number;
    currentPage?: number;
    totalPages?: number;
    hasNext?: boolean;
    hasPrevious?: boolean;
}

export interface QueryStringParameters {
    pageNumber?: number;
    pageSize?: number;
    filter?: string;
    sortActive?: string;
    sortDirection?: string;
}

export const mensagensConst = {
    ERRO_AO_CRIAR: "Erro ao criar o registro",
    ERRO_AO_ATUALIZAR: "Erro ao atualizar o registro",
    ERRO_AO_REMOVER: "Erro ao remover o registro",
    SUCESSO_AO_CRIAR: "Registro criado com sucesso",
    SUCESSO_AO_ATUALIZAR: "Registro atualizado com sucesso",
    SUCESSO_AO_REMOVER: "Registro removido com sucesso",
    MODELO_DIFERENTE: 'Modelo de equipamento instalado não pode ser diferente do existente do modelo do chamado',
    NUM_SERIE_RET_INST_OBRIGATORIO: "Informe o número de série instalado e retirado",
    EQUIP_RET_DIFERENTE_OS: "Equipamento retirado não pode ser diferente do equipamento do chamado",
    NUMERO_SERIE_INSTALADO_N_ENCONTRADO: "Número de série instalado não encontrado",
    NUMERO_SERIE_RETIRADO_N_ENCONTRADO: "Número de série retirado não encontrado",
    REPROCESSADO_COM_SUCESSO: "Reprocessado com sucesso"
}

export const toastTypesConst = {
    SUCCESS: "success",
    ERROR: "error",
}