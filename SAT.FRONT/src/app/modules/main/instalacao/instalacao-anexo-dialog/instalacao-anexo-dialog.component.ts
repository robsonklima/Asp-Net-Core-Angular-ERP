import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { InstalacaoAnexoService } from 'app/core/services/instalacao-anexo.service';
import { InstalacaoAnexo } from 'app/core/types/instalacao-anexo.types';
import { Instalacao } from 'app/core/types/instalacao.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { Utils } from 'app/core/utils/utils';
import { MessagesComponent } from 'app/layout/common/messages/messages.component';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
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
    private _snack: CustomSnackbarService,
    private _utils: Utils,
    private _dialog: MatDialog
  ) {
    this.instalacao = data?.instalacao;    
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
  }

  async onFileSelected(event) {
    await this.convertFile(event.target.files[0]).subscribe(base64 => {
      const nomeArquivo = this.getFileName(base64);
      
      if (!nomeArquivo) 
        return this._snack.exibirToast('O formato do seu arquivo é inválido', 'error');

      this.instalacaoAnexo = {
        codInstalacao: this.instalacao.codInstalacao,
        indAtivo: +statusConst.ATIVO,
        codUsuarioCad: this.userSession.usuario.codUsuario,
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        nomeAnexo: nomeArquivo,
        descAnexo: nomeArquivo,
        sourceAnexo: nomeArquivo,
        base64: base64,
      }      
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
    return moment().format('YYYYMMDDHHmmssSSS') + '.' + this._utils.obterExtensionBase64(base64);
  }

  getFileSize(base64: string): number {
    const decoded = atob(base64);
    return decoded.length;
  }

  deletar(codInstalAnexo: number) {
      const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
        data: {
          titulo: 'Confirmação',
          message: 'Deseja excluir este arquivo?',
          buttonText: { 
            ok: 'Sim',
            cancel: 'Não'
          }
        }
      });
  
      dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
        if (confirmacao) {
          this._instalacaoAnexoService.deletar(codInstalAnexo).subscribe(() => {
            this.obterAnexos();
          });
        }
      });
  }

  download(anexo: InstalacaoAnexo) {
    const downloadLink = document.createElement('a');
    const fileName = anexo.nomeAnexo;
    downloadLink.href = 'data:application/octet-stream;base64,' + anexo.base64;
    downloadLink.download = fileName;
    downloadLink.click();
  }

  salvar() {
    this._instalacaoAnexoService.criar(this.instalacaoAnexo).subscribe(() => {
      this._snack.exibirToast('Arquivo anexado com sucesso', 'success');
      this._dialogRef.close(true);
    }, () => {
      this._snack.exibirToast('Erro ao enviar o arquivo para o servidor', 'error');
    })
  }

  fechar() {
    this._dialogRef.close();
  }
}
