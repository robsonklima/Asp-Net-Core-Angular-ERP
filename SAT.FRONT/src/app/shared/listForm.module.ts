import { NgModule } from '@angular/core';
import { SharedModule } from 'app/shared/shared.module';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { TranslocoModule } from '@ngneat/transloco';
import { MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';

const maskConfigFunction: () => Partial<IConfig> = () => {
    return {
        validation: false,
    };
};

@NgModule({
    declarations: [],
    exports: [
        FormsModule,
        MatButtonModule,
        SharedModule,
        MatFormFieldModule,
        MatPaginatorModule,
        MatIconModule,
        TranslocoModule,
        MatSortModule,
        MatInputModule,
        NgxMatSelectSearchModule,
        MatProgressBarModule,
        MatSelectModule,
        MatProgressSpinnerModule,
        MatCheckboxModule
    ],
    imports: []
})

export class ListFormModule {
    static configureRoutes(routes: Routes): any[] {
        return [
            ListFormModule,
            NgxMaskModule.forRoot(maskConfigFunction),
            RouterModule.forChild(routes)
        ]
    }
}
