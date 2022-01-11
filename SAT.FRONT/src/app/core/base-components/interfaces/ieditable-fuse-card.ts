export interface IEditableFuseCard
{
    editar(): void;
    salvar(): void;
    cancelar(): void;
    isEqual(): boolean;
    isInvalid(): boolean;
    isEditing: boolean;
    isLoading: boolean;
    oldItem: any;
};