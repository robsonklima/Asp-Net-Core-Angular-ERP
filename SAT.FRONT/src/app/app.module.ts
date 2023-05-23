import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ExtraOptions, PreloadAllModules, RouterModule } from '@angular/router';
import { FuseModule } from '@fuse';
import { FuseConfigModule } from '@fuse/services/config';
import { MbscModule } from '@mobiscroll/angular';
import { AppComponent } from 'app/app.component';
import { appRoutes } from 'app/app.routing';
import { appConfig } from 'app/core/config/app.config';
import { CoreModule } from 'app/core/core.module';
import { LayoutModule } from 'app/layout/layout.module';
import { MarkdownModule } from 'ngx-markdown';

const routerConfig: ExtraOptions = {
    scrollPositionRestoration: 'enabled',
    useHash: true,
    preloadingStrategy: PreloadAllModules
};

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        MbscModule,
        FormsModule,
        BrowserModule,
        BrowserAnimationsModule,
        RouterModule.forRoot(appRoutes, routerConfig),
        FuseModule,
        FuseConfigModule.forRoot(appConfig),
        CoreModule,
        LayoutModule,
        MarkdownModule.forRoot({}),
    ],
    bootstrap: [
        AppComponent
    ],
    providers: []
})
export class AppModule { }