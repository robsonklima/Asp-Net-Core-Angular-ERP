export interface IEditableFuseCard
{
    editar(): void;
    salvar(): void;
    cancelar(): void;
    isEqual(): boolean;
    isEditing: boolean;
    isLoading: boolean;
    toNumber(value): number;
};