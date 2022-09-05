import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { AuthService } from 'app/core/auth/auth.service';
import { EmailService } from 'app/core/services/email.service';

@Component({
    selector: 'confirmacao-nova-senha',
    templateUrl: './confirmacao-nova-senha.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class ConfirmacaoNovaSenhaComponent implements OnInit {

    public loading: boolean = false;

    constructor(
        private _route: ActivatedRoute,
        private _authService: AuthService,
        private _router: Router,
        private _emailService: EmailService
    ) {
    }

    async ngOnInit() {
        this.loading = true;
        const codRecuperaSenhaCripto = this._route.snapshot.paramMap.get('codRecuperaSenha');

        // Chama o serviço
        this._authService.confirmaNovaSenha(codRecuperaSenhaCripto)
            .subscribe(
                // Success
                (response) => {
                    this._emailService.enviarEmail({
                        emailDestinatarios: [response.data.email],
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
                                    <p>Recebemos a confirmação para sua nova senha.</p>
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
                            this.loading = false;
                        },
                        (fail) => {
                            this._router.navigate(['sign-in'], {});
                        }
                    );
                },
                // Fail
                (response) => {
                    this._router.navigate(['sign-in'], {});
                }
            );
    }
}
