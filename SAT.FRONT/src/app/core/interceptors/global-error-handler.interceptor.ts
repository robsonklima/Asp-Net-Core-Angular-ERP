import { ErrorHandler, Injectable } from '@angular/core';
import { CustomSnackbarService } from '../services/custom-snackbar.service';
import { toastTypesConst } from '../types/generic.types';

@Injectable()
export class GlobalErrorHandlerInterceptor implements ErrorHandler {

    constructor(private _snack: CustomSnackbarService) {
    }

    handleError(error) {
        console.log(error);

        this._snack.exibirToast(error.message, toastTypesConst.ERROR);
    }
}