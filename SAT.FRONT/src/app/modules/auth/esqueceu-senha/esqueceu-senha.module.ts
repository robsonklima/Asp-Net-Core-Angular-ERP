import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FuseCardModule } from '@fuse/components/card';
import { FuseAlertModule } from '@fuse/components/alert';
import { SharedModule } from 'app/shared/shared.module';
import { EsqueceuSenhaComponent } from 'app/modules/auth/esqueceu-senha/esqueceu-senha.component';
import { esqueceuSenhaRoutes } from 'app/modules/auth/esqueceu-senha/esqueceu-senha.routing';

@NgModule({
    declarations: [
        EsqueceuSenhaComponent
    ],
    imports     : [
        RouterModule.forChild(esqueceuSenhaRoutes),
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressSpinnerModule,
        FuseCardModule,
        FuseAlertModule,
        SharedModule
    ]
})
export class EsqueceuSenhaModule
{
}
