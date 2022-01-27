import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'app/shared/shared.module';
import { VersionComponent } from './version.component';

@NgModule({
    declarations: [
        VersionComponent
    ],
    imports     : [
        CommonModule,
        SharedModule,
        RouterModule,
        MatIconModule,
        MatButtonModule,
        MatTooltipModule
    ],
    exports     : [
        VersionComponent
    ]
})
export class VersionModule
{

}
