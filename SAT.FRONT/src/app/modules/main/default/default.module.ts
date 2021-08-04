import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { TranslocoModule } from '@ngneat/transloco';
import { SharedModule } from 'app/shared/shared.module';
import { DefaultComponent } from './default.component';
import { defaultRoutes } from './default.routing';

@NgModule({
    declarations: [
        DefaultComponent
    ],
    imports: [
        RouterModule.forChild(defaultRoutes),
        MatButtonModule,
        MatIconModule,
        SharedModule,
        TranslocoModule
    ]
})
export class DefaultModule
{
}