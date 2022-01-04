import { EmailService } from '../services/email.service';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { appConfig as c } from 'app/core/config/app.config';
import { Router } from '@angular/router';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
} from '@angular/common/http';

export class HttpErrorInterceptor implements HttpInterceptor {
  constructor(
    private _emailSvc: EmailService,
    private _router: Router
  ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let newReq = req.clone();

    return next.handle(newReq).pipe(
      catchError((err) => {
        let error: any;

        if (err.error instanceof ErrorEvent) {
          error = {
            type: 'CLIENT_SIDE',
            status: err.status,
            message: err.error.message
          }
        } else {
          error = {
            type: 'SERVER_SIDE',
            status: err.status,
            message: err.message
          }
        }

        switch (error.status) {
          case 500:
            this.enviarEmail(error); 
            this._router.navigate(['500-internal-server-error']);
            break;
        
          case 403:
            this._router.navigate(['403-forbidden']);    
            break;

          default:
            this.enviarEmail(error);
            this._router.navigate(['500-internal-server-error']);
            break;
        }

        return throwError(error);
      })
    );
  }

  enviarEmail(error: any) {
    this._emailSvc.enviarEmail({
      nomeRemetente: c.system_user,
      emailRemetente: c.email_equipe,
      nomeDestinatario: c.system_user,
      emailDestinatario: c.email_equipe,
      assunto: 'Erro durante o uso do SAT.V2: FRONTEND',
      corpo: `Tipo: ${error.type}\n Status: ${error.status}\n Mensagem: ${error.message}`
    }).toPromise();
  }
}