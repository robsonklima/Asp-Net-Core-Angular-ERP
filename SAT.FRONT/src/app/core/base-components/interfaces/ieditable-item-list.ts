export interface IEditableItemList<Type>
{
    editableList: IEditableItem<Type>[];
    editar(item: IEditableItem<Type>): void;
    salvar(item: IEditableItem<Type>): void;
    cancelar(item: IEditableItem<Type>): void;
    isEqual(item: IEditableItem<Type>): boolean;
    isInvalid(item: IEditableItem<Type>): boolean;
    isEditing: boolean;
    isLoading: boolean;
    createEditableList(): IEditableItem<Type>[];
};

export interface IEditableItem<Type>
{
    item: Type;
    oldItem?: Type;
    isEditing: boolean;
    onEdit?: (e: IEditableItem<Type>) => void;
    onCancel?: (e: IEditableItem<Type>) => void;
    onSave?: (e: IEditableItem<Type>) => void;
    onDelete?: (e: IEditableItem<Type>) => void;
    isEqual?: (e: IEditableItem<Type>) => boolean;
    isInvalid?: (e: IEditableItem<Type>) => boolean;
};