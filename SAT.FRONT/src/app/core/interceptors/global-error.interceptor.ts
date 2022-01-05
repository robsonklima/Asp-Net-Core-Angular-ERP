import { HttpErrorResponse } from "@angular/common/http";
import { ErrorHandler, Injectable, NgZone } from "@angular/core";
import { EmailService } from "../services/email.service";
import { appConfig as c } from 'app/core/config/app.config';
import { Router } from "@angular/router";

@Injectable()
export class GlobalErrorInterceptor implements ErrorHandler
{
    constructor (
        private _zone: NgZone,
        private _emailSvc: EmailService,
        private _router: Router
    ) { }

    handleError(error: any)
    {
        if (!(error instanceof HttpErrorResponse) || !error)
        {
            error = error.rejection;
            return;
        }
        this._zone.run(() =>
        {
            const err: any = {
                type: 'CLIENT_SIDE',
                stack: error?.stack,
                message: error?.message,
                status: error?.status
            }

            //             if (err.status !== 401 && err.status !== 0) {
            //                 this._emailSvc.enviarEmail({
            //                     nomeRemetente: c.system_user,
            //                     emailRemetente: c.email_equipe,
            //                     nomeDestinatario: c.system_user,
            //                     emailDestinatario: c.email_equipe,
            //                     assunto: 'Erro durante o uso do SAT.V2: FRONTEND',
            //                     corpo: `Tipo: ${err.type}\n Mensagem: ${err.message}\n Stack: ${err.stack}`
            //                 }).toPromise();
            // 
            //                 this._router.navigate(['500-internal-server-error']);
            //             }

            console.log('Ocorreu um erro', error);
        });
    }
}