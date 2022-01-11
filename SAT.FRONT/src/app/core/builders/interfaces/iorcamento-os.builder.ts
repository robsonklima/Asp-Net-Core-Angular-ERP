import { Orcamento } from "app/core/types/orcamento.types";

export interface ISpecifyBaseOrcamentoOSBuilder
{
    specifyBase(): Promise<ISpecifyMateriaisOrcamentoOSBuilder>;
};

export interface ISpecifyMateriaisOrcamentoOSBuilder
{
    specifyMateriais(): Promise<ISpecifyMaoDeObraOrcamentoOSBuilder>;
};

export interface ISpecifyMaoDeObraOrcamentoOSBuilder
{
    specifyMaoDeObra(): Promise<ISpecifyDeslocamentoOrcamentoOSBuilder>;
};

export interface ISpecifyDeslocamentoOrcamentoOSBuilder
{
    specifyDeslocamento(): Promise<IOrcamentoOSBuilder>;
};

export interface IOrcamentoOSBuilder
{
    build(): Promise<Orcamento>;
};