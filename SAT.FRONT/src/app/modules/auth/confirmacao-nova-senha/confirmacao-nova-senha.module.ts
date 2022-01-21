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
import { ConfirmacaoNovaSenhaComponent } from 'app/modules/auth/confirmacao-nova-senha/confirmacao-nova-senha.component';
import { confirmacaoNovaSenhaRoutes } from 'app/modules/auth/confirmacao-nova-senha/confirmacao-nova-senha.routing';

@NgModule({
    declarations: [
        ConfirmacaoNovaSenhaComponent
    ],
    imports: [
        RouterModule.forChild(confirmacaoNovaSenhaRoutes),
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
export class ConfirmacaoNovaSenhaModule {
}
