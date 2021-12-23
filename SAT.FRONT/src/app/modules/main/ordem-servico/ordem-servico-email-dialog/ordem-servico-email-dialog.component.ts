import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { EmailService } from 'app/core/services/email.service';
import { Email } from 'app/core/types/email.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { OrdemServicoImpressaoComponent } from '../ordem-servico-impressao/ordem-servico-impressao.component';

@Component({
  selector: 'app-ordem-servico-email-dialog',
  templateUrl: './ordem-servico-email-dialog.component.html'
})
export class OrdemServicoEmailDialogComponent implements OnInit
{

  form: FormGroup;
  userSession: UserSession;
  codOs: number;
  ordemServico: OrdemServico;

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<OrdemServicoEmailDialogComponent>,
    private _formBuilder: FormBuilder,
    private _snack: MatSnackBar,
    private _osService: MatSnackBar,
    private _userService: UserService,
    private _activatedRoute: ActivatedRoute,
    private _emailSvc: EmailService)
  {
    if (data)
    {
      this.codOs = data.codOS;
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
  }

  async confirmar()
  {
    var corpoEmail: string = "Teste";

    var mailMessage: Email =
    {
      emailRemetente: 'marilia.pit@gmail.com',
      nomeRemetente: 'marilia pit',
      emailDestinatario: 'marilia.pit@gmail.com',
      nomeDestinatario: 'marilia pit',
      assunto: 'OS',
      corpo: corpoEmail
    };

    this._emailSvc.enviarEmail(mailMessage).toPromise();

    this.dialogRef.close(true);
  }
}