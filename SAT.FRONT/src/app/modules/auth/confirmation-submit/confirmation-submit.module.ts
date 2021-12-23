import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { FuseCardModule } from '@fuse/components/card';
import { SharedModule } from 'app/shared/shared.module';
import { AuthConfirmationSubmitComponent } from 'app/modules/auth/confirmation-submit/confirmation-submit.component';
import { authConfirmationSubmitRoutes } from 'app/modules/auth/confirmation-submit/confirmation-submit.routing';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FuseAlertModule } from '@fuse/components/alert/alert.module';

@NgModule({
    declarations: [
        AuthConfirmationSubmitComponent
    ],
    imports     : [
        RouterModule.forChild(authConfirmationSubmitRoutes),
        MatButtonModule,
        FuseCardModule,
        MatProgressSpinnerModule,
        SharedModule,
        FuseAlertModule
    ]
})
export class AuthConfirmationSubmitModule
{
}
