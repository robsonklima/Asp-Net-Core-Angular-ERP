import { Injectable } from '@angular/core';
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from 'app/core/auth/auth.service';
import { AuthUtils } from 'app/core/auth/auth.utils';
import { CustomSnackbarService } from '../services/custom-snackbar.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(
        private _authService: AuthService,
        private _snack: CustomSnackbarService
    ) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // Clone the request object
        let newReq = req.clone();

        // Request
        //
        // If the access token didn't expire, add the Authorization header.
        // We won't add the Authorization header if the access token expired.
        // This will force the server to return a "401 Unauthorized" response
        // for the protected API routes which our response interceptor will
        // catch and delete the access token from the local storage while logging
        // the user out from the app.
        if (this._authService.accessToken && !AuthUtils.isTokenExpired(this._authService.accessToken)) {
            newReq = req.clone({
                headers: req.headers.set('Authorization', 'Bearer ' + this._authService.accessToken)
            });
        }

        // Response
        return next.handle(newReq).pipe(
            catchError((error) => {
                // Catch "401 Unauthorized" responses
                if (error instanceof HttpErrorResponse && error.status === 401) {
                    this._authService.signOut();
                    location.reload();
                // Catch "500 Internal Server Error" responses
                } else if (error instanceof HttpErrorResponse && error.status === 500) {
                    // this is server side error
                    this._snack.exibirToast(error.error.errorMessage, 'error');
                } else if (error.error instanceof ErrorEvent) {
                    // this is client side error
                    this._snack.exibirToast(error.error.message, 'error');
                }
                
                return throwError(error);
            })
        );
    }
}
