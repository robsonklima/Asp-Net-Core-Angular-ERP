import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { SharedModule } from 'app/shared/shared.module';
import { AdvancedSearchComponent } from 'app/modules/admin/ui/advanced-search/advanced-search.component';
import { advancedSearchRoutes } from 'app/modules/admin/ui/advanced-search/advanced-search.routing';

@NgModule({
    declarations: [
        AdvancedSearchComponent
    ],
    imports     : [
        RouterModule.forChild(advancedSearchRoutes),
        MatButtonModule,
        MatCheckboxModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        FuseHighlightModule,
        SharedModule
    ]
})
export class AdvancedSearchModule
{
}
