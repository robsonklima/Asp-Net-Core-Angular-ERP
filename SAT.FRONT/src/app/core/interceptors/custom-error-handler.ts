import { Injectable, ErrorHandler } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { UserSession } from '../user/user.types';

@Injectable()
export class GlobalErrorHandlerService implements ErrorHandler {
    userSession: UserSession;

    constructor(
    ) {
    }

    handleError(e: any) {
      if (e instanceof HttpErrorResponse) {
        // Http Error
      } else {
        // Client Error
      }     
    }
} 