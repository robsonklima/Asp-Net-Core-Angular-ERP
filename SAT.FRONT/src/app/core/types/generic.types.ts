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
}

export const toastTypesConst = {
	SUCCESS: "success",
    ERROR: "error",
}