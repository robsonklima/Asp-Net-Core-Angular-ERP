import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { AuthService } from 'app/core/auth/auth.service';
import { EmailService } from 'app/core/services/email.service';
import { UsuarioDispositivoService } from 'app/core/services/usuario-dispositivo.service';
import { UsuarioDispositivo } from 'app/core/types/usuario-dispositivo.types';
import { Usuario } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { DeviceDetectorService } from 'ngx-device-detector';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { SmsService } from 'app/core/services/sms.service';
import { PerfilEnum } from 'app/core/types/perfil.types';
import { statusConst } from 'app/core/types/status-types';
import packageInfo from '../../../../../package.json';
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
    ipData: any = '';
    signInForm: FormGroup;
    showAlert: boolean = false;
    ano: string = moment().format("YYYY");
    versao: string = packageInfo.version;

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _usuarioDispositivoSvc: UsuarioDispositivoService,
        private _emailSvc: EmailService,
        private _userSvc: UserService,
        private _snack: CustomSnackbarService,
        private _authService: AuthService,
        private _formBuilder: FormBuilder,
        private device: DeviceDetectorService,
        private _router: Router,
        private _smsService: SmsService
    ) { }

    async ngOnInit() {
        this.signInForm = this._formBuilder.group({
            codUsuario: [undefined, [Validators.required]],
            senha: [undefined, [Validators.required]]
        });

        this.deviceInfo = this.device.getDeviceInfo();
        this.ipData = '';

        this.signInForm.get("senha").valueChanges.subscribe(txt => {
            const senhaForte = this._userSvc.verificarSenhaForte(txt);
        });
    }

    async signIn() {
        this.signInForm.disable();

        const form = this.signInForm.getRawValue();
        const codUsuario = form.codUsuario;
        const senha = form.senha;
        this._userSvc.obterPorCodigo(codUsuario).subscribe(async (usuario) => {
            const params = {
                codUsuario: codUsuario,
                sistemaOperacional: this.deviceInfo.os,
                navegador: this.deviceInfo.browser,
                versaoSO: this.deviceInfo.os_version,
                tipoDispositivo: this.deviceInfo.deviceType,
                ip: this.ipData.ip
            }
    
            const dispositivoData = await this._usuarioDispositivoSvc.obterPorParametros(params).toPromise();
            let dispositivo = dispositivoData.items.shift();
    
            if (!usuario || !usuario?.email) {
                this._snack.exibirToast('O usuário informado não possui e-mail cadastrado.', 'error');
                this.signInForm.enable();
            } else if (usuario.indAtivo != statusConst.ATIVO) {
                this._snack.exibirToast('O usuário informado não está ativo.', 'error');
                this.signInForm.enable();
            }
             else if (!dispositivo) {
                dispositivo = await this.cadastrarDispositivo();
                this._router.navigate(['confirmation-required']);
            } else if (dispositivo?.indAtivo) {
                this._authService
                    .signIn(codUsuario, senha)
                    .subscribe(() => {
                        const redirectURL = this._activatedRoute.snapshot.queryParamMap.get('redirectURL') || '/signed-in-redirect';
                        this._router.navigateByUrl(redirectURL);
                    }, (e) => {
                        this.signInForm.enable();
                        this.signInNgForm.resetForm();
                        this._snack.exibirToast(e?.message || 'Ocorreu um erro', 'error')
                    });
    
                this.signInForm.enable();
            } else {
                usuario.codPerfil === PerfilEnum.FILIAL_TECNICO_DE_CAMPO ? this.enviarSMS(usuario, dispositivo) : this.enviarEmail(usuario, dispositivo);
                this._router.navigate(['confirmation-required']);
            }
        }, () => {
            this._snack.exibirToast(`Usuário ou Senha incorreta!`, "error");
            this.signInForm.enable();
        });
    }

    private enviarSMS(usuario: Usuario, dispositivo: UsuarioDispositivo) {
        this._smsService.enviarSms({
            from: "SAT",
            to: usuario.tecnico?.fonePerto,
            text: `Oi ${usuario.nomeUsuario}, segue link para adicionar novo dispositivo: https://sat.perto.com.br/SAT.V2.FRONTEND/#/confirmation-submit/${dispositivo.codUsuarioDispositivo}`
        }).subscribe(() => {
            this._snack.exibirToast("Siga as instruções enviadas via SMS", "success");
            this._router.navigate(['sign-in'], {});
        }, () => {
            this._snack.exibirToast("Não foi possível enviar as instruções via SMS para o seu telefone", "error");
            this._router.navigate(['sign-in'], {});
        });
    }

    private enviarEmail(usuario: Usuario, dispositivo: UsuarioDispositivo) {
        this._emailSvc.enviarEmail({
            emailDestinatarios: [usuario.email],
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
                indAtivo: statusConst.ATIVO,
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
