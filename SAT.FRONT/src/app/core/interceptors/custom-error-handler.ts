import { Injectable, ErrorHandler } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { NGXLogger } from 'ngx-logger';

@Injectable()
export class GlobalErrorHandlerService implements ErrorHandler {
    constructor(
        private _logger: NGXLogger
    ) {}

    handleError(e: any) {
      if (e instanceof HttpErrorResponse) {
        
      } else {
          this._logger.error(e);
      }     
    }
} 