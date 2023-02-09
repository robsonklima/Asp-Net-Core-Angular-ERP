import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { InstalacaoAnexoService } from 'app/core/services/instalacao-anexo.service';
import { InstalacaoAnexo } from 'app/core/types/instalacao-anexo.types';
import { Instalacao } from 'app/core/types/instalacao.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { MessagesComponent } from 'app/layout/common/messages/messages.component';
import moment from 'moment';
import { Observable, ReplaySubject } from 'rxjs';

@Component({
  selector: 'app-instalacao-anexo-dialog',
  templateUrl: './instalacao-anexo-dialog.component.html'
})
export class InstalacaoAnexoDialogComponent implements OnInit {
  instalacao: Instalacao;
  instalacaoAnexo: InstalacaoAnexo;
  instalacaoAnexos: InstalacaoAnexo[] = [];
  userSession: UserSession;

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    public _dialogRef: MatDialogRef<MessagesComponent>,
    private _instalacaoAnexoService: InstalacaoAnexoService,
    private _userService: UserService,
    private _snack: CustomSnackbarService
  ) {
    this.instalacao = data?.instalacao;
    console.log(this.instalacao);
    
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit() {
    this.obterAnexos();
  }

  private async obterAnexos() {
    const data = await this._instalacaoAnexoService
      .obterPorParametros({ codInstalacao: this.instalacao.codInstalacao })
      .toPromise();

    this.instalacaoAnexos = data.items;

    console.log(this.instalacaoAnexos);
    
  }

  async onFileSelected(event) {
    await this.convertFile(event.target.files[0]).subscribe(base64 => {
      const nomeArquivo = this.getFileName(base64);
      
      if (!nomeArquivo) 
        return this._snack.exibirToast('O formato do seu arquivo é inválido', 'error');

      this.instalacaoAnexo = {
        codInstalacao: this.instalacao.codInstalacao,
        codInstalLote: this.instalacao.codInstalLote,
        indAtivo: +statusConst.ATIVO,
        codUsuarioCad: this.userSession.usuario.codUsuario,
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        nomeAnexo: nomeArquivo,
        descAnexo: nomeArquivo,
        sourceAnexo: nomeArquivo,
        base64: base64,
      }
      console.log(this.instalacaoAnexo);
      
      this.salvar();
    });
  }

  convertFile(file: File): Observable<string> {
    const result = new ReplaySubject<string>(1);
    const reader = new FileReader();
    reader.readAsBinaryString(file);
    reader.onload = (event) => result.next(btoa(event.target.result.toString()));
    return result;
  }

  getFileName(base64: string): string {
    return moment().format('YYYYMMDDHHmmssSSS') + '.' + this.getExtension(base64.substring(0, 5))
  }

  getFileSize(base64: string): number {
    const decoded = atob(base64);
    return decoded.length;
  }

  getExtension(str: string) {
    switch (str.toUpperCase())
    {
      case "IVBOR":
        return "png";
      case "/9J/4":
        return "jpg";
      case "AAAAF":
        return "mp4";
      case "JVBER":
        return "pdf";
      case "AAABA":
        return "ico";
      case "UMFYI":
        return "rar";
      case "E1XYD":
        return "rtf";
      case "U1PKC":
        return "txt";
      case "MQOWM":
      case "77U/M":
        return "srt";
      case "0M8R4":
        return "xls";
      case "UESDB":
        return "xlsx";
      default:
        return '';
    }
  }

  deletar(codInstalAnexo: number) {

  }

  download(anexo: InstalacaoAnexo) {

  }

  salvar() {
    this._instalacaoAnexoService.criar(this.instalacaoAnexo).subscribe(() => {
      this._snack.exibirToast('Arquivo anexado com sucesso', 'success');
      this._dialogRef.close(true);
    }, () => {
      this._snack.exibirToast('Erro ao enviar o arquivo para o servidor', 'error');
    })
  }
}
