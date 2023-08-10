import { AfterViewInit, Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ImportacaoArquivoService } from 'app/core/services/importacao-arquivo.service';
import { ImportacaoArquivo } from 'app/core/types/importacao-arquivo.types';
import { Utils } from 'app/core/utils/utils';
import { MessagesComponent } from 'app/layout/common/messages/messages.component';
import moment from 'moment';
import { Observable, ReplaySubject } from 'rxjs';

@Component({
	selector: 'app-importacao-arquivo-adendo',
	templateUrl: './importacao-arquivo-adendo.component.html'
})
export class ImportacaoArquivoAdendoComponent implements AfterViewInit {
	isLoading: boolean;
	importacaoArquivo: ImportacaoArquivo;

	constructor(
		public _dialog: MatDialog,
		private _importacaoArquivoSrv: ImportacaoArquivoService,
		private _snack: CustomSnackbarService,
		private _utils: Utils
	) { }

	ngAfterViewInit() {
		this.obterDados();
	}

	async obterDados() {
	}

	async onChange(evento)
	{
		this.isLoading = true;
		
		await this.convertFile(evento.target.files[0]).subscribe(base64 => {
			const descAnexo = this.getFileName(base64);
			const nomeArquivo = evento.target.files[0].name
			
			if (!descAnexo) 
			  return this._snack.exibirToast('O formato do seu arquivo é inválido', 'error');
	  
			this.importacaoArquivo = {
			  nomeAnexo: nomeArquivo,
			  descAnexo: descAnexo,
			  sourceAnexo: descAnexo,
			  base64: base64,
			}      
			this.salvar();
		  });
		
		this.isLoading = false;
	}
	
	  convertFile(file: File): Observable<string> {
		const result = new ReplaySubject<string>(1);
		const reader = new FileReader();
		reader.readAsBinaryString(file);
		reader.onload = (event) => result.next(btoa(event.target.result.toString()));
		return result;
	  }
	
	  getFileName(base64: string): string {
		return moment().format('YYYYMMDDHHmmssSSS') + '.' + this._utils.obterExtensionBase64(base64);
	  }
	
	  getFileSize(base64: string): number {
		const decoded = atob(base64);
		return decoded.length;
	  }

	  salvar() {
		this._importacaoArquivoSrv.criar(this.importacaoArquivo).subscribe(() => {
		  this._snack.exibirToast('Arquivo importado com sucesso', 'success');
		}, () => {
		  this._snack.exibirToast('Erro ao enviar o arquivo para o servidor', 'error');
		})
	  }
}

