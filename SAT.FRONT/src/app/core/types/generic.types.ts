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