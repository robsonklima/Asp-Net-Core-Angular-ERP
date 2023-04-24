import { Injectable, ErrorHandler } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { NGXLogger } from 'ngx-logger';
import { UserService } from '../user/user.service';
import { UserSession } from '../user/user.types';

@Injectable()
export class GlobalErrorHandlerService implements ErrorHandler {
    userSession: UserSession;

    constructor(
        private _logger: NGXLogger,
        private _userService: UserService
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    handleError(e: any) {
      if (e instanceof HttpErrorResponse) {
        this.logError(e);
      } else {
        this.logError(e);
      }     
    }

    logError(e: any) {
        this._logger.error((e.message || e.error?.message) + ' ' + this.userSession.usuario.codUsuario);
    }
} 