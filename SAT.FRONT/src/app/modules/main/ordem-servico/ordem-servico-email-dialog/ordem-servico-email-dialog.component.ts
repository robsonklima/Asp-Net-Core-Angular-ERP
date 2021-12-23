import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EmailService } from 'app/core/services/email.service';
import { Email } from 'app/core/types/email.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
  selector: 'app-ordem-servico-email-dialog',
  templateUrl: './ordem-servico-email-dialog.component.html'
})
export class OrdemServicoEmailDialogComponent implements OnInit
{

  form: FormGroup;
  userSession: UserSession;
  codOS: number;
  os: OrdemServico;

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<OrdemServicoEmailDialogComponent>,
    private _formBuilder: FormBuilder,
    private _snack: MatSnackBar,
    private _userService: UserService,
    private _emailSvc: EmailService)
  {
    if (data)
    {
      this.os = data.os;
    }

    this.userSession = JSON.parse(this._userService.userSession);
    this.criarForm();
  }

  async ngOnInit()
  {
  }

  criarForm()
  {
    this.form = this._formBuilder.group({
      email: [undefined, [Validators.required]]
    });
  }

  obterImpressao()
  {
    var contentToPrint = document.getElementById("print-area").innerHTML;
    return `<html><body>${contentToPrint}</body></html>`;
  }

  async confirmar()
  {
    var corpoEmail: string = this.obterImpressao();
    var mailMessage: Email =
    {
      emailRemetente: 'equipe.sat@perto.com.br',
      nomeRemetente: 'Equipe SAT',
      emailDestinatario: this.form.controls['email'].value,
      nomeDestinatario: '',
      assunto: 'OS ' + this.os.codOS,
      corpo: corpoEmail
    };

    this._emailSvc.enviarEmail(mailMessage).toPromise();
    this.dialogRef.close(true);
  }
}