import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { finalize } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { FuseAlertType } from '@fuse/components/alert';
import { AuthService } from 'app/core/auth/auth.service';
import { EmailService } from 'app/core/services/email.service';
import { Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { UserService } from 'app/core/user/user.service';
import { PerfilEnum } from 'app/core/types/perfil.types';
import { SmsService } from 'app/core/services/sms.service';

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
        private _router: Router,
        private _authService: AuthService,
        private _userService: UserService,
        private _formBuilder: FormBuilder,
        private _emailService: EmailService,
        private _smsService: SmsService,
        private _snack: CustomSnackbarService
    ) {
    }

    ngOnInit(): void {
        this.esqueceuSenhaFormGroup = this._formBuilder.group({
            codUsuario: ['', [Validators.required]]
        });
    }

    async sendResetLink() {
        const codUsuario = this.esqueceuSenhaFormGroup.get('codUsuario').value;
        const usuario = await this._userService.obterPorCodigo(codUsuario).toPromise();

        if (this.esqueceuSenhaFormGroup.invalid) 
            return;

        this.esqueceuSenhaFormGroup.disable();

        this.mostrarAlerta = false;

        this._authService.esqueceuSenha(codUsuario)
            .pipe(
                finalize(() => {
                    this.esqueceuSenhaFormGroup.enable();
                    this.esqueceuSenhaForm.resetForm();
                })
            )
            .subscribe((response) => {
                    if (usuario.codPerfil == PerfilEnum.FILIAL_TECNICO_DE_CAMPO) {
                        if (!usuario.tecnico?.fonePerto) 
                            return this._snack.exibirToast("Você não possuiu número de telefone cadastrado no sistema", "error");

                        this._smsService.enviarSms({
                            from: "SAT",
                            to: usuario.tecnico?.fonePerto,
                            text: `Oi ${response.data.nomeUsuario}, segue sua nova senha de acesso ao SAT: ${response.data.senha}`
                        }).subscribe(() => {
                            this._snack.exibirToast("Uma nova senha foi enviada via SMS para o seu telefone", "success");
                            this._router.navigate(['sign-in'], {});
                        }, () => {
                            this._snack.exibirToast("Não foi possível enviar uma nova senha via SMS para o seu telefone", "error");
                            this._router.navigate(['sign-in'], {});
                        });
                    } else {
                        this._emailService.enviarEmail({
                            nomeRemetente: "SAT",
                            emailRemetente: "aplicacao.sat@perto.com.br",
                            nomeDestinatario: response.data.nomeUsuario,
                            emailDestinatario: response.data.email,
                            nomeCC: 'Equipe SAT',
                            emailCC: 'equipe.sat@perto.com.br',
                            assunto: "Nova senha de acesso ao Sistema SAT",
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
                                        <p>Recebemos a solicitação para sua nova senha.</p>
                                        <p>Sua nova senha de acesso ao Sistema SAT é:</p>  
                                        <h4>${response.data.senha}</h4>             
                                        <p>Atenciosamente,<br> 
                                    Equipe SAT<br> 
                                    Perto S.A. – Tecnologia para Bancos e Varejo<br> 
                                    Ramal (51) 2126-6944<br> 
                                    Whatsapp: (51) 997144990<br>
                                    </body>
                                    </html>`
                        }).subscribe(
                            (success) => {
                                this._snack.exibirToast("Uma nova senha foi enviada para o seu e-mail", "success");
                                this._router.navigate(['sign-in'], {});
                            },
                            (fail) => {
                                this._snack.exibirToast("Não foi possível enviar uma nova senha para o seu e-mail", "error");
                                this._router.navigate(['sign-in'], {});
                            }
                        );
                    }
                },
                // Fail
                (response) => {
                    this._snack.exibirToast("Não foi possível enviar uma nova senha para o seu e-mail", "error");
                    this._router.navigate(['sign-in'], {});
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
