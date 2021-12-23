import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { AuthService } from 'app/core/auth/auth.service';
import { UsuarioDispositivoService } from 'app/core/services/usuario-dispositivo.service';
import moment from 'moment';

@Component({
    selector: 'auth-sign-in',
    templateUrl: './sign-in.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class AuthSignInComponent implements OnInit {
    @ViewChild('signInNgForm') signInNgForm: NgForm;
    signInForm: FormGroup;
    showAlert: boolean = false;

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _usuarioDispositivoSvc: UsuarioDispositivoService,
        private _authService: AuthService,
        private _formBuilder: FormBuilder,
        private _router: Router
    ) { }

    ngOnInit(): void {
        this.signInForm = this._formBuilder.group({
            codUsuario: ['', [Validators.required]],
            senha: ['', Validators.required]
        });
    }

    async signIn() {
        if (this.signInForm.invalid) return;
        this.signInForm.disable();
        let hash = this._authService.getUserHash();

        this._authService
            .signIn(this.signInForm.value.codUsuario, this.signInForm.value.senha, hash)
            .subscribe(
                () => {
                    const redirectURL = this._activatedRoute.snapshot.queryParamMap.get('redirectURL') || '/signed-in-redirect';
                    this._router.navigateByUrl(redirectURL);
                },
                (e) => {
                    this.signInForm.enable();
                    this.signInNgForm.resetForm();
                }
            );
    }

    adicionarDispositivo(hash: string) {
        this._usuarioDispositivoSvc.criar({
            dataHoraCad: moment().format('YYYY-MM-DD HH:mm'),
            indAtivo: 0,
            hash: hash,
            codUsuario: this.signInForm.value.codUsuario
        }).subscribe(() => {
            this._router.navigateByUrl('confirmation-required');
            this.signInForm.enable();
        });
    }
}
