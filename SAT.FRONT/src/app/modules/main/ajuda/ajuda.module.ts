import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AjudaFaqComponent } from './ajuda-faq/ajuda-faq.component';
import { HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatOptionModule } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MbscModule } from '@mobiscroll/angular';
import { MatExpansionModule } from '@angular/material/expansion';
import { TranslocoModule } from '@ngneat/transloco';
import { SharedModule } from 'app/shared/shared.module';
import { RouterModule } from '@angular/router';
import { ajudaRoutes } from './ajuda.routing';
import { AjudaTutorialComponent } from './ajuda-tutorial/ajuda-tutorial.component';
import { AjudaSuporteComponent } from './ajuda-suporte/ajuda-suporte.component';
import { FuseAlertModule } from '@fuse/components/alert';


@NgModule({
  declarations: [
    AjudaFaqComponent,
    AjudaTutorialComponent,
    AjudaSuporteComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(ajudaRoutes),
    SharedModule,
    MbscModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    TranslocoModule,
    MatSidenavModule,
    MatSelectModule,
    MatIconModule,
    MatTooltipModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatOptionModule,
    MatIconModule,
    FuseAlertModule,
    MatExpansionModule
  ]
})
export class AjudaModule { }
