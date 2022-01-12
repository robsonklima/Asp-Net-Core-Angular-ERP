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
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { InformacoesPessoaisComponent } from './informacoes-pessoais/informacoes-pessoais.component';
import { InformacoesTecnicasComponent } from './informacoes-tecnicas/informacoes-tecnicas.component';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';

const maskConfigFunction: () => Partial<IConfig> = () => {
    return {
        validation: false,
    };
};

@NgModule({
    declarations: [
        ConfiguracoesComponent,
        ConfiguracoesContaComponent,
        ConfiguracoesSegurancaComponent,
        InformacoesPessoaisComponent,
        InformacoesTecnicasComponent
    ],
    imports: [
        RouterModule.forChild(configuracoesRoutes),
        NgxMaskModule.forRoot(maskConfigFunction),
        NgxMatSelectSearchModule,
        SharedModule,
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
        MatProgressBarModule,
        MatProgressSpinnerModule,
        NgxMaskModule
    ]
})
export class ConfiguracoesModule {
}
