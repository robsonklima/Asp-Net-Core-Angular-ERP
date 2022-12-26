import { Component, Input, OnInit } from '@angular/core';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { EquipamentoContrato } from 'app/core/types/equipamento-contrato.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimento, RelatorioAtendimentoData } from 'app/core/types/relatorio-atendimento.types';

@Component({
    selector: 'app-ordem-servico-stn-form-informacao',
    templateUrl: './ordem-servico-stn-form-informacao.component.html'
})
export class OrdemServicoStnFormInformacaoComponent implements OnInit {
    @Input() codOS: number;
    os: OrdemServico;
    rat: RelatorioAtendimento;
    equipamento: EquipamentoContrato;

    constructor(
        private _ordemServicoService: OrdemServicoService,
        private _relatorioAtendimentoService: RelatorioAtendimentoService,
        private _equipamentoContratoService: EquipamentoContratoService,
    ) { }

    async ngOnInit() {
        this.os = await this._ordemServicoService.obterPorCodigo(this.codOS).toPromise();
        this.rat = (await this.obterRelatoriosAtendimento()).items.shift();
        this.equipamento = await this._equipamentoContratoService.obterPorCodigo(this.os.codEquipContrato).toPromise();

    }

    private async obterRelatoriosAtendimento(): Promise<RelatorioAtendimentoData> {
        return await this._relatorioAtendimentoService.obterPorParametros({ codOS: this.os.codOS }).toPromise();
    }

    salvar() { }
}
