import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FuseDrawerModule } from '@fuse/components/drawer';
import { TranslocoModule } from '@ngneat/transloco';
import { LayoutComponent } from 'app/layout/layout.component';
import { EmptyLayoutModule } from 'app/layout/layouts/empty/empty.module';
import { CenteredLayoutModule } from 'app/layout/layouts/horizontal/centered/centered.module';
import { EnterpriseLayoutModule } from 'app/layout/layouts/horizontal/enterprise/enterprise.module';
import { ModernLayoutModule } from 'app/layout/layouts/horizontal/modern/modern.module';
import { ClassicLayoutModule } from 'app/layout/layouts/vertical/classic/classic.module';
import { ClassyLayoutModule } from 'app/layout/layouts/vertical/classy/classy.module';
import { DenseLayoutModule } from 'app/layout/layouts/vertical/dense/dense.module';
import { SharedModule } from 'app/shared/shared.module';

const layoutModules = [
    // Empty
    EmptyLayoutModule,

    // Horizontal navigation
    CenteredLayoutModule,
    EnterpriseLayoutModule,
    ModernLayoutModule,

    // Vertical navigation
    ClassicLayoutModule,
    ClassyLayoutModule,
    DenseLayoutModule
];

@NgModule({
    declarations: [
        LayoutComponent,
    ],
    imports     : [
        MatIconModule,
        MatTooltipModule,
        FuseDrawerModule,
        SharedModule,
        TranslocoModule,
        ...layoutModules
    ],
    exports     : [
        LayoutComponent,
        ...layoutModules
    ]
})
export class LayoutModule
{
}
