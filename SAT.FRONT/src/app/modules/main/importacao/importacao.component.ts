import { DespesaCartaoCombustivelDetalheComponent } from './../despesa/despesa-cartao-combustivel-detalhe/despesa-cartao-combustivel-detalhe.component';
import { ConfirmacaoDialogComponent } from './../../../shared/confirmacao-dialog/confirmacao-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { ImportacaoService } from './../../../core/services/importacao.service';
import { ImportacaoAberturaOrdemServico, ImportacaoColunas, ImportacaoDados, ImportacaoEnum } from './../../../core/types/importacao.types';
import { Component, AfterViewInit } from '@angular/core';


@Component({
	selector: 'app-importacao',
	templateUrl: './importacao.component.html',
	styleUrls: ['./importacao.component.scss']
})

export class ImportacaoComponent implements AfterViewInit {

	configuracao = {

		atualizaImplantacao: {
			id: ImportacaoEnum.ATUALIZA_IMPLANTACAO,
			dados: ImportacaoDados.atualizaImplantacao,
			colunas: ImportacaoColunas.atualizaImplantacao
		},

		aberturaChamados: {
			id: ImportacaoEnum.ABERTURA_CHAMADOS,
			dados: ImportacaoDados.aberturaChamados,
			colunas: ImportacaoColunas.aberturaChamados
		},

		criacaoLotes: {
			id: ImportacaoEnum.CRIACAO_LOTES,
			dados: ImportacaoDados.criacaoLotes,
			colunas: ImportacaoColunas.criacaoLotes
		}

	}
	isLoading: boolean = false;
	config: any;
	planilha: ImportacaoAberturaOrdemServico[];
	idPlanilha: number;

	constructor(
		private _importacaoService: ImportacaoService,
		public _dialog: MatDialog

	) { }

	ngAfterViewInit() {


	}

	configura(newConfig: object) {

		this.idPlanilha = newConfig['id'];
		this.config = newConfig;
	}

	retornaPlanilha(json: any) {
		this.planilha = json;
	}

	async enviarDados() {
		this.isLoading = true;

		await this._importacaoService.aberturaChamadosEmMassa({

			id: this.idPlanilha,
			jsonImportacao: JSON.stringify(this.planilha)

		}).subscribe(r => {

			this._dialog.open(ConfirmacaoDialogComponent, {
				data: {
					titulo: 'Aviso!',
					message: Array.from(new Set(r)),
					hideCancel: true,
					buttonText: {
						ok: 'Ok',
					}
				}
			});

			this.isLoading = false;
		});
	}
}
