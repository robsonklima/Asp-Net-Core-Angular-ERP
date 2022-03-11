import { MatDialog } from '@angular/material/dialog';
import { Importacao, ImportacaoAberturaOrdemServico } from './../../../core/types/importacao.types';
import { Component, AfterViewInit } from '@angular/core';
import { ImportacaoConfiguracaoService } from 'app/core/services/importacao-configuracao.service';
import { ImportacaoTipoService } from 'app/core/services/importacao-tipo.service copy';
import { ImportacaoTipo } from 'app/core/types/importacao-configuracao.type';

@Component({
	selector: 'app-importacao',
	templateUrl: './importacao.component.html'
})

export class ImportacaoComponent implements AfterViewInit {
	isLoading: boolean = false;
	planilhaConfig: any;
	planilha: ImportacaoAberturaOrdemServico[];
	idPlanilha: number;
	importacaoTipos: ImportacaoTipo[];
	codImportacaoTipo: number;
	loading: boolean;

	constructor(
		private _importacaoConfService: ImportacaoConfiguracaoService,
		private _importacaoTipoService: ImportacaoTipoService,
		public _dialog: MatDialog

	) { }

	ngAfterViewInit() {
		this.obterDados();
	}

	async obterDados(){
		this.importacaoTipos = (await this._importacaoTipoService.obterPorParametros({}).toPromise()).items
	}

	async configura(codImportacaoTipo: number) {
		this.codImportacaoTipo = codImportacaoTipo;
		const config = (await this._importacaoConfService.obterPorParametros({codImportacaoTipo: codImportacaoTipo}).toPromise()).items;

		let configData = config.map((conf) => {
			return {
				
				dados: {
					[conf.propriedade]: ''
				},
				colunas: {
					title: conf.titulo,
					width: conf.largura,
					type: conf.tipoHeader,
					mask: conf.mascara
				}
			}
		});
		
		let dadosMap = [{}]
		configData.map(({dados}) => dados).forEach(dado => {
			dadosMap[0][Object.keys(dado)[0]] = '';
		});

		this.idPlanilha = codImportacaoTipo;
		this.planilhaConfig = {
			id: codImportacaoTipo,
			dados: dadosMap,
			colunas: configData.map(({colunas}) => colunas)
		}
	}

	retornaPlanilha(json: any) {
		this.jsonImportacaoMap(json);	
		this.planilha = json;
	}

	jsonImportacaoMap(planilhaJson: any) {

		// let planilhaLinhasMap = planilhaJson.map((row) => {
			// let importacaoCol: ImportacaoColuna[];
			// Object.keys(row).forEach( r => importacaoCol.push({campo: r}));
			
			// row.forEach(element => {
			// 	importacaoCol.push({
			// 		campo: Object.keys(element).toString(),
			// 		valor: element
			// 	});
			// });
		// 	console.log(importacaoCol);
		// 	console.log(row);
		// });

		let importacao: Importacao = {
			id: 1,
			importacaoLinhas: [{
				importacaoColunas: [{
					campo: '',
					valor: ''
				}],
				erro: 0,
				mensagem: ''
			}]
		}

		return importacao;
	}

	async enviarDados() {
		// this._importacaoService.importar({
		// 	id: this.idPlanilha,
		// 	jsonImportacao: JSON.stringify(this.planilha)
		// }).subscribe();
	}
}
