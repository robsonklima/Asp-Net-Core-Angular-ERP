import { Component, Input, OnInit } from '@angular/core';
import { FotoService } from 'app/core/services/foto.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { Foto } from 'app/core/types/foto.types';
import { RelatorioAtendimento, RelatorioAtendimentoData } from 'app/core/types/relatorio-atendimento.types';

@Component({
	selector: 'app-ordem-servico-rat-fotos',
	templateUrl: './ordem-servico-rat-fotos.component.html'
})
export class OrdemServicoRATFotosComponent implements OnInit {
	@Input() codOS: number;
	isLoading: boolean = true;
	rats: RelatorioAtendimento[] = [];
	qtdFotos: number = 0;

	constructor(
		private _fotoService: FotoService,
		private _relatorioAtendimentoService: RelatorioAtendimentoService
	) { }

	async ngOnInit() {
		this.rats = (await this.obterRATS()).items;
		this.obterFotosRAT();

		setTimeout(() => {
			this.isLoading = false;	
		}, 1200)
	}

	private async obterRATS(): Promise<RelatorioAtendimentoData> {
		return this._relatorioAtendimentoService
			.obterPorParametros({ codOS: this.codOS })
			.toPromise();
	}

	filtrarFotosRAT(tipo: string, fotos: Foto[]): Foto[] {
		let fotosFiltered: Foto[];

		if (tipo === 'RAT')
		{
			fotosFiltered = fotos.filter(f => !f.modalidade.includes('LAUDO'));
		} else if (tipo === 'LAUDO')
		{
			fotosFiltered = fotos.filter(f => f.modalidade.includes('LAUDO'));
		}

		return fotosFiltered;
	}

	private async obterFotosRAT() {
		for (const [i, rat] of this.rats.entries()) {
			if (!rat.numRAT || !rat.codOS) return;

			this.rats[i].fotos =
				(await this._fotoService.obterPorParametros(
					{
						codOS: rat.codOS,
						numRAT: rat.numRAT,
						sortActive: 'CodRATFotoSmartphone',
						sortDirection: 'desc'
					}
				).toPromise()).items.filter(f => !f.modalidade.includes('ASSINATURA'));

			this.qtdFotos = this.qtdFotos + this.rats[i].fotos?.length;
		}
	}
}
