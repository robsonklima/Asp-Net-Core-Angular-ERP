export interface Indicador {
    nome: string;
    valor: number;
    filho?: Indicador[];
}