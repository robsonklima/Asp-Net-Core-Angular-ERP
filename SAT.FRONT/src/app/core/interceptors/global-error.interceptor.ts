import { HttpErrorResponse } from "@angular/common/http";
import { ErrorHandler, Injectable, NgZone } from "@angular/core";
import { EmailService } from "../services/email.service";
import { appConfig as c } from 'app/core/config/app.config';
import { Router } from "@angular/router";

@Injectable()
export class GlobalErrorInterceptor implements ErrorHandler {
    constructor(
        private _zone: NgZone,
        private _emailSvc: EmailService,
        private _router: Router
    ) { }

    handleError(error: any) {
        if (!(error instanceof HttpErrorResponse)) {
            error = error.rejection;
        }
        this._zone.run(() => {
            const err: any = {
                type: 'CLIENT_SIDE',
                status: error?.status,
                message: error?.message || error || 'Undefined client error'
            }

            if (error.status !== 401 && error.status !== 0) {
                this._emailSvc.enviarEmail({
                    nomeRemetente: c.system_user,
                    emailRemetente: c.email_equipe,
                    nomeDestinatario: c.system_user,
                    emailDestinatario: c.email_equipe,
                    assunto: 'Erro durante o uso do SAT.V2: FRONTEND',
                    corpo: `Tipo: ${err.type}\n Status: ${err.status}\n Mensagem: ${err.message}`
                }).toPromise();

                this._router.navigate(['500-internal-server-error']);
            }
        });
    }
}