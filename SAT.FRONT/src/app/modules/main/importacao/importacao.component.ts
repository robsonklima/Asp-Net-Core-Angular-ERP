import { ImportacaoService } from './../../../core/services/importacao.service';
import { ImportacaoEnum, ImportacaoColunas, ImportacaoDados } from './../../../core/types/importacao.types';
import { Component, ElementRef, ViewChild, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import * as jspreadsheet from "jspreadsheet-ce";
import Enumerable from 'linq';

@Component({
	selector: 'app-importacao',
	templateUrl: './importacao.component.html',
	styleUrls: ['./importacao.component.scss']
})
export class ImportacaoComponent implements AfterViewInit {



	@ViewChild("spreadsheet") spreadsheet: ElementRef;

	table: any;
	dados: object[] = [];
	colunas: object[] = [];

	constructor(
		private _importacaoService: ImportacaoService,
		
		) { }

	ngAfterViewInit() {

		this.criaPlanilha();

	}

	criaPlanilha() {
		this.table = jspreadsheet(this.spreadsheet.nativeElement, {
			data: this.dados,
			columns: this.colunas,
			minDimensions: [
				this.colunas.length > 0 ? this.colunas.length : 27
				, 30],
		});
	}

	atualizaPlanilha() {
		this.spreadsheet.nativeElement.innerHTML = '';
		this.criaPlanilha();
	}

	configuraPlanilha(codConfig: number) {
		switch (codConfig) {
			case ImportacaoEnum.ATUALIZA_IMPLANTACAO:

				this.dados = ImportacaoDados.atualizaImplantacao;

				this.colunas = ImportacaoColunas.atualizaImplantacao;

				break;
			case ImportacaoEnum.ABERTURA_CHAMADOS:

				this.dados =ImportacaoDados.aberturaChamados;

				this.colunas =ImportacaoColunas.aberturaChamados;

				break;
			case ImportacaoEnum.CRIACAO_LOTES:

				this.dados = ImportacaoDados.criacaoLotes;

				this.colunas = ImportacaoColunas.criacaoLotes;

				break;
			default:
				break;
		}

		this.atualizaPlanilha();

	}

	async enviarDados() {

		this._importacaoService.aberturaChamadosEmMassa(this.table.getJson()).subscribe();

		console.log(this.table.getJson())
	}

}
