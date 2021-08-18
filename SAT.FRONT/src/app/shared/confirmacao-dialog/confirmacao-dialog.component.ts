import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirmacao-dialog',
  templateUrl: './confirmacao-dialog.component.html'
})
export class ConfirmacaoDialogComponent {
  mensagem: string = "Confirmação"
  botaoConfirmarTexto = "Sim"
  botaoCancelarTexto = "Não"

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<ConfirmacaoDialogComponent>) {
    if (data) {
      this.mensagem = data.message || this.mensagem;
      if (data.buttonText) {
        this.botaoConfirmarTexto = data.buttonText.ok || this.botaoConfirmarTexto;
        this.botaoCancelarTexto = data.buttonText.cancel || this.botaoCancelarTexto;
      }
    }
  }

  confirmar(): void {
    this.dialogRef.close(true);
  }
}
