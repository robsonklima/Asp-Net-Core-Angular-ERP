// import { ErrorHandler, Injectable } from '@angular/core';
// import { CustomSnackbarService } from '../services/custom-snackbar.service';
// import { formatosConst, mensagensConst, toastTypesConst } from '../types/generic.types';
// import { isDevMode } from '@angular/core';
// import { UserService } from '../user/user.service';
// import { UserSession } from '../user/user.types';
// import { NLogService } from '../services/n-log.service';
// import moment from 'moment';

// @Injectable()
// export class GlobalErrorHandlerInterceptor implements ErrorHandler {
//     userSession: UserSession;

//     constructor(
//         private _snack: CustomSnackbarService,
//         private _nLogService: NLogService,
//         private _userService: UserService
//     ) {
//         this.userSession = JSON.parse(this._userService.userSession);
//     }

//     handleError(error: Error) {
//         this._snack.exibirToast(mensagensConst.OCORREU_UM_ERRO, toastTypesConst.ERROR);

//         this._nLogService.criar({
//             time: moment().format(formatosConst.DATA_HORA_DB),
//             level: toastTypesConst.ERROR,
//             nested: {
//                 message: error.message,
//                 application: 'Frontend'
//             }
//         }).subscribe(() => {

//         });
//     }
// }