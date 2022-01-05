import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { appConfig } from 'app/core/config/app.config';
import { EmailService } from 'app/core/services/email.service';
import { Email } from 'app/core/types/email.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
  selector: 'app-email-dialog',
  templateUrl: './email-dialog.component.html'
})
export class EmailDialogComponent implements OnInit
{
  snackConfigInfo: MatSnackBarConfig = { duration: 4000, panelClass: 'info', verticalPosition: 'top', horizontalPosition: 'right' };
  snackConfigDanger: MatSnackBarConfig = { duration: 2000, panelClass: 'danger', verticalPosition: 'top', horizontalPosition: 'right' };
  snackConfigSuccess: MatSnackBarConfig = { duration: 2000, panelClass: 'success', verticalPosition: 'top', horizontalPosition: 'right' };

  form: FormGroup;
  userSession: UserSession;
  assuntoEmail: string = 'Lorem ipsum';
  conteudoEmail: string = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.';
  emailRemetente: string = appConfig.email_equipe;
  nomeRemetente: string = 'Equipe SAT';

  constructor (
    @Inject(MAT_DIALOG_DATA) protected data: any,
    protected dialogRef: MatDialogRef<EmailDialogComponent>,
    protected _formBuilder: FormBuilder,
    protected _snack: MatSnackBar,
    protected _userService: UserService,
    protected _emailSvc: EmailService)
  {
    if (data)
    {
      this.assuntoEmail = data.assuntoEmail || this.assuntoEmail;
      this.conteudoEmail = data.conteudoEmail || this.conteudoEmail;
      this.emailRemetente = data.emailRemetente || this.emailRemetente;
      this.nomeRemetente = data.nomeRemetente || this.nomeRemetente;
    }

    this.userSession = JSON.parse(this._userService.userSession);

    if (this.userSession && !data.emailRemetente)
      this.emailRemetente = this.userSession?.usuario?.email || this.emailRemetente;

    if (this.userSession && !data.nomeRemetente)
      this.nomeRemetente = this.userSession?.usuario?.nomeUsuario || this.nomeRemetente;

    this.criarForm();
  }

  async ngOnInit() { }

  criarForm()
  {
    this.form = this._formBuilder.group({
      email: [undefined, [Validators.required]]
    });
  }

  async confirmar()
  {
    var mailMessage: Email =
    {
      emailRemetente: this.emailRemetente,
      nomeRemetente: this.nomeRemetente,
      emailDestinatario: this.form.controls['email'].value,
      assunto: this.assuntoEmail,
      corpo: this.conteudoEmail
    };

    this._emailSvc.enviarEmail(mailMessage).subscribe(s =>
    {
      this._snack.open('E-mail enviado com sucesso.', null, this.snackConfigSuccess).afterDismissed().toPromise();
    },
      e =>
      {
        this._snack.open('Não foi possível enviar o e-mail.', null, this.snackConfigDanger).afterDismissed().toPromise();
      });

    this.dialogRef.close(true);
  }
}