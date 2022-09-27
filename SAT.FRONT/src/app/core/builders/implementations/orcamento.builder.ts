import { Injectable } from "@angular/core";
import { Orcamento } from "app/core/types/orcamento.types";
import {
    IOrcamentoOSBuilder, ISpecifyBaseOrcamentoOSBuilder, ISpecifyDeslocamentoOrcamentoOSBuilder,
    ISpecifyMaoDeObraOrcamentoOSBuilder, ISpecifyMateriaisOrcamentoOSBuilder
} from "../interfaces/iorcamento-os.builder";
import Enumerable from "linq";

@Injectable({
    providedIn: 'root'
})
export class OrcamentoBuilder implements
    ISpecifyBaseOrcamentoOSBuilder,
    ISpecifyMateriaisOrcamentoOSBuilder,
    ISpecifyMaoDeObraOrcamentoOSBuilder,
    ISpecifyDeslocamentoOrcamentoOSBuilder,
    IOrcamentoOSBuilder {
    constructor() { }

    specifyBase(): Promise<ISpecifyMateriaisOrcamentoOSBuilder> {
        throw new Error("Method not implemented.");
    }

    specifyMateriais(): Promise<ISpecifyMaoDeObraOrcamentoOSBuilder> {
        throw new Error("Method not implemented.");
    }

    specifyMaoDeObra(): Promise<ISpecifyDeslocamentoOrcamentoOSBuilder> {
        throw new Error("Method not implemented.");
    }

    specifyDeslocamento(): Promise<IOrcamentoOSBuilder> {
        throw new Error("Method not implemented.");
    }

    build(): Promise<Orcamento> {
        throw new Error("Method not implemented.");
    }

    calculaTotalizacao(orcamento: Orcamento): Orcamento {
        const valorMaoObra = orcamento?.maoDeObra?.valorTotal;
        const valorKmDeslocamento = orcamento?.orcamentoDeslocamento?.valorTotalKmDeslocamento;
        const valorKmRodado = orcamento?.orcamentoDeslocamento?.valorTotalKmRodado;
        const valorMateriais = Enumerable.from(orcamento?.orcamentoMateriais).sum(i => i?.valorTotal);
        const valorDescontos = Enumerable.from(orcamento?.descontos).sum(i => i.valorTotal);
        const valorDescontoMateriais = Enumerable.from(orcamento?.orcamentoMateriais).sum(i => i?.valorDesconto);

        orcamento.valorTotal = (valorMaoObra + valorKmDeslocamento + valorKmRodado + valorMateriais) - valorDescontos;
        orcamento.valorTotalDesconto = valorDescontos + valorDescontoMateriais;

        return orcamento;
    }
}