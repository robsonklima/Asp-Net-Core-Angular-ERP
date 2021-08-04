import { MatPaginatorIntl } from "@angular/material/paginator";

const portugueseRangeLabel = (page: number, pageSize: number, length: number) => {
  if (length == 0 || pageSize == 0) { return `0 de ${length}`; }
  
  length = Math.max(length, 0);

  const startIndex = page * pageSize;

  const endIndex = startIndex < length ?
      Math.min(startIndex + pageSize, length) :
      startIndex + pageSize;

  return `${startIndex + 1} - ${endIndex} de ${length}`;
}

export function getPortugueseIntl() {
  const paginatorIntl = new MatPaginatorIntl();
  paginatorIntl.itemsPerPageLabel = 'Ítens por página:';
  paginatorIntl.nextPageLabel = 'Próxima';
  paginatorIntl.previousPageLabel = 'Anterior';
  paginatorIntl.firstPageLabel = "Primeira";
  paginatorIntl.lastPageLabel = "Última";
  paginatorIntl.getRangeLabel = portugueseRangeLabel;
  
  return paginatorIntl;
}