import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { EmailService } from 'app/core/services/email.service';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UserService } from 'app/core/user/user.service';
import { EmailDialogComponent } from 'app/shared/email-dialog/email-dialog.component';

@Component({
  selector: 'app-ordem-servico-email-dialog',
  templateUrl: './ordem-servico-email-dialog.component.html',
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None,
})

export class OrdemServicoEmailDialogComponent extends EmailDialogComponent
{

  os: OrdemServico;

  constructor (
    @Inject(MAT_DIALOG_DATA) protected data: any,
    protected _dialog: MatDialog,
    protected _dialogRef: MatDialogRef<EmailDialogComponent>,
    protected _snack: CustomSnackbarService,
    protected _formBuilder: FormBuilder,
    protected _userService: UserService,
    protected _emailSvc: EmailService)
  {
    super(data, _dialogRef, _formBuilder, _snack, _userService, _emailSvc);

    this.os = data.os;
  }

  confirmar()
  {
    this.assuntoEmail = `OS ${this.os.codOS}`;
    this.conteudoEmail = this.obterImpressao();
    return super.confirmar();
  }

  obterImpressao()
  {
    var contentToPrint = document?.getElementById("print-area")?.innerHTML;
    return `<html><body>${contentToPrint}</body></html>`;
  }
}