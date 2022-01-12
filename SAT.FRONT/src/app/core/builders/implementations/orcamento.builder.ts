import { Injectable } from "@angular/core";
import { Orcamento } from "app/core/types/orcamento.types";
import Enumerable from "linq";
import { IOrcamentoOSBuilder, ISpecifyBaseOrcamentoOSBuilder, ISpecifyDeslocamentoOrcamentoOSBuilder, ISpecifyMaoDeObraOrcamentoOSBuilder, ISpecifyMateriaisOrcamentoOSBuilder } from "../interfaces/iorcamento-os.builder";

@Injectable({
    providedIn: 'root'
})
export class OrcamentoBuilder implements
    ISpecifyBaseOrcamentoOSBuilder,
    ISpecifyMateriaisOrcamentoOSBuilder,
    ISpecifyMaoDeObraOrcamentoOSBuilder,
    ISpecifyDeslocamentoOrcamentoOSBuilder,
    IOrcamentoOSBuilder
{
    constructor () { }

    specifyBase(): Promise<ISpecifyMateriaisOrcamentoOSBuilder>
    {
        throw new Error("Method not implemented.");
    }
    specifyMateriais(): Promise<ISpecifyMaoDeObraOrcamentoOSBuilder>
    {
        throw new Error("Method not implemented.");
    }
    specifyMaoDeObra(): Promise<ISpecifyDeslocamentoOrcamentoOSBuilder>
    {
        throw new Error("Method not implemented.");
    }
    specifyDeslocamento(): Promise<IOrcamentoOSBuilder>
    {
        throw new Error("Method not implemented.");
    }
    build(): Promise<Orcamento>
    {
        throw new Error("Method not implemented.");
    }

    calculaTotalizacao(orcamento: Orcamento): Orcamento
    {
        console.log(Enumerable.from(orcamento?.materiais).sum(i => i?.valorTotal));
        orcamento.valorTotal =
            (Enumerable.from(orcamento?.materiais).sum(i => i?.valorTotal) +
                orcamento?.maoDeObra?.valorTotal +
                orcamento?.orcamentoDeslocamento?.valorTotalKmDeslocamento) ?? 0;

        orcamento.valorTotalDesconto =
            Enumerable.from(orcamento.descontos).sum(i => i.valorTotal) ?? 0;

        return orcamento;
    }
}