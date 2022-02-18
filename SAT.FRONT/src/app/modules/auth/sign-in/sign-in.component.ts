import { ChangeDetectorRef, Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
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
import { JsonIPService } from 'app/core/services/jsonip.service';

@Component({
    selector: 'auth-sign-in',
    templateUrl: './sign-in.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class AuthSignInComponent implements OnInit {
    @ViewChild('signInNgForm') signInNgForm: NgForm;
    deviceInfo: any;
    ipData: any = '';
    signInForm: FormGroup;
    showAlert: boolean = false;
    snackConfigDanger: MatSnackBarConfig = {
        duration: 2000, panelClass: 'danger', verticalPosition: 'top', horizontalPosition: 'right'
    };

    public atendeRequisitos: boolean = true;
    public campoSenhaVazio: boolean = false;

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _usuarioDispositivoSvc: UsuarioDispositivoService,
        private _emailSvc: EmailService,
        private _userSvc: UserService,
        private _snack: MatSnackBar,
        private _authService: AuthService,
        private _jsonIP: JsonIPService,
        private _formBuilder: FormBuilder,
        private device: DeviceDetectorService,
        private _router: Router,
        private _cdr: ChangeDetectorRef,
    ) { }

    async ngOnInit() {
        this.signInForm = this._formBuilder.group({
            codUsuario: [undefined, [Validators.required]],
            senha: [undefined, [Validators.required,
            //Minimo 8 caracteres, pelo menos uma letra maiuscula, uma letra minuscula, um numero e um caractere especial
            Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[()[\\]{}=\\-~,.;<>:@$!%*?&])[A-Za-z\\d()[\\]{}=\\-~,.;<>:@$!%*?&]{8,}$')]]
        });

        this.deviceInfo = this.device.getDeviceInfo();
        this.ipData = ''; //await this._jsonIP.obterIP().toPromise();

        this.signInForm.get("senha").valueChanges.subscribe(value => {
            this.campoSenhaVazio = value.length == 0;
            if (this.campoSenhaVazio && !this.atendeRequisitos) {
                this.atendeRequisitos = true;
            }
        })
    }

    async signIn() {
        this.atendeRequisitos = true;
        if (this.signInForm.invalid) {
            // Invalido porque pode não estar batendo no pattern da senha
            if (this.signInForm.controls['senha'].value.length > 0) {
                this.atendeRequisitos = false;
                this._cdr.detectChanges();
            }
            return;
        }

        this.signInForm.disable();

        const form = this.signInForm.getRawValue();
        const codUsuario = form.codUsuario;
        const senha = form.senha;
        const usuario = await this._userSvc.obterPorCodigo(codUsuario).toPromise();

        const dispositivoData = await this._usuarioDispositivoSvc
            .obterPorParametros({
                codUsuario: codUsuario,
                sistemaOperacional: this.deviceInfo.os,
                navegador: this.deviceInfo.browser,
                versaoSO: this.deviceInfo.os_version,
                tipoDispositivo: this.deviceInfo.deviceType,
                ip: this.ipData.ip
            })
            .toPromise();

        let dispositivo = dispositivoData.items.shift();

        if (!usuario || !usuario?.email) {
            this._snack
                .open('O usuário informado não possui e-mail cadastrado.', null, this.snackConfigDanger)
                .afterDismissed()
                .toPromise();
        } else if (!dispositivo) {
            dispositivo = await this.cadastrarDispositivo();
            this.enviarEmail(codUsuario, usuario, dispositivo);
            this._router.navigate(['confirmation-required'], { state: { email: usuario.email } });
        } else if (dispositivo?.indAtivo) {
            this._authService
                .signIn(codUsuario, senha)
                .subscribe(() => {
                    const redirectURL = this._activatedRoute.snapshot.queryParamMap.get('redirectURL') || '/signed-in-redirect';
                    this._router.navigateByUrl(redirectURL);
                }, (e) => {
                    this.signInForm.enable();
                    this.signInNgForm.resetForm();
                    this._snack.open(e?.error?.errorMessage || 'Ocorreu um erro');
                });

            this.signInForm.enable();
        } else {
            this.enviarEmail(codUsuario, usuario, dispositivo);
            this._router.navigate(['confirmation-required'], { state: { email: usuario.email } });
        }
    }

    private enviarEmail(codUsuario: string, usuario: Usuario, dispositivo: UsuarioDispositivo) {
        this._emailSvc.enviarEmail({
            nomeRemetente: "SAT",
            emailRemetente: "aplicacao.sat@perto.com.br",
            nomeDestinatario: usuario.nomeUsuario,
            emailDestinatario: usuario.email,
            nomeCC: 'Equipe SAT',
            emailCC: 'equipe.sat@perto.com.br',
            assunto: "Ativação de Acesso ao Sistema SAT",
            corpo: `<p>Solicitação de Permissão de Acesso à sua Conta no SAT</p>
                    <p>Sistema Operacional: ${this.deviceInfo.os}</p>
                    <p>Versão SO: ${this.deviceInfo.os_version}</p>
                    <p>Navegador: ${this.deviceInfo.browser}</p>
                    <p>Versão Navegador: ${this.deviceInfo.browser_version}</p>
                    <p>Tipo de Dispositivo: ${this.deviceInfo.deviceType}</p>
                    <p>
                        Acesse o link para adicionar seu novo dispositivo
                        https://sat.perto.com.br/SAT.V2.FRONTEND/#/confirmation-submit/${dispositivo.codUsuarioDispositivo}
                    </p>`
        }).subscribe();
    }

    private cadastrarDispositivo(): Promise<UsuarioDispositivo> {
        return new Promise((resolve, reject) => {
            let dispositivo: UsuarioDispositivo = {
                dataHoraCad: moment().format('YYYY-MM-DD HH:mm'),
                indAtivo: 0,
                codUsuario: this.signInForm.value.codUsuario,
                sistemaOperacional: this.deviceInfo.os,
                navegador: this.deviceInfo.browser,
                versaoSO: this.deviceInfo.os_version,
                versaoNavegador: this.deviceInfo.browser_version,
                tipoDispositivo: this.deviceInfo.deviceType,
                ip: this.ipData.ip
            };

            this._usuarioDispositivoSvc.criar(dispositivo).subscribe((dispositivo) => {
                resolve(dispositivo);
            }, (err) => {
                reject(err);
            });
        })
    }
}
