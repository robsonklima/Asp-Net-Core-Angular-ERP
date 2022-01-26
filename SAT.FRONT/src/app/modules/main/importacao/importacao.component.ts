import { ConfirmacaoDialogComponent } from './../../../shared/confirmacao-dialog/confirmacao-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { ImportacaoService } from './../../../core/services/importacao.service';
import { ImportacaoAberturaOrdemServico, ImportacaoEnum } from './../../../core/types/importacao.types';
import { Component, AfterViewInit } from '@angular/core';
import { ImportacaoConfiguracaoService } from 'app/core/services/importacao-configuracao.service';
import { ImportacaoTipoService } from 'app/core/services/importacao-tipo.service copy';
import { ImportacaoTipo, ImportacaoTipoData } from 'app/core/types/importacao-configuracao.type';


@Component({
	selector: 'app-importacao',
	templateUrl: './importacao.component.html',
	styleUrls: ['./importacao.component.scss']
})

export class ImportacaoComponent implements AfterViewInit {

	
	isLoading: boolean = false;
	planilhaConfig: any;
	planilha: ImportacaoAberturaOrdemServico[];
	idPlanilha: number;
	importacaoTipos: ImportacaoTipo[];

	constructor(
		private _importacaoService: ImportacaoService,
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
		this.planilha = json;

		console.log({
			id:this.idPlanilha,
			jsonImportacao: JSON.stringify(this.planilha)
		})
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
