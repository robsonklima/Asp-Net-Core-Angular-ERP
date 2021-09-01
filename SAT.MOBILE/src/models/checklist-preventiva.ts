import { ChecklistPreventivaItem } from "./checklist-preventiva-item";
import { Foto } from "./foto";

export class ChecklistPreventiva {
    codChecklistPreventiva: number;
    codOS: number;
    tensaoSemCarga: number;
    tensaoComCarga: number;
    tensaoEntreTerraENeutro: number;
    temperatura: number;
    redeEstabilizada: number;
    possuiNoBreak: number;
    possuiArCondicionado: number;
    justificativa: string;
    indAtivo: number;
    itens: ChecklistPreventivaItem[] = [];
    fotos: Foto[] = [];
    realizado: boolean;
    codUsuarioCad: string;
}