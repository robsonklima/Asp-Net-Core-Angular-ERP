import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { AuthService } from 'app/core/auth/auth.service';
import { EmailService } from 'app/core/services/email.service';
import { UsuarioDispositivoService } from 'app/core/services/usuario-dispositivo.service';
import { UsuarioDispositivo } from 'app/core/types/usuario-dispositivo.types';
import { Usuario } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { DeviceDetectorService } from 'ngx-device-detector';
import moment from 'moment';

@Component({
    selector: 'auth-sign-in',
    templateUrl: './sign-in.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class AuthSignInComponent implements OnInit {
    @ViewChild('signInNgForm') signInNgForm: NgForm;
    deviceInfo: any;
    signInForm: FormGroup;
    showAlert: boolean = false;
    snackConfigDanger: MatSnackBarConfig = {
        duration: 2000, panelClass: 'danger', verticalPosition: 'top', horizontalPosition: 'right'
    };

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _usuarioDispositivoSvc: UsuarioDispositivoService,
        private _emailSvc: EmailService,
        private _userSvc: UserService,
        private _snack: MatSnackBar,
        private _authService: AuthService,
        private _formBuilder: FormBuilder,
        private device: DeviceDetectorService,
        private _router: Router
    ) { }

    ngOnInit(): void {
        this.signInForm = this._formBuilder.group({
            codUsuario: ['', [Validators.required]],
            senha: ['', Validators.required]
        });

        this.deviceInfo = this.device.getDeviceInfo();
    }

    async signIn() {
        if (this.signInForm.invalid) return;
        this.signInForm.disable();
        let hash = this._authService.getUserHash();
        const codUsuario = this.signInForm.value.codUsuario;
        const senha = this.signInForm.value.senha;
        const usuario = await this._userSvc.obterPorCodigo(codUsuario).toPromise();

        if (!hash) {
            if(usuario && usuario?.email) {
                hash = Math.random().toString(36).slice(2).toString();
                this._authService.setUserHash(hash);

                const dispositivo: UsuarioDispositivo = {
                    dataHoraCad: moment().format('YYYY-MM-DD HH:mm'),
                    indAtivo: 0,
                    hash: hash,
                    codUsuario: codUsuario,
                    sistemaOperacional: this.deviceInfo.os,
                    navegador: this.deviceInfo.browser,
                    versaoSO: this.deviceInfo.os_version,
                    versaoNavegador: this.deviceInfo.browser_version,
                    tipoDispositivo: this.deviceInfo.deviceType
                };

                await this._usuarioDispositivoSvc.criar(dispositivo).subscribe();
                this.enviarEmail(codUsuario, usuario, hash);
                this._router.navigateByUrl('confirmation-required');
            } else {
                this._snack.open('O usuário informado não possui e-mail cadastrado.', null, this.snackConfigDanger).afterDismissed().toPromise();
            }
            
            this.signInForm.enable();
        } else {
            let dispositivo = await this._usuarioDispositivoSvc
                .obterPorUsuarioEHash(codUsuario, hash)
                .toPromise();

            if (dispositivo?.indAtivo) {
                this._authService
                    .signIn(codUsuario, senha, hash)
                    .subscribe(() => {
                        const redirectURL = this._activatedRoute.snapshot.queryParamMap.get('redirectURL') || '/signed-in-redirect';
                        this._router.navigateByUrl(redirectURL).then(() => {
                            window.location.reload();
                        });;
                    }, (e) => {
                        this.signInForm.enable();
                        this.signInNgForm.resetForm();
                    }
                );

                this.signInForm.enable();
            } else {
                this.enviarEmail(codUsuario, usuario, hash);
                this._router.navigateByUrl('confirmation-required');
            }
        }
    }

    private enviarEmail(codUsuario: string, usuario: Usuario, hash: string) {
        this._emailSvc.enviarEmail({
            nomeRemetente: "SAT",
            emailRemetente: "aplicacao.sat@perto.com.br",
            nomeDestinatario: usuario.nomeUsuario,
            emailDestinatario: usuario.email,
            assunto: "Ativação de Acesso ao Sistema SAT",
            corpo: `<p>Solicitação de Permissão de Acesso à sua Conta no SAT</p>
                    <p>Sistema Operacional: ${this.deviceInfo.os}</p>
                    <p>Versão SO: ${this.deviceInfo.os_version}</p>
                    <p>Navegador: ${this.deviceInfo.browser}</p>
                    <p>Versão Navegador: ${this.deviceInfo.browser_version}</p>
                    <p>Tipo de Dispositivo: ${this.deviceInfo.deviceType}</p>
                    <p>
                        Acesse o link para adicionar seu novo dispositivo
                        https://sat.perto.com.br/SAT.V2.FRONTEND/#/confirmation-submit/${codUsuario}/${hash}
                    </p>`
        }).subscribe();
    }
}
