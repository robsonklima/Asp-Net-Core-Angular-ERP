export interface IEditableItemList
{
    editableList: IEditableItem[];
    editar(item: IEditableItem): void;
    salvar(item: IEditableItem): void;
    cancelar(item: IEditableItem): void;
    isEqual(item: IEditableItem): boolean;
    isInvalid(item: IEditableItem): boolean;
    isEditing: boolean;
    isLoading: boolean;
    toNumber(value): number;
    createEditableList(): IEditableItem[];
};

export interface IEditableItem
{
    item: any;
    oldItem?: any;
    isEditing: boolean;
    onEdit?: (e: IEditableItem) => void;
    onCancel?: (e: IEditableItem) => void;
    onSave?: (e: IEditableItem) => void;
    onDelete?: (e: IEditableItem) => void;
    isEqual?: (e: IEditableItem) => boolean;
    isInvalid?: (e: IEditableItem) => boolean;
};