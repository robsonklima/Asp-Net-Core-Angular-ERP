import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';

@Component({
    selector: 'auth-confirmation-required',
    templateUrl: './confirmation-required.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class AuthConfirmationRequiredComponent {
    email: string;

    constructor(
        private _router: Router
    ) {
        this.email = this.esconderEmail(this._router.getCurrentNavigation()?.extras?.state?.email); 
    }

    esconderEmail(email) {
        if (!email) return '';

        return email.replace(/(.{2})(.*)(?=@)/,
            (gp1, gp2, gp3) => {
                for (let i = 0; i < gp3.length; i++) {
                    gp2 += "*";
                } return gp2;
            });
    };
}
