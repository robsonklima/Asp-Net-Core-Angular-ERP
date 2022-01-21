import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { finalize } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { FuseAlertType } from '@fuse/components/alert';
import { AuthService } from 'app/core/auth/auth.service';
import { EmailService } from 'app/core/services/email.service';

@Component({
    selector: 'esqueceu-senha',
    templateUrl: './esqueceu-senha.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class EsqueceuSenhaComponent implements OnInit {

    @ViewChild('esqueceuSenhaForm') esqueceuSenhaForm: NgForm;

    alert: { type: FuseAlertType; message: string } =
        {
            type: 'success',
            message: 'Uma nova senha foi enviada para o seu e-mail'
        };

    esqueceuSenhaFormGroup: FormGroup;
    mostrarAlerta: boolean = false;

    constructor(
        private _authService: AuthService,
        private _formBuilder: FormBuilder,
        private _emailSvc: EmailService,
    ) {
    }

    ngOnInit(): void {
        this.esqueceuSenhaFormGroup = this._formBuilder.group({
            codUsuario: ['', [Validators.required]]
            // email: ['', [
            //     Validators.required,
            //     Validators.email,
            //     Validators.pattern('^[A-Za-z0-9._%+-]+@(perto|digicon).com.br$')]]
        });
    }

    sendResetLink(): void {

        //  this.mostrarErro = this.esqueceuSenhaFormGroup.invalid;

        // Verifica se é válido o form
        if (this.esqueceuSenhaFormGroup.invalid) {
            return;
        }

        // Disabilita o form
        this.esqueceuSenhaFormGroup.disable();

        // Esconde os alertas
        this.mostrarAlerta = false;

        // Chama o serviço
        this._authService.esqueceuSenha(this.esqueceuSenhaFormGroup.get('codUsuario').value)
            .pipe(
                finalize(() => {
                    this.esqueceuSenhaFormGroup.enable();
                    this.esqueceuSenhaForm.resetForm();
                })
            )
            .subscribe(
                // Success
                (response) => {
                    let url = 'https://sat.perto.com.br/SAT.V2.FRONTEND/#/confirmacao-nova-senha/' + encodeURIComponent(response.data.codRecuperaSenha);
                    this._emailSvc.enviarEmail({
                        nomeRemetente: "SAT",
                        emailRemetente: "aplicacao.sat@perto.com.br",
                        nomeDestinatario: response.data.nomeUsuario,
                        emailDestinatario: response.data.email,
                        nomeCC: 'Equipe SAT',
                        emailCC: 'equipe.sat@perto.com.br',
                        assunto: "Solicitação de nova senha de acesso ao Sistema SAT",
                        corpo: `<!DOCTYPE html>
                                <html>
                                <head>
                                <link href='http://fonts.googleapis.com/css?family=Roboto' rel='stylesheet' type='text/css'>                          
                                <style>
                                  body {
                                    font-family: 'Roboto', sans-serif;
                                    font-size: 12px;
                                  }
                                </style>
                                </head>
                                <body>                                
                                    <h3>Olá ${response.data.nomeUsuario},</h3>
                                    <p>Você solicitou uma nova senha de acesso ao Sistema SAT. Clique <a href="${url}"><b>aqui</b></a> para confirmar a solicitação e receber sua nova senha.</p>
                                    <p>Caso não tenha sido você, por favor, entre em contato com o setor resposável.</p>             
                                    <p>Atenciosamente,<br> 
                                Equipe SAT<br> 
                                Perto S.A. – Tecnologia para Bancos e Varejo<br> 
                                Ramal (51) 2126-6944<br> 
                                Whatsapp: (51) 997144990<br>
                                </body>
                                </html>`
                    }).subscribe(
                        (success) => {
                            this.alert.message = "Um e-mail de confirmação foi enviado para " + this.esconderEmail(response.data.email);
                            this.alert.type = 'success';
                            this.mostrarAlerta = true;
                        },
                        (fail) => {
                            this.alert.message = response.error;
                            this.alert.type = 'error';
                            this.mostrarAlerta = true;
                        }
                    );

                },
                // Fail
                (response) => {
                    this.alert.message = response.error;
                    this.alert.type = 'error';
                    this.mostrarAlerta = true;
                }
            );
    }

    esconderEmail(email) {
        if (!email) return '';

        return email.replace(/(.{2})(.*)(?=@)/,
            (gp1, gp2, gp3) => {
                for (let i = 0; i < gp3.length; i++) {
                    gp2 += "*";
                } return gp2;
            });
    };
}
