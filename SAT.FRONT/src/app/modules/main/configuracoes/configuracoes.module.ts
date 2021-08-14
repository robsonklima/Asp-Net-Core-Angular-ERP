import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSidenavModule } from '@angular/material/sidenav';
import { FuseAlertModule } from '@fuse/components/alert';
import { SharedModule } from 'app/shared/shared.module';
import { ConfiguracoesComponent } from 'app/modules/main/configuracoes/configuracoes.component';
import { ConfiguracoesContaComponent } from 'app/modules/main/configuracoes/conta/conta.component';
import { ConfiguracoesSegurancaComponent } from 'app/modules/main/configuracoes/seguranca/seguranca.component';
import { configuracoesRoutes } from 'app/modules/main/configuracoes/configuracoes.routing';
import { TranslocoModule } from '@ngneat/transloco';

@NgModule({
    declarations: [
        ConfiguracoesComponent,
        ConfiguracoesContaComponent,
        ConfiguracoesSegurancaComponent
    ],
    imports     : [
        RouterModule.forChild(configuracoesRoutes),
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatRadioModule,
        MatSelectModule,
        MatSidenavModule,
        MatSlideToggleModule,
        FuseAlertModule,
        TranslocoModule,
        SharedModule
    ]
})
export class ConfiguracoesModule
{
}
