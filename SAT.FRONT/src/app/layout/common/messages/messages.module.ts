import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { OverlayModule } from '@angular/cdk/overlay';
import { PortalModule } from '@angular/cdk/portal';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MessagesComponent } from 'app/layout/common/messages/messages.component';
import { SharedModule } from 'app/shared/shared.module';
import { MessageFormDialogComponent } from './message-form-dialog/message-form-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatOptionModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { TranslocoModule } from '@ngneat/transloco';
import { MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { MatMenuModule } from '@angular/material/menu';
import { FiltroModule } from 'app/modules/main/filtros/filtro.module';
import { MatSidenavModule } from '@angular/material/sidenav';
import { QuillModule } from 'ngx-quill';

@NgModule({
    declarations: [
        MessagesComponent,
        MessageFormDialogComponent
    ],
    imports     : [
        CommonModule,
        QuillModule.forRoot(),
        RouterModule,
        OverlayModule,
        MatIconModule,
        MatFormFieldModule,
        PortalModule,
        MatOptionModule,
        MatButtonModule,
        SharedModule,
        TranslocoModule,
        MatSortModule,
        MatInputModule,
        MatDialogModule,
        FuseHighlightModule,
        NgxMatSelectSearchModule,
        MatProgressBarModule,
        MatProgressSpinnerModule,
        MatCheckboxModule,
        MatSelectModule,
        MatDatepickerModule,
        MatTooltipModule,
        MatMenuModule,
        FiltroModule,
        MatSidenavModule
    ],
    exports     : [
        MessagesComponent
    ]
})
export class MessagesModule
{
}
