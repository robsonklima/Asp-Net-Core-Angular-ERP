export interface IEditableItemList
{
    editableList: IEditableItem[];
    editar(item: IEditableItem): void;
    salvar(item: IEditableItem): void;
    cancelar(item: IEditableItem): void;
    isEqual(item: IEditableItem): boolean;
    isEditing: boolean;
    isLoading: boolean;
    toNumber(value): number;
    createEditableList(): void;
};

export interface IEditableItem
{
    item: any;
    oldItem?: any;
    isEditing: boolean;
};