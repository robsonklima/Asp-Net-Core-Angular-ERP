import { Component, Inject } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatChipInputEvent } from '@angular/material/chips';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { appConfig } from 'app/core/config/app.config';
import { EmailService } from 'app/core/services/email.service';
import { Email } from 'app/core/types/email.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ENTER, COMMA, SEMICOLON } from '@angular/cdk/keycodes';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';

@Component({
    selector: 'app-email-dialog',
    templateUrl: './email-dialog.component.html'
})
export class EmailDialogComponent {
    public form: FormGroup;
    protected userSession: UserSession;
    protected assuntoEmail: string = 'Lorem ipsum';
    protected conteudoEmail: string = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.';
    protected emailRemetente: string = appConfig.email_equipe;
    protected nomeRemetente: string = 'Equipe SAT';
    public indOrcamento: boolean = false;
    public incluirLaudoExportacao: any;

    addOnBlur = true;
    readonly separatorKeysCodes = [ENTER, COMMA, SEMICOLON] as const;
    emails: string[] = [];

    constructor(
        @Inject(MAT_DIALOG_DATA) protected data: any,
        protected dialogRef: MatDialogRef<EmailDialogComponent>,
        protected _formBuilder: FormBuilder,
        protected _snack: CustomSnackbarService,
        protected _userService: UserService,
        protected _emailSvc: EmailService) {
        if (data) {
            this.assuntoEmail = data.assuntoEmail || this.assuntoEmail;
            this.conteudoEmail = data.conteudoEmail || this.conteudoEmail;
            this.emailRemetente = data.emailRemetente || this.emailRemetente;
            this.nomeRemetente = data.nomeRemetente || this.nomeRemetente;
            this.indOrcamento = data.indOrcamento || this.indOrcamento;
        }

        this.userSession = JSON.parse(this._userService.userSession);

        if (this.userSession && !data?.emailRemetente)
            this.emailRemetente = this.userSession?.usuario?.email || this.emailRemetente;

        if (this.userSession && !data?.nomeRemetente)
            this.nomeRemetente = this.userSession?.usuario?.nomeUsuario || this.nomeRemetente;

        if (data?.destinatarios?.length) {
            data?.destinatarios?.forEach(email => {
                this.emails.push(email);
            });
        }

        this.criarForm();
    }

    add(event: MatChipInputEvent): void {
        const value = (event.value || '').trim();

        if (value)
            this.emails.push(value);

        event.chipInput!.clear();
    }

    criarForm() {
        this.form = this._formBuilder.group({
            email: [undefined]
        });
    }

    removerEmail(email): void {
        const index = this.emails.indexOf(email);

        if (index >= 0)
            this.emails.splice(index, 1);
    }

    check(event: any) {
        this.incluirLaudoExportacao = event;
    }

    CopiarEmails() {
        navigator.clipboard.writeText(this.emails.join(';'));
		this._snack.exibirToast('Informação Copiada', 'info');
    }

    async confirmar() {
        var mailMessage: Email =
        {
            emailDestinatarios: this.emails,
            assunto: this.assuntoEmail,
            corpo: this.conteudoEmail,
        };

        this.dialogRef.close({ ...mailMessage, ...{ incluirLaudoExportacao: this.incluirLaudoExportacao?.checked ?? false } });
    }
}