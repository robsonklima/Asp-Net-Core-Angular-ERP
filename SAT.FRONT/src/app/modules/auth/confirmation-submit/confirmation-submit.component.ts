import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseAlertType } from '@fuse/components/alert';
import { SmsService } from 'app/core/services/sms.service';
import { UsuarioDispositivoService } from 'app/core/services/usuario-dispositivo.service';
import { statusConst } from 'app/core/types/status-types';
import { UsuarioDispositivo } from 'app/core/types/usuario-dispositivo.types';

@Component({
    selector     : 'auth-confirmation-submit',
    templateUrl  : './confirmation-submit.component.html',
    encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations
})
export class AuthConfirmationSubmitComponent implements OnInit {
    dispositivo: UsuarioDispositivo;
    isLoading: boolean;
    showAlert: boolean = false;
    alert: { type: FuseAlertType; message: string } = {
        type   : 'success',
        message: ''
    };
    snackConfigSuccess: MatSnackBarConfig = { duration: 2000, panelClass: 'success', verticalPosition: 'top', horizontalPosition: 'right' };

    constructor(
        private _route: ActivatedRoute,
        private _router: Router,
        private _snack: MatSnackBar,
        private _usuarioDispositivoSvc: UsuarioDispositivoService,
        private _smsService: SmsService,
    ) {}

    async ngOnInit() {
        this.showAlert = false;
        this.isLoading = true;
        const codUsuarioDispositivo = +this._route.snapshot.paramMap.get('codUsuarioDispositivo');

        this.dispositivo = await this._usuarioDispositivoSvc
            .obterPorCodigo(codUsuarioDispositivo)
            .toPromise();

        if (!this.dispositivo) {
            this.alert = {
                type   : 'error',
                message: 'O dispositivo não foi encontrado'
            };

            this.showAlert = true;
        }
        
        this.isLoading = false;
    }

    async adicionar() {
        this.isLoading = true;
        this.dispositivo.indAtivo = statusConst.ATIVO;
        await this._usuarioDispositivoSvc.atualizar(this.dispositivo).toPromise();
        await this._snack.open('Dispositivo cadastrado com sucesso.', null, this.snackConfigSuccess).afterDismissed().toPromise();
        this._router.navigateByUrl('sign-in');
    }
}
