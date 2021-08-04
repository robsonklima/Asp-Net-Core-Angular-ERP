import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseAlertType } from '@fuse/components/alert';
import { AuthService } from 'app/core/auth/auth.service';

@Component({
    selector     : 'auth-sign-in',
    templateUrl  : './sign-in.component.html',
    encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations
})
export class AuthSignInComponent implements OnInit {
    @ViewChild('signInNgForm') signInNgForm: NgForm;

    alert: { type: FuseAlertType; message: string } = {
        type   : 'success',
        message: ''
    };
    signInForm: FormGroup;
    showAlert: boolean = false;

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _authService: AuthService,
        private _formBuilder: FormBuilder,
        private _router: Router
    ) {}

    ngOnInit(): void
    {
        this.signInForm = this._formBuilder.group({
            codUsuario : ['', [Validators.required]],
            senha : ['', Validators.required]
        });
    }

    signIn(): void
    {
        if (this.signInForm.invalid)
        {
            return;
        }

        this.signInForm.disable();
        this.showAlert = false;

        this._authService.signIn(this.signInForm.value.codUsuario, this.signInForm.value.senha)
            .subscribe(
                () => {
                    const redirectURL = this._activatedRoute.snapshot.queryParamMap.get('redirectURL') || '/signed-in-redirect';
                    this._router.navigateByUrl(redirectURL);
                },
                (e) => {
                    this.signInForm.enable();
                    this.signInNgForm.resetForm();

                    this.alert = {
                        type   : 'error',
                        message: e?.error?.errorMessage
                    };

                    this.showAlert = true;
                }
            );
    }
}
